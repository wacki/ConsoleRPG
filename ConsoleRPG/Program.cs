﻿using System;
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
            MessageManager.instance.AddMsg("attack_creature", "The creature swing at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature at you");
            MessageManager.instance.AddMsg("attack_creature", "The creature at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature at you");
            // choose a direction prompt
            MessageManager.instance.AddMsg("choose_direction_prompt", "Choose a direction!");

            // user command invalid messages
            MessageManager.instance.AddMsg("user_command_invalid", "That is not a valid action!");

            // map tile look messages
            MessageManager.instance.AddMsg("map_look_safe", "This area looks pretty safe!");
            MessageManager.instance.AddMsg("map_look_safe", "I don't think I'll be running into any trouble by going there.");

            // quit game prompt messages
            MessageManager.instance.AddMsg("quit_game_prompt", "Are you sure you want to quit the game? (yes, no)");


            // Instantiate and run the game
            Game oGame = new Game();
            oGame.Run();
        }
    }
}
