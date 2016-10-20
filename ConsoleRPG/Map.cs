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

        //colour type for inital map
        // deprecated
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

        // tile resource for inital map
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

        //deprecated
        // first iteration of the map output
        public void Print()
        {
            Util.ConsoleWriteCol(color, Constants.eMapBackgroundColor, Constants.eMapTileSymbol);
        }

        //Message for when the player uses the look comand and notifies what each tile contains per tile type
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

        //returns an appropriate message for the tile we moved on
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
                case Type.Desert: areaString = "desert"; break;
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
    /// 
    class Map {
        private MapTile[,] m_rgTiles;
        private int m_iSizeX;
        private int m_iSizeY;
        // constructor generates a new map of given size
        public Map(int iSizeX, int iSizeY)
        {
            m_rgTiles = new MapTile[iSizeY, iSizeX];
            Generate();
        }

        //generation of the map tile and the percent chance of tile generation
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


                    // assign random tile events
                    randInt = rand.Next(0, 1000);
                    MapTileEvent tileEvent = MapTileEvent.Nothing;

                    int iMonsterChance = (int)(Constants.fMonsterSpawnChance * 1000);
                    int iTreasureChance = iMonsterChance + (int)(Constants.fTreasureSpawnChance * 1000);

                    if (randInt < iMonsterChance) // 10% chance of monster
                        tileEvent = MapTileEvent.Combat;
                    else if(randInt < iTreasureChance) // 10% chance of random loot
                        tileEvent = MapTileEvent.Treasure;                    

                    m_rgTiles[y, x] = new MapTile(type, tileEvent);
                }
            }
        }

        // get tiles at possition
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

        // deprecated
        // display the map
        public void Print(int iPlayerX, int iPlayerY)
        {
            for(int y = m_rgTiles.GetLength(0) - 1; y >= 0; y--) {
                Console.WriteLine();
                for(int x = 0; x < m_rgTiles.GetLength(1); x++) {
                    // print the specific tile
                    var tile = m_rgTiles[y, x];

                    if(iPlayerX == x && iPlayerY == y) {
                        Util.ConsoleWriteCol(Constants.eMapPlayerColor, tile.color, Constants.eMapPlayerSymbol);
                        continue;
                    }

                    tile.Print();
                }
            }

        }

        // draws the actual map
        public void Draw(int playerX, int playerY)
        {
            // get the console window handle
            var windowHandle = Util.GetConsoleHandle();
            // get a graphics context from the window handle
            using(var graphics = Graphics.FromHwnd(windowHandle))

                // rows
                for(int y = 0; y < m_rgTiles.GetLength(0); y++) {
                    Console.WriteLine();
                    // columns
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
