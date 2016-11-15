using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum ForwardSecrecyResult
    {
        /// <summary>
        /// At least one browser from our simulations negotiated a Forward Secrecy suite.
        /// </summary>
        AtLeastOne = 1,

        /// <summary>
        /// FS is achieved with modern clients. For example, the server supports ECDHE suites, but not DHE.
        /// </summary>
        ModernClients = 2,

        /// <summary>
        /// All simulated clients achieve FS. In other words, this requires an ECDHE + DHE combination to be supported.
        /// </summary>
        AllSimulatedClients = 4
    }
}