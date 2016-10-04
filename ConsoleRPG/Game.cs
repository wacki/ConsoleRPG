using System;

namespace ConsoleRPG {
    class Game {

        private bool m_bRunning;
        private Map m_oMap;
        private Character m_oCharacter;


        public Game()
        {
            m_oMap = new Map(Constants.iMapSizeX, Constants.iMapSizeY);

            // set up character
            m_oCharacter = new Character();
        }
        
        public void Run()
        {
            m_bRunning = true;
            while(m_bRunning) {
                Console.Clear();

                // 1. display map
                int playerX = m_oCharacter.coordinates.x;
                int playerY = m_oCharacter.coordinates.y;
                m_oMap.Print(playerX, playerY);
                
                // 2. handle user input?
                HandleUserInput();
                Console.ReadLine();
            }
        }
        
        public void Stop(bool prompt = false)
        {
            if(prompt) {                
                MessageManager.instance.PrintRandomMsg("quit_game_prompt");
                if(!Util.YesNoPrompt())
                    return;
            }

            m_bRunning = false;
        }
        
        private void HandleUserInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            // get user input for command
            Util.GetInput((string input, out string output) => {
                output = input;

                // temp input handling. This needs to be improved
                switch(input.ToLower()) {
                    case "quit": Stop(true); return true;
                    case "look": Look(); return true;
                    case "move": Move(); return true;
                    case "equip": Equip(); return true;
                    case "use": Use(); return true;
                }
                
                return false;
            
            }, MessageManager.instance.GetRandomMsg("user_command_invalid"));


        }

        #region user commands

        /// <summary>
        /// Called when player uses the look command
        /// </summary>
        private void Look()
        {
            // todo
            var dir = SelectDirection();
            var tile = GetTileInDir(dir);

            tile.PrintLookMessage();
        }

        /// <summary>
        /// Called when player uses the move command
        /// </summary>
        private void Move()
        {
            // Prompt user for direction
            var dir = SelectDirection();
            

            // todo
        }

        /// <summary>
        /// Called when player uses the equip command
        /// </summary>
        /// <param name="dir"></param>
        private void Equip()
        {
            // todo
        }

        /// <summary>
        /// Called when player uses the use command
        /// </summary>
        /// <param name="dir"></param>
        private void Use()
        {
            // todo
        }



        /// <summary>
        /// Called when player enteres a combat situation.
        /// </summary>
        private void Combat()
        {

        }


        #endregion

        // directions prompt
        private Directions SelectDirection()
        {
            Console.WriteLine(MessageManager.instance.GetRandomMsg("choose_direction_prompt") + "\n");

            for(int i = 0; i < 4; i++)
                Console.Write("({0}) {1}", i, (Directions)i);

            return (Directions)Util.GetIntegerInput(0, 3);
        }

        /// <summary>
        /// Get the map tile that lies in the given direction
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private MapTile GetTileInDir(Directions dir)
        {
            var newPosition = m_oCharacter.coordinates + GetDirectionVector(dir);
            return m_oMap.GetTileAt(newPosition);
        }

        private Vector2i GetDirectionVector(Directions dir)
        {
            switch(dir) {
                case Directions.North: return new Vector2i(0, 1);
                case Directions.East: return new Vector2i(1, 0);
                case Directions.South: return new Vector2i(0, -1);
                case Directions.West: return new Vector2i(-1, 0);
                default: return new Vector2i(0, 0);
            }

        }



    }
}
