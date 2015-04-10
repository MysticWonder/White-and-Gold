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
    class GameScreen:Menu
    {
        public KeyboardState keystate;
        

        public GameScreen()
        {
            type = "Game";
        }

        
        //check if player has paused the game
        public override void ProcessInput()
        {
            keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Enter))
            {
                type = "Pause";
            }

        }
    }
}
