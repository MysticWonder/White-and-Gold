using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    class TitleMenu : Menu
    {
        

        // constructor
        public TitleMenu()
        {
            prev = Mouse.GetState();
            type = "Title";



        }

        // properties
        public MouseState Prev
        {
            get { return prev; }
        }

        public MouseState Current
        {
            get { return current; }
        }

        public override void ProcessInput()
        {
            current = Mouse.GetState();
            if (current.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released &&
                current.Position.X >= 290 && current.Position.X <= 510 && current.Position.Y >= 160 && current.Position.Y <= 240)
            {
                // start the game
                type = "Game";

            }

            else if (current.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released &&
                current.Position.X >= 290 && current.Position.X <= 510 && current.Position.Y >= 280 && current.Position.Y <= 360)
            {
                //open external tool
                

            }
            //make the current state the previous state
            current = prev;
        }


    }
}
