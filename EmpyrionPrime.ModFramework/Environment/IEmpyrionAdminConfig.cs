using System;
using System.Collections.Generic;

namespace EmpyrionPrime.ModFramework.Environment
{
    public interface IEmpyrionAdminConfig
    {
        /// <summary>
        /// Map of SteamIds to their granted permission level
        /// </summary>
        IReadOnlyDictionary<long, int> Permissions { get; }

        /// <summary>
        /// Map of banned SteamIds to their ban expiration date
        /// </summary>
        IReadOnlyDictionary<long, DateTime> BannedUsers { get; }
    }
}
