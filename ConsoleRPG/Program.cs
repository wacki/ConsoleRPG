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
            MessageManager.instance.AddMsg("creature_attack", "The creature takes a swing at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature dashes at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature lunges at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature slashes at you");
            MessageManager.instance.AddMsg("creature_attack", "The creature tries to bite you");

            //Player attack Failure
            MessageManager.instance.AddMsg("player_attack_failure", "Your attack bounced off and the monster bites at you!");
            MessageManager.instance.AddMsg("player_attack_failure", "Your attack missed and the mosnter slashes at you!");
            MessageManager.instance.AddMsg("player_attack_failure", "The monster dodges your attack and hits you back!");
            //Player attack Sucsess
            MessageManager.instance.AddMsg("player_attack_sucsess", "Your attack hits deep, leaving a deep gouge");
            MessageManager.instance.AddMsg("player_attack_sucsess", "Your attack grazes the monster");
            MessageManager.instance.AddMsg("player_attack_sucsess", "You manage a slash across the mosnter's face!");

            //Player Combat Victory
            MessageManager.instance.AddMsg("user_combat_victory", "Victorious, you move on from the fight to continue exploring.");
            MessageManager.instance.AddMsg("user_combat_victory", "You finish off the monster, and continue on your path.");
            MessageManager.instance.AddMsg("user_combat_victory", "The fight is over, and you decide to push ahead.");

            //Player Combat Flee
            MessageManager.instance.AddMsg("user_combat_flee", "You barely escape the beast, and catch your breath before pressing on.");
            MessageManager.instance.AddMsg("user_combat_flee", "You manage to hide yourself, and hear the monster pass you by, you're safe for now.");
            MessageManager.instance.AddMsg("user_combat_flee", "You turn tail and run from the monster. You manage to lose it, and decide to continue.");

            // choose a direction prompt
            MessageManager.instance.AddMsg("choose_direction_prompt", "Choose a direction!");

            // user command invalid messages
            MessageManager.instance.AddMsg("user_command_invalid", "That is not a valid action!");

            // map tile look messages
            MessageManager.instance.AddMsg("map_look_safe", "The area ahead looks pretty safe!");
            MessageManager.instance.AddMsg("map_look_safe", "You don't think you'll encounter any danger by going there.");
            MessageManager.instance.AddMsg("map_look_safe", "You don't notice anything dangerous that way");
            MessageManager.instance.AddMsg("map_look_safe", "Nothing in that direction seems dangerous to you");
            MessageManager.instance.AddMsg("map_look_safe", "This {0} looks pretty safe!");
            MessageManager.instance.AddMsg("map_look_safe", "You don't think you'll be running into any trouble by going to that {0}.");

            MessageManager.instance.AddMsg("map_look_safe", "This {0} looks pretty safe!");


            MessageManager.instance.AddMsg("map_look_safe", "It looks like the {0} over there is pretty safe.");
            MessageManager.instance.AddMsg("map_look_combat", "You don't like the look of that {0} over there.");
            MessageManager.instance.AddMsg("map_look_treasure", "You notice something interesting in that {0}, what is that?");            

            // map tile move messages
            //MessageManager.instance.AddMsg("map_move_forest_safe", "You enter a forest area, nothing is here.");
            //MessageManager.instance.AddMsg("map_move_plain_safe", "You enter a grassy area, nothing is here.");
            //MessageManager.instance.AddMsg("map_move_mountain_safe", "You enter a mountain area, nothing is here.");
            //MessageManager.instance.AddMsg("map_move_water_safe", "You jump into a river, nothing is here, but it's wet!");
            //MessageManager.instance.AddMsg("map_move_forest_safe", "As you walk through the dark woods, all is quiet.");
            //MessageManager.instance.AddMsg("map_move_forest_safe", "The trees tower over you as you cross the forest, but encounter nothing.");
            //MessageManager.instance.AddMsg("map_move_plain_safe", "You cross a clear plains area, it's quite beautiful.");
            //MessageManager.instance.AddMsg("map_move_plain_safe", "The rolling hills of this plain hinder you, but you encounter no danger.");
            //MessageManager.instance.AddMsg("map_move_mountain_safe", "As you scale the rocky terrain, nothing bothers you.");
            //MessageManager.instance.AddMsg("map_move_mountain_safe", "On your climb up the mountain you encuonter a cave, thankfully it's occupant was not present.");
            //MessageManager.instance.AddMsg("map_move_water_safe", "You ford a small river, nothing is here, but you're all soaked!");
            //MessageManager.instance.AddMsg("map_move_water_safe", "This pond doesn't seem to have any monsters in it, and you take a moment to splash some cooling water on your face.");
            MessageManager.instance.AddMsg("map_move_safe", "You enter a {0}, nothing is here.");
            MessageManager.instance.AddMsg("map_move_combat", "You enter a {0}, and are attacked.");
            MessageManager.instance.AddMsg("map_move_treasure", "You enter a {0} and find something valulable!");


            MessageManager.instance.AddMsg("move_inacessible_area", "Something is telling you not to continue that direction, and you decide to pick a new direction");

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

            //Wyvern Messages
            MessageManager.instance.AddMsg("wyvern_monster_encounter", "Climbing the mountain, you hear some wings flapping. You look over your shoulder to see a wyvern swooping towards you!");
            MessageManager.instance.AddMsg("wyvern_monster_encounter", "As you crest a ridge, you look up to look directly into a wyvern's cave! Even more shocking, there's a wyvern inside!");

            //Centaur Messages
            MessageManager.instance.AddMsg("centaur_monster_encounter", "Walking across the plains, you hear a low rumbling, and you dodge to the side as a centaur charges by you, swinging an axe!");
            MessageManager.instance.AddMsg("centaur_monster_encounter", "You hear a voice call out to you across the plains, as you turn an arrow whizzes past your face! A centaur is shooting at you!");

            // start message
            MessageManager.instance.AddMsg("start_game_msg", "You wake up in an unknown location. You decide to explore the area to see what you can learn");

            // item messages
            MessageManager.instance.AddMsg("item_pickup", "Congratulations! You just picked {0} item");
            MessageManager.instance.AddMsg("item_pickup", "Destiny has given you this {0} item");
            MessageManager.instance.AddMsg("item_pickup", "Use this {0} wisely as such finds may be scarce.");
            MessageManager.instance.AddMsg("item_pickup", "Luck has shown you favour, and you manage to find a {0}");
            MessageManager.instance.AddMsg("item_pickup", "This {0} can surely help you through your tough journey");

            MessageManager.instance.AddMsg("item_use", "You decide it's time to use the {0}, and feel a new strength flow through you.");
            MessageManager.instance.AddMsg("item_use", "As you use your {0}, you know it was the right choice to do so.");
            MessageManager.instance.AddMsg("item_use", "Better to use your item now before you lose your chance to do so.");


            MessageManager.instance.AddMsg("item_equip", "You have equipped the {0}, and feel assured by it's weight.");
            MessageManager.instance.AddMsg("item_equip", "As you equip your {0}, its presence fills you with new confidence.");
            MessageManager.instance.AddMsg("item_equip", "You take up your {0}, treasuring it's find and looking forward to it's use.");

            // Instantiate and run the game
            Game oGame = new Game();
            oGame.Run();
        }
    }
}
