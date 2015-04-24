#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ship1;
        Texture2D ship2;
        SpriteFont menuFont;
        KeyboardState keystate;
        MouseState mousestate;
        Ship s1;
        public static int mouseLocX, mouseLocY;
        int modebuffer = 0;
        int KeyDirection = 0; // 1 up, 2 right, 3 down 4 left
        // game menu
        Menu menu;
        // menu and background textures
        Texture2D titlescreen;
        Texture2D pausescreen;
        Texture2D gameoverscreen;
        Texture2D background;
        // rectangles for background graphics
        Rectangle backRect1;
        Rectangle backRect2;

        // bullet attributes
        public static Bullet[] BulletsAr = new Bullet[200];
        int bulletcounter = 0;
        int testcounter = 0;
        int BulletBuffer = 0;
        public static Texture2D BulletE;
        public static Texture2D BulletA;
        public static Texture2D BulletG;
        public static Texture2D Explosion;
        public static Texture2D Gernade;

        // enemy attributes
        //random # generator
        Random RNG = new Random();
        Texture2D enemy01;
        //list of enemies
        public static List<Enemy> enemies = new List<Enemy>();

        // Variables for collisions
        Player p1 = new Player("P1");


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            // initialize a TitleMenu object
            menu = new TitleMenu();


            ship1 = Content.Load<Texture2D>("Ship1");
            ship2 = Content.Load<Texture2D>("Ship2");

            // initialize background rectangles
            backRect1 = new Rectangle(0, 0, 800, 600);
            backRect2 = new Rectangle(0, 600, 800, 600);

            // initialize players and ships
            s1 = new Ship(50, 50, ship1);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titlescreen = Content.Load<Texture2D>("menuStart");
            pausescreen = Content.Load<Texture2D>("menuPause");
            gameoverscreen = Content.Load<Texture2D>("menuGameOver");
            background = Content.Load<Texture2D>("backgroundScroll");
            BulletA = Content.Load<Texture2D>("bulleta");
            BulletE = Content.Load<Texture2D>("bullete");
            enemy01 = Content.Load<Texture2D>("enemy01");
            BulletG = Content.Load<Texture2D>("bulletg");
            Explosion = Content.Load<Texture2D>("bestexplosion");
            Gernade = Content.Load<Texture2D>("Gernade");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //---------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------Draw BG Methods-------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------

            //check input to determine the change in screens
            menu.ProcessInput();

            // enable mouse visibility on all menus besides the game screen
            if (menu.Type != "Game")
            {
                this.IsMouseVisible = true;
            }

            // check bounds of background rectangles
            if (backRect1.Y - background.Height >= 0)
            {
                backRect1.Y = backRect2.Y - background.Height;
            }

            else if (backRect2.Y - background.Height >= 0)
            {
                backRect2.Y = backRect1.Y - background.Height;
            }

            // scroll the background
            backRect1.Y += 5;
            backRect2.Y += 5;



            //---------------------------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------Input Methods---------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------

            //if the screen is the game, allow ship to move on screen
            if (menu.Type == "Game")
            {
                this.IsMouseVisible = false;

                // get keystate
                keystate = Keyboard.GetState();
                mousestate = Mouse.GetState();
                mouseLocX = mousestate.X;
                mouseLocY = mousestate.Y;
                //s1.Move();

                //// process movement
                if (keystate.IsKeyDown(Keys.A))
                {
                    KeyDirection = 4;
                    s1.Move(KeyDirection);

                }
                if (keystate.IsKeyDown(Keys.D))
                {
                    KeyDirection = 2;
                    s1.Move(KeyDirection);
                }
                if (keystate.IsKeyDown(Keys.W))
                {
                    KeyDirection = 1;
                    s1.Move(KeyDirection);
                }
                if (keystate.IsKeyDown(Keys.S))
                {
                    KeyDirection = 3;
                    s1.Move(KeyDirection);
                }

                
                // process fire
                if (mousestate.LeftButton == ButtonState.Pressed)
                {
                    // If the bullet buffer is 0 then create a bullet
                    // Otherwise, you recently shot a bullet so firing should be on cooldown
                    if (BulletBuffer == 0)
                    {
                        BulletBuffer = 20;
                        AddBullet(1, 0, 0, 0);
                    }
                }

                if (mousestate.RightButton == ButtonState.Pressed)
                {
                    // If the bullet buffer is 0 then create a bullet
                    // Otherwise, you recently shot a bullet so firing should be on cooldown
                    if (BulletBuffer == 0)
                    {
                        BulletBuffer = 20;
                        AddBullet(1, 0, 1, 100);
                    }
                }

                if (keystate.IsKeyDown(Keys.T))
                {
                    AddBullet(50, 50, 0, 0, 0, 0);
                }
                if (keystate.IsKeyDown(Keys.E))
                {
                    if (BulletBuffer == 0)
                    {
                        BulletBuffer = 5;
                        enemies.Add(new Enemy(new Rectangle(300, -50, 50, 50)));
                    }
                }



                //---------------------------------------------------------------------------------------------------------------------------
                //--------------------------------------------------Movement Methods---------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------------

                // For every bullet in the BulletsAr array, we need to update thier position (cause they're moving)
                foreach (Bullet var in BulletsAr)
                {
                    if (var != null)
                    {
                        var.BulletMove();

                        if (var.BULLETLOCATION.Y < 0 || var.BULLETLOCATION.Y > 1000)
                        {
                            RemoveBullet(var.ID);
                        }
                    }
                }

                //Update Enemy Movement
                foreach (Enemy e in enemies)
                {
                    e.Update();
                }
               

                //enemy despawn at bottom
                if (enemies.Count != 0)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (e.HEALTH <= 0)
                        {
                            enemies.Remove(e);
                            break;
                        }

                        if (e.POSITION.Y > 1000)
                        {
                            enemies.Remove(e);
                            break;
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------------
                //--------------------------------------------------Collision Methods--------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------------


                // for each bullet, check the collision to see if an enemy is hit or a player is hit
                foreach (Bullet var in BulletsAr)
                {
                    if (var != null)
                    {
                        if (var.Alligience == 0)
                        {
                            if (var.BULLETLOCATION.Intersects(s1.SHIPLOCATION))
                            {
                                RemoveBullet(var.ID);
                                p1.LoseLife();

                                if (p1.LostGame())
                                {
                                    menu.Type = "GameOver";
                                }
                            }
                        }

                        if (var.Alligience == 1)
                        {
                            foreach (Enemy evar in enemies)
                            {
                                if (var.BULLETLOCATION.Intersects(evar.POSITION))
                                {
                                    switch (var.BULSTYLE)
                                    {
                                        case 1:
                                            var.FUSE = 0;
                                            evar.TakeHit();
                                            evar.TakeHit();
                                            evar.TakeHit();
                                            break;
                                        case 2:
                                            evar.TakeHit();
                                            evar.TakeHit();
                                            evar.TakeHit();
                                            break;
                                        default: 
                                            RemoveBullet(var.ID);
                                            evar.TakeHit();
                                            break;
                                    }

                                }
                            }
                        }
                    }
                }
               

            }

            //---------------------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------Buffer Methods---------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------


            // Decrement the BulletBuffer variable so there's a cooldown on how fast you can fire
            if (BulletBuffer > 0)
            {
                BulletBuffer--;
            }

            // handle mode switching
            // check for mode switching
            if (modebuffer == 0)
            {
                modebuffer = 5;
                s1.ModeSwitch();

            }
            if (modebuffer > 0)
            {
                modebuffer--;
            }


            base.Update(gameTime);
        }

//---------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------END UPDATE METHOD--------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //determine which menu is drawn on the screen
            switch (menu.Type)
            {
                case "Title":
                    menu = new TitleMenu();
                    spriteBatch.Draw(titlescreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    break;
                case "Game":
                    menu = new GameScreen();
                    // Draw the backgrounds
                    spriteBatch.Draw(background, backRect1, Color.White);
                    spriteBatch.Draw(background, backRect2, Color.White);

                    // Draw the ship
                    if (s1.MODE == 0)
                    {
                        spriteBatch.Draw(ship1, s1.SHIPLOCATION, Color.White);
                    }
                    else if (s1.MODE == 1)
                    {
                        spriteBatch.Draw(ship2, s1.SHIPLOCATION, Color.White);
                    }


                    // For every bullet in the array that actually exists (isn't null), draw it
                    foreach (Bullet val in BulletsAr)
                    {
                        if (val != null)
                        {
                            val.BulletDraw(val, spriteBatch);
                        }
                    }

                    //draw enemy01
                    foreach (Enemy e in enemies)
                    {
                        spriteBatch.Draw(enemy01, e.POSITION, Color.White);
                    }


                    break;
                case "Pause":
                    menu = new PauseMenu();
                    spriteBatch.Draw(pausescreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    break;
                case "GameOver":
                    menu = new GameOverScreen();
                    spriteBatch.Draw(gameoverscreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    break;
            }




            spriteBatch.End();
            base.Draw(gameTime);
        }

//---------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------Bullet Methods---------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------

        // Adds the bullet to the array
        public virtual void AddBullet(int alligence, int color, int style, int fuse)
        {
            bulletcounter = 0;
            while (true)
            {
                if (BulletsAr[bulletcounter] == null)
                {

                    BulletsAr[bulletcounter] = new Bullet(s1.SHIPLOCATION.X + (s1.SHIPLOCATION.Width / 2) - 10, s1.SHIPLOCATION.Y, alligence, bulletcounter, style, fuse, color);
                    break;
                }
                else
                { bulletcounter++; }
            }
        }

        public virtual void AddBullet(int x, int y, int alligience, int color, int style, int fuse)
        {
            bulletcounter = 0;
            while (true)
            {
                if (BulletsAr[bulletcounter] == null)
                {

                    BulletsAr[bulletcounter] = new Bullet(x, y, alligience, bulletcounter, style, fuse, color);
                    break;
                }
                else
                { bulletcounter++; }
            }
        }

        // Removes the bullet with the specific id from the array
        // This opens up the BulletsAr[id] location to be filled by another slot
        public void RemoveBullet(int id)
        {
            BulletsAr[id] = null;
        }

    }

    public static class Vars
    {
        public static int screenWidth;
        public static int screenHeight;
    }

}
