namespace SslLabsLib.Enums
{
    public enum HstsPreloadStatus
    {
        Error,

        /// <summary>
        /// Either before the preload status is checked, or if the information is not available for some reason.
        /// </summary>
        Unknown,

        /// <summary>
        /// Header not present
        /// </summary>
        Absent,

        /// <summary>
        /// Header present and syntatically correct
        /// </summary>
        Present
    }
}