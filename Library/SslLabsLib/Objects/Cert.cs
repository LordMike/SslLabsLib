using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class Cert
    {
        /// <summary>
        /// Certificate subject
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Common names extracted from the subject
        /// </summary>
        [JsonProperty("commonNames")]
        public List<string> CommonNames { get; set; }

        /// <summary>
        /// Alternative names
        /// </summary>
        [JsonProperty("altNames")]
        public List<string> AltNames { get; set; }

        /// <summary>
        /// Timestamp before which the certificate is not valid
        /// </summary>
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("notBefore")]
        public DateTime NotBefore { get; set; }

        /// <summary>
        /// Timestamp after which the certificate is not valid
        /// </summary>
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("notAfter")]
        public DateTime NotAfter { get; set; }

        /// <summary>
        /// Issuer subject
        /// </summary>
        [JsonProperty("issuerSubject")]
        public string IssuerSubject { get; set; }

        /// <summary>
        /// Certificate signature algorithm
        /// </summary>
        [JsonProperty("sigAlg")]
        public string SigAlg { get; set; }

        /// <summary>
        /// Issuer name
        /// </summary>
        [JsonProperty("issuerLabel")]
        public string IssuerLabel { get; set; }

        /// <summary>
        /// A number that represents revocation information present in the certificate:
        /// </summary>
        [JsonProperty("revocationInfo")]
        public RevocationInfo RevocationInfo { get; set; }

        /// <summary>
        /// CRL URIs extracted from the certificate
        /// </summary>
        [JsonProperty("crlURIs")]
        public List<string> CrlURIs { get; set; }

        /// <summary>
        /// OCSP URIs extracted from the certificate
        /// </summary>
        [JsonProperty("ocspURIs")]
        public List<string> OcspUrIs { get; set; }

        /// <summary>
        /// A number that describes the revocation status of the certificate:
        /// </summary>
        [JsonProperty("revocationStatus")]
        public RevocationStatus RevocationStatus { get; set; }

        /// <summary>
        /// Same as revocationStatus, but only for the CRL information (if any).
        /// </summary>
        [JsonProperty("crlRevocationStatus")]
        public RevocationStatus CrlRevocationStatus { get; set; }

        /// <summary>
        /// Same as revocationStatus, but only for the OCSP information (if any).
        /// </summary>
        [JsonProperty("ocspRevocationStatus")]
        public RevocationStatus OcspRevocationStatus { get; set; }

        /// <summary>
        /// Server Gated Cryptography support; integer:
        /// </summary>
        [JsonProperty("sgc")]
        public ServerGatedCryptographySupport ServerGatedCryptography { get; set; }

        /// <summary>
        /// E for Extended Validation certificates; may be null if unable to determine
        /// </summary>
        [JsonProperty("validationType", NullValueHandling = NullValueHandling.Ignore)]
        public string ValidationType { get; set; }

        /// <summary>
        /// List of certificate issues, one bit per issue:
        /// </summary>
        [JsonProperty("issues")]
        public CertificateIssues Issues { get; set; }

        /// <summary>
        /// True if the certificate contains an embedded SCT; false otherwise.
        /// </summary> 
        [JsonProperty("sct")]
        public bool ContainsSignedCertificateTimestamp { get; set; }

        /// <summary>
        /// Describes the must staple feature extension status:
        /// </summary>
        [JsonProperty("mustStaple")]
        public MustStapleStatus MustStapleStatus { get; set; }

        /// <summary>
        /// SHA1 hash of certificate
        /// </summary>
        [JsonProperty("sha1Hash")]
        public string Sha1Hash { get; set; }

        /// <summary>
        /// SHA256 hash of certificate for HPKP pinning
        /// </summary>
        [JsonProperty("pinSha256")]
        public string PinSha256 { get; set; }

        public Cert()
        {
            CommonNames = new List<string>();
            AltNames = new List<string>();
            CrlURIs = new List<string>();
            OcspUrIs = new List<string>();
        }
    }
}