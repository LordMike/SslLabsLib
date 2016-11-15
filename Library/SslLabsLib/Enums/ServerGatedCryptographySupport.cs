using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum ServerGatedCryptographySupport
    {
        None = 0,

        /// <summary>
        /// Netscape SGC
        /// </summary>
        NetscapeSGC = 1,

        /// <summary>
        /// Microsoft SGC
        /// </summary>
        MicrosoftSGC = 2
    }
}