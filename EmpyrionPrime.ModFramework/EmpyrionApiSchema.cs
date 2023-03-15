using Eleon.Modding;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpyrionPrime.ModFramework
{
    public class ApiRequest
    {
        public CmdId CommandId { get; }
        public Type ArgumentType { get; }
        public CmdId ResponseId { get; }
        public Type ResponseType { get; }

        public ApiRequest(CmdId commandId, Type argumentType, CmdId responseId)
        {
            CommandId = commandId;
            ArgumentType = argumentType;
            ResponseId = responseId;
            ResponseType = EmpyrionApiSchema.ApiEvents[ResponseId];
        }
    }

    public static class EmpyrionApiSchema
    {
        public readonly static IReadOnlyDictionary<CmdId, Type> ApiEvents = new Dictionary<CmdId, Type> 
        {
            { CmdId.Event_Playfield_Loaded,                   typeof(PlayfieldLoad) },
            { CmdId.Event_Playfield_Unloaded,                 typeof(PlayfieldLoad) },
            { CmdId.Event_Playfield_List,                     typeof(PlayfieldList) },
            { CmdId.Event_Playfield_Stats,                    typeof(PlayfieldStats) },
            { CmdId.Event_Dedi_Stats,                         typeof(DediStats) },
            { CmdId.Event_GlobalStructure_List,               typeof(GlobalStructureList) },
            { CmdId.Event_Structure_BlockStatistics,          typeof(IdStructureBlockInfo) },
            { CmdId.Event_Player_Connected,                   typeof(Id) },
            { CmdId.Event_Player_Disconnected,                typeof(Id) },
            { CmdId.Event_Player_ChangedPlayfield,            typeof(IdPlayfield) },
            { CmdId.Event_Player_Info,                        typeof(PlayerInfo) },
            { CmdId.Event_Player_List,                        typeof(IdList) },
            { CmdId.Event_Player_Inventory,                   typeof(Inventory) },
            { CmdId.Event_Player_Credits,                     typeof(IdCredits) },
            { CmdId.Event_Player_ItemExchange,                typeof(ItemExchangeInfo) },
            { CmdId.Event_Player_DisconnectedWaiting,         typeof(Id) },
            { CmdId.Event_Entity_PosAndRot,                   typeof(IdPositionRotation) },
            { CmdId.Event_Faction_Changed,                    typeof(FactionChangeInfo) },
            { CmdId.Event_Get_Factions,                       typeof(FactionInfoList) },
            { CmdId.Event_Statistics,                         typeof(StatisticsParam) },
            { CmdId.Event_NewEntityId,                        typeof(Id) },
            { CmdId.Event_AlliancesAll,                       typeof(AlliancesTable) },
            { CmdId.Event_AlliancesFaction,                   typeof(AlliancesFaction) },
            { CmdId.Event_ChatMessage,                        typeof(ChatInfo) },
            { CmdId.Event_ChatMessageEx,                      typeof(ChatMsgData) },
            { CmdId.Event_BannedPlayers,                      typeof(IdList) },
            { CmdId.Event_DialogButtonIndex,                  typeof(IdAndIntValue) },
            { CmdId.Event_Ok,                                 null },
            { CmdId.Event_Error,                              typeof(ErrorInfo) },
            { CmdId.Event_TraderNPCItemSold,                  typeof(TraderNPCItemSoldInfo) },
            { CmdId.Event_Player_GetAndRemoveInventory,       typeof(Inventory) },
            { CmdId.Event_Playfield_Entity_List,              typeof(PlayfieldEntityList) },
            { CmdId.Event_ConsoleCommand,                     typeof(ConsoleCommandInfo) },
            { CmdId.Event_PdaStateChange,                     typeof(PdaStateInfo) },
            { CmdId.Event_GameEvent,                          typeof(GameEventData) },
        };

        public readonly static IReadOnlyList<ApiRequest> ApiRequests = new List<ApiRequest>
        {
            new ApiRequest(CmdId.Request_Playfield_List,                   null,                                CmdId.Event_Playfield_List),
            new ApiRequest(CmdId.Request_Playfield_Stats,                  typeof(PString),                     CmdId.Event_Playfield_Stats),
            new ApiRequest(CmdId.Request_Dedi_Stats,                       null,                                CmdId.Event_Dedi_Stats),
            new ApiRequest(CmdId.Request_GlobalStructure_List,             null,                                CmdId.Event_GlobalStructure_List),
            new ApiRequest(CmdId.Request_GlobalStructure_Update,           typeof(PString),                     CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Structure_Touch,                  typeof(Id),                          CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Structure_BlockStatistics,        typeof(Id),                          CmdId.Event_Structure_BlockStatistics),
            new ApiRequest(CmdId.Request_Player_Info,                      typeof(Id),                          CmdId.Event_Player_Info),
            new ApiRequest(CmdId.Request_Player_List,                      null,                                CmdId.Event_Player_List),
            new ApiRequest(CmdId.Request_Player_GetInventory,              typeof(Id),                          CmdId.Event_Player_Inventory),
            new ApiRequest(CmdId.Request_Player_SetInventory,              typeof(Inventory),                   CmdId.Event_Player_Inventory),
            new ApiRequest(CmdId.Request_Player_AddItem,                   typeof(IdItemStack),                 CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Player_Credits,                   typeof(Id),                          CmdId.Event_Player_Credits),
            new ApiRequest(CmdId.Request_Player_SetCredits,                typeof(IdCredits),                   CmdId.Event_Player_Credits),
            new ApiRequest(CmdId.Request_Player_AddCredits,                typeof(IdCredits),                   CmdId.Event_Player_Credits),
            new ApiRequest(CmdId.Request_Blueprint_Finish,                 typeof(Id),                          CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Blueprint_Resources,              typeof(BlueprintResources),          CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Player_ChangePlayerfield,         typeof(IdPlayfieldPositionRotation), CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Player_ItemExchange,              typeof(ItemExchangeInfo),            CmdId.Event_Player_ItemExchange),
            new ApiRequest(CmdId.Request_Player_SetPlayerInfo,             typeof(PlayerInfoSet),               CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_Teleport,                  typeof(IdPositionRotation),          CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_ChangePlayfield,           typeof(IdPlayfieldPositionRotation), CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_Destroy,                   typeof(Id),                          CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_PosAndRot,                 typeof(Id),                          CmdId.Event_Entity_PosAndRot),
            new ApiRequest(CmdId.Request_Entity_Spawn,                     typeof(EntitySpawnInfo),             CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Get_Factions,                     typeof(Id),                          CmdId.Event_Get_Factions),
            new ApiRequest(CmdId.Request_NewEntityId,                      null,                                CmdId.Event_NewEntityId),
            new ApiRequest(CmdId.Request_AlliancesAll,                     null,                                CmdId.Event_AlliancesAll),
            new ApiRequest(CmdId.Request_AlliancesFaction,                 typeof(AlliancesFaction),            CmdId.Event_AlliancesFaction),
            new ApiRequest(CmdId.Request_Load_Playfield,                   typeof(PlayfieldLoad),               CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_ConsoleCommand,                   typeof(PString),                     CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_GetBannedPlayers,                 null,                                CmdId.Event_BannedPlayers),
            new ApiRequest(CmdId.Request_InGameMessage_SinglePlayer,       typeof(IdMsgPrio),                   CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_InGameMessage_AllPlayers,         typeof(IdMsgPrio),                   CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_InGameMessage_Faction,            typeof(IdMsgPrio),                   CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_ShowDialog_SinglePlayer,          typeof(DialogBoxData),               CmdId.Event_DialogButtonIndex),
            new ApiRequest(CmdId.Request_Player_GetAndRemoveInventory,     typeof(Id),                          CmdId.Event_Player_GetAndRemoveInventory),
            new ApiRequest(CmdId.Request_Playfield_Entity_List,            typeof(PString),                     CmdId.Event_Playfield_Entity_List),
            new ApiRequest(CmdId.Request_Entity_Destroy2,                  typeof(IdPlayfield),                 CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_Export,                    typeof(EntityExportInfo),            CmdId.Event_Ok),
            new ApiRequest(CmdId.Request_Entity_SetName,                   typeof(IdPlayfieldName),             CmdId.Event_Ok),
        };
    }
}
