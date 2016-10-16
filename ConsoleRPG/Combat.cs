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
        static Random s_RandomNumber = new Random();
        //When a object (ie Player or Monster) attack another object
        private static bool Attack(Character character, Monster monster)
        {
            MessageManager.instance.PrintRandomMsg("attack_creature");
            int iPlayerRoll = Roll() + Roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterRoll = Roll();
            iPlayerRoll += character.iBaseAttack;
            iMonsterRoll += monster.iBaseAttack;
            return Damage(iPlayerRoll, iMonsterRoll, character, monster);

        }
        //determans then damges the loser of the fight
        private static bool Damage(int iPlayerDamage, int iMonsterDamage, Character character, Monster monster)
        {
            int iTotalDamage = iPlayerDamage - iMonsterDamage;
            if (iTotalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_sucsess");
                Console.WriteLine("You delt {0} dammage to the {1}", iTotalDamage, monster.sName);
                monster.iHealth -= iTotalDamage;
                return IsAlive(true, character, monster);
            }
            else
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
                iTotalDamage = Math.Abs(iTotalDamage);
                Console.WriteLine("The {1} delt {0} dammage to you!", iTotalDamage, monster.sName);
                character.iHealth -= iTotalDamage;
                return IsAlive(false, character, monster);
            }
        }

        //If the monster is killed will it drop loot?
        private static int DropLoot()
        {
            int iRanNumber = s_RandomNumber.Next(100, 500);
            Console.WriteLine("You found {0} gold on the monsters corpse! continue?", iRanNumber);
            Console.ReadLine();
            return iRanNumber;
        }
        //the roll for the attack
        private static int Roll()
        {
            int iRanNumber;
            iRanNumber = s_RandomNumber.Next(1, 7);

            return iRanNumber;
        }
        //check if anyone has died
        private static bool IsAlive(bool bIsMonster, Character character, Monster monster)
        {
            if (bIsMonster == true)
            {
                if (monster.iHealth > 0)
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if (character.iHealth > 0)
                {
                    return true;
                }
                else { return false; }
            }
        }


        //the player wants to run away
        private static bool Run(Character character, Monster monster)
        {
            Console.WriteLine("You attempt to flee");
            int iPlayerDamage = Roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterDamage = Roll();
            iPlayerDamage += character.iBaseAttack;
            iMonsterDamage += monster.iBaseAttack;
            int iTotalDamage = iMonsterDamage - iPlayerDamage;
            if (iTotalDamage > 0)
            {
                MessageManager.instance.PrintRandomMsg("player_attack_failure");
                iTotalDamage = Math.Abs(iTotalDamage);
                Console.WriteLine("The {1} delt {0} dammage to you! But", iTotalDamage, monster.sName);
                character.iHealth -= iTotalDamage;
                IsAlive(false, character, monster);
                return false;
            }
            else
            {
                Console.WriteLine("You escaped the {0} unscaved and", monster.sName);
                return false;
            }

        }
        //when the fight occurs
        public static string Fight(Character character, MapTile tile)
        {
            Monster monster = tile.GetMonster();
            bool bCombatActive = true;


            //("What would you like to do?")

            while (bCombatActive == true)
            {
                //sets Cursor at top
                Console.SetCursorPosition(0, 0);
                //draws the background and fight
                DrawBackground(tile);
                DrawFight(monster);

                //sets the console to beyond the battle screen and print the combat inputs
                Console.SetCursorPosition(0, 54);
                Console.WriteLine("You have encountered a {0}", monster.sName);
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


                    // Imput handeling for combat
                    switch (input.ToLower())
                    {

                        case "attack": bCombatActive = Attack(character, monster); return true;
                        case "run": bCombatActive = Run(character, monster); return true;

                    }

                    return false;

                }, MessageManager.instance.GetRandomMsg("user_command_invalid"));
            }
            //if the player leaves combat and is still alive have them flee
            if (monster.iHealth > 0)
            {
                Console.ReadLine();
                return MessageManager.instance.GetRandomMsg("user_combat_flee");
            }
            else
            {
                //sets the position of the Cusor inorder to correct the Battle menu after killing the monster
                Console.SetCursorPosition(0, 55);
                Util.ClearCurrentConsoleLine();
                Console.SetCursorPosition(0, 55);
                BattleMenu(character, monster);
                //changes to after the attack and information to print victory
                Console.SetCursorPosition(0, 63);
                // if we win and monster is dead do this
                tile.eTileEvent = MapTileEvent.Nothing;
                MessageManager.instance.GetRandomMsg("user_combat_victory");
                character.iGold += DropLoot();
                //In case player wins combat
                return MessageManager.instance.GetRandomMsg("user_combat_victory");
            }

        }
//Controls the Health screen of player and the Monster during the fight
        public static void BattleMenu(Character character, Monster monster)
        {

            string sPlayerHealth = "";
            string sMonstorHealth = "";
            int iMonster = monster.iHealth;
            for (int i = 0; i < 1; i++)
            {

                Util.ConsoleWriteCol(ConsoleColor.Yellow, " ");
                for (i = 0; i < character.iHealth; i++)
                {

                    Util.ConsoleWriteCol(ConsoleColor.Red, ConsoleColor.Red, " ");
                }
                sPlayerHealth = character.iHealth.ToString();
                Util.ConsoleWriteCol(ConsoleColor.Yellow, sPlayerHealth);
                int space = (34 - i) + (iMonster - monster.iHealth);
                for (i = 0; i < space; i++)
                {
                    Console.Write(" ");
                }
                sMonstorHealth = monster.iHealth.ToString();
                Util.ConsoleWriteCol(ConsoleColor.Yellow, sMonstorHealth);
                for (i = 0; i < monster.iHealth; i++)
                {

                    Util.ConsoleWriteCol(ConsoleColor.Red, ConsoleColor.Red, " ");
                }
                Console.WriteLine();

            }
        }
// Draws the Backgrounds for the combat
        private static void DrawBackground(MapTile battleZone)
        {

            Console.SetCursorPosition(0, 0);
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

        }
//Draws the Player vs the Selected monster 
        private static void DrawFight(Monster monster)
        {
            Console.SetCursorPosition(0, 0);
            var windowHandle = Util.GetConsoleHandle();
            using (var graphics = Graphics.FromHwnd(windowHandle))
                graphics.DrawImage(monster.image, 0, 0, 720, 720);



            Console.SetCursorPosition(0, 55);
        }
    }
}
