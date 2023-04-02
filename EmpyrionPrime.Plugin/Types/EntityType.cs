namespace EmpyrionPrime.Plugin.Types
{
    /// <summary>
    /// Entity type, used in some API requests
    /// </summary>
    public enum EntityType : int
    {
        Unknown          = 0,
        Player           = 1,
        BA               = 2,
        CV               = 3,
        SV               = 4,
        HV               = 5,
        AstRes           = 6,
        AstVoxel         = 7,
        EscapePod        = 8,
        Animal           = 9,
        Turret           = 10,
        Item             = 11,
        PlayerDrone      = 12,
        Trader           = 13,
        UndergroundRes   = 14,
        EnemyDrone       = 15,
        PlayerBackpack   = 16,
        DropContainer    = 17,
        ExplosiveDevice  = 18,
        PlayerBike       = 19,
        PlayerBikeFolded = 20,
        Asteroid         = 21,
        Civilian         = 22,
        Cyborg           = 23,
        TroopTransport   = 24
    }
}
