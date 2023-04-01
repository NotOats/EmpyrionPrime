using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework
{
    /// <summary>
    /// Interface for asynchronous Empyrion Api Requests
    /// </summary>
    public interface IApiRequests : IEmpyrionApi
    {

        /// <summary>
        /// Command:  CmdId.Request_Playfield_List
        /// Argument: null
        /// Response: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldList</returns>
        Task<PlayfieldList> PlayfieldList();

        /// <summary>
        /// Command:  CmdId.Request_Playfield_List
        /// Argument: null
        /// Response: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldList</returns>
        Task<PlayfieldList> PlayfieldList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Playfield_Stats
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Stats
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldStats</returns>
        Task<PlayfieldStats> PlayfieldStats(PString arg);

        /// <summary>
        /// Command:  CmdId.Request_Playfield_Stats
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Stats
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldStats</returns>
        Task<PlayfieldStats> PlayfieldStats(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Dedi_Stats
        /// Argument: null
        /// Response: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Dedi_Stats
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.DediStats</returns>
        Task<DediStats> DediStats();

        /// <summary>
        /// Command:  CmdId.Request_Dedi_Stats
        /// Argument: null
        /// Response: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Dedi_Stats
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.DediStats</returns>
        Task<DediStats> DediStats(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_List
        /// Argument: null
        /// Response: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.GlobalStructureList</returns>
        Task<GlobalStructureList> GlobalStructureList();

        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_List
        /// Argument: null
        /// Response: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.GlobalStructureList</returns>
        Task<GlobalStructureList> GlobalStructureList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_Update
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_Update
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task GlobalStructureUpdate(PString arg);

        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_Update
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_Update
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task GlobalStructureUpdate(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Structure_Touch
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_Touch
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task StructureTouch(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Structure_Touch
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_Touch
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task StructureTouch(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Structure_BlockStatistics
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_BlockStatistics
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdStructureBlockInfo</returns>
        Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Structure_BlockStatistics
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_BlockStatistics
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdStructureBlockInfo</returns>
        Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_Info
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Info
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayerInfo</returns>
        Task<PlayerInfo> PlayerInfo(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_Info
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Info
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayerInfo</returns>
        Task<PlayerInfo> PlayerInfo(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_List
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        Task<IdList> PlayerList();

        /// <summary>
        /// Command:  CmdId.Request_Player_List
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        Task<IdList> PlayerList(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_GetInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerGetInventory(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_GetInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerGetInventory(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetInventory
        /// Argument: Eleon.Modding.Inventory
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerSetInventory(Inventory arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_SetInventory
        /// Argument: Eleon.Modding.Inventory
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerSetInventory(Inventory arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_AddItem
        /// Argument: Eleon.Modding.IdItemStack
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddItem
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task PlayerAddItem(IdItemStack arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_AddItem
        /// Argument: Eleon.Modding.IdItemStack
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddItem
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task PlayerAddItem(IdItemStack arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_Credits
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Credits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerCredits(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_Credits
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Credits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerCredits(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetCredits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerSetCredits(IdCredits arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_SetCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetCredits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerSetCredits(IdCredits arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_AddCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddCredits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerAddCredits(IdCredits arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_AddCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddCredits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        Task<IdCredits> PlayerAddCredits(IdCredits arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Finish
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Finish
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task BlueprintFinish(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Finish
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Finish
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task BlueprintFinish(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Resources
        /// Argument: Eleon.Modding.BlueprintResources
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Resources
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task BlueprintResources(BlueprintResources arg);

        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Resources
        /// Argument: Eleon.Modding.BlueprintResources
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Resources
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task BlueprintResources(BlueprintResources arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_ChangePlayerfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ChangePlayerfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_ChangePlayerfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ChangePlayerfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_ItemExchange
        /// Argument: Eleon.Modding.ItemExchangeInfo
        /// Response: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ItemExchange
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.ItemExchangeInfo</returns>
        Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_ItemExchange
        /// Argument: Eleon.Modding.ItemExchangeInfo
        /// Response: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ItemExchange
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.ItemExchangeInfo</returns>
        Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_SetPlayerInfo
        /// Argument: Eleon.Modding.PlayerInfoSet
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetPlayerInfo
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task PlayerSetPlayerInfo(PlayerInfoSet arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_SetPlayerInfo
        /// Argument: Eleon.Modding.PlayerInfoSet
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetPlayerInfo
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task PlayerSetPlayerInfo(PlayerInfoSet arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Teleport
        /// Argument: Eleon.Modding.IdPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Teleport
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntityTeleport(IdPositionRotation arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_Teleport
        /// Argument: Eleon.Modding.IdPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Teleport
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntityTeleport(IdPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_ChangePlayfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_ChangePlayfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntityChangePlayfield(IdPlayfieldPositionRotation arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_ChangePlayfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_ChangePlayfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntityChangePlayfield(IdPlayfieldPositionRotation arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntityDestroy(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntityDestroy(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_PosAndRot
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_PosAndRot
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdPositionRotation</returns>
        Task<IdPositionRotation> EntityPosAndRot(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_PosAndRot
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_PosAndRot
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdPositionRotation</returns>
        Task<IdPositionRotation> EntityPosAndRot(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Spawn
        /// Argument: Eleon.Modding.EntitySpawnInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Spawn
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntitySpawn(EntitySpawnInfo arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_Spawn
        /// Argument: Eleon.Modding.EntitySpawnInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Spawn
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntitySpawn(EntitySpawnInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Get_Factions
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Get_Factions
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.FactionInfoList</returns>
        Task<FactionInfoList> GetFactions(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Get_Factions
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Get_Factions
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.FactionInfoList</returns>
        Task<FactionInfoList> GetFactions(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_NewEntityId
        /// Argument: null
        /// Response: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_NewEntityId
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Id</returns>
        Task<Id> NewEntityId();

        /// <summary>
        /// Command:  CmdId.Request_NewEntityId
        /// Argument: null
        /// Response: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_NewEntityId
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Id</returns>
        Task<Id> NewEntityId(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_AlliancesAll
        /// Argument: null
        /// Response: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesAll
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesTable</returns>
        Task<AlliancesTable> AlliancesAll();

        /// <summary>
        /// Command:  CmdId.Request_AlliancesAll
        /// Argument: null
        /// Response: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesAll
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesTable</returns>
        Task<AlliancesTable> AlliancesAll(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_AlliancesFaction
        /// Argument: Eleon.Modding.AlliancesFaction
        /// Response: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesFaction
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesFaction</returns>
        Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg);

        /// <summary>
        /// Command:  CmdId.Request_AlliancesFaction
        /// Argument: Eleon.Modding.AlliancesFaction
        /// Response: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesFaction
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesFaction</returns>
        Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Load_Playfield
        /// Argument: Eleon.Modding.PlayfieldLoad
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Load_Playfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task LoadPlayfield(PlayfieldLoad arg);

        /// <summary>
        /// Command:  CmdId.Request_Load_Playfield
        /// Argument: Eleon.Modding.PlayfieldLoad
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Load_Playfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task LoadPlayfield(PlayfieldLoad arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_ConsoleCommand
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ConsoleCommand
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task ConsoleCommand(PString arg);

        /// <summary>
        /// Command:  CmdId.Request_ConsoleCommand
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ConsoleCommand
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task ConsoleCommand(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_GetBannedPlayers
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GetBannedPlayers
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        Task<IdList> GetBannedPlayers();

        /// <summary>
        /// Command:  CmdId.Request_GetBannedPlayers
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GetBannedPlayers
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        Task<IdList> GetBannedPlayers(TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_SinglePlayer
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_SinglePlayer
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageSinglePlayer(IdMsgPrio arg);

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_SinglePlayer
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_SinglePlayer
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageSinglePlayer(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_AllPlayers
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_AllPlayers
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageAllPlayers(IdMsgPrio arg);

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_AllPlayers
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_AllPlayers
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageAllPlayers(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_Faction
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_Faction
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageFaction(IdMsgPrio arg);

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_Faction
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_Faction
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task InGameMessageFaction(IdMsgPrio arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_ShowDialog_SinglePlayer
        /// Argument: Eleon.Modding.DialogBoxData
        /// Response: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ShowDialog_SinglePlayer
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdAndIntValue</returns>
        Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg);

        /// <summary>
        /// Command:  CmdId.Request_ShowDialog_SinglePlayer
        /// Argument: Eleon.Modding.DialogBoxData
        /// Response: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ShowDialog_SinglePlayer
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdAndIntValue</returns>
        Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Player_GetAndRemoveInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetAndRemoveInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerGetAndRemoveInventory(Id arg);

        /// <summary>
        /// Command:  CmdId.Request_Player_GetAndRemoveInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetAndRemoveInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        Task<Inventory> PlayerGetAndRemoveInventory(Id arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Playfield_Entity_List
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Entity_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldEntityList</returns>
        Task<PlayfieldEntityList> PlayfieldEntityList(PString arg);

        /// <summary>
        /// Command:  CmdId.Request_Playfield_Entity_List
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Entity_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldEntityList</returns>
        Task<PlayfieldEntityList> PlayfieldEntityList(PString arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy2
        /// Argument: Eleon.Modding.IdPlayfield
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy2
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntityDestroy2(IdPlayfield arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy2
        /// Argument: Eleon.Modding.IdPlayfield
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy2
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntityDestroy2(IdPlayfield arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_Export
        /// Argument: Eleon.Modding.EntityExportInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Export
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntityExport(EntityExportInfo arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_Export
        /// Argument: Eleon.Modding.EntityExportInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Export
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntityExport(EntityExportInfo arg, TimeSpan timeout);


        /// <summary>
        /// Command:  CmdId.Request_Entity_SetName
        /// Argument: Eleon.Modding.IdPlayfieldName
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_SetName
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        Task EntitySetName(IdPlayfieldName arg);

        /// <summary>
        /// Command:  CmdId.Request_Entity_SetName
        /// Argument: Eleon.Modding.IdPlayfieldName
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_SetName
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        Task EntitySetName(IdPlayfieldName arg, TimeSpan timeout);

    }
}