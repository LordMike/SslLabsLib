using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Options;
using Newtonsoft.Json;
using SslLabsCli.Utilities;
using SslLabsLib;
using SslLabsLib.Enums;
using SslLabsLib.Objects;

namespace SslLabsCli
{
    class Program
    {
        private static readonly SslLabsClient Client = new SslLabsClient();

        static int Main(string[] args)
        {
            // SslLabsCli ssllabs.com --progress --new --nowait

            Options options = new Options();

            OptionSet parser = new OptionSet();
            parser.Add("p|progress", "Show progress while waiting", s => options.Progress = true);
            parser.Add("n|new", "Force a new scan", s => options.New = true);
            parser.Add("w|nowait", "Exit if no scan is available", s => options.NoWait = true);
            parser.Add("s|save", "Save the scan to a file", s => options.Save = s);

            List<string> leftoverArgs = parser.Parse(args);
            options.Hostname = leftoverArgs.FirstOrDefault();

            if (string.IsNullOrEmpty(options.Hostname))
            {
                Console.WriteLine("Usage: ");
                Console.WriteLine("  SslLabsCli [options] ssllabs.com");
                Console.WriteLine();
                Console.WriteLine("Options");
                parser.WriteOptionDescriptions(Console.Out);
                Console.WriteLine();

                return 1;
            }

            Host analysis = HandleFetch(options);

            if (analysis.Status == AnalysisStatus.ERROR)
            {
                AwesomeConsole.WriteLine("An error occurred", ConsoleColor.Red);
                AwesomeConsole.Write("Status: ");
                AwesomeConsole.WriteLine(analysis.StatusMessage, ConsoleColor.Cyan);

                AwesomeConsole.WriteLine("Messages from SSLLabs");
                Info info = Client.GetInfo();

                foreach (string msg in info.Messages)
                    AwesomeConsole.WriteLine("  " + msg, ConsoleColor.Yellow);

                return 3;
            }

            if (analysis.Status != AnalysisStatus.READY)
            {
                AwesomeConsole.WriteLine("Analysis not available", ConsoleColor.DarkYellow);
                return 2;
            }

            PresentAnalysis(analysis);

            if (!string.IsNullOrEmpty(options.Save))
            {
                File.WriteAllText(options.Save, JsonConvert.SerializeObject(analysis, Formatting.Indented));
            }

            return 0;
        }

        private static Host HandleFetch(Options options)
        {
            Action<Host> progress = prg =>
            {
                if (prg.Endpoints == null)
                {
                    using (AwesomeConsole.BeginSequentialWrite())
                        AwesomeConsole.WriteLine("Progress {0}", prg.Status);

                    return;
                }

                float max = prg.Endpoints.Count * 100;
                float pct = prg.Endpoints.Sum(s => (float)(s.Progress == -1 ? 0 : s.Progress)) / max;

                string current = prg.Endpoints.SkipWhile(s => s.Progress == 100).Select(s => s.StatusDetailsMessage).FirstOrDefault();
                List<Tuple<int, string>> states = prg.Endpoints.Select(s => new Tuple<int, string>(s.Progress, s.StatusMessage)).ToList();

                using (AwesomeConsole.BeginSequentialWrite())
                {
                    AwesomeConsole.Write("Progress {0:P}", pct);
                    AwesomeConsole.Write(" (servers: ");
                    for (int i = 0; i < states.Count; i++)
                    {
                        Tuple<int, string> state = states[i];

                        if (state.Item1 == 100)
                            AwesomeConsole.Write("{0}", ConsoleColor.DarkGreen, state.Item2);
                        else
                            AwesomeConsole.Write("{0}", ConsoleColor.Yellow, state.Item2);

                        if (i > 0)
                            AwesomeConsole.Write(" | ");
                    }
                    AwesomeConsole.Write(") (current: ");
                    AwesomeConsole.Write("{0}", ConsoleColor.Cyan, current);
                    AwesomeConsole.WriteLine(")");
                }
            };

            if (!options.Progress)
                progress = null;

            AnalyzeOptions analyzeOptions = AnalyzeOptions.ReturnAllIfDone;

            if (options.New)
                analyzeOptions |= AnalyzeOptions.StartNew;
            else
                analyzeOptions |= AnalyzeOptions.FromCache;

            Host analysis = Client.GetAnalysisBlocking(options.Hostname, null, analyzeOptions, progress);

            return analysis;
        }

        private static void PresentAnalysis(Host hostResult)
        {
            Console.WriteLine("== Basic ==");
            Console.WriteLine("Host: " + hostResult.Hostname + ":" + hostResult.Port);
            Console.WriteLine("Public: " + hostResult.IsPublic);
            Console.WriteLine();

            for (int i = 0; i < hostResult.Endpoints.Count; i++)
            {
                Key key = hostResult.Endpoints[i].Details.Key;
                Cert cert = hostResult.Endpoints[i].Details.Cert;

                Console.WriteLine("== Certificate [" + i + "] ==");
                Console.WriteLine("CN: " + string.Join(", ", cert.CommonNames));
                Console.WriteLine("Issuer: " + cert.IssuerLabel);
                Console.WriteLine("Validity: " + cert.NotBefore.ToUniversalTime().ToString("g") + " -> " + cert.NotAfter.ToUniversalTime().ToString("g") + " UTC");
                Console.WriteLine("Signature: " + cert.SigAlg);
                Console.WriteLine("Key: " + key.Alg + " " + key.Size);
                Console.WriteLine();
            }


            Console.WriteLine("== Endpoints ==");

            for (int i = 0; i < hostResult.Endpoints.Count; i++)
            {
                Endpoint endpoint = hostResult.Endpoints[i];

                Console.WriteLine("[" + i + "] == " + endpoint.IpAddress);
                Console.WriteLine("[" + i + "] Protocols: " + string.Join(", ", endpoint.Details.Protocols.Select(s => s.Name + " " + s.Version)));
                Console.WriteLine("[" + i + "] Grade (ignore trust): " + endpoint.Grade + " (" + endpoint.GradeTrustIgnored + ")");

                Console.WriteLine("[" + i + "] Suites (" + (endpoint.Details.Suites.Preference ? "preferred" : "not preferred") + ")");
                foreach (Ciphersuite suite in endpoint.Details.Suites.List)
                {
                    Console.WriteLine("[" + i + "]   " + suite.Name + " " + (suite.Q == 1 ? "(WEAK)" : ""));
                }

                Console.WriteLine();
            }
        }
    }

    internal class Options
    {
        public bool Progress { get; set; }

        public bool New { get; set; }

        public bool NoWait { get; set; }

        public string Hostname { get; set; }

        public string Save { get; set; }
    }
}
