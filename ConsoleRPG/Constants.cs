using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    static class Constants {
        public static readonly int iMapSizeX = 30;
        public static readonly int iMapSizeY = 20;

        public static readonly ConsoleColor eMapPlayerColor = ConsoleColor.Red;
        public static readonly ConsoleColor eMapBackgroundColor = ConsoleColor.Black;
        public static readonly string eMapTileSymbol = "█";
        public static readonly string eMapPlayerSymbol = "+";
    }

    enum Direction {
        North, East, South, West
    }

    enum MapTileEvent {
        Nothing, Combat, Treasure
    }
}
