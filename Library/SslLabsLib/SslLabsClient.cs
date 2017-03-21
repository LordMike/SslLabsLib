using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using SslLabsLib.Enums;
using SslLabsLib.Objects;

namespace SslLabsLib
{
    public class SslLabsClient
    {
        /// <summary>
        /// Time between lookups, before a scan has started (before mvoing to IN_PROGRESS)
        /// </summary>
        public TimeSpan WaitTimePreScan { get; set; }

        /// <summary>
        /// Time between lookups, once a scan has started (moved to IN_PROGRESS, before READY or ERROR)
        /// </summary>
        public TimeSpan WaitTimeScan { get; set; }

        /// <summary>
        /// Time to pause, once SSLLabs report the service is overloaded. This is not the same as rate limiting.
        /// </summary>
        public TimeSpan WaitTimeOverloaded { get; set; }

        private readonly HttpClient _restClient;

        public int MaxAssesments { get; private set; }

        public int CurrentAssesments { get; private set; }

        public event Action CurrentAssesmentsChanged;

        public event Action MaxAssesmentsChanged;

        public SslLabsClient()
            : this(new Uri("https://api.ssllabs.com/api/v2/"))
        {
        }

        public SslLabsClient(Uri baseUrl)
        {
            if (baseUrl == null)
                throw new ArgumentNullException("baseUrl");

            _restClient = new HttpClient();
            _restClient.BaseAddress = baseUrl;

            WaitTimePreScan = TimeSpan.FromSeconds(10);
            WaitTimeScan = TimeSpan.FromSeconds(5);
            WaitTimeOverloaded = TimeSpan.FromSeconds(30);
        }

        public Info GetInfo()
        {
            string json = _restClient.GetStringAsync("info").Result;
            return JsonConvert.DeserializeObject<Info>(json);
        }

        public StatusCodes GetStatusCodes()
        {
            string json = _restClient.GetStringAsync("getStatusCodes").Result;
            return JsonConvert.DeserializeObject<StatusCodes>(json);
        }

        public Host GetAnalysis(string hostname, int? maxAge = null, AnalyzeOptions options = AnalyzeOptions.None)
        {
            Host hostResult;
            AnalysisResult result = GetAnalysisInternal(hostname, maxAge, options, out hostResult);

            switch (result)
            {
                case AnalysisResult.Success:
                    return hostResult;
                case AnalysisResult.Error:
                case AnalysisResult.UnknownError:
                    throw new Exception("The server was unable to handle the request (" + result + ")");
                case AnalysisResult.Maintenance:
                    throw new Exception("The server was unable to handle the request due to Maintenance (HTTP 503)");
                case AnalysisResult.RateLimit:
                    throw new Exception("The server was unable to handle the request due to rate limits (HTTP 429)");
                case AnalysisResult.Overloaded:
                    throw new Exception("The server was unable to handle the request due to being overloaded (HTTP 529)");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Endpoint GetCachedEndpointAnalysis(string hostname, IPAddress endpoint)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms["s"] = endpoint.ToString();
            parms["host"] = hostname;

            HttpResponseMessage resp = _restClient.GetAsync(BuildUrl("getEndpointData", parms)).Result;

            ReadXHeaders(resp);

            string json = resp.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Endpoint>(json);
        }

        public TryStartResult TryStartAnalysis(string hostname, int? maxAge = null, AnalyzeOptions options = AnalyzeOptions.None)
        {
            Host hostResult;
            return TryStartAnalysis(hostname, maxAge, out hostResult, options);
        }

        public TryStartResult TryStartAnalysis(string hostname, int? maxAge, out Host hostResult, AnalyzeOptions options = AnalyzeOptions.None)
        {
            AnalysisResult result = GetAnalysisInternal(hostname, maxAge, options, out hostResult);

            if (result == AnalysisResult.Success)
                return TryStartResult.Ok;

            if (result == AnalysisResult.RateLimit)
                return TryStartResult.RateLimit;

            return TryStartResult.NotStarted;
        }

        public Host GetAnalysisBlocking(string hostname, int? maxAge = null, AnalyzeOptions options = AnalyzeOptions.None, Action<Host> progressCallback = null)
        {
            // Deactivate ReturnAll, activate ReturnAllWhenDone
            options &= ~AnalyzeOptions.ReturnAll;
            options |= AnalyzeOptions.ReturnAllIfDone;

            // Initial request
            Host hostResult;
            AnalysisResult result = GetAnalysisInternal(hostname, maxAge, options, out hostResult);

            // Loop till we're done
            TimeSpan toWait = WaitTimePreScan;

            switch (result)
            {
                case AnalysisResult.Error:
                case AnalysisResult.UnknownError:
                    throw new Exception("The server was unable to handle the request (" + result + ")");
                case AnalysisResult.Maintenance:
                    throw new Exception("The server was unable to handle the request due to Maintenance (HTTP 503)");
                case AnalysisResult.Overloaded:
                case AnalysisResult.RateLimit:
                    toWait = WaitTimeOverloaded;
                    break;
            }

            if (hostResult != null && hostResult.Status == AnalysisStatus.READY)
                return hostResult;

            progressCallback?.Invoke(hostResult);

            // Deactivate StartNew
            options &= ~AnalyzeOptions.StartNew;

            while (true)
            {
                Thread.Sleep(toWait);

                // Perform next analysis
                result = GetAnalysisInternal(hostname, maxAge, options, out hostResult);

                if (result == AnalysisResult.Error || result == AnalysisResult.UnknownError)
                    throw new Exception("The server was unable to handle the request (" + result + ")");

                if (result == AnalysisResult.Maintenance)
                    throw new Exception("The server was unable to handle the request due to Maintenance (HTTP 503)");

                if (result == AnalysisResult.RateLimit || result == AnalysisResult.Overloaded)
                {
                    toWait = WaitTimeOverloaded;
                }
                else if (result == AnalysisResult.Success)
                {
                    toWait = WaitTimePreScan;

                    // Success
                    // States: DNS, ERROR, IN_PROGRESS, and READY.
                    if (hostResult.Status == AnalysisStatus.DNS || hostResult.Status == AnalysisStatus.IN_PROGRESS)
                    {
                        // Underways
                        toWait = WaitTimeScan;
                    }
                    else if (hostResult.Status == AnalysisStatus.READY || hostResult.Status == AnalysisStatus.ERROR)
                    {
                        // We're done
                        break;
                    }
                    else
                        throw new InvalidOperationException("Unknown response...");
                }
                else
                    // Unknown?
                    throw new InvalidOperationException("Unknown state...");

                progressCallback?.Invoke(hostResult);
            }

            return hostResult;
        }

        private string BuildUrl(string method, Dictionary<string, string> parms)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(method);

            bool first = true;
            foreach (KeyValuePair<string, string> pair in parms)
            {
                if (first)
                    sb.Append("?");
                else
                    sb.Append("&");

                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(Uri.EscapeUriString(pair.Value));

                first = false;
            }

            return sb.ToString();
        }

        private void ReadXHeaders(HttpResponseMessage resp)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in resp.Headers)
            {
                int tmp;
                if (header.Key == "X-Max-Assessments" && int.TryParse(header.Value.FirstOrDefault(), out tmp))
                {
                    bool changed = tmp != MaxAssesments;
                    MaxAssesments = tmp;

                    if (changed)
                        MaxAssesmentsChanged?.Invoke();
                }
                else if (header.Key == "X-Current-Assessments" && int.TryParse(header.Value.FirstOrDefault(), out tmp))
                {
                    bool changed = tmp != MaxAssesments;
                    CurrentAssesments = tmp;

                    if (changed)
                        CurrentAssesmentsChanged?.Invoke();
                }
            }
        }

        private AnalysisResult GetAnalysisInternal(string hostname, int? maxAge, AnalyzeOptions options, out Host hostResult)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms["host"] = hostname;

            if (options.HasFlag(AnalyzeOptions.Publish))
                parms["publish"] = "on";

            if (options.HasFlag(AnalyzeOptions.StartNew))
                parms["startNew"] = "on";
            else if (options.HasFlag(AnalyzeOptions.FromCache))
                parms["fromCache"] = "on";

            if (options.HasFlag(AnalyzeOptions.ReturnAll))
                parms["all"] = "on";
            else if (options.HasFlag(AnalyzeOptions.ReturnAllIfDone))
                parms["all"] = "done";

            if (options.HasFlag(AnalyzeOptions.IgnoreMismatch))
                parms["ignoreMismatch"] = "on";

            if (maxAge.HasValue)
                parms["maxAge"] = maxAge.ToString();

            HttpResponseMessage resp;
            try
            {
                resp = _restClient.GetAsync(BuildUrl("analyze", parms)).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }

            ReadXHeaders(resp);

            hostResult = null;
            if (resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.InternalServerError)
                // 400 - invocation error (e.g., invalid parameters)
                // 500 - internal error
                return AnalysisResult.Error;

            if (resp.StatusCode == (HttpStatusCode)429)
                // 429 - client request rate too high
                return AnalysisResult.RateLimit;

            if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)
                // 503 - the service is not available (e.g., down for maintenance)
                return AnalysisResult.Maintenance;

            if (resp.StatusCode == (HttpStatusCode)529)
                // 529 - the service is overloaded
                return AnalysisResult.Overloaded;

            if (resp.StatusCode != HttpStatusCode.OK)
                return AnalysisResult.UnknownError;

            string json = resp.Content.ReadAsStringAsync().Result;
            hostResult = JsonConvert.DeserializeObject<Host>(json);

            return AnalysisResult.Success;
        }

        enum AnalysisResult
        {
            Success,
            Error,
            UnknownError,
            Maintenance,
            RateLimit,
            Overloaded
        }
    }
}
