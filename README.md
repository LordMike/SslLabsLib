# SSL Labs API client for .NET
This is an SSL Labs API client wrapper for [SSLLabs asssesment tool](https://www.ssllabs.com/), based on the official [SSL Labs API Documentation](https://github.com/ssllabs/ssllabs-scan/blob/stable/ssllabs-api-docs.md)

[![Build status](https://ci.appveyor.com/api/projects/status/hcetxym0o320fakj?svg=true)](https://ci.appveyor.com/project/LordMike/ssllabslib)


# Installation
The easiest way to get the library is to use Nuget. The library has been published [here](https://www.nuget.org/packages/SslLabsLib/), and is also available [at Github](https://github.com/LordMike/SslLabsLib) in source form. The following Nuget command will fetch the package for you.

    Install-Package SslLabsLib

# Usage
The SSL Labs API is built up around a polling method, where you regularly poll the API for the status on an ongoing assesment. The client has been designed to mimick this. The following is a series of common commands you will be using.

Fetch the status on the assesment of `google.com`. Does not return the actual analysis, may start a new analysis.

    GetAnalysis("google.com")

Fetch the analysis of `google.com` from cache (do not start a new analysis), only if it has been completed.

    GetAnalysis("google.com", null, AnalyzeOptions.FromCache | AnalyzeOptions.ReturnAllIfDone)

Fetch the cached analysis of "google.com" if it is at most 24 hours old, else begin a new analysis and return its status.

    GetAnalysis("google.com", 24, AnalyzeOptions.ReturnAllIfDone)

Fetch an analysis, waiting till it is ready. Only fetch from cache if it is at most 24 hours old, and also publish the results. Ignore any mismatched certificates.

    GetAnalysisBlocking("google.com", 24, AnalyzeOptions.Publish | AnalyzeOptions.IgnoreMismatch)

## AnalyzeOptions
There are a number of options available, originally documented [here](https://github.com/ssllabs/ssllabs-scan/blob/stable/ssllabs-api-docs.md#invoke-assessment-and-check-progress). I will repeat the corresponding `AnalyzeOptions` here.

* **AnalyzeOptions.Publish** -- Sets `publish` to `on`. Will publish any new scans to the SSL Labs frontpage. Leaving this off will not publish the results.

* **AnalyzeOptions.StartNew** -- Sets `startNew` to `on`. Will forcefully start a new scan, ignoring any cache serverside. Will override the use of `AnalyzeOptions.FromCache` if set.

* **AnalyzeOptions.FromCache** -- Sets `fromCache` to `on`. Will request the scan from cache if it is available, will prevent starting new scans. Is overriden if `AnalyzeOptions.StartNew` is set.

* **AnalyzeOptions.ReturnAll** -- Sets `all` to `on`. Will return all data available, at all times. Overrides `AnalyzeOptions.ReturnAllIfDone` if set.

* **AnalyzeOptions.ReturnAllIfDone** -- Sets `all` to `done`. Will return all data available, when the entire analysis is done. Is used to reduce the number of requests, while saving bandwith. Is overriden by `AnalyzeOptions.ReturnAllIfDone` if set.

* **AnalyzeOptions.IgnoreMismatch** -- Sets `ignoreMismatch` to `on`. Will instruct SSL Labs to proceed with the analysis, even if the certificate provided by the host does not match the domain queried.

# Example clients
The following example clients are provided.

* **SslLabsCli** -- implementation of `ssllabs-scan` in C#, to showcase the Client and how to use it. Is very straight forward.

* **SslLabsMassScan** -- implementation of a builk scanner, which can take a domain name list and sequentiall scan the domains. Can be used for data analysis. This scanner will dynamically adjust to the limits set by the API.
