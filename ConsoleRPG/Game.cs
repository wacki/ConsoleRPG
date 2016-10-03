using System;

namespace ConsoleRPG {
    class Game {

        private bool m_bRunning;

        public Game()
        {
        }
        
        public void Run()
        {
            m_bRunning = true;
            while(m_bRunning) {
                Console.Clear();

                // 1. display map

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
                    case "q": Stop(true); return true;
                }
                
                return false;
            
            }, "That is not a valid instruction!");
            
        }
    }
}
