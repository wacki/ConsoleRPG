using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    /// <summary>
    /// Holds all relevant information for the player character.
    /// Stats, inventory, etc.
    /// </summary>
    class Character {
        // player location on the map
        private Vector2i m_oCoordinates; 
        public Vector2i coordinates { get { return m_oCoordinates; } }
        
        // player health 
        int m_iHealth = Constants.iPlayerStartHealth;
        public int iHealth
        {
            get { return m_iHealth; }
            set { m_iHealth = value; }
        }

        // player base attack
        private int m_fBaseAttack = 1;
        public int iBaseAttack
        {
            get { return m_fBaseAttack; }
            set { m_fBaseAttack = value; }
        }

        // player gold
        private int m_fGold;
        public int iGold
        {
            get { return m_fGold; }
            set { m_fGold = value; }
        }

        // does the player have any items in his inventory
        public bool hasItems
        {
            get { return m_oInventory.Count > 0; }
        }
        // amount of items in the inventory at the current time
        public int numItems
        {
            get { return m_oInventory.Count; }
        }

        // private Inventory m_oInventory;
        // private EquippableItem m_oEquippedItem;
        private List<Item> m_oInventory;

        // Constructor, also handles player spawn and starting inventory
        public Character()
        {
            //gives the player 5 health potions
            m_oInventory = new List<Item>();
            for(int i = 0; i < 5; i++)
                m_oInventory.Add(new Item("Health Potion", false, 2, 0));


            // spawn player in the middle of the map
            m_oCoordinates = new Vector2i(Constants.iMapSizeX / 2, Constants.iMapSizeY / 2);
        }

        // Move the player by 'dir' amount
        public void Move(Vector2i dir)
        {
            m_oCoordinates += dir;
        }

        // Add an item to the player
        public void AddItem(Item item)
        {
            m_oInventory.Add(item);
        }

        // returns string containing all of the items in the character's inventory
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

        // Use the item at itemIndex in the inventory
        public string Use(int itemIndex)
        {
            var item = m_oInventory[itemIndex];
            
            // useing health potions
            m_iHealth += item.iHealthBonus;
            m_oInventory.Remove(item);

            
            return MessageManager.instance.GetRandomMsg("character_used_item", item.sName);
        }

        // return a string containing the current player health
        public string GetStatusString()
        {
            string str = "You have " + iHealth + "health";
            return str;
        }
    }
}
