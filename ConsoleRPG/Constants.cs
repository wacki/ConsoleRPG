using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    static class Constants {
        //map size constance
        public static readonly int iMapSizeX = 23;
        public static readonly int iMapSizeY = 20;

        //player and map original base for colour
        public static readonly ConsoleColor eMapPlayerColor = ConsoleColor.Red;
        public static readonly ConsoleColor eMapBackgroundColor = ConsoleColor.Black;
        public static readonly string eMapTileSymbol = "█";
        public static readonly string eMapPlayerSymbol = "+";

        //player gold required to win and the starting health
        public static readonly int iGoldAmountToWin = 1000;
        public static readonly int iPlayerStartHealth = 30;
        
        //defualt text and background colours
        public const ConsoleColor eDefaultTextColor = ConsoleColor.White;
        public const ConsoleColor eDefaultTextBackgroundColor = ConsoleColor.Black;
        
        //error text and background colours
        public const ConsoleColor eErrorTextColor = ConsoleColor.Red;
        public const ConsoleColor eErrorTextBackgroundColor = ConsoleColor.DarkRed;

        //warning text and background colours
        public const ConsoleColor eWarningTextColor = ConsoleColor.Yellow;
        public const ConsoleColor eWarningTextBackgroundColor = ConsoleColor.Black;

        //monster span percent chance (15%) AND treasure spawn chance (10%)
        public const float fMonsterSpawnChance = 0.15f;
        public const float fTreasureSpawnChance = 0.1f;
        
    }

    //Enum Direction for player movement
    public enum Direction {
        North, East, South, West
    }

    //Enum for map tile events 
    public enum MapTileEvent {
        Nothing, Combat, Treasure
    }
}
