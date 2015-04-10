using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

// Changes Made by:
// Phillip Fowler

namespace T1_1
{
    class Enemy
    {
        //attributes
        //public Vector2 position;
        private Rectangle position;
        private Boolean alive;
        private int health;

        //default constructor
        public Enemy(Rectangle pos)
        {
            position = pos;
            alive = true;
            health = 3;
        }

        // Properties
        public int HEALTH { get { return health; } }
        public Rectangle POSITION { get { return position; } }


        //movement
        public void Move()
        {
            position.Y++;
        }

        public void TakeHit()
        {
            health--;
        }
        
    }
}
