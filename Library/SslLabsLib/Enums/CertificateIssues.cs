using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum CertificateIssues
    {
        None = 0,

        /// <summary>
        /// No chain of trust
        /// </summary>
        NoChainOfTrust = 1,

        /// <summary>
        /// Not before
        /// </summary>
        NotBefore = 2,

        /// <summary>
        /// Not after
        /// </summary>
        NotAfter = 4,

        /// <summary>
        /// Hostname mismatch
        /// </summary>
        HostnameMismatch = 8,

        /// <summary>
        /// Revoked
        /// </summary>
        Revoked = 16,

        /// <summary>
        /// Bad common name
        /// </summary>
        BadCommonName = 32,

        /// <summary>
        /// Self-signed
        /// </summary>
        SelfSigned = 64,

        /// <summary>
        /// Blacklisted
        /// </summary>
        Blacklisted = 128,

        /// <summary>
        /// Insecure signature
        /// </summary>
        InsecureSignature = 256
    }
}