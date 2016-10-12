using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace ConsoleRPG {


    /// <summary>
    /// Holds information about a specific map tile
    /// </summary>
    public class MapTile {
        public enum Type {
            Plains,
            Water,
            Wood,
            Mountains,
            Desert
        };


        private Type m_eType;
        public MapTileEvent eTileEvent;
        private Bitmap m_bitmap;
        public Bitmap image { get { return m_bitmap; } }

        public ConsoleColor color
        {
            get
            {
                switch(m_eType) {
                    case Type.Plains: return ConsoleColor.Green;
                    case Type.Water: return ConsoleColor.Blue;
                    case Type.Wood: return ConsoleColor.DarkGreen;
                    case Type.Mountains: return ConsoleColor.Gray;
                    case Type.Desert: return ConsoleColor.Yellow;
                    default: return ConsoleColor.White;
                }
            }
        }


        public MapTile(Type type, MapTileEvent tileEvent)
        {
            m_eType = type;
            eTileEvent = tileEvent;

            switch(type) {
                case Type.Plains: m_bitmap = Properties.Resources.plains; break;
                case Type.Water: m_bitmap = Properties.Resources.water; break;
                case Type.Wood: m_bitmap = Properties.Resources.wood; break;
                case Type.Mountains: m_bitmap = Properties.Resources.mountain; break;
                case Type.Desert: m_bitmap = Properties.Resources.desert; break;
            }

        }

        public void Print()
        {
            Util.ConsoleWriteCol(color, Constants.eMapBackgroundColor, Constants.eMapTileSymbol);
        }

        public string GetLookMessage()
        {
            string areaString = "";
            string eventTypeString = "";

            switch(eTileEvent) {
                case MapTileEvent.Nothing: eventTypeString = "safe"; break;
                case MapTileEvent.Combat: eventTypeString = "combat"; break;
                case MapTileEvent.Treasure: eventTypeString = "treasure"; break;
            }

            switch(m_eType) {
                case Type.Plains: areaString = "grassy area"; break;
                case Type.Mountains: areaString = "mountanous area"; break;
                case Type.Water: areaString = "water area"; break;
                case Type.Wood: areaString = "forest"; break;
                case Type.Desert: areaString = "desert"; break;
            }


            return MessageManager.instance.GetRandomMsg("map_look_" + eventTypeString, areaString);
        }

        public string GetMoveMessage()
        {
            string areaString = "";
            string eventTypeString = "";

            switch(eTileEvent) {
                case MapTileEvent.Nothing: eventTypeString = "safe"; break;
                case MapTileEvent.Combat: eventTypeString = "combat"; break;
                case MapTileEvent.Treasure: eventTypeString = "treasure"; break;
            }

            switch(m_eType) {
                case Type.Plains: areaString = "plains"; break;
                case Type.Mountains: areaString = "mountains"; break;
                case Type.Water: areaString = "water"; break;
                case Type.Wood: areaString = "woods"; break;
            }


            return MessageManager.instance.GetRandomMsg("map_move_" + eventTypeString, areaString);
        }

        // returns a monster CLASS to be used in combat
        // this is currently just a placeholder.
        public Monster GetMonster()
        {
            // todo: spawn monster based on tile type
            if(eTileEvent == MapTileEvent.Combat)
                return new Monster(m_eType);

            return null;
        }

        // returns a treasure class to be looted by the character
        public string GetTreasure()
        {
            // todo: spawn monster based on tile type
            if(eTileEvent == MapTileEvent.Combat)
                return "TREASURE_TODO";

            return "NO TREASURE TODO";
        }

        public MapTileEvent GetEventType()
        {
            return eTileEvent;
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
                    else if(randInt < 300) // 10% chance of tile being a forest
                        type = MapTile.Type.Desert;
                    else if(randInt < 600) // 30% chance of tile being a forest
                        type = MapTile.Type.Wood;



                    randInt = rand.Next(0, 1000);
                    MapTileEvent tileEvent = MapTileEvent.Nothing;
                    if(randInt < 100) // 10% chance of monster
                        tileEvent = MapTileEvent.Combat;
                    else if(randInt < 200) // 10% chance of random loot
                        tileEvent = MapTileEvent.Treasure;

                    tileEvent = MapTileEvent.Combat;

                    m_rgTiles[y, x] = new MapTile(type, tileEvent);
                }
            }
        }


        public MapTile GetTileAt(Vector2i pos)
        {
            return GetTileAt(pos.x, pos.y);
        }

        public MapTile GetTileAt(int x, int y)
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

        public void Draw(int playerX, int playerY)
        {
            var windowHandle = Util.GetConsoleHandle();
            using(var graphics = Graphics.FromHwnd(windowHandle))

                for(int y = 0; y < m_rgTiles.GetLength(0); y++) {
                    Console.WriteLine();
                    for(int x = 0; x < m_rgTiles.GetLength(1); x++) {
                        // print the specific tile
                        var tile = m_rgTiles[y, x];

                        graphics.DrawImage(tile.image, x * 32, (Constants.iMapSizeY - 1) * 32 - y * 32, 32, 32);

                        // hero test
                        if(x == playerX && y == playerY) {
                            var image1 = Properties.Resources.hero;
                            graphics.DrawImage(image1, x * 32 + 8, (Constants.iMapSizeY - 1) * 32 - y * 32 + 8, 16, 16);
                        }
                    }
                }
            Console.SetCursorPosition(0, 52);
        }
    }
}
