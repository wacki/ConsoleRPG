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
                
            }
        }
        
        public void Stop(bool prompt = false)
        {
            if(prompt) {
                Console.WriteLine("Are you sure you want to quit the game? (yes, no)");
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
            
            }, "That is not a valid instruction!");
            
        }

        #region user commands

        /// <summary>
        /// Called when player uses the look command
        /// </summary>
        private void Look()
        {
            // todo
        }

        /// <summary>
        /// Called when player uses the move command
        /// </summary>
        private void Move()
        {
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






    }
}
