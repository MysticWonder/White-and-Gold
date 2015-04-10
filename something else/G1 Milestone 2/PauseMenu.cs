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
    class PauseMenu:Menu
    {
        
        //set menu type
        public PauseMenu()
        {
            prev = Mouse.GetState();
            type = "Pause";
            
        }

        //changes game state after checking user input
        public override void ProcessInput()
        {
            //retrieve current mouse state
            current = Mouse.GetState();

            //compare current MouseState to previous and check if a button has been clicked
            if(current.LeftButton==ButtonState.Pressed&&prev.LeftButton==ButtonState.Released&&
                current.Position.X>=290&&current.Position.X<=510&&
                current.Position.Y>=160&&current.Position.Y<=240)
            {
                // returns to game screen
                type = "Game";
            }

            else if(current.LeftButton==ButtonState.Pressed&&prev.LeftButton==ButtonState.Released&&
                current.Position.X>=290&&current.Position.X<=510&&current.Position.Y>=280&&current.Position.Y<=360)
            {
                //returns to title screen
                type = "Title";
            }

            //make the current state the previous state
            prev=current;
        }
    }
}
