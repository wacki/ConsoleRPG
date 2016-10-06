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

            // map tile move messages
            MessageManager.instance.AddMsg("map_move_forest_safe", "You enter a forest area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_plain_safe", "You enter a grassy area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_mountain_safe", "You enter a mountain area, nothing is here.");
            MessageManager.instance.AddMsg("map_move_water_safe", "You jump into a river, nothing is here, but it's wet!");


            // quit game prompt messages
            MessageManager.instance.AddMsg("quit_game_prompt", "Are you sure you want to quit the game? (yes, no)");

            //Generic Monster Messages
            MessageManager.instance.AddMsg("normal_monster_encounter", "You are ambushed and attacked from behind!");
            MessageManager.instance.AddMsg("normal_monster_encounter", "You jump backwards as a creature emerges from the bushes!");
            MessageManager.instance.AddMsg("normal_monster_encounter", "You feel your skin crawl and turn around to find a monster about to attack you!");

            //Goblin Messages
            MessageManager.instance.AddMsg("goblin_monster_encounter", "You hear the unmistakable cackle of a goblin before it leaps out to fight you!");
            MessageManager.instance.AddMsg("goblin_monster_encounter", "You feel a tugging on your bag, and turn around to find a goblin trying to steal your stuff!");

            //Orc Messages
            MessageManager.instance.AddMsg("orc_monster_encounter", "A paralyzing war cry cuts through the air as you spin around to see an orc charging right at you!");
            MessageManager.instance.AddMsg("orc_monster_encounter", "You come across an orc who's set up camp, he doesn't seem too happy that you have disturbed his meal time!");

            //Wolf Messages
            MessageManager.instance.AddMsg("wolf_monster_encounter", "A snarl freezes you in your steps as a lone wolf emerges from hiding.");
            MessageManager.instance.AddMsg("wolf_monster_encounter", "Growling draws your gaze to the side and you are amazed you missed the wolf standing right next to the trail!");

            //Giant Spider Messages
            MessageManager.instance.AddMsg("spider_monster_encounter", "A clacking sound in the trees draws your gaze up, to find a giant spider lowering itself on top of you!");
            MessageManager.instance.AddMsg("spider_monster_encounter", "Walking between two trees, you get caught in a giant web! You cut yourself free just as a giant spider comes to check it's trap.");

            //Lizardfolk Messages
            MessageManager.instance.AddMsg("lizard_monster_encounter", "A hissing sound is all the warning you get as a Lizardfolk jumps out of the sands and jumps at you!");
            MessageManager.instance.AddMsg("lizard_monster_encounter", "As you walk across the sands, the ground suddenly gives way underneath you, landing you in a Lizardfolk hide-away!");

            //Alligator Messages
            MessageManager.instance.AddMsg("alligator_monster_encounter", "As you walk along the water edge, an explosion of water and movement launches itself at you from the water!");
            MessageManager.instance.AddMsg("alligator_monster_encounter", "An alligator is resting on the trail you're taking. It doesn't look like it wants to move and motions aggressively at you.");
            // start message
            MessageManager.instance.AddMsg("start_game_msg", "You wake up in an unknown location.");

            // item messages
            MessageManager.instance.AddMsg("item_pickup", "Congratulation !!! You just picked {0} item");
            MessageManager.instance.AddMsg("item_pickup", "Destiny has given you this {0} item");
            MessageManager.instance.AddMsg("item_pickup", "Use this {0} item wisely as this can be your biggest reward");
            MessageManager.instance.AddMsg("item_pickup", "Luck has showered its {0} item upon you who is the choosen one");
            MessageManager.instance.AddMsg("item_pickup", "This {0} item can surely help you through your tough journey");

            MessageManager.instance.AddMsg("item_use", "Enhance your capability by using this legendary {0} item");
            MessageManager.instance.AddMsg("item_use", "Have no regrets after using this {0} item");
            MessageManager.instance.AddMsg("item_use", "If you just use then later you might loose, but if you wisely use then enemies might loose");


            MessageManager.instance.AddMsg("item_equip", "You just equipped the legendary {0} item");
            MessageManager.instance.AddMsg("item_use", "God answered your prayers and thus has given you this {0} item to equip");
            MessageManager.instance.AddMsg("item_use", "Hard Work always pays & your {0} item always stays");

            // Instantiate and run the game
            Game oGame = new Game();
            oGame.Run();
        }
    }
}
