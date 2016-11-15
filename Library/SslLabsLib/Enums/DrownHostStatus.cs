namespace SslLabsLib.Enums
{
    // ReSharper disable InconsistentNaming
    public enum DrownHostStatus
    {
        /// <summary>
        /// Error occurred in test
        /// </summary>
        Error,

        /// <summary>
        /// Before the status is checked
        /// </summary>
        Unknown,

        /// <summary>
        /// Not checked if already vulnerable server found
        /// </summary>
        Not_checked,

        /// <summary>
        /// Not checked (same host)
        /// </summary>
        Not_checked_same_host,

        /// <summary>
        /// When SSL v2 not supported by server
        /// </summary>
        Handshake_failure,

        /// <summary>
        /// SSL v2 supported but not same rsa key
        /// </summary>
        Sslv2,

        /// <summary>
        /// Vulnerable (same key with SSL v2)
        /// </summary>
        Key_match,

        /// <summary>
        /// Vulnerable (same hostname with SSL v2)
        /// </summary>
        Hostname_match
    }
    // ReSharper restore InconsistentNaming
}