namespace SslLabsLib.Enums
{
    public enum HpkpStatus
    {
        /// <summary>
        /// Either before the server is checked or when its HTTP response headers are not available
        /// </summary>
        Unknown,

        /// <summary>
        /// Header not present
        /// </summary>
        Absent,

        /// <summary>
        /// Header present, but couldn't be parsed
        /// </summary>
        Invalid,

        /// <summary>
        /// Header present and syntatically correct, but HPKP is disabled
        /// </summary>
        Disabled,

        /// <summary>
        /// Header present and syntatically correct, incorrectly used
        /// </summary>
        Incomplete,

        /// <summary>
        /// Header present, syntatically correct, and correctly used
        /// </summary>
        Valid
    }
}