namespace SslLabsLib.Enums
{
    public enum OpenSSLLuckyMinus20Result
    {
        TestFailed = -1,
        Unknown = 0,

        /// <summary>
        /// Not vulnerable
        /// </summary>
        NotVulnerable = 1,

        /// <summary>
        /// Vulnerable and insecure
        /// </summary>
        VulnerableAndInsecure = 2
    }
}