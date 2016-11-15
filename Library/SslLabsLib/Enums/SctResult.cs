using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum SctResult
    {
        None = 0,

        /// <summary>
        /// SCT in certificate
        /// </summary>
        SctInCertificate = 1,

        /// <summary>
        /// SCT in the stapled OCSP response
        /// </summary>
        SctInStapledOcspResponse = 2,

        /// <summary>
        /// SCT in the TLS extension (ServerHello)
        /// </summary>
        SctInTlsExtension = 4
    }
}