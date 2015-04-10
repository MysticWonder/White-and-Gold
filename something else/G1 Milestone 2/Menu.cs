using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Changes Made by:
// Angie Davern

namespace T1_1
{
    //represents a general menu
    abstract class Menu
    {
        protected string type;
        protected MouseState prev;
        protected MouseState current;

        public Menu()
        {

        }

        //returns string to indicate what type of menu shuld be drawn
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        //determines how user input changes the game state, to be overridden in child classes
        public virtual void ProcessInput()
        {

        }

    }
}
