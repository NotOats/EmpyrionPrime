namespace EmpyrionPrime.Plugin.Types
{
    /// <summary>
    /// Message priority used in <see cref="Eleon.Modding.IdMsgPrio">IdMsgPrio</see>
    /// </summary>
    public enum MessagePriority : byte
    {
        /// <summary>
        /// Audio alert with red box on top of the screen
        /// </summary>
        Alarm = 0,

        /// <summary>
        /// Yellow box on top of the screen, no audio
        /// </summary>
        Alert = 1,

        Info = 2
    }
}
