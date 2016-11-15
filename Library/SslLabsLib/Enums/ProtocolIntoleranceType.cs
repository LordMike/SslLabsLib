using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum ProtocolIntoleranceType
    {
        /// <summary>
        /// TLS 1.0
        /// </summary>
        Tls10 = 1,

        /// <summary>
        /// TLS 1.1
        /// </summary>
        Tls11 = 2,

        /// <summary>
        /// TLS 1.2
        /// </summary>
        Tls12 = 4,

        /// <summary>
        /// TLS 1.3
        /// </summary>
        Tls13 = 8,

        /// <summary>
        /// TLS 1.152
        /// </summary>
        Tls1152 = 16,

        /// <summary>
        /// TLS 2.152
        /// </summary>
        Tls2152 = 32
    }
}