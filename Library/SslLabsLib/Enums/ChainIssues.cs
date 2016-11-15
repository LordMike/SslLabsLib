using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum ChainIssues
    {
        None = 0,

        /// <summary>
        /// Unused
        /// </summary>
        Unused = 1,

        /// <summary>
        /// Incomplete chain (set only when we were able to build a chain by adding missing intermediate certificates from external sources)
        /// </summary>
        IncompleteChain = 2,

        /// <summary>
        /// Chain contains unrelated or duplicate certificates (i.e., certificates that are not part of the same chain)
        /// </summary>
        HasExcessCertficates = 4,

        /// <summary>
        /// The certificates form a chain (trusted or not), but the order is incorrect
        /// </summary>
        BadOrder = 8,

        /// <summary>
        /// Contains a self-signed root certificate (not set for self-signed leafs)
        /// </summary>
        SelfSignedRoot = 16,

        /// <summary>
        /// The certificates form a chain (if we added external certificates, bit 1 will be set), but we could not validate it. If the leaf was trusted, that means that we built a different chain we trusted.
        /// </summary>
        UnableToValidate = 32
    }
}