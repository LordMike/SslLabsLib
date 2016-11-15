namespace SslLabsLib.Enums
{
    public enum PoodleResult
    {
        Timeout = -3,
        TlsNotSupported = -2,
        TestFailed = -1,
        Unknown = 0,
        NotVulnerable = 1,
        Vulnerable = 2
    }
}