using Eleon;

namespace EmpyrionPrime.Plugin.Api
{
    /// <summary>
    /// A chat message handler
    /// </summary>
    /// <param name="messageData">The chat message's data</param>
    public delegate void ChatMessageHandler(MessageData messageData);

    /// <summary>
    /// Extended Empyrion api used to replicate some functions of IModApi.
    /// This interface extends the basic Empyrion api.
    /// </summary>
    public interface IExtendedEmpyrionApi : IBasicEmpyrionApi
    {
        /// <summary>
        /// Event triggers on each new chat message received from IModApi.IApplication.ChatMessageSent
        /// </summary>
        event ChatMessageHandler ChatMessage;

        /// <summary>
        /// Forwards a chat message to IModApi.IApplication.SendChatMessage
        /// </summary>
        /// <param name="messageData">The chat message data</param>
        void SendChatMessage(MessageData messageData);
    }
}
