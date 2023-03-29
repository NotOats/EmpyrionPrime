using AutoBogus;
using Eleon.Modding;
using EmpyrionPrime.RemoteClient.Epm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Tests.TheoryData;

internal class EmpyrionApiCommandsTheoryData : TheoryData<GameEventId, object>
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

    private static readonly IReadOnlyDictionary<GameEventId, object?> BogusApiObjects = new Dictionary<GameEventId, object?>
    {
        // Eleon CmdId values
        { GameEventId.Event_Playfield_Loaded,                   FakeObject<PlayfieldLoad>() },
        { GameEventId.Event_Playfield_Unloaded,                 FakeObject<PlayfieldLoad>() },
        { GameEventId.Request_Playfield_List,                   null },
        { GameEventId.Event_Playfield_List,                     FakeObject<PlayfieldList>() },
        { GameEventId.Request_Playfield_Stats,                  FakeObject<PString>() },
        { GameEventId.Event_Playfield_Stats,                    FakeObject<PlayfieldStats>() },
        { GameEventId.Request_Dedi_Stats,                       null },
        { GameEventId.Event_Dedi_Stats,                         FakeObject<DediStats>() },
        { GameEventId.Request_GlobalStructure_List,             null },
        { GameEventId.Request_GlobalStructure_Update,           FakeObject<PString>() },
        { GameEventId.Event_GlobalStructure_List,               FakeObject<GlobalStructureList>() },
        { GameEventId.Request_Structure_Touch,                  FakeObject<Id>() },
        { GameEventId.Request_Structure_BlockStatistics,        FakeObject<Id>() },
        { GameEventId.Event_Structure_BlockStatistics,          FakeObject<IdStructureBlockInfo>() },
        { GameEventId.Event_Player_Connected,                   FakeObject<Id>() },
        { GameEventId.Event_Player_Disconnected,                FakeObject<Id>() },
        { GameEventId.Event_Player_ChangedPlayfield,            FakeObject<IdPlayfield>() },
        { GameEventId.Request_Player_Info,                      FakeObject<Id>() },
        { GameEventId.Event_Player_Info,                        FakeObject<PlayerInfo>() },
        { GameEventId.Request_Player_List,                      null },
        { GameEventId.Event_Player_List,                        FakeObject<IdList>() },
        { GameEventId.Request_Player_GetInventory,              FakeObject<Id>() },
        { GameEventId.Request_Player_SetInventory,              FakeObject<Inventory>() },
        { GameEventId.Event_Player_Inventory,                   FakeObject<Inventory>() },
        { GameEventId.Request_Player_AddItem,                   FakeObject<IdItemStack>() },
        { GameEventId.Request_Player_Credits,                   FakeObject<Id>() },
        { GameEventId.Request_Player_SetCredits,                FakeObject<IdCredits>() },
        { GameEventId.Request_Player_AddCredits,                FakeObject<IdCredits>() },
        { GameEventId.Event_Player_Credits,                     FakeObject<IdCredits>() },
        { GameEventId.Request_Blueprint_Finish,                 FakeObject<Id>() },
        { GameEventId.Request_Blueprint_Resources,              FakeObject<BlueprintResources>() },
        { GameEventId.Request_Player_ChangePlayerfield,         FakeObject<IdPlayfieldPositionRotation>() },
        { GameEventId.Request_Player_ItemExchange,              FakeObject<ItemExchangeInfo>() },
        { GameEventId.Event_Player_ItemExchange,                FakeObject<ItemExchangeInfo>() },
        { GameEventId.Request_Player_SetPlayerInfo,             FakeObject<PlayerInfoSet>() },
        { GameEventId.Event_Player_DisconnectedWaiting,         FakeObject<Id>() },
        { GameEventId.Request_Entity_Teleport,                  FakeObject<IdPositionRotation>() },
        { GameEventId.Request_Entity_ChangePlayfield,           FakeObject<IdPlayfieldPositionRotation>() },
        { GameEventId.Request_Entity_Destroy,                   FakeObject<Id>() },
        { GameEventId.Request_Entity_PosAndRot,                 FakeObject<Id>() },
        { GameEventId.Event_Entity_PosAndRot,                   FakeObject<IdPositionRotation>() },
        { GameEventId.Event_Faction_Changed,                    FakeObject<FactionChangeInfo>() },
        { GameEventId.Request_Entity_Spawn,                     FakeObject<EntitySpawnInfo>() },
        { GameEventId.Request_Get_Factions,                     FakeObject<Id>() },
        { GameEventId.Event_Get_Factions,                       FakeObject<FactionInfoList>() },
        { GameEventId.Event_Statistics,                         FakeObject<StatisticsParam>() },
        { GameEventId.Request_NewEntityId,                      null },
        { GameEventId.Event_NewEntityId,                        FakeObject<Id>() },
        { GameEventId.Request_AlliancesAll,                     null },
        { GameEventId.Event_AlliancesAll,                       FakeObject<AlliancesTable>() },
        { GameEventId.Request_AlliancesFaction,                 FakeObject<AlliancesFaction>() },
        { GameEventId.Event_AlliancesFaction,                   FakeObject<AlliancesFaction>() },
        { GameEventId.Request_Load_Playfield,                   FakeObject<PlayfieldLoad>() },
        { GameEventId.Event_ChatMessage,                        FakeObject<ChatInfo>() },
        { GameEventId.Event_ChatMessageEx,                      FakeObject<ChatMsgData>() },
        { GameEventId.Request_ConsoleCommand,                   FakeObject<PString>() },
        { GameEventId.Request_GetBannedPlayers,                 null },
        { GameEventId.Event_BannedPlayers,                      FakeObject<IdList>() },
        { GameEventId.Request_InGameMessage_SinglePlayer,       FakeObject<IdMsgPrio>() },
        { GameEventId.Request_InGameMessage_AllPlayers,         FakeObject<IdMsgPrio>() },
        { GameEventId.Request_InGameMessage_Faction,            FakeObject<IdMsgPrio>() },
        { GameEventId.Request_ShowDialog_SinglePlayer,          FakeObject<DialogBoxData>() },
        { GameEventId.Event_DialogButtonIndex,                  FakeObject<IdAndIntValue>() },
        { GameEventId.Event_Ok,                                 null },
        { GameEventId.Event_Error,                              FakeObject<ErrorInfo>() },
        { GameEventId.Event_TraderNPCItemSold,                  FakeObject<TraderNPCItemSoldInfo>() },
        { GameEventId.Request_Player_GetAndRemoveInventory,     FakeObject<Id>() },
        { GameEventId.Event_Player_GetAndRemoveInventory,       FakeObject<Inventory>() },
        { GameEventId.Request_Playfield_Entity_List,            FakeObject<PString>() },
        { GameEventId.Event_Playfield_Entity_List,              FakeObject<PlayfieldEntityList>() },
        { GameEventId.Request_Entity_Destroy2,                  FakeObject<IdPlayfield>() },
        { GameEventId.Request_Entity_Export,                    FakeObject<EntityExportInfo>() },
        { GameEventId.Event_ConsoleCommand,                     FakeObject<ConsoleCommandInfo>() },
        { GameEventId.Request_Entity_SetName,                   FakeObject<IdPlayfieldName>() },
        // For some reason Eleon made these structs instead of classes... skip testing
        //{ GameEventId.Event_PdaStateChange,                     FakeObject<PdaStateInfo>() },
        //{ GameEventId.Event_GameEvent,                          FakeObject<GameEventData>() },
    };
}
