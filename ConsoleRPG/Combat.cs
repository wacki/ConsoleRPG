using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    
    class Combat
    {
        //adds a random number for use across the program
        static Random rRandomNumber = new Random();
        //When a object (ie Player or Monster) attack another object
        private void attack()
        {
            MessageManager.instance.PrintRandomMsg("attack_creature");
            int iPlayerRoll = roll();
            MessageManager.instance.PrintRandomMsg("creature_attack");
            int iMonsterRoll = roll();
            //iPlayerRoll += Character.getAttack;
            //iMonsterRoll += Monster.getAttack;
            damage(iPlayerRoll, iMonsterRoll);
        }
        //damges the loser of the fight
        private void damage(int iPlayerDamage, int iMonsterDamage)
        {
           
        }

        //If the monster is killed will it drop loot?
        private void dropLoot()
        {

        }
        //the roll for the attack
        private int roll()
        {
            int iRanNumber;
            iRanNumber = rRandomNumber.Next(1, 7);

            return iRanNumber;
        }
        //check if anyone has died
        private void lifeCheck()
        {

        }

        //the player wants to run away
        private void run()
        {

        }
        //when the fight occurs
        public void fight()
        {

        }
    }
}
