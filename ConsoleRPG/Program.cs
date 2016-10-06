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

            //setup attack messages
            //Player attack
            MessageManager.instance.AddMsg("attack_creature","You swing at the creature");
            MessageManager.instance.AddMsg("attack_creature", "You dash at the creature");
            MessageManager.instance.AddMsg("attack_creature", "You lunge at the creature");
            MessageManager.instance.AddMsg("attack_creature", "You slash at the creature");
            //Monster attack
            MessageManager.instance.AddMsg("attack_creature", "The creature took a swing at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature dashed at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature lunged at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature slashed at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature tried to bite you");

            //Player attack Failure
            MessageManager.instance.AddMsg("player_attack_failure", "Your attack bounced off");
            MessageManager.instance.AddMsg("player_attack_failure", "Your attack missed");
            MessageManager.instance.AddMsg("player_attack_failure", "It dogged you attack!");
            //Player attack Sucsess
            MessageManager.instance.AddMsg("player_attack_sucsess", "Your attack hit deep");
            MessageManager.instance.AddMsg("player_attack_sucsess", "Your attack grased it");
            MessageManager.instance.AddMsg("player_attack_sucsess", "It took your attact to the face!");
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
