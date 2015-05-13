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

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    public class Enemy : Game1
    {
        //attributes
        //public Vector2 position;
        enum MovementState { StraightDown, HardLeft, HardRight, Pattern1, Pattern2, Pattern3, Pattern4, Idle};
        MovementState curmove;
        private Rectangle position;
        private int health;
        private int FireCD;
        private int timer; // for patterns of movement
        private int mode;

        Random RNG = new Random();
        int movementRand;

        //default constructor
        public Enemy(Rectangle pos)
        {
            position = pos;
            //alive = true;
            health = difficulty;
            movementRand = RNG.Next(1, 9);
            switch (movementRand)
            {
                case 1:
                    curmove = MovementState.StraightDown;
                    break;
                case 2:
                    curmove = MovementState.HardLeft;
                    break;
                case 3:
                    curmove = MovementState.HardRight;
                    break;
                case 4:
                    curmove = MovementState.Pattern1;
                    break;
                case 5:
                    curmove = MovementState.Pattern2;
                    break;
                case 6:
                    curmove = MovementState.Pattern3;
                    break;
                case 7:
                    curmove = MovementState.Pattern4;
                    break;
                case 8:
                    curmove = MovementState.Idle;
                    break;
            }
            FireCD = 10; // set a base value. Enemies wont be able to shoot IMMEDIATELY after being spawned
            timer = -1; // -1 means that it has yet to start moving
            mode = RNG.Next(0,2);
        }

        // Properties
        public int HEALTH { get { return health; } }
        public Rectangle POSITION { get { return position; } }
        public int MODE { get { return mode; } }

        //movement
        public void Update()
        {
            // Move the enemy
            if (position.Y < 10)
            {
                position.Y++;
            }

            switch (curmove)
            {
                case MovementState.StraightDown:
                    position.Y++;
                    break;
                case MovementState.HardLeft:
                    
                    position.X--;
                    break;
                case MovementState.HardRight:
                    
                    position.X++;
                    break;
                case MovementState.Pattern1: // In this pattern the enemy swoops to the right. Starting by moving directly down, and ending directly right
                    // decrement the timer
                    if (timer > 0)
                    {
                        timer--;
                    }

                    if (timer < 0) // set the timer up initially
                    {
                        timer = 200;
                    }

                    if (timer > 150)
                    {
                        position.Y += 3;
                    }
                    
                    if(timer > 100 && timer < 150)
                    {
                        position.Y += 3;
                        position.X += 1;
                    }
                    if (timer > 50 && timer < 100)
                    {
                        position.Y += 2;
                        position.X += 2;
                    }
                    if (timer > 0 && timer < 50)
                    {
                        position.Y += 1;
                        position.X += 3;
                    }
                   
                    if (timer  == 0)
                    {
                        position.X += 3;
                    }
                    break;
                case MovementState.Pattern2: // In this pattern the enemy swoops to the left. Starting by moving directly down, and ending directly left
                    // decrement the timer
                    if (timer > 0)
                    {
                        timer--;
                    }

                    if (timer < 0) // set the timer up initially
                    {
                        timer = 200;
                    }

                    if (timer > 150)
                    {
                        position.Y += 3;
                    }

                    if (timer > 100 && timer < 150)
                    {
                        position.Y += 3;
                        position.X -= 1;
                    }
                    if (timer > 50 && timer < 100)
                    {
                        position.Y += 2;
                        position.X -= 2;
                    }
                    if (timer > 0 && timer < 50)
                    {
                        position.Y += 1;
                        position.X -= 3;
                    }

                    if (timer == 0)
                    {
                        position.X -= 3;
                    }
                    break;
                case MovementState.Idle: // a simple back and forth movement
                    // decrement the timer
                   

                    if (timer > 0)
                    {
                        timer--;
                    }

                    if (timer < 0)
                    {
                        timer = 200;
                    }

                    if (timer > 100)
                    {
                        position.X += 1;
                    }

                    if (timer < 100 && timer > 0)
                    {
                        position.X -= 1;
                    }

                    if (timer == 0)
                    {
                        timer = 200;
                    }
                    break;
                case MovementState.Pattern3:
                    {
                        

                        if (timer < 0)
                        {
                            timer = 1;
                        }
                        
                        if (timer == 0)
                        {
                            position.X -= 2;
                            position.Y += 1;
                        }
                        if (timer == 1)
                        {
                            position.X += 2;
                            position.Y += 1;
                        }

                        if (position.X == 750)
                        {
                            timer = 0;
                        }

                        if (position.X == 0)
                        {
                            timer = 1;
                        }
                        break;
                    }

            }

            FireCD--;
            if (FireCD <= 0)
            {
                Fire();
                FireCD = 70;
            }

        }

        // Create a bullet
        private void Fire()
        {
            
            int bulletcounter = 0;

            
            while (true)
            {
                if (BulletsAr[bulletcounter] == null)
                {
                    Bullet b1 = new Bullet(POSITION.X + (POSITION.Width / 2), POSITION.Y + POSITION.Height, 0, bulletcounter, 0, 0, mode);
                    BulletsAr[bulletcounter] = b1;
                    
                    break;
                }
                else
                { bulletcounter++; }
            } 
            
            
        }

        // Decrement health
        public void TakeHit()
        {
            health--;
        }


    }
}
