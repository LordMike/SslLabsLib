using System;
using SslLabsLib;
using SslLabsLib.Enums;
using SslLabsLib.Objects;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SslLabsClient client = new SslLabsClient();

            Analysis analysis = client.GetAnalysisBlocking("mbwarez.dk", null, AnalyzeOptions.StartNew, analysis1 =>
            {
                Console.WriteLine("Status: " + analysis1.Status + " (" + analysis1.StatusMessage + ")");
            });

            Console.WriteLine(analysis);
        }
    }
}
