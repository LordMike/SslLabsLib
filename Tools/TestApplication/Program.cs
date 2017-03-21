using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib;
using SslLabsLib.Enums;
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
            client.GetAnalysis("ssllabs.com", null, AnalyzeOptions.FromCache | AnalyzeOptions.ReturnAll);
            


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
