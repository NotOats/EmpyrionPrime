using Eleon;
using Eleon.Modding;

namespace EmpyrionPrime.Plugin
{
    public delegate void ChatMessageHandler(MessageData messageData);

    public interface IEmpyrionGameApi
    {
        ModGameAPI ModGameAPI { get; }

        event ChatMessageHandler ChatMessage;

        void SendChatMessage(MessageData messageData);
    }
}
