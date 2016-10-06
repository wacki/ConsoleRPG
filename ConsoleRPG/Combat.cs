using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{

    static class CombatManager
    {
        //adds a random number for use across the program
        static Random rRandomNumber = new Random();
        //When a object (ie Player or Monster) attack another object
        private static void attack()
        {
            MessageManager.instance.PrintRandomMsg("attack_creature");
            int iPlayerRoll = roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterRoll = roll();
            //iPlayerRoll += Character.getAttack;
            //iMonsterRoll += Monster.getAttack;
            damage(iPlayerRoll, iMonsterRoll);
        }
        //determans then damges the loser of the fight
        private static void damage(int iPlayerDamage, int iMonsterDamage)
        {
            int i_totalDamage = iPlayerDamage - iMonsterDamage;
            if(i_totalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_sucsess");
                //Console.WriteLine("You delt {0} dammage to the {1}", i_totalDamage, Monsters.name);
                //Monster.health -= i_totalDamage;
                lifeCheck(true);
            }
            else
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
               i_totalDamage = Math.Abs(i_totalDamage);
                //Console.WriteLine("The {1} delt {0} dammage to you!", i_totalDamage, Monsters.name);
                // Character.health -= i_totalDamage;\
                lifeCheck(false);
            }
        }

        //If the monster is killed will it drop loot?
        private static void dropLoot()
        {
            //to do
        }
        //the roll for the attack
        private static int roll()
        {
            int iRanNumber;
            iRanNumber = rRandomNumber.Next(1, 7);

            return iRanNumber;
        }
        //check if anyone has died
        private static bool lifeCheck(bool bIsMonster)
        {
            return true;
        }
        

        //the player wants to run away
        private static void run()
        {
            Console.WriteLine("You attempt to flee");
            int iPlayerDamage = roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterDamage = roll();
           // iPlayerDamage += Character.getAttack;
          // iMonsterDamage += Monster.getAttack;
            int i_totalDamage = iMonsterDamage - iPlayerDamage;
            if (i_totalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
                i_totalDamage = Math.Abs(i_totalDamage);
                //Console.WriteLine("The {1} delt {0} dammage to you!", i_totalDamage, Monsters.name);
                // Character.health -= i_totalDamage;\
                lifeCheck(false);
            }
            else
            {
                //Console.WriteLine("You escaped the {0} unscaved", Monsters.name);            
            }

        }
        //when the fight occurs
        public static string fight(Character character, MapTile tile)
        {
            Monster monster = tile.GetMonster();

            Console.WriteLine("You have encountered a {0}", monster.name);
            //("What would you like to do?")
            Util.GetInput((string input, out string output) => {
                output = input;

                // temp input handling. This needs to be improved
                switch(input.ToLower()) {
                    case "1": attack(); return true;
                    case "2": run(); return true;
                }

                return false;

            }, MessageManager.instance.GetRandomMsg("user_command_invalid"));

            // if we win and monster is dead do this
            tile.eTileEvent = MapTileEvent.Nothing;
            // else dont change it.

            //In case player wins combat
            return MessageManager.instance.GetRandomMsg("user_combat_victory");

            //In case player flees combat
            return MessageManager.instance.GetRandomMsg("user_combat_flee");
        }
    }
}
