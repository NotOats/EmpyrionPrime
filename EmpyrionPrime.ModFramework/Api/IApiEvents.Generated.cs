using Eleon.Modding;
using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework
{
    /// <summary>
    /// Interface for subscribing to Empyrion Api Events
    /// </summary>
    public interface IApiEvents : IEmpyrionApi
    {
        /// <summary>
        /// Command:  CmdId.Event_Playfield_Loaded
        /// DataType: Eleon.Modding.PlayfieldLoad
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Loaded
        /// </summary>
        event AsyncGameEventHandler<PlayfieldLoad> PlayfieldLoaded;

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Unloaded
        /// DataType: Eleon.Modding.PlayfieldLoad
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Unloaded
        /// </summary>
        event AsyncGameEventHandler<PlayfieldLoad> PlayfieldUnloaded;

        /// <summary>
        /// Command:  CmdId.Event_Playfield_List
        /// DataType: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_List
        /// </summary>
        event AsyncGameEventHandler<PlayfieldList> PlayfieldList;

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Stats
        /// DataType: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Stats
        /// </summary>
        event AsyncGameEventHandler<PlayfieldStats> PlayfieldStats;

        /// <summary>
        /// Command:  CmdId.Event_Dedi_Stats
        /// DataType: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Dedi_Stats
        /// </summary>
        event AsyncGameEventHandler<DediStats> DediStats;

        /// <summary>
        /// Command:  CmdId.Event_GlobalStructure_List
        /// DataType: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_GlobalStructure_List
        /// </summary>
        event AsyncGameEventHandler<GlobalStructureList> GlobalStructureList;

        /// <summary>
        /// Command:  CmdId.Event_Structure_BlockStatistics
        /// DataType: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Structure_BlockStatistics
        /// </summary>
        event AsyncGameEventHandler<IdStructureBlockInfo> StructureBlockStatistics;

        /// <summary>
        /// Command:  CmdId.Event_Player_Connected
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Connected
        /// </summary>
        event AsyncGameEventHandler<Id> PlayerConnected;

        /// <summary>
        /// Command:  CmdId.Event_Player_Disconnected
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Disconnected
        /// </summary>
        event AsyncGameEventHandler<Id> PlayerDisconnected;

        /// <summary>
        /// Command:  CmdId.Event_Player_ChangedPlayfield
        /// DataType: Eleon.Modding.IdPlayfield
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_ChangedPlayfield
        /// </summary>
        event AsyncGameEventHandler<IdPlayfield> PlayerChangedPlayfield;

        /// <summary>
        /// Command:  CmdId.Event_Player_Info
        /// DataType: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Info
        /// </summary>
        event AsyncGameEventHandler<PlayerInfo> PlayerInfo;

        /// <summary>
        /// Command:  CmdId.Event_Player_List
        /// DataType: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_List
        /// </summary>
        event AsyncGameEventHandler<IdList> PlayerList;

        /// <summary>
        /// Command:  CmdId.Event_Player_Inventory
        /// DataType: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Inventory
        /// </summary>
        event AsyncGameEventHandler<Inventory> PlayerInventory;

        /// <summary>
        /// Command:  CmdId.Event_Player_Credits
        /// DataType: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Credits
        /// </summary>
        event AsyncGameEventHandler<IdCredits> PlayerCredits;

        /// <summary>
        /// Command:  CmdId.Event_Player_ItemExchange
        /// DataType: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_ItemExchange
        /// </summary>
        event AsyncGameEventHandler<ItemExchangeInfo> PlayerItemExchange;

        /// <summary>
        /// Command:  CmdId.Event_Player_DisconnectedWaiting
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_DisconnectedWaiting
        /// </summary>
        event AsyncGameEventHandler<Id> PlayerDisconnectedWaiting;

        /// <summary>
        /// Command:  CmdId.Event_Entity_PosAndRot
        /// DataType: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Entity_PosAndRot
        /// </summary>
        event AsyncGameEventHandler<IdPositionRotation> EntityPosAndRot;

        /// <summary>
        /// Command:  CmdId.Event_Faction_Changed
        /// DataType: Eleon.Modding.FactionChangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Faction_Changed
        /// </summary>
        event AsyncGameEventHandler<FactionChangeInfo> FactionChanged;

        /// <summary>
        /// Command:  CmdId.Event_Get_Factions
        /// DataType: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Get_Factions
        /// </summary>
        event AsyncGameEventHandler<FactionInfoList> GetFactions;

        /// <summary>
        /// Command:  CmdId.Event_Statistics
        /// DataType: Eleon.Modding.StatisticsParam
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Statistics
        /// </summary>
        event AsyncGameEventHandler<StatisticsParam> Statistics;

        /// <summary>
        /// Command:  CmdId.Event_NewEntityId
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_NewEntityId
        /// </summary>
        event AsyncGameEventHandler<Id> NewEntityId;

        /// <summary>
        /// Command:  CmdId.Event_AlliancesAll
        /// DataType: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_AlliancesAll
        /// </summary>
        event AsyncGameEventHandler<AlliancesTable> AlliancesAll;

        /// <summary>
        /// Command:  CmdId.Event_AlliancesFaction
        /// DataType: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_AlliancesFaction
        /// </summary>
        event AsyncGameEventHandler<AlliancesFaction> AlliancesFaction;

        /// <summary>
        /// Command:  CmdId.Event_ChatMessage
        /// DataType: Eleon.Modding.ChatInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ChatMessage
        /// </summary>
        event AsyncGameEventHandler<ChatInfo> ChatMessage;

        /// <summary>
        /// Command:  CmdId.Event_ChatMessageEx
        /// DataType: Eleon.Modding.ChatMsgData
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ChatMessageEx
        /// </summary>
        event AsyncGameEventHandler<ChatMsgData> ChatMessageEx;

        /// <summary>
        /// Command:  CmdId.Event_BannedPlayers
        /// DataType: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_BannedPlayers
        /// </summary>
        event AsyncGameEventHandler<IdList> BannedPlayers;

        /// <summary>
        /// Command:  CmdId.Event_DialogButtonIndex
        /// DataType: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_DialogButtonIndex
        /// </summary>
        event AsyncGameEventHandler<IdAndIntValue> DialogButtonIndex;

        /// <summary>
        /// Command:  CmdId.Event_Ok
        /// DataType: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Ok
        /// </summary>
        event AsyncGameEventHandler Ok;

        /// <summary>
        /// Command:  CmdId.Event_Error
        /// DataType: Eleon.Modding.ErrorInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Error
        /// </summary>
        event AsyncGameEventHandler<ErrorInfo> Error;

        /// <summary>
        /// Command:  CmdId.Event_TraderNPCItemSold
        /// DataType: Eleon.Modding.TraderNPCItemSoldInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_TraderNPCItemSold
        /// </summary>
        event AsyncGameEventHandler<TraderNPCItemSoldInfo> TraderNPCItemSold;

        /// <summary>
        /// Command:  CmdId.Event_Player_GetAndRemoveInventory
        /// DataType: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_GetAndRemoveInventory
        /// </summary>
        event AsyncGameEventHandler<Inventory> PlayerGetAndRemoveInventory;

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Entity_List
        /// DataType: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Entity_List
        /// </summary>
        event AsyncGameEventHandler<PlayfieldEntityList> PlayfieldEntityList;

        /// <summary>
        /// Command:  CmdId.Event_ConsoleCommand
        /// DataType: Eleon.Modding.ConsoleCommandInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ConsoleCommand
        /// </summary>
        event AsyncGameEventHandler<ConsoleCommandInfo> ConsoleCommand;

        /// <summary>
        /// Command:  CmdId.Event_PdaStateChange
        /// DataType: Eleon.Modding.PdaStateInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_PdaStateChange
        /// </summary>
        event AsyncGameEventHandler<PdaStateInfo> PdaStateChange;

        /// <summary>
        /// Command:  CmdId.Event_GameEvent
        /// DataType: Eleon.Modding.GameEventData
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_GameEvent
        /// </summary>
        event AsyncGameEventHandler<GameEventData> GameEvent;

    }
}