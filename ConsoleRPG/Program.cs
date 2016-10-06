using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {

    /* Todo:
     *   1. Check variables for correct hungarian notation. Ugh...
     * 
     */



    class Program {
        static void Main(string[] args)
        {
            // Init message manager singleton
            MessageManager.Init();

            // Set up necessary messages
            MessageManager.instance.AddMsg("test", "this is a test message.");
            MessageManager.instance.PrintRandomMsg("test");

            // choose a direction prompt
            MessageManager.instance.AddMsg("choose_direction_prompt", "Choose a direction!");

            // user command invalid messages
            MessageManager.instance.AddMsg("user_command_invalid", "That is not a valid action!");

            // map tile look messages
            MessageManager.instance.AddMsg("map_look_safe", "This area looks pretty safe!");
            MessageManager.instance.AddMsg("map_look_safe", "I don't think I'll be running into any trouble by going there.");

            // map tile move messages
            MessageManager.instance.AddMsg("map_move_forest_safe", "You enter a forest area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_plain_safe", "You enter a grassy area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_mountain_safe", "You enter a mountain area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_water_safe", "You jump into a river, nothing is here, but it's wet!");


            // quit game prompt messages
            MessageManager.instance.AddMsg("quit_game_prompt", "Are you sure you want to quit the game? (yes, no)");

            // start message
            MessageManager.instance.AddMsg("start_game_msg", "You wake up in an unknown location.");

            // Instantiate and run the game
            Game oGame = new Game();
            oGame.Run();
        }
    }
}
