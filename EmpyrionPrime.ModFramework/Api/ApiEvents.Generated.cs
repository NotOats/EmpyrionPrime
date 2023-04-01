using Eleon.Modding;
using System;

namespace EmpyrionPrime.ModFramework
{
    /// <summary>
    /// Interface for subscribing to Empyrion Api Events
    /// </summary>
    public partial class ApiEvents
    {
        /// <summary>
        /// Command:  CmdId.Event_Playfield_Loaded
        /// DataType: Eleon.Modding.PlayfieldLoad
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Loaded
        /// </summary>
        public event AsyncGameEventHandler<PlayfieldLoad> PlayfieldLoaded
        {
            add { AddHandler(CmdId.Event_Playfield_Loaded, value); }
            remove { RemoveHandler(CmdId.Event_Playfield_Loaded, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Unloaded
        /// DataType: Eleon.Modding.PlayfieldLoad
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Unloaded
        /// </summary>
        public event AsyncGameEventHandler<PlayfieldLoad> PlayfieldUnloaded
        {
            add { AddHandler(CmdId.Event_Playfield_Unloaded, value); }
            remove { RemoveHandler(CmdId.Event_Playfield_Unloaded, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Playfield_List
        /// DataType: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_List
        /// </summary>
        public event AsyncGameEventHandler<PlayfieldList> PlayfieldList
        {
            add { AddHandler(CmdId.Event_Playfield_List, value); }
            remove { RemoveHandler(CmdId.Event_Playfield_List, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Stats
        /// DataType: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Stats
        /// </summary>
        public event AsyncGameEventHandler<PlayfieldStats> PlayfieldStats
        {
            add { AddHandler(CmdId.Event_Playfield_Stats, value); }
            remove { RemoveHandler(CmdId.Event_Playfield_Stats, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Dedi_Stats
        /// DataType: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Dedi_Stats
        /// </summary>
        public event AsyncGameEventHandler<DediStats> DediStats
        {
            add { AddHandler(CmdId.Event_Dedi_Stats, value); }
            remove { RemoveHandler(CmdId.Event_Dedi_Stats, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_GlobalStructure_List
        /// DataType: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_GlobalStructure_List
        /// </summary>
        public event AsyncGameEventHandler<GlobalStructureList> GlobalStructureList
        {
            add { AddHandler(CmdId.Event_GlobalStructure_List, value); }
            remove { RemoveHandler(CmdId.Event_GlobalStructure_List, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Structure_BlockStatistics
        /// DataType: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Structure_BlockStatistics
        /// </summary>
        public event AsyncGameEventHandler<IdStructureBlockInfo> StructureBlockStatistics
        {
            add { AddHandler(CmdId.Event_Structure_BlockStatistics, value); }
            remove { RemoveHandler(CmdId.Event_Structure_BlockStatistics, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_Connected
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Connected
        /// </summary>
        public event AsyncGameEventHandler<Id> PlayerConnected
        {
            add { AddHandler(CmdId.Event_Player_Connected, value); }
            remove { RemoveHandler(CmdId.Event_Player_Connected, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_Disconnected
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Disconnected
        /// </summary>
        public event AsyncGameEventHandler<Id> PlayerDisconnected
        {
            add { AddHandler(CmdId.Event_Player_Disconnected, value); }
            remove { RemoveHandler(CmdId.Event_Player_Disconnected, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_ChangedPlayfield
        /// DataType: Eleon.Modding.IdPlayfield
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_ChangedPlayfield
        /// </summary>
        public event AsyncGameEventHandler<IdPlayfield> PlayerChangedPlayfield
        {
            add { AddHandler(CmdId.Event_Player_ChangedPlayfield, value); }
            remove { RemoveHandler(CmdId.Event_Player_ChangedPlayfield, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_Info
        /// DataType: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Info
        /// </summary>
        public event AsyncGameEventHandler<PlayerInfo> PlayerInfo
        {
            add { AddHandler(CmdId.Event_Player_Info, value); }
            remove { RemoveHandler(CmdId.Event_Player_Info, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_List
        /// DataType: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_List
        /// </summary>
        public event AsyncGameEventHandler<IdList> PlayerList
        {
            add { AddHandler(CmdId.Event_Player_List, value); }
            remove { RemoveHandler(CmdId.Event_Player_List, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_Inventory
        /// DataType: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Inventory
        /// </summary>
        public event AsyncGameEventHandler<Inventory> PlayerInventory
        {
            add { AddHandler(CmdId.Event_Player_Inventory, value); }
            remove { RemoveHandler(CmdId.Event_Player_Inventory, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_Credits
        /// DataType: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_Credits
        /// </summary>
        public event AsyncGameEventHandler<IdCredits> PlayerCredits
        {
            add { AddHandler(CmdId.Event_Player_Credits, value); }
            remove { RemoveHandler(CmdId.Event_Player_Credits, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_ItemExchange
        /// DataType: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_ItemExchange
        /// </summary>
        public event AsyncGameEventHandler<ItemExchangeInfo> PlayerItemExchange
        {
            add { AddHandler(CmdId.Event_Player_ItemExchange, value); }
            remove { RemoveHandler(CmdId.Event_Player_ItemExchange, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_DisconnectedWaiting
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_DisconnectedWaiting
        /// </summary>
        public event AsyncGameEventHandler<Id> PlayerDisconnectedWaiting
        {
            add { AddHandler(CmdId.Event_Player_DisconnectedWaiting, value); }
            remove { RemoveHandler(CmdId.Event_Player_DisconnectedWaiting, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Entity_PosAndRot
        /// DataType: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Entity_PosAndRot
        /// </summary>
        public event AsyncGameEventHandler<IdPositionRotation> EntityPosAndRot
        {
            add { AddHandler(CmdId.Event_Entity_PosAndRot, value); }
            remove { RemoveHandler(CmdId.Event_Entity_PosAndRot, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Faction_Changed
        /// DataType: Eleon.Modding.FactionChangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Faction_Changed
        /// </summary>
        public event AsyncGameEventHandler<FactionChangeInfo> FactionChanged
        {
            add { AddHandler(CmdId.Event_Faction_Changed, value); }
            remove { RemoveHandler(CmdId.Event_Faction_Changed, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Get_Factions
        /// DataType: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Get_Factions
        /// </summary>
        public event AsyncGameEventHandler<FactionInfoList> GetFactions
        {
            add { AddHandler(CmdId.Event_Get_Factions, value); }
            remove { RemoveHandler(CmdId.Event_Get_Factions, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Statistics
        /// DataType: Eleon.Modding.StatisticsParam
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Statistics
        /// </summary>
        public event AsyncGameEventHandler<StatisticsParam> Statistics
        {
            add { AddHandler(CmdId.Event_Statistics, value); }
            remove { RemoveHandler(CmdId.Event_Statistics, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_NewEntityId
        /// DataType: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_NewEntityId
        /// </summary>
        public event AsyncGameEventHandler<Id> NewEntityId
        {
            add { AddHandler(CmdId.Event_NewEntityId, value); }
            remove { RemoveHandler(CmdId.Event_NewEntityId, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_AlliancesAll
        /// DataType: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_AlliancesAll
        /// </summary>
        public event AsyncGameEventHandler<AlliancesTable> AlliancesAll
        {
            add { AddHandler(CmdId.Event_AlliancesAll, value); }
            remove { RemoveHandler(CmdId.Event_AlliancesAll, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_AlliancesFaction
        /// DataType: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_AlliancesFaction
        /// </summary>
        public event AsyncGameEventHandler<AlliancesFaction> AlliancesFaction
        {
            add { AddHandler(CmdId.Event_AlliancesFaction, value); }
            remove { RemoveHandler(CmdId.Event_AlliancesFaction, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_ChatMessage
        /// DataType: Eleon.Modding.ChatInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ChatMessage
        /// </summary>
        public event AsyncGameEventHandler<ChatInfo> ChatMessage
        {
            add { AddHandler(CmdId.Event_ChatMessage, value); }
            remove { RemoveHandler(CmdId.Event_ChatMessage, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_ChatMessageEx
        /// DataType: Eleon.Modding.ChatMsgData
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ChatMessageEx
        /// </summary>
        public event AsyncGameEventHandler<ChatMsgData> ChatMessageEx
        {
            add { AddHandler(CmdId.Event_ChatMessageEx, value); }
            remove { RemoveHandler(CmdId.Event_ChatMessageEx, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_BannedPlayers
        /// DataType: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_BannedPlayers
        /// </summary>
        public event AsyncGameEventHandler<IdList> BannedPlayers
        {
            add { AddHandler(CmdId.Event_BannedPlayers, value); }
            remove { RemoveHandler(CmdId.Event_BannedPlayers, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_DialogButtonIndex
        /// DataType: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_DialogButtonIndex
        /// </summary>
        public event AsyncGameEventHandler<IdAndIntValue> DialogButtonIndex
        {
            add { AddHandler(CmdId.Event_DialogButtonIndex, value); }
            remove { RemoveHandler(CmdId.Event_DialogButtonIndex, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Ok
        /// DataType: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Ok
        /// </summary>
        public event AsyncGameEventHandler Ok
        {
            add { AddHandler(CmdId.Event_Ok, value); }
            remove { RemoveHandler(CmdId.Event_Ok, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Error
        /// DataType: Eleon.Modding.ErrorInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Error
        /// </summary>
        public event AsyncGameEventHandler<ErrorInfo> Error
        {
            add { AddHandler(CmdId.Event_Error, value); }
            remove { RemoveHandler(CmdId.Event_Error, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_TraderNPCItemSold
        /// DataType: Eleon.Modding.TraderNPCItemSoldInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_TraderNPCItemSold
        /// </summary>
        public event AsyncGameEventHandler<TraderNPCItemSoldInfo> TraderNPCItemSold
        {
            add { AddHandler(CmdId.Event_TraderNPCItemSold, value); }
            remove { RemoveHandler(CmdId.Event_TraderNPCItemSold, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Player_GetAndRemoveInventory
        /// DataType: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Player_GetAndRemoveInventory
        /// </summary>
        public event AsyncGameEventHandler<Inventory> PlayerGetAndRemoveInventory
        {
            add { AddHandler(CmdId.Event_Player_GetAndRemoveInventory, value); }
            remove { RemoveHandler(CmdId.Event_Player_GetAndRemoveInventory, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_Playfield_Entity_List
        /// DataType: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_Playfield_Entity_List
        /// </summary>
        public event AsyncGameEventHandler<PlayfieldEntityList> PlayfieldEntityList
        {
            add { AddHandler(CmdId.Event_Playfield_Entity_List, value); }
            remove { RemoveHandler(CmdId.Event_Playfield_Entity_List, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_ConsoleCommand
        /// DataType: Eleon.Modding.ConsoleCommandInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_ConsoleCommand
        /// </summary>
        public event AsyncGameEventHandler<ConsoleCommandInfo> ConsoleCommand
        {
            add { AddHandler(CmdId.Event_ConsoleCommand, value); }
            remove { RemoveHandler(CmdId.Event_ConsoleCommand, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_PdaStateChange
        /// DataType: Eleon.Modding.PdaStateInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_PdaStateChange
        /// </summary>
        public event AsyncGameEventHandler<PdaStateInfo> PdaStateChange
        {
            add { AddHandler(CmdId.Event_PdaStateChange, value); }
            remove { RemoveHandler(CmdId.Event_PdaStateChange, value); }
        }

        /// <summary>
        /// Command:  CmdId.Event_GameEvent
        /// DataType: Eleon.Modding.GameEventData
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Event_GameEvent
        /// </summary>
        public event AsyncGameEventHandler<GameEventData> GameEvent
        {
            add { AddHandler(CmdId.Event_GameEvent, value); }
            remove { RemoveHandler(CmdId.Event_GameEvent, value); }
        }

    }
}
