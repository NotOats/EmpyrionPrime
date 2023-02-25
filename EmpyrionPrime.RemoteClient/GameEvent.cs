using Eleon.Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EmpyrionPrime.RemoteClient
{
    public readonly struct GameEvent
    {
        public int ClientId { get; }
        public CommandId Id { get; }
        public ushort SequenceNumber { get; }
        public object Payload { get; }

        public GameEvent(int clientId, CommandId id, ushort sequenceNumber, object payload)
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
