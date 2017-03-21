using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SslLabsLib;
using SslLabsLib.Enums;
using SslLabsLib.Objects;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace TestApplication
{
    class Program
    {
        private static readonly List<ErrorEventArgs> Errors = new List<ErrorEventArgs>();

        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new FailingContractResolver(),
                Error = Error
            };

            SslLabsClient client = new SslLabsClient();

            // Run a method call

            //client.GetInfo();
            var analysis = client.GetAnalysis("ssllabs.com", null, AnalyzeOptions.FromCache | AnalyzeOptions.ReturnAll);

            //var res2 = File.ReadAllText("out_control.txt");
            //var analysis = JsonConvert.DeserializeObject<Host>(res2);

            //var res1 = JsonConvert.SerializeObject(analysis, Formatting.Indented);
            //File.WriteAllText("out_res.txt", res1);

            // Examine JSON errors
            Console.WriteLine($"There were {Errors.Count} JSON errors");

            foreach (ErrorEventArgs error in Errors)
            {
                Console.WriteLine($"Message : {error.ErrorContext.Error.Message}");
                Console.WriteLine($"  Path  : {error.ErrorContext.Path}");
                Console.WriteLine($"  Member: {error.ErrorContext.Member}");

                Console.WriteLine();
            }
        }

        private static void Error(object sender, ErrorEventArgs errorEventArgs)
        {
            Errors.Add(errorEventArgs);
            errorEventArgs.ErrorContext.Handled = true;
        }
    }
}
