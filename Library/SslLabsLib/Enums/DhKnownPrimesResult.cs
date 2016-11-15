namespace SslLabsLib.Enums
{
    public enum DhKnownPrimesResult
    {
        /// <summary>
        /// No
        /// </summary>
        No = 0,

        /// <summary>
        /// Yes, but they're not weak
        /// </summary>
        Yes = 1,

        /// <summary>
        /// Yes and they're weak
        /// </summary>
        YesAndWeak = 2
    }
}