namespace SslLabsLib.Enums
{
    public enum MustStapleStatus
    {
        /// <summary>
        /// Not supported
        /// </summary>
        NotSupported = 0,

        /// <summary>
        /// Supported, but OCSP response is not stapled
        /// </summary>
        Supported = 1,

        /// <summary>
        /// Supported, OCSP response is stapled
        /// </summary>
        SupportedAndStapled = 2
    }
}