using Eleon;
using Eleon.Modding;

namespace EmpyrionPrime.Plugin
{
    public delegate void GameEventHandler(CmdId commandId, ushort sequenceNumber, object data);
    public delegate void ChatMessageHandler(MessageData messageData);

    public interface IEmpyrionGameApi
    {
        ModGameAPI ModGameAPI { get; }

        event ChatMessageHandler ChatMessage;
        event GameEventHandler GameEvent;

        void SendChatMessage(MessageData messageData);
    }
}
