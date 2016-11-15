namespace SslLabsLib.Enums
{
    public enum SessionResumptionResult
    {
        /// <summary>
        /// Session resumption is not enabled and we're seeing empty session IDs
        /// </summary>
        Supported = 0,

        /// <summary>
        /// Endpoint returns session IDs, but sessions are not resumed
        /// </summary>
        FaultyImplementation = 1,

        /// <summary>
        /// Session resumption is enabled
        /// </summary>
        IntolerantServer = 2
    }
}