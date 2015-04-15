﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    public class Ship
    {
        private Player player;
        private Bullet bullet;
        private int Mode = 0; // Default Mode, 1 is other mode
        private Rectangle shiplocation;
        private KeyboardState prevstate;
        private KeyboardState currentstate;

        public int X { get; set; }
        public int Y { get; set; }

        // Properties
        public Player PLAYER { get { return player; } }
        public int MODE { get { return Mode; } set { Mode = value; } }
        public Rectangle SHIPLOCATION { get { return shiplocation; } }

        public Ship(int x, int y, Texture2D image)
        {
            shiplocation = new Rectangle(x, y, image.Width / 5, image.Height / 5);
        }

        public void Move()
        {
            this.shiplocation.X = WHITEANDGOLDANDBLACKANDBLUE.Game1.mouseLocX;
            this.shiplocation.Y = WHITEANDGOLDANDBLACKANDBLUE.Game1.mouseLocY;
            //switch (direction)
            //{
            //    case 0:
            //        // do nothing
            //        break;
            //    case 1:
            //        if ((shiplocation.Y - 3) > 0) { shiplocation.Y = shiplocation.Y - 3; }
            //        break;
            //    case 2:
            //        if ((shiplocation.X + 3) < 750) { shiplocation.X = shiplocation.X + 3; }
            //        break;
            //    case 3:
            //        if ((shiplocation.Y + 3) < 430) { shiplocation.Y = shiplocation.Y + 3; }
            //        break;
            //    case 4:
            //        if ((shiplocation.X - 3) > 0) { shiplocation.X = shiplocation.X - 3; }
            //        break;
            //}

        }

        public void Fire()
        {

        }
        public void ModeSwitch()
        {
            currentstate = Keyboard.GetState();
            if (Mode == 0 && prevstate.IsKeyDown(Keys.Space) == false && currentstate.IsKeyDown(Keys.Space) == true)
            {
                Mode = 1;

            }
            else if (Mode == 1 && prevstate.IsKeyDown(Keys.Space) == false && currentstate.IsKeyDown(Keys.Space) == true)
            {
                Mode = 0;

            }

            prevstate = currentstate;
        }
    }
}
