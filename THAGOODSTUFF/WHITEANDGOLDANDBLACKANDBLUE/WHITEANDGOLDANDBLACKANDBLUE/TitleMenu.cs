using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    class TitleMenu : Menu
    {
        //how we set difficulty
        public string diffFileContents;
        public StreamReader diffFileReader = null;


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
                //set difficulty
                diffFileReader = new StreamReader("gameDifficulty.txt");
                diffFileContents = Console.ReadLine();
                switch (diffFileContents)
                {
                    case "Easy":
                        Game1.Difficulty = 1;
                        break;
                    case "Medium":
                        Game1.Difficulty = 2;
                        break;
                    case "Hard":
                        Game1.Difficulty = 3;
                        break;
                }
                diffFileContents = null;
                diffFileReader.Close();
                
                // start the game
                type = "Game";



            }

            else if (current.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released &&
                current.Position.X >= 290 && current.Position.X <= 510 && current.Position.Y >= 280 && current.Position.Y <= 360)
            {
                //open external tool and change difficulty
                AdjustGame changeDiff = new AdjustGame();
                changeDiff.ShowDialog();
                

            }
            //make the current state the previous state
            current = prev;
        }


    }
}
