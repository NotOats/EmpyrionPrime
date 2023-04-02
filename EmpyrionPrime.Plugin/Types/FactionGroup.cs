namespace EmpyrionPrime.Plugin.Types
{
    /// <summary>
    /// Faction group used in some Empyrion API calls
    /// </summary>
    public enum FactionGroup : byte
    {
        // Use EmpyrionNetAPIAccess version since it seems to be the most up to date
        // https://github.com/GitHub-TC/EmpyrionNetAPIAccess/blob/master/EmpyrionNetAPIDefinitions/APIEnums.cs
        Faction  = 0,
        Player   = 1,
        Alien    = 2,
        Predator = 3,
        Prey     = 4,
        Admin    = 5,

        /*
        // https://empyrion.fandom.com/wiki/Game_API_GlobalStructureInfo#public_byte_factionGroup
        Faction  = 0,
        Privat   = 1,
        Zirax    = 2,
        Predator = 3,
        Prey     = 4,
        Admin    = 5,
        Talon    = 6,
        Polaris  = 7,
        Alien    = 8,
        Unknown  = 11,
        Public   = 10,
        None     = 12,
        Decored  = 255,
        */
    }
}
