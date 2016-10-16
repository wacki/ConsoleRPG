using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    class Character {
        //player location on the map
        private Vector2i m_oCoordinates; 
        public Vector2i coordinates { get { return m_oCoordinates; } }
        
        //player health 
        int m_iHealth = Constants.iPlayerStartHealth;
        public int iHealth
        {
            get { return m_iHealth; }
            set { m_iHealth = value; }
        }

        //player base attack
        private int m_fBaseAttack = 1;
        public int iBaseAttack
        {
            get { return m_fBaseAttack; }
            set { m_fBaseAttack = value; }
        }

        //plauyer gold
        private int m_fGold;
        public int iGold
        {
            get { return m_fGold; }
            set { m_fGold = value; }
        }

        //player has any inventory
        public bool hasItems
        {
            get { return m_oInventory.Count > 0; }
        }
        //player inventory amount
        public int numItems
        {
            get { return m_oInventory.Count; }
        }

        // private Inventory m_oInventory;
        // private EquippableItem m_oEquippedItem;
        private List<Item> m_oInventory;

        //player spawn and starting inventory
        public Character()
        {
            //gives the player 5 health potions
            m_oInventory = new List<Item>();
            for(int i = 0; i < 5; i++)
                m_oInventory.Add(new Item("Health Potion", false, 2, 0));


            // spawn player in the middle of the map
            m_oCoordinates = new Vector2i(Constants.iMapSizeX / 2, Constants.iMapSizeY / 2);
        }

        //player movement
        public void Move(Vector2i dir)
        {
            m_oCoordinates += dir;
        }

        //player reciveing and item
        public void AddItem(Item item)
        {
            m_oInventory.Add(item);
        }

        //checks inventory and notifies the player what it contains
        public string GetItemListString()
        {
            string result = "";
            for(int i = 0; i < m_oInventory.Count; i++) {
                var item = m_oInventory[i];
                result += "(" + i + ") " + item.sName + " | ";
            }
            result += "\n";
            return result;
        }

        //player useing an item
        public string Use(int itemIndex)
        {
            var item = m_oInventory[itemIndex];
            
            // useing health potions
            m_iHealth += item.iHealthBonus;
            m_oInventory.Remove(item);

            
            return MessageManager.instance.GetRandomMsg("character_used_item", item.sName);
        }
        //gives player their curent health
        public string GetStatusString()
        {
            string str = "You have " + iHealth + "health";
            return str;
        }
    }
}
