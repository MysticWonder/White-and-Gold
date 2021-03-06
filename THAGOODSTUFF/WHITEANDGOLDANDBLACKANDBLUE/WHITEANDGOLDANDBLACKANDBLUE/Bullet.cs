﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    public class Bullet : Game1
    {
        // Attributes
        private int PorE; // 0 is enemy, 1 is player
        private int xpos;
        private int ypos;
        private Rectangle imagelocation;
        private int idnum;
        private int fuse;
        private int bulstyle;
        private int color; // 0 blue, 1 white

        // Properties
        public Rectangle BULLETLOCATION { get { return imagelocation; } }
        public int ID { get { return idnum; } }
        public int FUSE { get { return fuse; } set { fuse = value; } }
        public int BULSTYLE { get { return bulstyle; } }
        public int COLOR { get { return color; } }
        public int Alligience { get { return PorE; } }

        // Constructor
        public Bullet(int x, int y, int alligience, int id, int style, int bfuse, int bcolor)
        {
            xpos = x;
            ypos = y;
            PorE = alligience;
            idnum = id;
            bulstyle = style;
            fuse = bfuse;

            // Make the allied bullets ovel shaped, make the enemy bullets circular
            if (alligience == 1)
            {
                if (style == 1)
                {

                    imagelocation = new Rectangle(x, y, 20, 20);
                }
                else
                imagelocation = new Rectangle(x, y, 10, 30);
            }
            else
            {
                imagelocation = new Rectangle(x, y, 20, 20);
            }
            color = bcolor;

        }

        // Draw every single bullet
       public void BulletDraw(Bullet b1, SpriteBatch batch)
        {
            switch (bulstyle)
            {
                case 0:
                    if (Alligience == 1)
                    {
                        if (color == 0) // blue
                        {

                            batch.Draw(BulletA0, BULLETLOCATION, Color.White);
                        }
                        else // white
                        {

                            batch.Draw(BulletA1, BULLETLOCATION, Color.White);
                        }
                    }
                    else
                    {
                        if (color == 0) // blue
                        {

                            batch.Draw(BulletE0, BULLETLOCATION, Color.White);
                        }
                        else // white
                        {

                            batch.Draw(BulletE1, BULLETLOCATION, Color.White);
                        }
                    }
                break;
                case 1: // Gernade
                    if (Alligience == 1)
                    {
                        batch.Draw(Gernade, BULLETLOCATION, Color.LightGreen);
                    }
                    else
                    {
                        batch.Draw(Gernade, BULLETLOCATION, Color.Red);
                    }
                break;
                case 2:
                    batch.Draw(Explosion, new Rectangle(imagelocation.X - 50,imagelocation.Y - 50, 100,100), Color.LightGreen);

                break;

            }

            
        }

        // Depending on the style of the bullet
        // move the bullet
        public void BulletMove()
        {
            if (bulstyle == 0) // The default bullet
            {
                if (PorE == 0)
                {
                    imagelocation.Y += 5;
                }
                if (PorE == 1)
                {
                    imagelocation.Y -= 5;
                }
            }
            if (bulstyle == 1) // Gernade P1
            {
                if (PorE == 1)
                {
                    
                    imagelocation.Y -= 2;
                    

                    fuse--;

                    if (fuse <= 0)
                    {
                        fuse = 30;
                        bulstyle = 2;
                    }
                }
            }
            if (bulstyle == 2) // Geernade P2
            {
                fuse--;
                if (fuse <= 0)
                {
                    imagelocation.Y = -10;
                }
            }
            
        }


    }
}
