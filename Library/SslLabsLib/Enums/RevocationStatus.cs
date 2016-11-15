namespace SslLabsLib.Enums
{
    public enum RevocationStatus
    {
        /// <summary>
        /// Not checked
        /// </summary>
        NotChecked = 0,

        /// <summary>
        /// Certificate revoked
        /// </summary>
        CertificateRevoked = 1,

        /// <summary>
        /// Certificate not revoked
        /// </summary>
        CertificateNotRevoked = 2,

        /// <summary>
        /// Revocation check error
        /// </summary>
        RevocationCheckError = 3,

        /// <summary>
        /// No revocation information
        /// </summary>
        NoRevocationInformation = 4,

        /// <summary>
        /// Internal error
        /// </summary>
        InternalError = 5,
    }
}