using System;

namespace SslLabsLib.Enums
{
    /// <summary>
    /// Indicates domain name delegation with and without the www prefix
    /// </summary>
    [Flags]
    public enum Delegation
    {
        /// <summary>
        /// Non-prefixed access
        /// </summary>
        NonPrefixedAccess = 1,

        /// <summary>
        /// Prefixed access
        /// </summary>
        PrefixedAccess = 2
    }
}