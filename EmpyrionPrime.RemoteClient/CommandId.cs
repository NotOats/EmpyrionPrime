﻿using Eleon.Modding;

namespace EmpyrionPrime.RemoteClient
{
	public enum CommandId : int
	{
		// Imported from Eleon.Modding
		Event_Playfield_Loaded                   = CmdId.Event_Playfield_Loaded,
		Event_Playfield_Unloaded                 = CmdId.Event_Playfield_Unloaded,
		Request_Playfield_List                   = CmdId.Request_Playfield_List,
		Event_Playfield_List                     = CmdId.Event_Playfield_List,
		Request_Playfield_Stats                  = CmdId.Request_Playfield_Stats,
		Event_Playfield_Stats                    = CmdId.Event_Playfield_Stats,
		Request_Dedi_Stats                       = CmdId.Request_Dedi_Stats,
		Event_Dedi_Stats                         = CmdId.Event_Dedi_Stats,
		Request_GlobalStructure_List             = CmdId.Request_GlobalStructure_List,
		Request_GlobalStructure_Update           = CmdId.Request_GlobalStructure_Update,
		Event_GlobalStructure_List               = CmdId.Event_GlobalStructure_List,
		Request_Structure_Touch                  = CmdId.Request_Structure_Touch,
		Request_Structure_BlockStatistics        = CmdId.Request_Structure_BlockStatistics,
		Event_Structure_BlockStatistics          = CmdId.Event_Structure_BlockStatistics,
		Event_Player_Connected                   = CmdId.Event_Player_Connected,
		Event_Player_Disconnected                = CmdId.Event_Player_Disconnected,
		Event_Player_ChangedPlayfield            = CmdId.Event_Player_ChangedPlayfield,
		Request_Player_Info                      = CmdId.Request_Player_Info,
		Event_Player_Info                        = CmdId.Event_Player_Info,
		Request_Player_List                      = CmdId.Request_Player_List,
		Event_Player_List                        = CmdId.Event_Player_List,
		Request_Player_GetInventory              = CmdId.Request_Player_GetInventory,
		Request_Player_SetInventory              = CmdId.Request_Player_SetInventory,
		Event_Player_Inventory                   = CmdId.Event_Player_Inventory,
		Request_Player_AddItem                   = CmdId.Request_Player_AddItem,
		Request_Player_Credits                   = CmdId.Request_Player_Credits,
		Request_Player_SetCredits                = CmdId.Request_Player_SetCredits,
		Request_Player_AddCredits                = CmdId.Request_Player_AddCredits,
		Event_Player_Credits                     = CmdId.Event_Player_Credits,
		Request_Blueprint_Finish                 = CmdId.Request_Blueprint_Finish,
		Request_Blueprint_Resources              = CmdId.Request_Blueprint_Resources,
		Request_Player_ChangePlayerfield         = CmdId.Request_Player_ChangePlayerfield,
		Request_Player_ItemExchange              = CmdId.Request_Player_ItemExchange,
		Event_Player_ItemExchange                = CmdId.Event_Player_ItemExchange,
		Request_Player_SetPlayerInfo             = CmdId.Request_Player_SetPlayerInfo,
		Event_Player_DisconnectedWaiting         = CmdId.Event_Player_DisconnectedWaiting,
		Request_Entity_Teleport                  = CmdId.Request_Entity_Teleport,
		Request_Entity_ChangePlayfield           = CmdId.Request_Entity_ChangePlayfield,
		Request_Entity_Destroy                   = CmdId.Request_Entity_Destroy,
		Request_Entity_PosAndRot                 = CmdId.Request_Entity_PosAndRot,
		Event_Entity_PosAndRot                   = CmdId.Event_Entity_PosAndRot,
		Event_Faction_Changed                    = CmdId.Event_Faction_Changed,
		Request_Entity_Spawn                     = CmdId.Request_Entity_Spawn,
		Request_Get_Factions                     = CmdId.Request_Get_Factions,
		Event_Get_Factions                       = CmdId.Event_Get_Factions,
		Event_Statistics                         = CmdId.Event_Statistics,
		Request_NewEntityId                      = CmdId.Request_NewEntityId,
		Event_NewEntityId                        = CmdId.Event_NewEntityId,
		Request_AlliancesAll                     = CmdId.Request_AlliancesAll,
		Event_AlliancesAll                       = CmdId.Event_AlliancesAll,
		Request_AlliancesFaction                 = CmdId.Request_AlliancesFaction,
		Event_AlliancesFaction                   = CmdId.Event_AlliancesFaction,
		Request_Load_Playfield                   = CmdId.Request_Load_Playfield,
		Event_ChatMessage                        = CmdId.Event_ChatMessage,
		Event_ChatMessageEx                      = CmdId.Event_ChatMessageEx,
		Request_ConsoleCommand                   = CmdId.Request_ConsoleCommand,
		Request_GetBannedPlayers                 = CmdId.Request_GetBannedPlayers,
		Event_BannedPlayers                      = CmdId.Event_BannedPlayers,
		Request_InGameMessage_SinglePlayer       = CmdId.Request_InGameMessage_SinglePlayer,
		Request_InGameMessage_AllPlayers         = CmdId.Request_InGameMessage_AllPlayers,
		Request_InGameMessage_Faction            = CmdId.Request_InGameMessage_Faction,
		Request_ShowDialog_SinglePlayer          = CmdId.Request_ShowDialog_SinglePlayer,
		Event_DialogButtonIndex                  = CmdId.Event_DialogButtonIndex,
		Event_Ok                                 = CmdId.Event_Ok,
		Event_Error                              = CmdId.Event_Error,
		Event_TraderNPCItemSold                  = CmdId.Event_TraderNPCItemSold,
		Request_Player_GetAndRemoveInventory     = CmdId.Request_Player_GetAndRemoveInventory,
		Event_Player_GetAndRemoveInventory       = CmdId.Event_Player_GetAndRemoveInventory,
		Request_Playfield_Entity_List            = CmdId.Request_Playfield_Entity_List,
		Event_Playfield_Entity_List              = CmdId.Event_Playfield_Entity_List,
		Request_Entity_Destroy2                  = CmdId.Request_Entity_Destroy2,
		Request_Entity_Export                    = CmdId.Request_Entity_Export,
		Event_ConsoleCommand                     = CmdId.Event_ConsoleCommand,
		Request_Entity_SetName                   = CmdId.Request_Entity_SetName,
		Event_PdaStateChange                     = CmdId.Event_PdaStateChange,
		Event_GameEvent                          = CmdId.Event_GameEvent,

		// Custom Events from EPM
		Request_Chat                             = 200,
		Event_Chat                               = 201,
		Request_DialogAction                     = 202,
	}
}