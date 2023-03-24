using Eleon;
using EmpyrionPrime.Schema.ModInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EmpyrionPrime.RemoteClient.Tests")]
namespace EmpyrionPrime.RemoteClient
{
    internal static class CommandSerializer
    {
        public static byte[] Serialize(CommandId id, object obj)
        {
            if(!CommandIdTypeMap.TryGetValue(id, out Type type) || type == null)
                return null;

            if(type != obj.GetType())
                throw new ArgumentException("Object does not match the type associated with this CommandId", nameof(obj));

            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.NonGeneric.Serialize(stream, obj);

                return stream.ToArray();
            }
        }

        public static object Deserialize(CommandId id, byte[] data)
        {
            if (!CommandIdTypeMap.TryGetValue(id, out Type type) || type == null)
                return null;

            using (var stream = new MemoryStream(data))
            {
                return ProtoBuf.Serializer.NonGeneric.Deserialize(type, stream);
            }
        }

        private readonly static IReadOnlyDictionary<CommandId, Type> CommandIdTypeMap =
            ApiSchema.CommandIdTypeMap.ToDictionary(x => (CommandId)x.Key, x => x.Value)
            .Union(new Dictionary<CommandId, Type>
            {
                // Custom Events from EPM
                // TODO: Move these into their own custom EPM remote handler
                { CommandId.Request_Chat,         typeof(MessageData) },
                { CommandId.Event_Chat,           typeof(MessageData) },
                { CommandId.Request_DialogAction, null },
            }).ToDictionary(x => x.Key, x => x.Value);
    }
}
