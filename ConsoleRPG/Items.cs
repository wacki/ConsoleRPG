using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    // Base item class holds most of the important information for items
    class Item
    {
        // Item set names if they are equipable and what bonuses they give
        protected string m_sName;        
        protected bool m_bEquippable;
        protected int m_HealthBonus = 0;
        protected int m_BaseAttackBonus = 0;

        public string sName { get { return m_sName; } }
        public int iHealthBonus { get { return m_HealthBonus; } }
        public int iBaseAttackBonus { get { return m_BaseAttackBonus; } }
        public bool bEquippable { get { return m_bEquippable; } }

        public Item(string name, bool equippable, int healthBonus, int baseAttackBonus)
        {
            m_sName = name;
            m_bEquippable = equippable;
            m_HealthBonus = healthBonus;
            m_BaseAttackBonus = baseAttackBonus;
        }

    }

    //class HealthPotion: Item
    //{
    //    public HealthPotion()
    //    {
    //        m_sName = "Health Potion";
    //        m_bEquippable = false;
    //        m_fHealthBonus = 5;
    //        m_fBaseAttackBonus = 0;
    //    }
    //}

    //class StrenghtPotion : Item
    //{
    //    public StrenghtPotion()
    //    {
    //        m_sName = "Strength Potion";
    //        m_bEquippable = false;
    //        m_fHealthBonus = 0;
    //        m_fBaseAttackBonus = 3;
    //    }
    //}

    //class Sword : Item
    //{
    //    public Sword()
    //    {
    //        m_sName = "Great Sword";
    //        m_bEquippable = false;
    //        m_fHealthBonus = 0;
    //        m_fBaseAttackBonus = 2;
    //    }
    //}

    //class Axe : Item
    //{
    //    public Axe()
    //    {
    //        m_sName = "Battle Axe";
    //        m_bEquippable = false;
    //        m_fHealthBonus = 0;
    //        m_fBaseAttackBonus = 4;
    //    }
    //}
}
