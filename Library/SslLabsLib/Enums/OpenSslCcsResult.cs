namespace SslLabsLib.Enums
{
    public enum OpenSslCcsResult
    {
        TestFailed = -1,
        Unknown = 0,

        /// <summary>
        /// Not vulnerable
        /// </summary>
        NotVulnerable = 1,

        /// <summary>
        /// Possibly vulnerable, but not exploitable
        /// </summary>
        PossibleVulnerable = 2,

        /// <summary>
        /// Vulnerable and exploitable
        /// </summary>
        Vulnerable = 3
    }
}