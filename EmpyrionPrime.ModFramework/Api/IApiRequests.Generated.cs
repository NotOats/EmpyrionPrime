using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework
{
    public interface IApiRequests : IEmpyrionApi
    {
        /// <summary>
        /// Command:  CmdId.Request_Playfield_List
        /// Argument: null
        /// Response: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_List
        /// </summary>
        Task<PlayfieldList> PlayfieldList();

        Task<PlayfieldList> PlayfieldList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Playfield_Stats
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Stats
        /// </summary>
        Task<PlayfieldStats> PlayfieldStats(PString arg);

        Task<PlayfieldStats> PlayfieldStats(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Dedi_Stats
        /// Argument: null
        /// Response: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Dedi_Stats
        /// </summary>
        Task<DediStats> DediStats();

        Task<DediStats> DediStats(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_List
        /// Argument: null
        /// Response: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_List
        /// </summary>
        Task<GlobalStructureList> GlobalStructureList();

        Task<GlobalStructureList> GlobalStructureList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_Update
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_Update
        /// </summary>
        Task GlobalStructureUpdate(PString arg);

        Task GlobalStructureUpdate(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Structure_Touch
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_Touch
        /// </summary>
        Task StructureTouch(Id arg);

        Task StructureTouch(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Structure_BlockStatistics
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_BlockStatistics
        /// </summary>
        Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg);

        Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_Info
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Info
        /// </summary>
        Task<PlayerInfo> PlayerInfo(Id arg);

        Task<PlayerInfo> PlayerInfo(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_List
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_List
        /// </summary>
        Task<IdList> PlayerList();

        Task<IdList> PlayerList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_GetInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetInventory
        /// </summary>
        Task<Inventory> PlayerGetInventory(Id arg);

        Task<Inventory> PlayerGetInventory(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetInventory
        /// Argument: Eleon.Modding.Inventory
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetInventory
        /// </summary>
        Task<Inventory> PlayerSetInventory(Inventory arg);

        Task<Inventory> PlayerSetInventory(Inventory arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_AddItem
        /// Argument: Eleon.Modding.IdItemStack
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddItem
        /// </summary>
        Task PlayerAddItem(IdItemStack arg);

        Task PlayerAddItem(IdItemStack arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_Credits
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Credits
        /// </summary>
        Task<IdCredits> PlayerCredits(Id arg);

        Task<IdCredits> PlayerCredits(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetCredits
        /// </summary>
        Task<IdCredits> PlayerSetCredits(IdCredits arg);

        Task<IdCredits> PlayerSetCredits(IdCredits arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_AddCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddCredits
        /// </summary>
        Task<IdCredits> PlayerAddCredits(IdCredits arg);

        Task<IdCredits> PlayerAddCredits(IdCredits arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Finish
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Finish
        /// </summary>
        Task BlueprintFinish(Id arg);

        Task BlueprintFinish(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Resources
        /// Argument: Eleon.Modding.BlueprintResources
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Resources
        /// </summary>
        Task BlueprintResources(BlueprintResources arg);

        Task BlueprintResources(BlueprintResources arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_ChangePlayerfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ChangePlayerfield
        /// </summary>
        Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg);

        Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_ItemExchange
        /// Argument: Eleon.Modding.ItemExchangeInfo
        /// Response: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ItemExchange
        /// </summary>
        Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg);

        Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetPlayerInfo
        /// Argument: Eleon.Modding.PlayerInfoSet
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetPlayerInfo
        /// </summary>
        Task PlayerSetPlayerInfo(PlayerInfoSet arg);

        Task PlayerSetPlayerInfo(PlayerInfoSet arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Teleport
        /// Argument: Eleon.Modding.IdPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Teleport
        /// </summary>
        Task EntityTeleport(IdPositionRotation arg);

        Task EntityTeleport(IdPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_ChangePlayfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_ChangePlayfield
        /// </summary>
        Task EntityChangePlayfield(IdPlayfieldPositionRotation arg);

        Task EntityChangePlayfield(IdPlayfieldPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy
        /// </summary>
        Task EntityDestroy(Id arg);

        Task EntityDestroy(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_PosAndRot
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_PosAndRot
        /// </summary>
        Task<IdPositionRotation> EntityPosAndRot(Id arg);

        Task<IdPositionRotation> EntityPosAndRot(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Spawn
        /// Argument: Eleon.Modding.EntitySpawnInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Spawn
        /// </summary>
        Task EntitySpawn(EntitySpawnInfo arg);

        Task EntitySpawn(EntitySpawnInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Get_Factions
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Get_Factions
        /// </summary>
        Task<FactionInfoList> GetFactions(Id arg);

        Task<FactionInfoList> GetFactions(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_NewEntityId
        /// Argument: null
        /// Response: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_NewEntityId
        /// </summary>
        Task<Id> NewEntityId();

        Task<Id> NewEntityId(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_AlliancesAll
        /// Argument: null
        /// Response: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesAll
        /// </summary>
        Task<AlliancesTable> AlliancesAll();

        Task<AlliancesTable> AlliancesAll(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_AlliancesFaction
        /// Argument: Eleon.Modding.AlliancesFaction
        /// Response: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesFaction
        /// </summary>
        Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg);

        Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Load_Playfield
        /// Argument: Eleon.Modding.PlayfieldLoad
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Load_Playfield
        /// </summary>
        Task LoadPlayfield(PlayfieldLoad arg);

        Task LoadPlayfield(PlayfieldLoad arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_ConsoleCommand
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ConsoleCommand
        /// </summary>
        Task ConsoleCommand(PString arg);

        Task ConsoleCommand(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GetBannedPlayers
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GetBannedPlayers
        /// </summary>
        Task<IdList> GetBannedPlayers();

        Task<IdList> GetBannedPlayers(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_SinglePlayer
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_SinglePlayer
        /// </summary>
        Task InGameMessageSinglePlayer(IdMsgPrio arg);

        Task InGameMessageSinglePlayer(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_AllPlayers
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_AllPlayers
        /// </summary>
        Task InGameMessageAllPlayers(IdMsgPrio arg);

        Task InGameMessageAllPlayers(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_Faction
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_Faction
        /// </summary>
        Task InGameMessageFaction(IdMsgPrio arg);

        Task InGameMessageFaction(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_ShowDialog_SinglePlayer
        /// Argument: Eleon.Modding.DialogBoxData
        /// Response: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ShowDialog_SinglePlayer
        /// </summary>
        Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg);

        Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_GetAndRemoveInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetAndRemoveInventory
        /// </summary>
        Task<Inventory> PlayerGetAndRemoveInventory(Id arg);

        Task<Inventory> PlayerGetAndRemoveInventory(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Playfield_Entity_List
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Entity_List
        /// </summary>
        Task<PlayfieldEntityList> PlayfieldEntityList(PString arg);

        Task<PlayfieldEntityList> PlayfieldEntityList(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy2
        /// Argument: Eleon.Modding.IdPlayfield
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy2
        /// </summary>
        Task EntityDestroy2(IdPlayfield arg);

        Task EntityDestroy2(IdPlayfield arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Export
        /// Argument: Eleon.Modding.EntityExportInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Export
        /// </summary>
        Task EntityExport(EntityExportInfo arg);

        Task EntityExport(EntityExportInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_SetName
        /// Argument: Eleon.Modding.IdPlayfieldName
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_SetName
        /// </summary>
        Task EntitySetName(IdPlayfieldName arg);

        Task EntitySetName(IdPlayfieldName arg, TimeSpan timeout);



    }
}