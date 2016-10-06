using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Item
    {
        protected string name = "Item Name";        
        protected bool equipableornonequipable = true;
        protected float healthbonus = 0f;
        protected float baseattackbonus = 0f;
    }

    class HealthPotion: Item
    {
        public HealthPotion()
        {
            name = "Health Potion";
            equipableornonequipable = false;
            healthbonus = 5;
            baseattackbonus = 0;
        }
    }

    class StrenghtPotion : Item
    {
        public StrenghtPotion()
        {
            name = "Strenght Potion";
            equipableornonequipable = false;
            healthbonus = 0;
            baseattackbonus = 3;
        }
    }

    class Sword : Item
    {
        public Sword()
        {
            name = "Sword";
            equipableornonequipable = false;
            healthbonus = 0;
            baseattackbonus = 1;
        }
    }

    class Axe : Item
    {
        public Axe()
        {
            name = "Axe";
            equipableornonequipable = false;
            healthbonus = 5;
            baseattackbonus = 2;
        }
    }
}
