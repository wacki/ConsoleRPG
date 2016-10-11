using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    public class Monster {

        //Define variables for the defining what the monsters are
        string m_sName;
        public string name { get { return m_sName; } }
        int m_iHealth;
        public int health
        {
            get { return m_iHealth; }
            set { m_iHealth = value; }
        }
        int m_iBaseAttack;
        public int baseAttack { get { return m_iBaseAttack; } }


        int m_iMonsterSelector;
        int m_iSpecialMonster;

        //Create a construct that picks a random number that fits into the following string array
        public Monster(MapTile.Type tileType)
        {
            //Define random number generators for picking monsters

            Random RandomMonster = new Random();
            Random SpecialYN = new Random();

            //30% chance of getting a special monster                   
            m_iSpecialMonster = SpecialYN.Next(0, 10);


            //Outlining the possibilities for monsters based on tile location
            switch(tileType) {
                case MapTile.Type.Mountains:

                    //Spawning special monster
                    if(m_iSpecialMonster < 3) {
                        m_iMonsterSelector = 6;
                    }

                    //Spawning standard monster
                    else {
                        m_iMonsterSelector = RandomMonster.Next(0, 3);
                    }

                    break;

                case MapTile.Type.Plains:

                    //Spawning special monster
                    if(m_iSpecialMonster < 3) {
                        m_iMonsterSelector = 7;
                    }
                    //Spawning standard monster
                    else {
                        m_iMonsterSelector = RandomMonster.Next(0, 3);
                    }

                    break;

                case MapTile.Type.Desert:

                    //Spawning special monster
                    if(m_iSpecialMonster < 3) {
                        m_iMonsterSelector = 4;
                    }
                    //Spawning standard monster
                    else {
                        m_iMonsterSelector = RandomMonster.Next(0, 3);
                    }

                    break;

                case MapTile.Type.Wood:

                    //Spawning special monster
                    if(m_iSpecialMonster < 3) {
                        m_iMonsterSelector = 3;
                    }
                    //Spawning standard monster
                    else {
                        m_iMonsterSelector = RandomMonster.Next(0, 3);
                    }

                    break;

                case MapTile.Type.Water:

                    //Spawning special monster
                    if(m_iSpecialMonster < 3) {
                        m_iMonsterSelector = 5;
                    }
                    //Spawning standard monster
                    else {
                        m_iMonsterSelector = RandomMonster.Next(0, 3);
                    }

                    break;
            }

            //Use the random selection from the above array to deliver monster stats
            switch(m_iMonsterSelector) {
                /* Monster types
                 * 3 Standard:
                 * Goblin: Health: 4, Attack: 1
                 * Orc: Health: 8, Attack: 4
                 * Wolf: Health: 6, Attack: 3
                 * 
                 * Mountain: Wyvern: Health: 16, Attack: 6
                 * 
                 * Plains: Centaur: Health 10, Attack: 4
                 * 
                 * Forest: Giant Spider: Health 12, Attack: 4
                 * 
                 * Desert: Lizardfolk: Health: 10, Attack: 5
                 * 
                 * River: Alligator: Health: 14, Attack: 2
                 */

                //Goblin 
                case 0: {
                        m_sName = "Goblin";
                        m_iHealth = 4;
                        m_iBaseAttack = 3;

                        break;
                    }

                //Orc
                case 1: {
                        m_sName = "Orc";
                        m_iHealth = 8;
                        m_iBaseAttack = 6;

                        break;
                    }

                //Wolf
                case 2: {
                        m_sName = "Wolf";
                        m_iHealth = 6;
                        m_iBaseAttack = 5;

                        break;
                    }

                //Giant Spider
                case 3: {
                        m_sName = "Giant Spider";
                        m_iHealth = 12;
                        m_iBaseAttack = 6;

                        break;
                    }

                //Lizardfolk
                case 4: {
                        m_sName = "Lizardfolk";
                        m_iHealth = 10;
                        m_iBaseAttack = 7;

                        break;
                    }

                //Alligator
                case 5: {
                        m_sName = "Alligator";
                        m_iHealth = 14;
                        m_iBaseAttack = 4;

                        break;
                    }

                //Wyvern
                case 6: {
                        m_sName = "Wyvern";
                        m_iHealth = 16;
                        m_iBaseAttack = 8;

                        break;
                    }

                //Centaur
                case 7: {
                        m_sName = "Centaur";
                        m_iHealth = 10;
                        m_iBaseAttack = 6;

                        break;
                    }
            }
        }

        //Construct to determine what monster player encounters
        public void Identify()
        {
            string[] MonsterNames = new string[6];
            MonsterNames[0] = "Goblin";
            MonsterNames[1] = "Orc";
            MonsterNames[2] = "Wolf";
            MonsterNames[3] = "Giant Spider";
            MonsterNames[4] = "Lizardfolk";
            MonsterNames[5] = "Alligator";
            MonsterNames[6] = "Wyvern";
            MonsterNames[7] = "Centaur";

            //State to the player what monster they are fighting
            Console.WriteLine("You are fighting a {0}", MonsterNames[m_iMonsterSelector]);

        }
    }
}
