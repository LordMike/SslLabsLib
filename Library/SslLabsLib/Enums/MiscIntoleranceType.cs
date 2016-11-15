using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum MiscIntoleranceType
    {
        /// <summary>
        /// Extension intolerance
        /// </summary>
        ExtensionIntolerance = 1,

        /// <summary>
        /// Long handshake intolerance
        /// </summary>
        LongHandshakeIntolerance = 2,

        /// <summary>
        /// Long handshake intolerance workaround success
        /// </summary>
        LongHandshakeIntoleranceWithWorkaround = 4
    }
}