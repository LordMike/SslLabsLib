using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SslLabsLib.Code;
using SslLabsLib.Enums;

namespace SslLabsLib.Objects
{
    public class EndpointDetails
    {
        /// <summary>
        /// Endpoint assessment starting time, in milliseconds since 1970. 
        /// This field is useful when test results are retrieved in several HTTP invocations. 
        /// Then, you should check that the hostStartTime value matches the startTime value of the host.
        /// </summary>
        [JsonConverter(typeof(MillisecondEpochConverter))]
        [JsonProperty("hostStartTime")]
        public DateTime HostStartTime { get; set; }

        /// <summary>
        /// Key information
        /// </summary>
        [JsonProperty("key")]
        public Key Key { get; set; }

        /// <summary>
        /// Certificate information
        /// </summary>
        [JsonProperty("cert")]
        public Cert Cert { get; set; }

        /// <summary>
        /// Chain information
        /// </summary>
        [JsonProperty("chain")]
        public Chain Chain { get; set; }

        /// <summary>
        /// Supported protocols
        /// </summary>
        [JsonProperty("protocols")]
        public List<Protocol> Protocols { get; set; }

        /// <summary>
        /// Supported cipher suites
        /// </summary>
        [JsonProperty("suites")]
        public Suites Suites { get; set; }

        /// <summary>
        /// Contents of the HTTP Server response header when known. 
        /// This field could be absent for one of two reasons: 
        /// 1) the HTTP request failed (check httpStatusCode)
        /// 2) there was no Server response header returned.
        /// </summary>
        [JsonProperty("serverSignature")]
        public string ServerSignature { get; set; }

        /// <summary>
        /// True if this endpoint is reachable via a hostname with the www prefix
        /// </summary>
        [JsonProperty("prefixDelegation")]
        public bool PrefixDelegation { get; set; }

        /// <summary>
        /// True if this endpoint is reachable via a hostname without the www prefix
        /// </summary>
        [JsonProperty("nonPrefixDelegation")]
        public bool NonPrefixDelegation { get; set; }

        /// <summary>
        /// True if the endpoint is vulnerable to the BEAST attack
        /// </summary>
        [JsonProperty("vulnBeast")]
        public bool VulnBeast { get; set; }

        /// <summary>
        /// This is an integer value that describes the endpoint support for renegotiation:
        /// </summary>
        [JsonProperty("renegSupport")]
        public RenegotiationSupport RenegSupport { get; set; }

        /// <summary>
        /// The contents of the Strict-Transport-Security (STS) response header, if seen
        /// </summary>
        [Obsolete("Deprecated")]
        [JsonProperty("stsResponseHeader")]
        public string StrictTransportSecurityResponseHeader { get; set; }

        /// <summary>
        /// The maxAge parameter extracted from the STS parameters; null if STS not seen, or -1 if the specified value is invalid (e.g., not a zero or a positive integer; the maximum value currently supported is 2,147,483,647)
        /// </summary>
        [Obsolete("Deprecated")]
        [JsonProperty("stsMaxAge")]
        public int StrictTransportSecurityMaxAge { get; set; }

        /// <summary>
        /// True if the includeSubDomains STS parameter is set; null if STS not seen
        /// </summary>
        [Obsolete("Deprecated")]
        [JsonProperty("stsSubdomains")]
        public bool StrictTransportSecuritySubdomains { get; set; }

        /// <summary>
        /// The contents of the Public-Key-Pinning response header, if seen
        /// </summary>
        [Obsolete("Deprecated")]
        [JsonProperty("pkpResponseHeader")]
        public string PublicKeyPinningResponseHeader { get; set; }

        /// <summary>
        /// This is an integer value that describes endpoint support for session resumption.
        /// </summary>
        [JsonProperty("sessionResumption")]
        public SessionResumptionResult SessionResumption { get; set; }

        /// <summary>
        /// Integer value that describes supported compression methods
        /// </summary>
        [JsonProperty("compressionMethods")]
        public CompressionMethodsSupported CompressionMethods { get; set; }

        /// <summary>
        /// True if the server supports NPN
        /// </summary>
        [JsonProperty("supportsNpn")]
        public bool SupportsNpn { get; set; }

        /// <summary>
        /// List of supported protocols, separated by spaces
        /// </summary>
        [JsonProperty("npnProtocols", NullValueHandling = NullValueHandling.Ignore)]
        public string NpnProtocols { get; set; }

        /// <summary>
        /// Indicates support for Session Tickets
        /// </summary>
        [JsonProperty("sessionTickets")]
        public SessionTicketsResult SessionTickets { get; set; }

        /// <summary>
        /// True if OCSP stapling is deployed on the server
        /// </summary>
        [JsonProperty("ocspStapling")]
        public bool OcspStapling { get; set; }

        /// <summary>
        /// RevocationStatus for the stapled OCSP response.
        /// </summary>
        [JsonProperty("staplingRevocationStatus")]
        public RevocationStatus StaplingRevocationStatus { get; set; }

        /// <summary>
        /// Description of the problem with the stapled OCSP response, if any.
        /// </summary>
        [JsonProperty("staplingRevocationErrorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string StaplingRevocationErrorMessage { get; set; }

        /// <summary>
        /// If SNI support is required to access the web site.
        /// </summary>
        [JsonProperty("sniRequired")]
        public bool SniRequired { get; set; }

        /// <summary>
        /// Status code of the final HTTP response seen. When submitting HTTP requests, redirections are followed, but only if they lead to the same hostname. 
        /// If this field is not available, that means the HTTP request failed.
        /// </summary>
        [JsonProperty("httpStatusCode")]
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// Available on a server that responded with a redirection to some other hostname.
        /// </summary>
        [JsonProperty("httpForwarding", NullValueHandling = NullValueHandling.Ignore)]
        public string HttpForwarding { get; set; }

        /// <summary>
        /// True if the server supports at least one RC4 suite.
        /// </summary>
        [JsonProperty("supportsRc4")]
        public bool SupportsRc4 { get; set; }

        /// <summary>
        /// True if RC4 is used with modern clients.
        /// </summary>
        [JsonProperty("rc4WithModern")]
        public bool Rc4WithModern { get; set; }

        /// <summary>
        /// True if only RC4 suites are supported.
        /// </summary>
        [JsonProperty("rc4Only")]
        public bool Rc4Only { get; set; }

        /// <summary>
        /// Indicates support for Forward Secrecy
        /// </summary>
        [JsonProperty("forwardSecrecy")]
        public ForwardSecrecyResult ForwardSecrecy { get; set; }

        /// <summary>
        /// Indicates protocol version intolerance issues:
        /// </summary>
        [JsonProperty("protocolIntolerance")]
        public ProtocolIntoleranceType ProtocolIntolerance { get; set; }

        /// <summary>
        /// Indicates various other types of intolerance:
        /// </summary>
        [JsonProperty("miscIntolerance")]
        public MiscIntoleranceType MiscIntolerance { get; set; }

        /// <summary>
        /// Client simulation details
        /// </summary>
        [JsonProperty("sims")]
        public SimulationDetails Simulations { get; set; }

        /// <summary>
        /// True if the server is vulnerable to the Heartbleed attack.
        /// </summary>
        [JsonProperty("heartbleed")]
        public bool Heartbleed { get; set; }

        /// <summary>
        /// True if the server supports the Heartbeat extension.
        /// </summary>
        [JsonProperty("heartbeat")]
        public bool Heartbeat { get; set; }

        /// <summary>
        /// Results of the CVE-2014-0224 test
        /// </summary>
        [JsonProperty("openSslCcs")]
        public OpenSslCcsResult OpenSslCcs { get; set; }

        /// <summary>
        /// Results of the CVE-2016-2107 test:
        /// </summary>
        [JsonProperty("openSSLLuckyMinus20")]
        public OpenSSLLuckyMinus20Result OpenSSLLuckyMinus20 { get; set; }

        /// <summary>
        /// True if the endpoint is vulnerable to POODLE; false otherwise
        /// </summary>
        [JsonProperty("poodle")]
        public bool Poodle { get; set; }

        /// <summary>
        /// Results of the POODLE TLS test
        /// </summary>
        [JsonProperty("poodleTls")]
        public PoodleResult PoodleTls { get; set; }

        /// <summary>
        /// True if the server supports TLS_FALLBACK_SCSV, false if it doesn't. This field will not be available if the server's support 
        /// for TLS_FALLBACK_SCSV can't be tested because it supports only one protocol version (e.g., only TLS 1.2).
        /// </summary>
        [JsonProperty("fallbackScsv")]
        public bool FallbackScsv { get; set; }

        /// <summary>
        /// True of the server is vulnerable to the FREAK attack, meaning it supports 512-bit key exchange.
        /// </summary>
        [JsonProperty("freak")]
        public bool Freak { get; set; }

        /// <summary>
        /// Information about the availability of certificate transparency information (embedded SCTs):
        /// </summary>
        [JsonProperty("hasSct")]
        public SctResult HasSct { get; set; }

        /// <summary>
        /// List of hex-encoded DH primes used by the server
        /// </summary>
        [JsonProperty("dhPrimes")]
        public string[] DhPrimes { get; set; }

        /// <summary>
        /// Whether the server uses known DH primes:
        /// </summary>
        [JsonProperty("dhUsesKnownPrimes")]
        public DhKnownPrimesResult DhUsesKnownPrimes { get; set; }

        /// <summary>
        /// True if the DH ephemeral server value is reused.
        /// </summary>
        [JsonProperty("dhYsReuse")]
        public bool DhYsReuse { get; set; }

        /// <summary>
        /// True if the server uses DH parameters weaker than 1024 bits.
        /// </summary>
        [JsonProperty("logjam")]
        public bool LogJam { get; set; }

        /// <summary>
        /// True if the server takes into account client preferences when deciding if to use ChaCha20 suites.
        /// </summary>
        [JsonProperty("chaCha20Preference")]
        public bool ChaCha20Preference { get; set; }

        /// <summary>
        /// Server's HSTS policy. Experimental.
        /// </summary>
        [JsonProperty("hstsPolicy")]
        public HstsPolicy HstsPolicy { get; set; }

        /// <summary>
        /// Information about preloaded HSTS policies.
        /// </summary>
        [JsonProperty("hstsPreloads")]
        public List<HstsPreload> HstsPreloads { get; set; }

        /// <summary>
        /// Server's HPKP policy. Experimental.
        /// </summary>
        [JsonProperty("hpkpPolicy")]
        public HpkpPolicy HpkpPolicy { get; set; }

        /// <summary>
        /// Server's HPKP RO (Report Only) policy. Experimental.
        /// </summary>
        [JsonProperty("hpkpRoPolicy")]
        public HpkpPolicy HpkpRoPolicy { get; set; }

        /// <summary>
        /// List of drown hosts. Experimental.
        /// </summary>
        [JsonProperty("drownHosts")]
        public List<DrownHost> DrownHosts { get; set; }

        /// <summary>
        /// True if error occurred in drown test.
        /// </summary>
        [JsonProperty("drownErrors")]
        public bool DrownErrors { get; set; }

        /// <summary>
        /// True if server vulnerable to drown attack.
        /// </summary>
        [JsonProperty("drownVulnerable")]
        public bool DrownVulnerable { get; set; }

        public EndpointDetails()
        {
            Key = new Key();
            Cert = new Cert();
            Chain = new Chain();
            Protocols = new List<Protocol>();
            Suites = new Suites();
            Simulations = new SimulationDetails();
            HstsPreloads = new List<HstsPreload>();
            DrownHosts = new List<DrownHost>();
        }
    }
}