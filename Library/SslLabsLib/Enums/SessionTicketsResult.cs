using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum SessionTicketsResult
    {
        /// <summary>
        /// Session tickets are supported
        /// </summary>
        Supported = 1,

        /// <summary>
        /// The implementation is faulty [not implemented]
        /// </summary>
        FaultyImplementation = 2,

        /// <summary>
        /// The server is intolerant to the extension
        /// </summary>
        IntolerantServer = 4
    }
}