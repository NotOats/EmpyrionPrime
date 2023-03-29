using Eleon;
using EmpyrionPrime.Schema.ModInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EmpyrionPrime.RemoteClient.Tests")]
namespace EmpyrionPrime.RemoteClient.Epm.Serializers
{
    internal static class GameEventSerializer
    {
        public static byte[] Serialize(GameEventId id, object obj)
        {
            if (!CommandIdTypeMap.TryGetValue(id, out Type type) || type == null)
                return null;

            if (type != obj.GetType())
                throw new ArgumentException("Object does not match the type associated with this CommandId", nameof(obj));

            // TODO: Use MemoryPool and CommunityToolkit.HighPerformance's IMemoryOwner.AsStream()?
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.NonGeneric.Serialize(stream, obj);

                return stream.ToArray();
            }
        }

        public static object Deserialize(GameEventId id, byte[] data)
        {
            if (!CommandIdTypeMap.TryGetValue(id, out Type type) || type == null)
                return null;

            using (var stream = new MemoryStream(data))
            {
                return ProtoBuf.Serializer.NonGeneric.Deserialize(type, stream);
            }
        }

        private readonly static IReadOnlyDictionary<GameEventId, Type> CommandIdTypeMap =
            ApiSchema.CommandIdTypeMap.ToDictionary(x => (GameEventId)x.Key, x => x.Value)
            .Union(new Dictionary<GameEventId, Type>
            {
                // Custom Events from EPM
                // TODO: Move these into their own custom EPM remote handler
                { GameEventId.Request_Chat,         typeof(MessageData) },
                { GameEventId.Event_Chat,           typeof(MessageData) },
                { GameEventId.Request_DialogAction, null },
            }).ToDictionary(x => x.Key, x => x.Value);
    }
}
