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
        private static bool attack(Character character, Monster monster)
        {
            MessageManager.instance.PrintRandomMsg("attack_creature");
            int iPlayerRoll = roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterRoll = roll();
            iPlayerRoll += character.baseAttack;
            iMonsterRoll += monster.baseAttack;
            return damage(iPlayerRoll, iMonsterRoll, character, monster);
            
        }
        //determans then damges the loser of the fight
        private static bool damage(int iPlayerDamage, int iMonsterDamage, Character character, Monster monster)
        {
            int i_totalDamage = iPlayerDamage - iMonsterDamage;
            if(i_totalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_sucsess");
                Console.WriteLine("You delt {0} dammage to the {1}", i_totalDamage, monster.name);
                monster.health -= i_totalDamage;
               return IsAlive(true, character, monster);
            }
            else
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
               i_totalDamage = Math.Abs(i_totalDamage);
                Console.WriteLine("The {1} delt {0} dammage to you!", i_totalDamage, monster.name);
               character.health -= i_totalDamage;
               return IsAlive(false, character, monster);
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
        private static bool IsAlive(bool bIsMonster, Character character, Monster monster)
        {
            if(bIsMonster == true)
            {
                if (monster.health > 0)
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if (character.health > 0)
                {
                    return true;
                }
                else { return false; }
            }
        }
        

        //the player wants to run away
        private static bool run(Character character, Monster monster)
        {
            Console.WriteLine("You attempt to flee");
            int iPlayerDamage = roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterDamage = roll();
            iPlayerDamage += character.baseAttack;
            iMonsterDamage += monster.baseAttack;
            int i_totalDamage = iMonsterDamage - iPlayerDamage;
            if (i_totalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
                i_totalDamage = Math.Abs(i_totalDamage);
                Console.WriteLine("The {1} delt {0} dammage to you! But", i_totalDamage, monster.name);
                character.health -= i_totalDamage;
               IsAlive(false, character, monster);
                return true;
            }
            else
            {
                Console.WriteLine("You escaped the {0} unscaved and", monster.name);
                return true;       
            }

        }
        //when the fight occurs
        public static string fight(Character character, MapTile tile)
        {
            Monster monster = tile.GetMonster();
            bool bCombatActive = true;
            Console.WriteLine("You have encountered a {0}", monster.name);
            //("What would you like to do?")
            Console.WriteLine("<1>Attack <2>Run away");
            while (bCombatActive == true)
            {
                Util.GetInput((string input, out string output) =>
                {
                    output = input;

                    // temp input handling. This needs to be improved
                    switch (input.ToLower())
                    {
                        case "1": bCombatActive = attack(character, monster); return true;
                        case "2": bCombatActive = run(character, monster); return true;
                    }

                    return false;

                }, MessageManager.instance.GetRandomMsg("user_command_invalid"));
            }

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
