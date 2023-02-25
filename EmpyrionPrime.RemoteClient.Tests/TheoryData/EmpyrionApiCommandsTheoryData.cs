using AutoBogus;
using Eleon.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Tests.TheoryData
{
    internal class EmpyrionApiCommandsTheoryData : TheoryData<CommandId, object>
    {
        public EmpyrionApiCommandsTheoryData()
        {
            foreach(var kvp in BogusApiObjects)
            {
                if(kvp.Value != null)
                    Add(kvp.Key, kvp.Value);
            }
        }

        private static object FakeObject<T>() where T : class
        {
            return new AutoFaker<T>().Generate();
        }

        private static readonly IReadOnlyDictionary<CommandId, object?> BogusApiObjects = new Dictionary<CommandId, object?>
        {
            // Eleon CmdId values
            { CommandId.Event_Playfield_Loaded,                   FakeObject<PlayfieldLoad>() },
            { CommandId.Event_Playfield_Unloaded,                 FakeObject<PlayfieldLoad>() },
            { CommandId.Request_Playfield_List,                   null },
            { CommandId.Event_Playfield_List,                     FakeObject<PlayfieldList>() },
            { CommandId.Request_Playfield_Stats,                  FakeObject<PString>() },
            { CommandId.Event_Playfield_Stats,                    FakeObject<PlayfieldStats>() },
            { CommandId.Request_Dedi_Stats,                       null },
            { CommandId.Event_Dedi_Stats,                         FakeObject<DediStats>() },
            { CommandId.Request_GlobalStructure_List,             null },
            { CommandId.Request_GlobalStructure_Update,           FakeObject<PString>() },
            { CommandId.Event_GlobalStructure_List,               FakeObject<GlobalStructureList>() },
            { CommandId.Request_Structure_Touch,                  FakeObject<Id>() },
            { CommandId.Request_Structure_BlockStatistics,        FakeObject<Id>() },
            { CommandId.Event_Structure_BlockStatistics,          FakeObject<IdStructureBlockInfo>() },
            { CommandId.Event_Player_Connected,                   FakeObject<Id>() },
            { CommandId.Event_Player_Disconnected,                FakeObject<Id>() },
            { CommandId.Event_Player_ChangedPlayfield,            FakeObject<IdPlayfield>() },
            { CommandId.Request_Player_Info,                      FakeObject<Id>() },
            { CommandId.Event_Player_Info,                        FakeObject<PlayerInfo>() },
            { CommandId.Request_Player_List,                      null },
            { CommandId.Event_Player_List,                        FakeObject<IdList>() },
            { CommandId.Request_Player_GetInventory,              FakeObject<Id>() },
            { CommandId.Request_Player_SetInventory,              FakeObject<Inventory>() },
            { CommandId.Event_Player_Inventory,                   FakeObject<Inventory>() },
            { CommandId.Request_Player_AddItem,                   FakeObject<IdItemStack>() },
            { CommandId.Request_Player_Credits,                   FakeObject<Id>() },
            { CommandId.Request_Player_SetCredits,                FakeObject<IdCredits>() },
            { CommandId.Request_Player_AddCredits,                FakeObject<IdCredits>() },
            { CommandId.Event_Player_Credits,                     FakeObject<IdCredits>() },
            { CommandId.Request_Blueprint_Finish,                 FakeObject<Id>() },
            { CommandId.Request_Blueprint_Resources,              FakeObject<BlueprintResources>() },
            { CommandId.Request_Player_ChangePlayerfield,         FakeObject<IdPlayfieldPositionRotation>() },
            { CommandId.Request_Player_ItemExchange,              FakeObject<ItemExchangeInfo>() },
            { CommandId.Event_Player_ItemExchange,                FakeObject<ItemExchangeInfo>() },
            { CommandId.Request_Player_SetPlayerInfo,             FakeObject<PlayerInfoSet>() },
            { CommandId.Event_Player_DisconnectedWaiting,         FakeObject<Id>() },
            { CommandId.Request_Entity_Teleport,                  FakeObject<IdPositionRotation>() },
            { CommandId.Request_Entity_ChangePlayfield,           FakeObject<IdPlayfieldPositionRotation>() },
            { CommandId.Request_Entity_Destroy,                   FakeObject<Id>() },
            { CommandId.Request_Entity_PosAndRot,                 FakeObject<Id>() },
            { CommandId.Event_Entity_PosAndRot,                   FakeObject<IdPositionRotation>() },
            { CommandId.Event_Faction_Changed,                    FakeObject<FactionChangeInfo>() },
            { CommandId.Request_Entity_Spawn,                     FakeObject<EntitySpawnInfo>() },
            { CommandId.Request_Get_Factions,                     FakeObject<Id>() },
            { CommandId.Event_Get_Factions,                       FakeObject<FactionInfoList>() },
            { CommandId.Event_Statistics,                         FakeObject<StatisticsParam>() },
            { CommandId.Request_NewEntityId,                      null },
            { CommandId.Event_NewEntityId,                        FakeObject<Id>() },
            { CommandId.Request_AlliancesAll,                     null },
            { CommandId.Event_AlliancesAll,                       FakeObject<AlliancesTable>() },
            { CommandId.Request_AlliancesFaction,                 FakeObject<AlliancesFaction>() },
            { CommandId.Event_AlliancesFaction,                   FakeObject<AlliancesFaction>() },
            { CommandId.Request_Load_Playfield,                   FakeObject<PlayfieldLoad>() },
            { CommandId.Event_ChatMessage,                        FakeObject<ChatInfo>() },
            { CommandId.Event_ChatMessageEx,                      FakeObject<ChatMsgData>() },
            { CommandId.Request_ConsoleCommand,                   FakeObject<PString>() },
            { CommandId.Request_GetBannedPlayers,                 null },
            { CommandId.Event_BannedPlayers,                      FakeObject<IdList>() },
            { CommandId.Request_InGameMessage_SinglePlayer,       FakeObject<IdMsgPrio>() },
            { CommandId.Request_InGameMessage_AllPlayers,         FakeObject<IdMsgPrio>() },
            { CommandId.Request_InGameMessage_Faction,            FakeObject<IdMsgPrio>() },
            { CommandId.Request_ShowDialog_SinglePlayer,          FakeObject<DialogBoxData>() },
            { CommandId.Event_DialogButtonIndex,                  FakeObject<IdAndIntValue>() },
            { CommandId.Event_Ok,                                 null },
            { CommandId.Event_Error,                              FakeObject<ErrorInfo>() },
            { CommandId.Event_TraderNPCItemSold,                  FakeObject<TraderNPCItemSoldInfo>() },
            { CommandId.Request_Player_GetAndRemoveInventory,     FakeObject<Id>() },
            { CommandId.Event_Player_GetAndRemoveInventory,       FakeObject<Inventory>() },
            { CommandId.Request_Playfield_Entity_List,            FakeObject<PString>() },
            { CommandId.Event_Playfield_Entity_List,              FakeObject<PlayfieldEntityList>() },
            { CommandId.Request_Entity_Destroy2,                  FakeObject<IdPlayfield>() },
            { CommandId.Request_Entity_Export,                    FakeObject<EntityExportInfo>() },
            { CommandId.Event_ConsoleCommand,                     FakeObject<ConsoleCommandInfo>() },
            { CommandId.Request_Entity_SetName,                   FakeObject<IdPlayfieldName>() },
            // For some reason Eleon made these structs instead of classes... skip testing
            //{ CommandId.Event_PdaStateChange,                     FakeObject<PdaStateInfo>() },
            //{ CommandId.Event_GameEvent,                          FakeObject<GameEventData>() },
        };
    }
}
