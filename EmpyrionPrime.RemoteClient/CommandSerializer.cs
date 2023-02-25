using Eleon.Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("EmpyrionPrime.RemoteClient.Tests")]
namespace EmpyrionPrime.RemoteClient
{
    internal static class CommandSerializer
    {
        public static byte[] Serialize(CommandId id, object obj)
        {
            if (!ProtobufWrappers.TryGetValue(id, out IProfoBufWrapper wrapper))
                return null;

            if (wrapper == null)
                return null;

            if (wrapper.ObjectType != obj.GetType())
                throw new ArgumentException("Object does not match the type associated with this CommandId", nameof(obj));

            return wrapper.Serialize(obj);
        }

        public static object Deserialize(CommandId id, byte[] data)
        {
            if (!ProtobufWrappers.TryGetValue(id, out IProfoBufWrapper wrapper))
                return null;

            return wrapper.Deserialize(data);
        }

        private readonly static IReadOnlyDictionary<CommandId, IProfoBufWrapper> ProtobufWrappers = new Dictionary<CommandId, IProfoBufWrapper> 
        {
            // From Eleon.Modding.CmdId
            { CommandId.Event_Playfield_Loaded,                   new ProtoBufWrapper<PlayfieldLoad>() },
            { CommandId.Event_Playfield_Unloaded,                 new ProtoBufWrapper<PlayfieldLoad>() },
            { CommandId.Request_Playfield_List,                   null },
            { CommandId.Event_Playfield_List,                     new ProtoBufWrapper<PlayfieldList>() },
            { CommandId.Request_Playfield_Stats,                  new ProtoBufWrapper<PString>() },
            { CommandId.Event_Playfield_Stats,                    new ProtoBufWrapper<PlayfieldStats>() },
            { CommandId.Request_Dedi_Stats,                       null },
            { CommandId.Event_Dedi_Stats,                         new ProtoBufWrapper<DediStats>() },
            { CommandId.Request_GlobalStructure_List,             null },
            { CommandId.Request_GlobalStructure_Update,           new ProtoBufWrapper<PString>() },
            { CommandId.Event_GlobalStructure_List,               new ProtoBufWrapper<GlobalStructureList>() },
            { CommandId.Request_Structure_Touch,                  new ProtoBufWrapper<Id>() },
            { CommandId.Request_Structure_BlockStatistics,        new ProtoBufWrapper<Id>() },
            { CommandId.Event_Structure_BlockStatistics,          new ProtoBufWrapper<IdStructureBlockInfo>() },
            { CommandId.Event_Player_Connected,                   new ProtoBufWrapper<Id>() },
            { CommandId.Event_Player_Disconnected,                new ProtoBufWrapper<Id>() },
            { CommandId.Event_Player_ChangedPlayfield,            new ProtoBufWrapper<IdPlayfield>() },
            { CommandId.Request_Player_Info,                      new ProtoBufWrapper<Id>() },
            { CommandId.Event_Player_Info,                        new ProtoBufWrapper<PlayerInfo>() },
            { CommandId.Request_Player_List,                      null },
            { CommandId.Event_Player_List,                        new ProtoBufWrapper<IdList>() },
            { CommandId.Request_Player_GetInventory,              new ProtoBufWrapper<Id>() },
            { CommandId.Request_Player_SetInventory,              new ProtoBufWrapper<Inventory>() },
            { CommandId.Event_Player_Inventory,                   new ProtoBufWrapper<Inventory>() },
            { CommandId.Request_Player_AddItem,                   new ProtoBufWrapper<IdItemStack>() },
            { CommandId.Request_Player_Credits,                   new ProtoBufWrapper<Id>() },
            { CommandId.Request_Player_SetCredits,                new ProtoBufWrapper<IdCredits>() },
            { CommandId.Request_Player_AddCredits,                new ProtoBufWrapper<IdCredits>() },
            { CommandId.Event_Player_Credits,                     new ProtoBufWrapper<IdCredits>() },
            { CommandId.Request_Blueprint_Finish,                 new ProtoBufWrapper<Id>() },
            { CommandId.Request_Blueprint_Resources,              new ProtoBufWrapper<BlueprintResources>() },
            { CommandId.Request_Player_ChangePlayerfield,         new ProtoBufWrapper<IdPlayfieldPositionRotation>() },
            { CommandId.Request_Player_ItemExchange,              new ProtoBufWrapper<ItemExchangeInfo>() },
            { CommandId.Event_Player_ItemExchange,                new ProtoBufWrapper<ItemExchangeInfo>() },
            { CommandId.Request_Player_SetPlayerInfo,             new ProtoBufWrapper<PlayerInfoSet>() },
            { CommandId.Event_Player_DisconnectedWaiting,         new ProtoBufWrapper<Id>() },
            { CommandId.Request_Entity_Teleport,                  new ProtoBufWrapper<IdPositionRotation>() },
            { CommandId.Request_Entity_ChangePlayfield,           new ProtoBufWrapper<IdPlayfieldPositionRotation>() },
            { CommandId.Request_Entity_Destroy,                   new ProtoBufWrapper<Id>() },
            { CommandId.Request_Entity_PosAndRot,                 new ProtoBufWrapper<Id>() },
            { CommandId.Event_Entity_PosAndRot,                   new ProtoBufWrapper<IdPositionRotation>() },
            { CommandId.Event_Faction_Changed,                    new ProtoBufWrapper<FactionChangeInfo>() },
            { CommandId.Request_Entity_Spawn,                     new ProtoBufWrapper<EntitySpawnInfo>() },
            { CommandId.Request_Get_Factions,                     new ProtoBufWrapper<Id>() },
            { CommandId.Event_Get_Factions,                       new ProtoBufWrapper<FactionInfoList>() },
            { CommandId.Event_Statistics,                         new ProtoBufWrapper<StatisticsParam>() },
            { CommandId.Request_NewEntityId,                      null },
            { CommandId.Event_NewEntityId,                        new ProtoBufWrapper<Id>() },
            { CommandId.Request_AlliancesAll,                     null },
            { CommandId.Event_AlliancesAll,                       new ProtoBufWrapper<AlliancesTable>() },
            { CommandId.Request_AlliancesFaction,                 new ProtoBufWrapper<AlliancesFaction>() },
            { CommandId.Event_AlliancesFaction,                   new ProtoBufWrapper<AlliancesFaction>() },
            { CommandId.Request_Load_Playfield,                   new ProtoBufWrapper<PlayfieldLoad>() },
            { CommandId.Event_ChatMessage,                        new ProtoBufWrapper<ChatInfo>() },
            { CommandId.Event_ChatMessageEx,                      new ProtoBufWrapper<ChatMsgData>() },
            { CommandId.Request_ConsoleCommand,                   new ProtoBufWrapper<PString>() },
            { CommandId.Request_GetBannedPlayers,                 null },
            { CommandId.Event_BannedPlayers,                      new ProtoBufWrapper<IdList>() },
            { CommandId.Request_InGameMessage_SinglePlayer,       new ProtoBufWrapper<IdMsgPrio>() },
            { CommandId.Request_InGameMessage_AllPlayers,         new ProtoBufWrapper<IdMsgPrio>() },
            { CommandId.Request_InGameMessage_Faction,            new ProtoBufWrapper<IdMsgPrio>() },
            { CommandId.Request_ShowDialog_SinglePlayer,          new ProtoBufWrapper<DialogBoxData>() },
            { CommandId.Event_DialogButtonIndex,                  new ProtoBufWrapper<IdAndIntValue>() },
            { CommandId.Event_Ok,                                 null },
            { CommandId.Event_Error,                              new ProtoBufWrapper<ErrorInfo>() },
            { CommandId.Event_TraderNPCItemSold,                  new ProtoBufWrapper<TraderNPCItemSoldInfo>() },
            { CommandId.Request_Player_GetAndRemoveInventory,     new ProtoBufWrapper<Id>() },
            { CommandId.Event_Player_GetAndRemoveInventory,       new ProtoBufWrapper<Inventory>() },
            { CommandId.Request_Playfield_Entity_List,            new ProtoBufWrapper<PString>() },
            { CommandId.Event_Playfield_Entity_List,              new ProtoBufWrapper<PlayfieldEntityList>() },
            { CommandId.Request_Entity_Destroy2,                  new ProtoBufWrapper<IdPlayfield>() },
            { CommandId.Request_Entity_Export,                    new ProtoBufWrapper<EntityExportInfo>() },
            { CommandId.Event_ConsoleCommand,                     new ProtoBufWrapper<ConsoleCommandInfo>() },
            { CommandId.Request_Entity_SetName,                   new ProtoBufWrapper<IdPlayfieldName>() },
            { CommandId.Event_PdaStateChange,                     new ProtoBufWrapper<PdaStateInfo>() },
            { CommandId.Event_GameEvent,                          new ProtoBufWrapper<GameEventData>() },

            // Custom Events

        };

        private interface IProfoBufWrapper
        {
            Type ObjectType { get; }

            byte[] Serialize(object obj);
            object Deserialize(byte[] data);
        }

        private class ProtoBufWrapper<T> : IProfoBufWrapper
        {
            private readonly static IReadOnlyList<MethodInfo> _protoBufMethods = typeof(ProtoBuf.Serializer).GetMethods().ToList();

            private readonly MethodInfo _serializer;
            private readonly MethodInfo _deserializer;

            public Type ObjectType { get; }

            public ProtoBufWrapper()
            {
                ObjectType = typeof(T);

                // Look for this method: public static void Serialize<T>(Stream destination, T instance)
                _serializer = _protoBufMethods.Single(
                    x => x.Name == "Serialize"
                        && x.IsGenericMethodDefinition
                        && x.GetParameters().Length == 2
                        && x.GetParameters()[0].ParameterType == typeof(Stream))
                    .MakeGenericMethod(typeof(T));

                if (_serializer == null)
                    throw new Exception($"Failed to find Serialize method for type '{ObjectType}'");

                /*
                // GetMethod is supposed to be faster/more efficient, might not be true since we have to do it above...
                _deserializer = _protoBufMethods.Single(
                    x => x.Name == "Deserialize"
                        && x.IsGenericMethodDefinition
                        && x.GetParameters().Length == 1
                        && x.GetParameters()[0].ParameterType == typeof(Stream))
                    .MakeGenericMethod(typeof(T));
                */

                // There's only one Deserialize: public static T Deserialize<T>(Stream source)
                _deserializer = typeof(ProtoBuf.Serializer)
                    .GetMethod("Deserialize", new Type[] { typeof(Stream) })
                    .MakeGenericMethod(typeof(T));

                if (_deserializer == null)
                    throw new Exception($"Failed to find Deserialize method for type '{ObjectType}'");
            }

            public byte[] Serialize(object obj)
            {
                using (var stream = new MemoryStream())
                {
                    _serializer.Invoke(null, new object[] { stream, obj });

                    return stream.ToArray();
                }
            }

            public object Deserialize(byte[] data)
            {
                using (var stream = new MemoryStream(data))
                {
                    return _deserializer.Invoke(null, new object[] { stream });
                }
            }
        }
    }
}
