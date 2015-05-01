using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    public class Player
    {
        // Attributes
        private string playerName;
        private int LivesLeft;

        // Properties
        public String PLAYERNAME { get { return playerName; } }

        public int LIVESLEFT { get { return LivesLeft; } set { LivesLeft = value; } }

        // Constructor
        public Player(string name)
        {
            LivesLeft = 3;
            playerName = name;
        }

        // return true if the game is over and the player is dead
        public bool LostGame()
        {
            // Returns true if you're dead, returns false if you're not
            return LivesLeft <= 0;
        }

        // Decrement the LivesLeft Variable when you get hit
        public void LoseLife()
        {
            // Decrement
            LivesLeft--;
        }
    }
}
