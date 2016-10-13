using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    static class Constants {
        public static readonly int iMapSizeX = 23;
        public static readonly int iMapSizeY = 20;

        public static readonly ConsoleColor eMapPlayerColor = ConsoleColor.Red;
        public static readonly ConsoleColor eMapBackgroundColor = ConsoleColor.Black;
        public static readonly string eMapTileSymbol = "█";
        public static readonly string eMapPlayerSymbol = "+";

        public static readonly int iGoldAmountToWin = 1000;
        public static readonly int iPlayerStartHealth = 30;

        public const ConsoleColor eDefaultTextColor = ConsoleColor.White;
        public const ConsoleColor eDefaultTextBackgroundColor = ConsoleColor.Black;

        public const ConsoleColor eErrorTextColor = ConsoleColor.Red;
        public const ConsoleColor eErrorTextBackgroundColor = ConsoleColor.DarkRed;

        public const ConsoleColor eWarningTextColor = ConsoleColor.Yellow;
        public const ConsoleColor eWarningTextBackgroundColor = ConsoleColor.Black;

        public const float fMonsterSpawnChance = 0.15f;
        public const float fTreasureSpawnChance = 0.1f;
        
    }

    public enum Direction {
        North, East, South, West
    }

    public enum MapTileEvent {
        Nothing, Combat, Treasure
    }
}
