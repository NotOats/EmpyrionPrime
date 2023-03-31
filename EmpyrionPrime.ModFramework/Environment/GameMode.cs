namespace EmpyrionPrime.ModFramework.Environment
{
    public enum GameMode
    {
        /// <summary>
        /// Invalid game mode
        /// </summary>
        Invalid,

        /// <summary>
        /// Default survival
        /// </summary>
        Survival,

        /// <summary>
        /// Creative, full access
        /// </summary>
        Creative,

        /// <summary>
        /// Limited Creative mode: no block mirroring, etc
        /// </summary>
        Freedom,

        /// <summary>
        /// Debug mode
        /// </summary>
        Debug = 100
    }
}
