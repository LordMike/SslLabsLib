using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Mono.Options;
using Newtonsoft.Json;
using SslLabsLib;
using SslLabsLib.Enums;
using SslLabsLib.Objects;
using Timer = System.Timers.Timer;

namespace SslLabsMassScan
{
    class Program
    {
        private const int DefaultMaxAssesments = 10;
        private const int DefaultMaxAge = 168;

        static int Main(string[] args)
        {
            Options options = new Options();
            options.MaxConcurrentAssesments = DefaultMaxAssesments;
            options.MaxAge = DefaultMaxAge;

            OptionSet parser = new OptionSet();
            parser.Add("f|file=", "Input file, one host pr. line, required", s => options.Input = s);
            parser.Add("o|output=", "Output directory, will be created", s => options.Output = s);
            parser.Add("n|new", "Forces new scans", s => options.ForceNew = true);
            parser.Add("p|publish", "Published scans", s => options.Publish = true);
            parser.Add("w|overwrite", "Overwrite local scans, older than MaxAge", s => options.Overwrite = true);
            parser.Add<int>("a|maxage=", "Specify a MaxAge parameter, default: " + DefaultMaxAge, s => options.MaxAge = s);
            parser.Add<int>("m|max=", "Max concurrent scans, default: " + DefaultMaxAssesments, s => options.MaxConcurrentAssesments = s);
            parser.Add("e|endpoint=", "Endpoint. 'prod' or 'dev'", s => options.Endpoint = s);

            List<string> excessArgs = parser.Parse(args);

            if (string.IsNullOrEmpty(options.Input) || !File.Exists(options.Input))
            {
                Console.WriteLine("Missing input file");
                Console.WriteLine();
                Console.WriteLine("Help:");
                parser.WriteOptionDescriptions(Console.Out);

                return 1;
            }

            if (string.IsNullOrEmpty(options.Output))
            {
                Console.WriteLine("Missing output directory");
                Console.WriteLine();
                Console.WriteLine("Help:");
                parser.WriteOptionDescriptions(Console.Out);

                return 1;
            }

            if (!Directory.Exists(options.Output))
                Directory.CreateDirectory(options.Output);

            Queue<string> domains = new Queue<string>();
            bool anyBadInput = false;
            foreach (string line in File.ReadLines(options.Input))
            {
                if (Uri.CheckHostName(line) != UriHostNameType.Dns)
                {
                    Console.WriteLine("Bad input: " + line);
                    anyBadInput = true;
                }
                else
                {
                    domains.Enqueue(line);
                }
            }

            if (anyBadInput)
            {
                Console.WriteLine("One or more bad lines found - please correct");

                return 2;
            }

            Console.WriteLine("Beginning work on {0:N0} domains", domains.Count);

            Uri endpoint = new Uri("https://api.ssllabs.com/api/v2/");
            if (options.Endpoint == "dev")
                endpoint = new Uri("https://api.dev.ssllabs.com/api/v2/");

            int completedTasks = 0;
            SslLabsClient client = new SslLabsClient(endpoint);

            client.WaitTimePreScan = TimeSpan.FromSeconds(20);
            client.WaitTimeScan = TimeSpan.FromSeconds(10);

            Info sslLabsInfo = client.GetInfo();

            int? maxAge = options.MaxAge;

            AnalyzeOptions startOptions = AnalyzeOptions.None;
            if (options.ForceNew)
            {
                startOptions |= AnalyzeOptions.StartNew;
                maxAge = null;
            }
            else
                startOptions = AnalyzeOptions.ReturnAllIfDone;

            if (options.Publish)
                startOptions |= AnalyzeOptions.Publish;

            DateTime lastStatus = DateTime.UtcNow;
            object lastStatusLock = new object();
            Action printStatus = () =>
            {
                lock (lastStatusLock)
                {
                    if ((DateTime.UtcNow - lastStatus).TotalSeconds < 5)
                        return;

                    lastStatus = DateTime.UtcNow;

                    Console.WriteLine("Queue: {0:N0}, running: {1:N0} (cur.limit: {2:N0}), completed: {3:N0}", domains.Count, client.CurrentAssesments, client.MaxAssesments, completedTasks);
                }
            };

            AutoResetEvent limitChangedEvent = new AutoResetEvent(false);

            client.MaxAssesmentsChanged += () => limitChangedEvent.Set();
            client.CurrentAssesmentsChanged += () => limitChangedEvent.Set();

            while (domains.Any())
            {
                string domain = domains.Peek();
                string scanPath = Path.Combine(options.Output, domain + ".scan");

                // Is it done already?
                Host hostResult;

                if (File.Exists(scanPath))
                {
                    if (options.Overwrite && maxAge.HasValue)
                    {
                        hostResult = JsonConvert.DeserializeObject<Host>(File.ReadAllText(scanPath));

                        int age = (int)(DateTime.UtcNow - hostResult.TestTime).TotalHours;
                        if (age > maxAge)
                        {
                            // Process this
                            hostResult = null;
                        }
                        else
                        {
                            // Skip
                            domains.Dequeue();
                            continue;
                        }
                    }
                    else
                    {
                        // Skip
                        domains.Dequeue();
                        continue;
                    }
                }

                // Attempt to start the task
                while (true)
                {
                    TryStartResult didStart;
                    try
                    {
                        didStart = client.TryStartAnalysis(domain, maxAge, out hostResult, startOptions);
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("(Domain: " + domain + ") Webexception starting scan, waiting 3s: " + ex.Message);
                        limitChangedEvent.WaitOne(3000);
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("(Domain: " + domain + ") Exception while starting scan, waiting 30s: " + ex.Message);
                        Thread.Sleep(30000);
                        continue;
                    }

                    if (didStart == TryStartResult.RateLimit)
                    {
                        Thread.Sleep(sslLabsInfo.NewAssessmentCoolOff);
                        continue;
                    }

                    if (didStart == TryStartResult.Ok)
                    {
                        printStatus();
                        break;
                    }

                    // Wait for one to free up, fall back to trying every 30s
                    limitChangedEvent.WaitOne(30000);

                    printStatus();
                }

                // The task was started
                domains.Dequeue();
                //Console.WriteLine("Started " + domain);

                Task.Factory.StartNew(() =>
                {
                    Host innerHostResult = null;
                    if (hostResult != null && hostResult.Status == AnalysisStatus.READY)
                        // Use the one we fetched immediately
                        innerHostResult = hostResult;

                    while (innerHostResult == null)
                    {
                        try
                        {
                            // Block till we have an analysis
                            innerHostResult = client.GetAnalysisBlocking(domain);
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine("(Domain: " + domain + ") Webexception waiting for scan, waiting 3s: " + ex.Message);
                            Thread.Sleep(3000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("(Domain: " + domain + ") Exception while waiting for scan, waiting 30s: " + ex.Message);
                            Thread.Sleep(30000);
                        }
                    }

                    File.WriteAllText(scanPath, JsonConvert.SerializeObject(innerHostResult));

                    Console.WriteLine("Completed " + domain);

                    Interlocked.Increment(ref completedTasks);

                    limitChangedEvent.Set();
                });
            }

            Timer timer = new Timer(2000);
            timer.Elapsed += (sender, eventArgs) => printStatus();
            timer.Start();

            while (true)
            {
                Info info = client.GetInfo();
                if (info.CurrentAssessments == 0)
                    break;

                // Wait for tasks to finish, fall back to checking every 15s
                limitChangedEvent.WaitOne(15000);
            }

            timer.Stop();

            return 0;
        }
    }

    public class Options
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public bool ForceNew { get; set; }
        public bool Publish { get; set; }
        public int MaxConcurrentAssesments { get; set; }
        public bool Overwrite { get; set; }
        public int MaxAge { get; set; }
        public string Endpoint { get; set; }
        public bool Check { get; set; }
        public string CheckDns { get; set; }
    }
}
