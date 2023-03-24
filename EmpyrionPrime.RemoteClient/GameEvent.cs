using Eleon.Modding;

namespace EmpyrionPrime.RemoteClient
{
    public readonly struct GameEvent
    {
        public int ClientId { get; }
        public CmdId Id { get; }
        public ushort SequenceNumber { get; }
        public object Payload { get; }

        public GameEvent(int clientId, CmdId id, ushort sequenceNumber, object payload)
        {
            ClientId = clientId;
            Id = id;
            SequenceNumber = sequenceNumber;
            Payload = payload;
        }

        public override string ToString()
        {
            return $"GameEvent<{Id}>(Client: {ClientId}, Seq: {SequenceNumber}, PayloadType: {Payload?.GetType()}";
        }
    }
}
