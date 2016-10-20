using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    static class Constants {
        // Map size
        public static readonly int iMapSizeX = 23;
        public static readonly int iMapSizeY = 20;

        // Color map and symbols used in the first iteration of the game
        public static readonly ConsoleColor eMapPlayerColor = ConsoleColor.Red;
        public static readonly ConsoleColor eMapBackgroundColor = ConsoleColor.Black;
        public static readonly string eMapTileSymbol = "█";
        public static readonly string eMapPlayerSymbol = "+";

        // Gold required to win the game
        public static readonly int iGoldAmountToWin = 1000;
        // Starting health of the player
        public static readonly int iPlayerStartHealth = 30;
        
        // default text foreground and background color
        public const ConsoleColor eDefaultTextColor = ConsoleColor.White;
        public const ConsoleColor eDefaultTextBackgroundColor = ConsoleColor.Black;
        
        // error text foreground and background color
        public const ConsoleColor eErrorTextColor = ConsoleColor.Red;
        public const ConsoleColor eErrorTextBackgroundColor = ConsoleColor.DarkRed;

        // warning text foreground and background color
        public const ConsoleColor eWarningTextColor = ConsoleColor.Yellow;
        public const ConsoleColor eWarningTextBackgroundColor = ConsoleColor.Black;

        //monster span percent chance (15%) AND treasure spawn chance (10%)
        public const float fMonsterSpawnChance = 0.15f;
        public const float fTreasureSpawnChance = 0.1f;
        
    }

    // directions enum
    public enum Direction {
        North, East, South, West
    }

    //Enum for map tile events 
    public enum MapTileEvent {
        Nothing, Combat, Treasure
    }
}
