using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum AnalyzeOptions
    {
        /// <summary>
        /// No options are set
        /// </summary>
        None = 0,

        /// <summary>
        /// Publish the results of the scan.
        /// </summary>
        Publish = 1,

        /// <summary>
        /// Start a new scan, even if a cached version exists.
        /// Cannot co-exist with FromCache
        /// </summary>
        StartNew = 2,

        /// <summary>
        /// Allows fetching a scan from the cache.
        /// Cannot co-exist with StartNew
        /// </summary>
        FromCache = 4,

        /// <summary>
        /// Fetches all information available at all times.
        /// </summary>
        ReturnAll = 8,

        /// <summary>
        /// Fetches all information available only when the assesment is done.
        /// </summary>
        ReturnAllIfDone = 16,

        /// <summary>
        /// Proceed with the assesment even if the site presents a certificate which doesn't match its name.
        /// </summary>
        IgnoreMismatch = 32,
    }
}