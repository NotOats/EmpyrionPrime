using Eleon;
using Eleon.Modding;

namespace EmpyrionPrime.Plugin
{
    public delegate void GameEventHandler(CmdId commandId, ushort sequenceNumber, object data);
    public delegate void ChatMessageHandler(MessageData messageData);

    public interface IEmpyrionApi {}

    public interface IBasicEmpyrionApi : IEmpyrionApi
    {
        event GameEventHandler GameEvent;

        void SendEvent(CmdId id, ushort sequenceNumber, object data);
    }

    public interface IExtendedEmpyrionApi : IBasicEmpyrionApi
    {
        event ChatMessageHandler ChatMessage;

        void SendChatMessage(MessageData messageData);
    }
}
