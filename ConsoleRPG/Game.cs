using System;

namespace ConsoleRPG {
    class Game {

        private bool m_bRunning;
        private bool m_bRestart;
        private Map m_oMap;
        private Character m_oCharacter;

        private ConsoleColor m_feedbackMsgForeground;
        private ConsoleColor m_feedbackMsgBackground;
        private string m_sFeedbackMsg = "";


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

        private void Run()
        {
            m_sFeedbackMsg = MessageManager.instance.GetRandomMsg("start_game_msg");

            m_bRunning = true;
            while(m_bRunning) {
                Console.Clear();

                // 1. display map
                int playerX = m_oCharacter.coordinates.x;
                int playerY = m_oCharacter.coordinates.y;
                //m_oMap.Print(playerX, playerY);
                m_oMap.Draw(playerX, playerY);

                // 2. display feedback from previous action           
                PrintFeedbackMsg();

                // 3. handle user input?
                PrintInputOptions();
                HandleUserInput();
            }

            if(m_bRestart)
                Start();
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

                // we allow for commands with arguments, so we split the string
                char[] splitter = { ' ' };
                string[] inputSplit = input.Split(splitter);

                input = input.Trim().ToLower();
                if(inputSplit.Length < 1)
                    return false;

                string command = inputSplit[0];
                string payload = input.Substring(command.Length).Trim();

                Console.WriteLine("COMMAND: " + command + " PAYLOAD: " + payload);

                // temp input handling. This needs to be improved
                switch(command.ToLower()) {
                    case "quit": Stop(true); return true;
                    case "look": Look(payload); return true;
                    case "move": Move(payload); return true;
                    case "inventory": ShowInventory(); return true;
                    case "status": Status(); return true;
                    case "use": Use(payload); return true;
                }

                return false;

            }, MessageManager.instance.GetRandomMsg("user_command_invalid"));


        }

        #region user commands

        private void PrintInputOptions()
        {
            Console.WriteLine("What would you like to do? \nlook | move | equip | use | status | quit");
        }


        /// <summary>
        /// Called when player uses the look command
        /// </summary>
        private void Look(string direction)
        {
            // todo: duplicated code in the move command. move this to a function
            Direction dir;
            switch(direction) {
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
                    SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("invalid_look_command"));
                    return;
            }


            var tile = GetTileInDir(dir);

            if(tile != null)
                SetFeedbackMsg(tile.GetLookMessage());
            else
                SetFeedbackMsg("TODO: NOTHING THERE MESSAGE (map border)");
        }

        /// <summary>
        /// Called when player uses the move command
        /// </summary>
        private void Move(string direction)
        {
            Direction dir;
            switch(direction) {
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

            var tile = GetTileInDir(dir);
            var dirVec = GetDirectionVector(dir);

            if(tile != null) {
                m_oCharacter.Move(dirVec);

                m_sFeedbackMsg = tile.GetMoveMessage();

                switch(tile.GetEventType()) {
                    case MapTileEvent.Nothing: break;
                    case MapTileEvent.Combat: Combat(tile); break;
                    case MapTileEvent.Treasure: break;
                }
            }
            else
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
        private void Use(string itemString)
        {
            if(!m_oCharacter.hasItems) {
                SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("no_items_to_use"));
                return;
            }


            int chosenItem;

            if(!int.TryParse(itemString, out chosenItem)) {
                SetFeedbackMsgError(MessageManager.instance.GetRandomMsg("you_dont_have_that_item"));
            }
            
            m_sFeedbackMsg = m_oCharacter.Use(chosenItem);            
        }

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
            m_sFeedbackMsg = CombatManager.fight(m_oCharacter, tile);

            DeathCheck();
            WinCheck();
        }

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

        private void DeathCheck()
        {
            if(m_oCharacter.health <= 0) {
                Console.WriteLine("You were slain on your journey.");
                m_bRestart = Util.YesNoPrompt();

                Stop(false);
            }
        }

        private void WinCheck()
        {
            if(m_oCharacter.gold >= Constants.iGoldAmountToWin) {
                Console.WriteLine("You win the game. Wanna play again?");
                m_bRestart = Util.YesNoPrompt();

                Stop(false);
            }
        }

        // directions prompt
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


        private void SetFeedbackMsgError(string msg)
        {
            SetFeedbackMsg(msg, Constants.eErrorTextColor, Constants.eErrorTextBackgroundColor);
        }

        private void SetFeedbackMsgWarning(string msg)
        {
            SetFeedbackMsg(msg, Constants.eWarningTextColor, Constants.eWarningTextBackgroundColor);
        }

        private void SetFeedbackMsg(string msg, ConsoleColor foreground = Constants.eDefaultTextColor, ConsoleColor background = Constants.eDefaultTextBackgroundColor)
        {
            m_sFeedbackMsg = msg;
            m_feedbackMsgForeground = foreground;
            m_feedbackMsgBackground = background;
        }


    }
}
