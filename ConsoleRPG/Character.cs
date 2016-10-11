using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    class Character {
        private Vector2i m_oCoordinates; 
        public Vector2i coordinates { get { return m_oCoordinates; } }
        
        int m_iHealth = Constants.iPlayerStartHealth;
        public int health
        {
            get { return m_iHealth; }
            set { m_iHealth = value; }
        }
        private int m_fBaseAttack = 1;
        public int baseAttack
        {
            get { return m_fBaseAttack; }
            set { m_fBaseAttack = value; }
        }
        private int m_fGold;
        public int gold
        {
            get { return m_fBaseAttack; }
            set { m_fBaseAttack = value; }
        }
        public bool hasItems
        {
            get { return m_oInventory.Count > 0; }
        }
        public int numItems
        {
            get { return m_oInventory.Count; }
        }

        // private Inventory m_oInventory;
        // private EquippableItem m_oEquippedItem;
        private List<Item> m_oInventory;


        public Character()
        {
            // todo
            m_oInventory = new List<Item>();
            for(int i = 0; i < 5; i++)
                m_oInventory.Add(new Item("Health Potion", false, 2, 0));


            // for now just spawn him in the middle of the map
            m_oCoordinates = new Vector2i(Constants.iMapSizeX / 2, Constants.iMapSizeY / 2);
        }

        public void Move(Vector2i dir)
        {
            m_oCoordinates += dir;
        }

        public void AddItem(Item item)
        {
            m_oInventory.Add(item);
        }

        public void PrintItemList()
        {
            for(int i = 0; i < m_oInventory.Count; i++) {
                var item = m_oInventory[i];
                Console.Write("(" + i + ") " + item.name + " | ");
            }
            Console.WriteLine();
        }

        public void Use(int itemIndex)
        {
            var item = m_oInventory[itemIndex];

            // todo: sanity check
            MessageManager.instance.PrintRandomMsg("character_used_item", item.name);
            m_iHealth += item.healthBonus;
        }

        public string GetStatusString()
        {
            return "TODO: STATUS STRING";
        }
    }
}
