using System;
using System.Collections.Generic;
using System.Drawing;
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
            int iPlayerRoll = roll() + roll();
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
        private static int dropLoot()
        {
            int i_RanNumber = rRandomNumber.Next(100, 500);
            Console.WriteLine("You found {0} gold on the monsters corpse! continue?", i_RanNumber);
            Console.ReadLine();
            return i_RanNumber;
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
                return false;
            }
            else
            {
                Console.WriteLine("You escaped the {0} unscaved and", monster.name);
                return false;       
            }

        }
        //when the fight occurs
        public static string fight(Character character, MapTile tile)
        {
            Monster monster = tile.GetMonster();
            bool bCombatActive = true;
            
            
            //("What would you like to do?")
           
            while (bCombatActive == true)
            {
                Console.SetCursorPosition(0,0);
                Draw(tile);
                Console.SetCursorPosition(0, 54);
                Console.WriteLine("You have encountered a {0}", monster.name);
                Console.SetCursorPosition(0, 55);
                Util.ClearCurrentConsoleLine();
                Console.SetCursorPosition(0, 55);
                BattleMenu(character, monster);
                Console.WriteLine("Attack | Run away");
                
                Util.GetInput((string input, out string output) =>
                {
                    Console.SetCursorPosition(0, 58);
                    Util.ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, 59);
                    output = input;
                    

                    // temp input handling. This needs to be improved
                    switch (input.ToLower())
                    {
                        
                        case "attack": bCombatActive = attack(character, monster); return true;
                        case "run": bCombatActive = run(character, monster); return true;
                            
                    }

                    return false;

                }, MessageManager.instance.GetRandomMsg("user_command_invalid"));
            }

            if (monster.health > 0)
            {
                Console.ReadLine();
                return MessageManager.instance.GetRandomMsg("user_combat_flee");
            }
            else
            {
                Console.SetCursorPosition(0, 55);
                Util.ClearCurrentConsoleLine();
                Console.SetCursorPosition(0, 55);
                BattleMenu(character, monster);
                Console.SetCursorPosition(0, 63);
                // if we win and monster is dead do this
                tile.eTileEvent = MapTileEvent.Nothing;
                MessageManager.instance.GetRandomMsg("user_combat_victory");
               character.gold += dropLoot();
                //In case player wins combat
                return MessageManager.instance.GetRandomMsg("user_combat_victory");
            }
            
        }

        public static void BattleMenu(Character character, Monster monster)
        {
            
            string s_PlayerHealth = "";
            string s_MonstorHealth = "";
            int i_Monster = monster.health;
            for (int i = 0; i < 1; i++)
            {
                
                Util.ConsoleWriteCol(ConsoleColor.Yellow, " ");
                for (i = 0; i < character.health; i++)
                {
                    
                    Util.ConsoleWriteCol(ConsoleColor.Red,ConsoleColor.Red, " ");
                }
                s_PlayerHealth = character.health.ToString();
                Util.ConsoleWriteCol(ConsoleColor.Yellow, s_PlayerHealth);
                 int space = (34 - i) + (i_Monster - monster.health);
                for (i = 0; i < space; i++)
                {
                    Console.Write(" ");
                }
                s_MonstorHealth = monster.health.ToString();
                Util.ConsoleWriteCol(ConsoleColor.Yellow, s_MonstorHealth);
                for (i = 0; i < monster.health; i++)
                {

                    Util.ConsoleWriteCol(ConsoleColor.Red, ConsoleColor.Red, " ");
                }
                Console.WriteLine();
                
            }
        }

        private static void Draw(MapTile battleZone)
        {
            

            var windowHandle = Util.GetConsoleHandle();
            using (var graphics = Graphics.FromHwnd(windowHandle))

                for (int y = 0; y < Constants.iMapSizeY - 1; y++)
                {
                    Console.WriteLine();
                    for (int x = 0; x < Constants.iMapSizeX; x++)
                    {

                        graphics.DrawImage(battleZone.image, x * 32, y * 32, 32, 32);

                    }
                }
            Console.SetCursorPosition(0, 55);
        }
    }
}
