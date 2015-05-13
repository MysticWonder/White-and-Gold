#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using System.IO;
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
        SpriteFont gameFont;
        Texture2D ship1;
        Texture2D ship2;
        KeyboardState keystate;
        MouseState mousestate;
        Stopwatch watch;
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
        Texture2D scrollin;
        // rectangles for background graphics
        Rectangle backRect1;
        Rectangle backRect2;

        // bullet attributes
        public static Bullet[] BulletsAr = new Bullet[200];
        int bulletcounter = 0;
        int BulletBuffer = 0;
        public static Texture2D BulletE1;
        public static Texture2D BulletA1;
        public static Texture2D BulletE0;
        public static Texture2D BulletA0;
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

        //Difficulty attributes and prop
        public static int difficulty = 0;
        public static int Difficulty { get { return difficulty; } set { difficulty = value; } }

        // score attributes
        int prevscore;
        int scoreadd = 1;
        Scores scoreRead;
        int highScore;

        //sound!!
        public static SoundEffect pew, muzak;
        public static SoundEffectInstance pewInstance, muzakInstance;
        public static bool muzakStarted;


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


            ship1 = Content.Load<Texture2D>("Ship1.5");
            ship2 = Content.Load<Texture2D>("Ship2.5");

            // initialize background rectangles
            backRect1 = new Rectangle(0, 0, 800, 700);
            backRect2 = new Rectangle(0, 700, 800, 700);

            // set resolutions
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.ApplyChanges();

            // allow scaling to be applied to other classes if needed (ex. ship boundaries)
            Vars.screenHeight = graphics.PreferredBackBufferHeight;
            Vars.screenWidth = graphics.PreferredBackBufferWidth;

                        // initialize players and ships
            s1 = new Ship((Vars.screenWidth / 2), ((Vars.screenHeight * 9) / 10), ship1);

            // intialize the watch
            watch = new Stopwatch();

            // initialize the scorekeeping object
            scoreRead = new Scores();

            //muzak hasnt started
            muzakStarted = false;
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
            titlescreen = Content.Load<Texture2D>("menuTitle");
            pausescreen = Content.Load<Texture2D>("menuPaused");
            gameoverscreen = Content.Load<Texture2D>("menuGameOver");
            background = Content.Load<Texture2D>("SPAAAAACE");
            scrollin = Content.Load<Texture2D>("SPACE2");
            BulletA0 = Content.Load<Texture2D>("PBulletBlue");
            BulletA1 = Content.Load<Texture2D>("PBulletWhite");
            BulletE0 = Content.Load<Texture2D>("EBulletBlack");
            BulletE1 = Content.Load<Texture2D>("EBulletWhite");
            enemy01 = Content.Load<Texture2D>("NME2");
            BulletG = Content.Load<Texture2D>("BLET");
            Explosion = Content.Load<Texture2D>("bestexplosion");
            Gernade = Content.Load<Texture2D>("Gernade");
            gameFont = Content.Load<SpriteFont>("Font1");
            muzak = Content.Load<SoundEffect>("Muzak");
            pew = Content.Load<SoundEffect>("Pew2");
            
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

            //Muzak
            if (muzakStarted == false)
            {
                muzakInstance = muzak.CreateInstance();
                muzakInstance.IsLooped = true;
                muzakInstance.Play();
                muzakStarted = true;
            }
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

            // start the game time
            watch.Start();

            // increase score
            p1.SCORE += scoreadd * 5;



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
                        if (s1.MODE == 1)
                        {
                            pewInstance = pew.CreateInstance();
                            pewInstance.Volume = .5f;
                            pewInstance.Pitch = -.5f;
                            pewInstance.Play();
                            AddBullet(1, 1, 0, 0);
                        }
                        if (s1.MODE == 0)
                        {
                            pewInstance = pew.CreateInstance();
                            pewInstance.Volume = .5f;
                            pewInstance.Pitch = -.5f;
                            pewInstance.Play();
                            AddBullet(1, 0, 0, 0);
                        }
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
                for (int i = 0; i < BulletsAr.Length; i++)
                {
                    if (BulletsAr[i] != null)
                    {
                        BulletsAr[i].BulletMove();

                        if (BulletsAr[i].BULLETLOCATION.Y < 0 || BulletsAr[i].BULLETLOCATION.Y > 1000)
                        {
                            RemoveBullet(BulletsAr[i].ID);
                        }
                    }
                }

                //Update Enemy Movement
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Update();
                }
               

                //enemy despawn at bottom
                if (enemies.Count != 0)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].HEALTH <= 0)
                        {
                            enemies.Remove(enemies[i]);
                            continue;
                        }

                        if (enemies[i].POSITION.Y > 1000)
                        {
                            enemies.Remove(enemies[i]);
                            continue;
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------------
                //--------------------------------------------------Collision Methods--------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------------


                // for each bullet, check the collision to see if an enemy is hit or a player is hit
                if (BulletsAr.Length != 0)
                {
                    for (int i = 0; i < BulletsAr.Length; i++)
                    {
                        if (BulletsAr[i] != null)
                        {
                            if (BulletsAr[i].Alligience == 0)
                            {
                                if (BulletsAr[i].BULLETLOCATION.Intersects(s1.SHIPLOCATION) && BulletsAr[i].COLOR != s1.MODE)
                                {
                                    RemoveBullet(BulletsAr[i].ID);
                                    p1.LoseLife();

                                    if (p1.LostGame())
                                    {
                                        menu.Type = "GameOver";
                                        enemies.Clear();
                                        for (int b = 0; b < BulletsAr.Length; b++)
                                        {
                                            if (BulletsAr[b] != null)
                                            {
                                                RemoveBullet(BulletsAr[b].ID);
                                            }
                                        }
                                        p1.LIVESLEFT = 3;
                                        watch.Reset();
                                        highScore = scoreRead.WriteScore(p1.SCORE);
                                        prevscore = p1.SCORE;
                                        p1.SCORE = 0;
                                        s1.X = Vars.screenWidth / 2;
                                        s1.Y = (Vars.screenHeight * 9) / 10;
                                    }
                                }
                            }
                            if (BulletsAr[i] != null)
                            {
                                if (BulletsAr[i].Alligience == 1)
                                {
                                    for (int e = 0; e < enemies.Count; e++)
                                    {
                                        if (enemies[e] != null && enemies[e].POSITION != null && BulletsAr[i] != null)
                                        {
                                            if (BulletsAr[i].BULLETLOCATION.Intersects(enemies[e].POSITION) && BulletsAr[i].COLOR == enemies[e].MODE)
                                            {
                                                switch (BulletsAr[i].BULSTYLE)
                                                {
                                                    case 1:
                                                        BulletsAr[i].FUSE = 0;
                                                        enemies[e].TakeHit();
                                                        enemies[e].TakeHit();
                                                        enemies[e].TakeHit();
                                                        break;
                                                    case 2:
                                                        enemies[e].TakeHit();
                                                        enemies[e].TakeHit();
                                                        enemies[e].TakeHit();
                                                        break;
                                                    default:
                                                        RemoveBullet(BulletsAr[i].ID);
                                                        enemies[e].TakeHit();
                                                        p1.SCORE += 1000;
                                                        break;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //END LOOP

                foreach (Enemy e in enemies.ToArray())
                {
                    if (e.POSITION.Intersects(s1.SHIPLOCATION))
                    {
                        e.TakeHit();
                        e.TakeHit();
                        e.TakeHit();
                        p1.LoseLife();
                        if (p1.LostGame())
                        {
                            menu.Type = "GameOver";
                            enemies.Clear();
                            for (int i = 0; i < BulletsAr.Length; i++)
                            {
                                if (BulletsAr[i] != null)
                                {
                                    RemoveBullet(BulletsAr[i].ID);
                                }
                            }
                            p1.LIVESLEFT = 3;
                            watch.Reset();
                            highScore = scoreRead.WriteScore(p1.SCORE);
                            prevscore = p1.SCORE;
                            p1.SCORE = 0;
                            s1.X = Vars.screenWidth / 2;
                            s1.Y = (Vars.screenHeight * 9) / 10;
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
            //---------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------Enemy Spawn---------------------------------------------------------------
            //---------------------------------------------------------------------------------------------------------------------------
            int spawnEnemyRoll = RNG.Next(1,60);
            if (spawnEnemyRoll <= 2)
            {
                if (menu.Type == "Game")
                {
                    enemies.Add(new Enemy(new Rectangle(RNG.Next(1, 600), -50, 75, 75)));
                }              
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
            GraphicsDevice.Clear(Color.Black);

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
                    spriteBatch.Draw(scrollin, backRect2, Color.White);

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
                        Color enemyMode;
                        if (e.MODE == 0)
                        {
                            enemyMode = Color.White;
                        }
                        else { enemyMode = Color.Red; }

                        spriteBatch.Draw(enemy01, e.POSITION, enemyMode);
                    }

                    spriteBatch.DrawString(gameFont, "Lives: " + p1.LIVESLEFT, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(gameFont, "Score: " + p1.SCORE, new Vector2(400, 0), Color.White);


                    break;
                case "Pause":
                    menu = new PauseMenu();
                    spriteBatch.Draw(pausescreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    break;
                case "GameOver":
                    menu = new GameOverScreen();
                    spriteBatch.Draw(gameoverscreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(gameFont, "Score: " + prevscore, new Vector2(250, 400), Color.White);
                    spriteBatch.DrawString(gameFont, "High Score: " + highScore, new Vector2(250, 425), Color.White);
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
