using Eleon.Modding;
using EmpyrionPrime.ModFramework.Extensions;
using System;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework
{
    /// <summary>
    /// Interface for asynchronous Empyrion Api Requests
    /// </summary>
    public partial class ApiRequests
    {
        /// <summary>
        /// Command:  CmdId.Request_Playfield_List
        /// Argument: null
        /// Response: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldList</returns>
        public Task<PlayfieldList> PlayfieldList()
        {
            return PlayfieldList(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Playfield_List
        /// Argument: null
        /// Response: Eleon.Modding.PlayfieldList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldList</returns>
        public async Task<PlayfieldList> PlayfieldList(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Playfield_List))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Playfield_List, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (PlayfieldList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Playfield_Stats
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Stats
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldStats</returns>
        public Task<PlayfieldStats> PlayfieldStats(PString arg)
        {
            return PlayfieldStats(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Playfield_Stats
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Stats
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldStats</returns>
        public async Task<PlayfieldStats> PlayfieldStats(PString arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Playfield_Stats))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Playfield_Stats, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (PlayfieldStats)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Dedi_Stats
        /// Argument: null
        /// Response: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Dedi_Stats
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.DediStats</returns>
        public Task<DediStats> DediStats()
        {
            return DediStats(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Dedi_Stats
        /// Argument: null
        /// Response: Eleon.Modding.DediStats
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Dedi_Stats
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.DediStats</returns>
        public async Task<DediStats> DediStats(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Dedi_Stats))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Dedi_Stats, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (DediStats)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_List
        /// Argument: null
        /// Response: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.GlobalStructureList</returns>
        public Task<GlobalStructureList> GlobalStructureList()
        {
            return GlobalStructureList(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_List
        /// Argument: null
        /// Response: Eleon.Modding.GlobalStructureList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.GlobalStructureList</returns>
        public async Task<GlobalStructureList> GlobalStructureList(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_GlobalStructure_List))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_GlobalStructure_List, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (GlobalStructureList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_Update
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_Update
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task GlobalStructureUpdate(PString arg)
        {
            return GlobalStructureUpdate(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_GlobalStructure_Update
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GlobalStructure_Update
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task GlobalStructureUpdate(PString arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_GlobalStructure_Update, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Structure_Touch
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_Touch
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task StructureTouch(Id arg)
        {
            return StructureTouch(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Structure_Touch
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_Touch
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task StructureTouch(Id arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Structure_Touch, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Structure_BlockStatistics
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_BlockStatistics
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdStructureBlockInfo</returns>
        public Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg)
        {
            return StructureBlockStatistics(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Structure_BlockStatistics
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdStructureBlockInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Structure_BlockStatistics
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdStructureBlockInfo</returns>
        public async Task<IdStructureBlockInfo> StructureBlockStatistics(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Structure_BlockStatistics))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Structure_BlockStatistics, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdStructureBlockInfo)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_Info
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Info
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayerInfo</returns>
        public Task<PlayerInfo> PlayerInfo(Id arg)
        {
            return PlayerInfo(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_Info
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.PlayerInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Info
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayerInfo</returns>
        public async Task<PlayerInfo> PlayerInfo(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_Info))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_Info, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (PlayerInfo)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_List
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        public Task<IdList> PlayerList()
        {
            return PlayerList(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_List
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        public async Task<IdList> PlayerList(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_List))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_List, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_GetInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public Task<Inventory> PlayerGetInventory(Id arg)
        {
            return PlayerGetInventory(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_GetInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public async Task<Inventory> PlayerGetInventory(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_GetInventory))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_GetInventory, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (Inventory)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_SetInventory
        /// Argument: Eleon.Modding.Inventory
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public Task<Inventory> PlayerSetInventory(Inventory arg)
        {
            return PlayerSetInventory(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_SetInventory
        /// Argument: Eleon.Modding.Inventory
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public async Task<Inventory> PlayerSetInventory(Inventory arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_SetInventory))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_SetInventory, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (Inventory)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_AddItem
        /// Argument: Eleon.Modding.IdItemStack
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddItem
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task PlayerAddItem(IdItemStack arg)
        {
            return PlayerAddItem(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_AddItem
        /// Argument: Eleon.Modding.IdItemStack
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddItem
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task PlayerAddItem(IdItemStack arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Player_AddItem, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_Credits
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Credits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public Task<IdCredits> PlayerCredits(Id arg)
        {
            return PlayerCredits(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_Credits
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_Credits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public async Task<IdCredits> PlayerCredits(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_Credits))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_Credits, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdCredits)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_SetCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetCredits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public Task<IdCredits> PlayerSetCredits(IdCredits arg)
        {
            return PlayerSetCredits(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_SetCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetCredits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public async Task<IdCredits> PlayerSetCredits(IdCredits arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_SetCredits))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_SetCredits, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdCredits)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_AddCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddCredits
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public Task<IdCredits> PlayerAddCredits(IdCredits arg)
        {
            return PlayerAddCredits(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_AddCredits
        /// Argument: Eleon.Modding.IdCredits
        /// Response: Eleon.Modding.IdCredits
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_AddCredits
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdCredits</returns>
        public async Task<IdCredits> PlayerAddCredits(IdCredits arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_AddCredits))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_AddCredits, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdCredits)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Finish
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Finish
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task BlueprintFinish(Id arg)
        {
            return BlueprintFinish(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Finish
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Finish
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task BlueprintFinish(Id arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Blueprint_Finish, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Resources
        /// Argument: Eleon.Modding.BlueprintResources
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Resources
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task BlueprintResources(BlueprintResources arg)
        {
            return BlueprintResources(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Blueprint_Resources
        /// Argument: Eleon.Modding.BlueprintResources
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Blueprint_Resources
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task BlueprintResources(BlueprintResources arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Blueprint_Resources, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_ChangePlayerfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ChangePlayerfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg)
        {
            return PlayerChangePlayerfield(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_ChangePlayerfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ChangePlayerfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task PlayerChangePlayerfield(IdPlayfieldPositionRotation arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Player_ChangePlayerfield, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_ItemExchange
        /// Argument: Eleon.Modding.ItemExchangeInfo
        /// Response: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ItemExchange
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.ItemExchangeInfo</returns>
        public Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg)
        {
            return PlayerItemExchange(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_ItemExchange
        /// Argument: Eleon.Modding.ItemExchangeInfo
        /// Response: Eleon.Modding.ItemExchangeInfo
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_ItemExchange
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.ItemExchangeInfo</returns>
        public async Task<ItemExchangeInfo> PlayerItemExchange(ItemExchangeInfo arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_ItemExchange))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_ItemExchange, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (ItemExchangeInfo)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_SetPlayerInfo
        /// Argument: Eleon.Modding.PlayerInfoSet
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetPlayerInfo
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task PlayerSetPlayerInfo(PlayerInfoSet arg)
        {
            return PlayerSetPlayerInfo(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_SetPlayerInfo
        /// Argument: Eleon.Modding.PlayerInfoSet
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_SetPlayerInfo
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task PlayerSetPlayerInfo(PlayerInfoSet arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Player_SetPlayerInfo, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_Teleport
        /// Argument: Eleon.Modding.IdPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Teleport
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntityTeleport(IdPositionRotation arg)
        {
            return EntityTeleport(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_Teleport
        /// Argument: Eleon.Modding.IdPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Teleport
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntityTeleport(IdPositionRotation arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_Teleport, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_ChangePlayfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_ChangePlayfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntityChangePlayfield(IdPlayfieldPositionRotation arg)
        {
            return EntityChangePlayfield(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_ChangePlayfield
        /// Argument: Eleon.Modding.IdPlayfieldPositionRotation
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_ChangePlayfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntityChangePlayfield(IdPlayfieldPositionRotation arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_ChangePlayfield, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntityDestroy(Id arg)
        {
            return EntityDestroy(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy
        /// Argument: Eleon.Modding.Id
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntityDestroy(Id arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_Destroy, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_PosAndRot
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_PosAndRot
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdPositionRotation</returns>
        public Task<IdPositionRotation> EntityPosAndRot(Id arg)
        {
            return EntityPosAndRot(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_PosAndRot
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.IdPositionRotation
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_PosAndRot
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdPositionRotation</returns>
        public async Task<IdPositionRotation> EntityPosAndRot(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Entity_PosAndRot))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Entity_PosAndRot, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdPositionRotation)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_Spawn
        /// Argument: Eleon.Modding.EntitySpawnInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Spawn
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntitySpawn(EntitySpawnInfo arg)
        {
            return EntitySpawn(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_Spawn
        /// Argument: Eleon.Modding.EntitySpawnInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Spawn
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntitySpawn(EntitySpawnInfo arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_Spawn, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Get_Factions
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Get_Factions
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.FactionInfoList</returns>
        public Task<FactionInfoList> GetFactions(Id arg)
        {
            return GetFactions(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Get_Factions
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.FactionInfoList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Get_Factions
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.FactionInfoList</returns>
        public async Task<FactionInfoList> GetFactions(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Get_Factions))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Get_Factions, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (FactionInfoList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_NewEntityId
        /// Argument: null
        /// Response: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_NewEntityId
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Id</returns>
        public Task<Id> NewEntityId()
        {
            return NewEntityId(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_NewEntityId
        /// Argument: null
        /// Response: Eleon.Modding.Id
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_NewEntityId
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Id</returns>
        public async Task<Id> NewEntityId(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_NewEntityId))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_NewEntityId, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (Id)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_AlliancesAll
        /// Argument: null
        /// Response: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesAll
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesTable</returns>
        public Task<AlliancesTable> AlliancesAll()
        {
            return AlliancesAll(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_AlliancesAll
        /// Argument: null
        /// Response: Eleon.Modding.AlliancesTable
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesAll
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesTable</returns>
        public async Task<AlliancesTable> AlliancesAll(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_AlliancesAll))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_AlliancesAll, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (AlliancesTable)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_AlliancesFaction
        /// Argument: Eleon.Modding.AlliancesFaction
        /// Response: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesFaction
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesFaction</returns>
        public Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg)
        {
            return AlliancesFaction(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_AlliancesFaction
        /// Argument: Eleon.Modding.AlliancesFaction
        /// Response: Eleon.Modding.AlliancesFaction
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_AlliancesFaction
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.AlliancesFaction</returns>
        public async Task<AlliancesFaction> AlliancesFaction(AlliancesFaction arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_AlliancesFaction))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_AlliancesFaction, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (AlliancesFaction)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Load_Playfield
        /// Argument: Eleon.Modding.PlayfieldLoad
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Load_Playfield
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task LoadPlayfield(PlayfieldLoad arg)
        {
            return LoadPlayfield(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Load_Playfield
        /// Argument: Eleon.Modding.PlayfieldLoad
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Load_Playfield
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task LoadPlayfield(PlayfieldLoad arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Load_Playfield, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_ConsoleCommand
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ConsoleCommand
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task ConsoleCommand(PString arg)
        {
            return ConsoleCommand(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_ConsoleCommand
        /// Argument: Eleon.Modding.PString
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ConsoleCommand
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task ConsoleCommand(PString arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_ConsoleCommand, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_GetBannedPlayers
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GetBannedPlayers
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        public Task<IdList> GetBannedPlayers()
        {
            return GetBannedPlayers(DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_GetBannedPlayers
        /// Argument: null
        /// Response: Eleon.Modding.IdList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_GetBannedPlayers
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdList</returns>
        public async Task<IdList> GetBannedPlayers(TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_GetBannedPlayers))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_GetBannedPlayers, null)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_SinglePlayer
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_SinglePlayer
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task InGameMessageSinglePlayer(IdMsgPrio arg)
        {
            return InGameMessageSinglePlayer(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_SinglePlayer
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_SinglePlayer
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task InGameMessageSinglePlayer(IdMsgPrio arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_InGameMessage_SinglePlayer, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_AllPlayers
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_AllPlayers
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task InGameMessageAllPlayers(IdMsgPrio arg)
        {
            return InGameMessageAllPlayers(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_AllPlayers
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_AllPlayers
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task InGameMessageAllPlayers(IdMsgPrio arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_InGameMessage_AllPlayers, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_Faction
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_Faction
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task InGameMessageFaction(IdMsgPrio arg)
        {
            return InGameMessageFaction(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_InGameMessage_Faction
        /// Argument: Eleon.Modding.IdMsgPrio
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_InGameMessage_Faction
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task InGameMessageFaction(IdMsgPrio arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_InGameMessage_Faction, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_ShowDialog_SinglePlayer
        /// Argument: Eleon.Modding.DialogBoxData
        /// Response: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ShowDialog_SinglePlayer
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.IdAndIntValue</returns>
        public Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg)
        {
            return ShowDialogSinglePlayer(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_ShowDialog_SinglePlayer
        /// Argument: Eleon.Modding.DialogBoxData
        /// Response: Eleon.Modding.IdAndIntValue
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_ShowDialog_SinglePlayer
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.IdAndIntValue</returns>
        public async Task<IdAndIntValue> ShowDialogSinglePlayer(DialogBoxData arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_ShowDialog_SinglePlayer))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_ShowDialog_SinglePlayer, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (IdAndIntValue)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Player_GetAndRemoveInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetAndRemoveInventory
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public Task<Inventory> PlayerGetAndRemoveInventory(Id arg)
        {
            return PlayerGetAndRemoveInventory(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Player_GetAndRemoveInventory
        /// Argument: Eleon.Modding.Id
        /// Response: Eleon.Modding.Inventory
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Player_GetAndRemoveInventory
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.Inventory</returns>
        public async Task<Inventory> PlayerGetAndRemoveInventory(Id arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Player_GetAndRemoveInventory))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Player_GetAndRemoveInventory, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (Inventory)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Playfield_Entity_List
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Entity_List
        /// </summary>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldEntityList</returns>
        public Task<PlayfieldEntityList> PlayfieldEntityList(PString arg)
        {
            return PlayfieldEntityList(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Playfield_Entity_List
        /// Argument: Eleon.Modding.PString
        /// Response: Eleon.Modding.PlayfieldEntityList
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Playfield_Entity_List
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a Eleon.Modding.PlayfieldEntityList</returns>
        public async Task<PlayfieldEntityList> PlayfieldEntityList(PString arg, TimeSpan timeout)
        {
#if DEBUG
            using(new DebugLog(_logger, CmdId.Request_Playfield_Entity_List))
#endif
            {
                var result = await _requestBroker.SendGameRequest(CmdId.Request_Playfield_Entity_List, arg)
                    .TimeoutAfter((int)timeout.TotalMilliseconds);

                return (PlayfieldEntityList)result;
            }
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy2
        /// Argument: Eleon.Modding.IdPlayfield
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy2
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntityDestroy2(IdPlayfield arg)
        {
            return EntityDestroy2(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_Destroy2
        /// Argument: Eleon.Modding.IdPlayfield
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Destroy2
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntityDestroy2(IdPlayfield arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_Destroy2, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_Export
        /// Argument: Eleon.Modding.EntityExportInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Export
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntityExport(EntityExportInfo arg)
        {
            return EntityExport(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_Export
        /// Argument: Eleon.Modding.EntityExportInfo
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_Export
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntityExport(EntityExportInfo arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_Export, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }
        /// <summary>
        /// Command:  CmdId.Request_Entity_SetName
        /// Argument: Eleon.Modding.IdPlayfieldName
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_SetName
        /// </summary>
        /// <returns>Task wrapping a null</returns>
        public Task EntitySetName(IdPlayfieldName arg)
        {
            return EntitySetName(arg, DefaultTimeout);
        }

        /// <summary>
        /// Command:  CmdId.Request_Entity_SetName
        /// Argument: Eleon.Modding.IdPlayfieldName
        /// Response: null
        /// Docs: https://empyrion.fandom.com/wiki/Game_API_CmdId#Request_Entity_SetName
        /// </summary>
        /// <param name="timeout">Timeout after which the task will cancel</param>
        /// <returns>Task wrapping a null</returns>
        public async Task EntitySetName(IdPlayfieldName arg, TimeSpan timeout)
        {
            await _requestBroker.SendGameRequest(CmdId.Request_Entity_SetName, arg)
                .TimeoutAfter((int)timeout.TotalMilliseconds);
        }

    }
}
