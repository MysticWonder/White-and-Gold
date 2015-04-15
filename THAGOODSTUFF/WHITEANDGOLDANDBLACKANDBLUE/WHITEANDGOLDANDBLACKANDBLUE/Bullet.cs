using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    class Bullet : Game1
    {
        private int PorE; // 0 is enemy, 1 is player
        private int xpos;
        private int ypos;
        private Rectangle imagelocation;
        private int idnum;
        private int fuse;
        private int bulstyle;
        private Game1 g1 = new Game1();
        private int color;
        SpriteBatch Spritebatch;
        Texture2D boomboom;


        public Rectangle BULLETLOCATION { get { return imagelocation; } }
        public int ID { get { return idnum; } }

        public int Alligience { get { return PorE; } }

        public Bullet(int x, int y, int alligience, int id, int style, int bfuse, int color)
        {
            xpos = x;
            ypos = y;
            PorE = alligience;
            idnum = id;
            bulstyle = style;
            fuse = bfuse;
            imagelocation = new Rectangle(x, y, 20, 20);

        }



        public void BulletMove()
        {
            if (bulstyle == 0)
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
            if (bulstyle == 1)
            {
                if (PorE == 1)
                {
                    if (fuse > 50)
                    {
                        imagelocation.Y -= 2;
                    }
                    else
                    {
                        imagelocation.X -= 2;
                    }

                    fuse--;

                    if (fuse <= 0)
                    {
                        
                        imagelocation.Y = -10;
                    }
                }
            }
            
        }


    }
}
