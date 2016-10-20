using System;

namespace ConsoleRPG {
    class Game {

        // is the game running
        private bool m_bRunning;
        // should the game restart
        private bool m_bRestart;
        // map instance
        private Map m_oMap;
        // character instance
        private Character m_oCharacter;

        // feedback msg color
        private ConsoleColor m_feedbackMsgForeground = Constants.eDefaultTextColor;
        private ConsoleColor m_feedbackMsgBackground = Constants.eDefaultTextBackgroundColor;
        // current feedback msg string
        private string m_sFeedbackMsg = "";
        
        // Outputs a feedback message to the player
        private void PrintFeedbackMsg()
        {
            var prevForeground = Console.ForegroundColor;
            var prevBackground = Console.BackgroundColor;

            Console.ForegroundColor = m_feedbackMsgForeground;
            Console.BackgroundColor = m_feedbackMsgBackground;

            Console.WriteLine("\n\n" + m_sFeedbackMsg + "\n");

            Console.ForegroundColor = prevForeground;
            Console.BackgroundColor = prevBackground;
        }

        // Starts a game, resetting all of the stats
        public void Start()
        {
            // reset restart variable
            m_bRestart = false;

            // generate new map
            m_oMap = new Map(Constants.iMapSizeX, Constants.iMapSizeY);

            // set up fresh character
            m_oCharacter = new Character();

            // Run the game
            Run();
        }

        // runs the actual game loop
        private void Run()
        {
            // set the feedback msg to the start game message
            m_sFeedbackMsg = MessageManager.instance.GetRandomMsg("start_game_msg");

            // run the game loop
            m_bRunning = true;
            while(m_bRunning) {
                Console.Clear();

                // 1. display map
                int iPlayerX = m_oCharacter.coordinates.x;
                int iPlayerY = m_oCharacter.coordinates.y;
                //m_oMap.Print(playerX, playerY);
                m_oMap.Draw(iPlayerX, iPlayerY);

                // 2. display feedback from previous action           
                PrintFeedbackMsg();

                // 3. handle user input
                PrintInputOptions();
                HandleUserInput();
            }

            if(m_bRestart)
                Start();
        }
        // Stops the game, optionally prompts the player before doing so
        public void Stop(bool bPrompt = false)
        {
            if(bPrompt) {
                MessageManager.instance.PrintRandomMsg("quit_game_prompt");
                if(!Util.YesNoPrompt())
                    return;
            }

            m_bRunning = false;
        }

        //Handles primary input in the main map screen
        private void HandleUserInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            // get user input for command
            Util.GetInput((string sInput, out string sOutput) => {
                sOutput = sInput;

                // we allow for commands with arguments, so we split the string
                char[] shrgSplitter = { ' ' };
                string[] srgInputSplit = sInput.Split(shrgSplitter);

                sInput = sInput.Trim().ToLower();
                if(srgInputSplit.Length < 1)
                    return false;

                string sCommand = srgInputSplit[0];
                string sPayload = sInput.Substring(sCommand.Length).Trim();

                Console.WriteLine("COMMAND: " + sCommand + " PAYLOAD: " + sPayload);

                // temp input handling. This needs to be improved
                switch(sCommand.ToLower()) {
                    case "quit": Stop(true); return true;
                    case "look": Look(sPayload); return true;
                    case "move": Move(sPayload); return true;
                    case "inventory": ShowInventory(); return true;
                    case "status": Status(); return true;
                    case "use": Use(sPayload); return true;
                }

                return false;

            }, MessageManager.instance.GetRandomMsg("user_command_invalid"));


        }

        #region user commands

        // prints out all of the different options the player has
        private void PrintInputOptions()
        {
            Console.WriteLine("What would you like to do? \nlook | move | equip | use | status | quit");
        }


        /// <summary>
        /// Called when player uses the look command
        /// </summary>
        private void Look(string sDirection)
        {
            // todo: duplicated code in the move command. move this to a function
            Direction dir;
            switch(sDirection) {
                case "n":
                case "north":
                    dir = Direction.North;
                    break;

                case "s":
                case "south":
                    dir = Direction.South;
                    break;

                case "e":
                case "east":
                    dir = Direction.East;
                    break;

                case "w":
                case "west":
                    dir = Direction.West;
                    break;

                default:
                    // return error if command was wrong
                    SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("invalid_look_command"));
                    return;
            }

            // retrieve the tile we're looking at
            var tile = GetTileInDir(dir);

            if(tile != null)
                SetFeedbackMsg(tile.GetLookMessage());
            else
                SetFeedbackMsg("TODO: NOTHING THERE MESSAGE (map border)");
        }

        /// <summary>
        /// Called when player uses the move command
        /// </summary>
        private void Move(string sDirection)
        {
            Direction dir;
            switch(sDirection) {
                case "n":
                case "north":
                    dir = Direction.North;
                    break;

                case "s":
                case "south":
                    dir = Direction.South;
                    break;

                case "e":
                case "east":
                    dir = Direction.East;
                    break;

                case "w":
                case "west":
                    dir = Direction.West;
                    break;

                default:
                    // DISPLAY ERROR MESSAGE
                    SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("invalid_move_command"));
                    return;
            }

            // retrieve tile we're walking on
            var tile = GetTileInDir(dir);
            var dirVec = GetDirectionVector(dir);

            if(tile != null) {
                // handle the actual move of the character
                m_oCharacter.Move(dirVec);

                m_sFeedbackMsg = tile.GetMoveMessage();

                switch(tile.GetEventType()) {
                    case MapTileEvent.Nothing: break;
                    case MapTileEvent.Combat: Combat(tile); break;
                    case MapTileEvent.Treasure: break;
                }
            }
            else
                // inaccessible area
                SetFeedbackMsg(MessageManager.instance.GetRandomMsg("move_inacessible_area"));
        }

        /// <summary>
        /// Called when player uses the equip command
        /// </summary>
        private void Equip()
        {
            // todo
            m_sFeedbackMsg = "NOT IMPLEMENTED YET";
        }

        /// <summary>
        /// Called when player uses the use command
        /// </summary>
        private void Use(string sItemString)
        {
            if(!m_oCharacter.hasItems) {
                SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("no_items_to_use"));
                return;
            }


            int iChosenItem;

            if(!int.TryParse(sItemString, out iChosenItem)) {
                SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("you_dont_have_that_item"));
            }
            
            m_sFeedbackMsg = m_oCharacter.Use(iChosenItem);            
        }

        // output the current inventory
        private void ShowInventory()
        {
            SetFeedbackMsg(m_oCharacter.GetItemListString());
        }


        /// <summary>
        /// Called when player enteres a combat situation.
        /// </summary>
        private void Combat(MapTile tile)
        {
            Console.Clear();
            m_sFeedbackMsg = CombatManager.Fight(m_oCharacter, tile);

            DeathCheck();
            WinCheck();
        }

        // output our current HP
        private void Status()
        {
            m_sFeedbackMsg = m_oCharacter.GetStatusString();
        }

        /// <summary>
        /// Called when player enteres a combat situation.
        /// </summary>
        private void Treasure(MapTile tile)
        {
            m_sFeedbackMsg = "You just found treasure! (NOT IMPLEMENTED YET)";


        }

        #endregion
        //Checks if the players health is at 0 and if so promps game end
        private void DeathCheck()
        {
            if(m_oCharacter.iHealth <= 0) {
                Console.WriteLine("You were slain on your journey.");
                m_bRestart = Util.YesNoPrompt();

                Stop(false);
            }
        }
        
        //Checks if the player has recived enought gold to win
        private void WinCheck()
        {
            if(m_oCharacter.iGold >= Constants.iGoldAmountToWin) {
                Console.WriteLine("You win the game. Wanna play again?");
                m_bRestart = Util.YesNoPrompt();

                Stop(false);
            }
        }

        // directions prompt
        // deprecated
        private Direction SelectDirection()
        {
            Console.WriteLine(MessageManager.instance.GetRandomMsg("choose_direction_prompt") + "\n");

            for(int i = 0; i < 4; i++)
                Console.Write("({0}){1} ", i, (Direction)i);

            return (Direction)Util.GetIntegerInput(0, 3);
        }

        /// <summary>
        /// Get the map tile that lies in the given direction
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private MapTile GetTileInDir(Direction dir)
        {
            var newPosition = m_oCharacter.coordinates + GetDirectionVector(dir);
            return m_oMap.GetTileAt(newPosition);
        }

        // get vector for a given direction
        private Vector2i GetDirectionVector(Direction dir)
        {
            switch(dir) {
                case Direction.North: return new Vector2i(0, 1);
                case Direction.East: return new Vector2i(1, 0);
                case Direction.South: return new Vector2i(0, -1);
                case Direction.West: return new Vector2i(-1, 0);
                default: return new Vector2i(0, 0);
            }

        }

        // set the current feedback message using the error style
        private void SetFeedbackMsgError(string msg)
        {
            SetFeedbackMsg(msg, Constants.eErrorTextColor, Constants.eErrorTextBackgroundColor);
        }

        // set the current feedback message using the warning style
        private void SetFeedbackMsgWarning(string msg)
        {
            SetFeedbackMsg(msg, Constants.eWarningTextColor, Constants.eWarningTextBackgroundColor);
        }

        // set the feedback message using a custom or default style
        private void SetFeedbackMsg(string msg, ConsoleColor foreground = Constants.eDefaultTextColor, ConsoleColor background = Constants.eDefaultTextBackgroundColor)
        {
            m_sFeedbackMsg = msg;
            m_feedbackMsgForeground = foreground;
            m_feedbackMsgBackground = background;
        }


    }
}
