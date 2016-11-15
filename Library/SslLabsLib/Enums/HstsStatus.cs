namespace SslLabsLib.Enums
{
    public enum HstsStatus
    {
        /// <summary>
        /// Either before the server is checked or when its HTTP response headers are not available
        /// </summary>
        Unknown,

        /// <summary>
        /// Header not present
        /// </summary>
        Absent,

        /// <summary>
        /// Header present and syntatically correct
        /// </summary>
        Present,

        /// <summary>
        /// Header present, but couldn't be parsed
        /// </summary>
        Invalid,

        /// <summary>
        /// Header present and syntatically correct, but HSTS is disabled
        /// </summary>
        Disabled
    }
}