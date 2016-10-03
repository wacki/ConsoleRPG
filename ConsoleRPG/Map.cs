using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {

    /// <summary>
    /// Holds information about a specific map tile
    /// </summary>
    class MapTile {
        public enum Type {
            Plains,
            Water,
            Wood,
            Mountains
        };


        private Type m_eType;

        public ConsoleColor color
        {
            get
            {
                switch(m_eType) {
                    case Type.Plains: return ConsoleColor.Green;
                    case Type.Water: return ConsoleColor.Blue;
                    case Type.Wood: return ConsoleColor.DarkGreen;
                    case Type.Mountains: return ConsoleColor.Gray;
                    default: return ConsoleColor.White;
                }
            }
        }


        public MapTile(Type type)
        {
            m_eType = type;
        }

        public void Print()
        {
            Util.ConsoleWriteCol(color, Constants.eMapBackgroundColor, Constants.eMapTileSymbol);
        }

    }


    /// <summary>
    /// Representation of ingame map
    /// 
    /// Coordinates work as follows:
    ///       
    ///           x
    ///      ----------->
    ///    ^ oooooooooooo
    ///    | oooooooooooo
    ///    | oooooooooooo
    ///  y | oooooooooooo
    ///    | oooooooooooo
    ///    | oooooooooooo
    /// 
    /// </summary>
    class Map {
        private MapTile[,] m_rgTiles;
        private int m_iSizeX;
        private int m_iSizeY;

        public Map(int sizeX, int sizeY)
        {
            m_rgTiles = new MapTile[sizeY, sizeX];
            Generate();
        }

        private void Generate()
        {
            Random rand = new Random();
            

            for(int x = 0; x < m_rgTiles.GetLength(1); x++) {
                for(int y = 0; y < m_rgTiles.GetLength(0); y++) {
                    int randInt = rand.Next(0, 1000);

                    MapTile.Type type = MapTile.Type.Plains;
                    if(randInt < 100) // 10% chance of tile being water
                        type = MapTile.Type.Water;
                    else if(randInt < 200) // 20% chance of tile being mountains
                        type = MapTile.Type.Mountains;
                    else if(randInt < 500) // 30% chance of tile being a forest
                        type = MapTile.Type.Wood;
                    
                    m_rgTiles[y, x] = new MapTile(type);
                }
            }
        }

        private MapTile GetTileAt(int x, int y)
        {
            if(x < 0 || m_rgTiles.GetLength(1) <= x ||
               y < 0 || m_rgTiles.GetLength(0) <= y)
                return null;

            return m_rgTiles[y, x];
        }

        public void Print(int playerX, int playerY)
        {
            for(int y = m_rgTiles.GetLength(0) - 1; y >= 0; y--) {
                Console.WriteLine();
                for(int x = 0; x < m_rgTiles.GetLength(1); x++) {
                    // print the specific tile
                    var tile = m_rgTiles[y, x];

                    if(playerX == x && playerY == y) {
                        Util.ConsoleWriteCol(Constants.eMapPlayerColor, tile.color, Constants.eMapPlayerSymbol);
                        continue;
                    }

                    tile.Print();
                }
            }

        }
    }
}
