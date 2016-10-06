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

        // private Inventory m_oInventory;
        // private EquippableItem m_oEquippedItem;

        public Character()
        {
            // todo

            // for now just spawn him in the middle of the map
            m_oCoordinates = new Vector2i(Constants.iMapSizeX / 2, Constants.iMapSizeY / 2);
        }

        public void Move(Vector2i dir)
        {
            m_oCoordinates += dir;
        }

    }
}
