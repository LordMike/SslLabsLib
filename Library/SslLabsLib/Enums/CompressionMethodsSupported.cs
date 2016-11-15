using System;

namespace SslLabsLib.Enums
{
    [Flags]
    public enum CompressionMethodsSupported
    {
        None = 0,
        Deflate = 1
    }
}