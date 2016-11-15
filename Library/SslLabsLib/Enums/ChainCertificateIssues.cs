using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum ChainCertificateIssues
    {
        None = 0,

        /// <summary>
        /// Certificate not yet valid
        /// </summary>
        NotYetValid = 1,

        /// <summary>
        /// Certificate expired
        /// </summary>
        Expired = 2,

        /// <summary>
        /// Weak key
        /// </summary>
        WeakKey = 4,

        /// <summary>
        /// Weak signature
        /// </summary>
        WeakSignature = 8,

        /// <summary>
        /// Blacklisted
        /// </summary>
        Blacklisted = 16
    }
}