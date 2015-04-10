using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    class GameOverScreen : Menu
    {
        // constructor
        public GameOverScreen()
        {
            prev = Mouse.GetState();
            type = "GameOver";
        }

        // respond to user input
        public override void ProcessInput()
        {
            // get the current Mouse State
            current = Mouse.GetState();
            if (current.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released &&
                current.Position.X >= 290 && current.Position.X <= 510 &&
                current.Position.Y >= 160 && current.Position.Y <= 240)
            {
                type = "Game";
            }

            else if (current.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released &&
                current.Position.X >= 290 && current.Position.X <= 510 &&
                current.Position.Y >= 280 && current.Position.Y <= 380)
            {
                type = "Title";
            }
            current = prev;
        }
    }
}
