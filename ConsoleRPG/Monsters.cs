using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    public class Monsters
    {
        string m_sName;
        int m_iHealth;
        int m_iAttackBase;

        public Monsters() 
            {

            }

        public void Identify()
        {
            string[] MonsterNames = new string[6];
            MonsterNames[0] = "Goblin";
            MonsterNames[1] = "Orc";
            MonsterNames[2] = "Wolf";
            MonsterNames[3] = "Giant Spider";
            MonsterNames[4] = "Lizardfolk";
            MonsterNames[5] = "Alligator";

            Console.WriteLine("You are fighting a {0}", MonsterNames[0]);
        }

        //Goblin stats

    }
}
