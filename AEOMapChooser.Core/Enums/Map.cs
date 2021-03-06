﻿using System.Diagnostics;

namespace AEOMapChooser.Core.Enums
{
    [DebuggerDisplay("{Type} {Name}")]
    public class Map : Enumeration
    {
        private static int incr = 0;

        public MapType Type { get; }

        // Data taken from https://overwatch.gamepedia.com/Maps
        public static Map Hanamura = new Map("Hanamura", MapType.Assault);
        public static Map HorizonLunarColony = new Map("Horizon Lunar Colony", MapType.Assault);
        public static Map TempleOfAnubis = new Map("Temple of Anubis", MapType.Assault);
        public static Map VolskayaIndustries = new Map("Volskaya Industries", MapType.Assault);

        public static Map Dorado = new Map("Dorado", MapType.Escort);
        public static Map Rialto = new Map("Rialto", MapType.Escort);
        public static Map Junkertown = new Map("Junkertown", MapType.Escort);
        public static Map Route66 = new Map("Route 66", MapType.Escort);
        public static Map WatchpointGibraltar = new Map("Watchpoint: Gibraltar", MapType.Escort);

        public static Map BlizzardWorld = new Map("Blizzard World", MapType.Hybrid);
        public static Map Eichenwalde = new Map("Eichenwalde", MapType.Hybrid);
        public static Map Hollywood = new Map("Hollywood", MapType.Hybrid);
        public static Map KingsRow = new Map("King's Row", MapType.Hybrid);
        public static Map Numbani = new Map("Numbani", MapType.Hybrid);

        public static Map Ilios = new Map("Ilios", MapType.Control);
        public static Map LijiangTower = new Map("Lijiang Tower", MapType.Control);
        public static Map Nepal = new Map("Nepal", MapType.Control);
        public static Map Oasis = new Map("Oasis", MapType.Control);

        public static Map Ayutthaya = new Map("Ayutthaya", MapType.Arcade);
        public static Map BlackForest = new Map("Black Forest", MapType.Arcade);
        public static Map ChateauGuillard = new Map("Château Guillard", MapType.Arcade);
        public static Map EcopointAntarctica = new Map("Ecopoint: Antarctica", MapType.Arcade);
        public static Map Necropolis = new Map("Necropolis", MapType.Arcade);
        public static Map Petra = new Map("Petra", MapType.Arcade);

        public Map()
        {
        }

        private Map(string name, MapType type) : base(incr++, name)
        {
            Type = type;
        }
    }
}
