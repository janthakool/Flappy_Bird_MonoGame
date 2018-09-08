using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Flappy_Bird
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    struct Unit
    {
        public int x;
        public int y;
        public Texture2D pic;
        public int lives;
        public double speed;
        public int time;
        public int score;
        


    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        bool isOverGame = false;

        int W, H;
        Pillar pillar = new Pillar(); //
        Texture2D bird;
        Texture2D bird_fall;

        MovingFloor Floor;
        //TopPipe pipe_top;
        //BotPipe pipe_bot;

        int putStone;
        Texture2D bg;

        Rectangle bg1;
        //Rectangle test;

        SpriteFont font;

        Song GameSong;
        Song GameSong2;
        SoundEffect fly;
        SoundEffect smash;
        SoundEffect fall;

        Unit player;
        //List<Unit> PipeList;

        Random randomGenerator;

        KeyboardState currenKey;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Game Window

            graphics.PreferredBackBufferWidth = W = pillar.W= 600;
            graphics.PreferredBackBufferHeight = H = pillar.H= 385;
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
            //PipeList = new List<Unit>();
            randomGenerator = new Random();


            player.lives = 2;
            player.speed = 4.0;
            player.score = 0;

            player.x = 15;
            player.y = H / 2 - 50;

            int bg_h = 450;
            int bg_w = 700;
            bg1 = new Rectangle(0, H - bg_h, bg_w, bg_h);
            

            Floor = new MovingFloor();
            //pipe_top = new TopPipe();
            //pipe_bot = new BotPipe();
            pillar.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);
            pillar.pic = Content.Load<Texture2D>("pipe_bot");
            // TODO: use this.Content to load your game content here
            bird = Content.Load<Texture2D>("bird2_fly");
            bird_fall = Content.Load<Texture2D>("bird2_fall");

            bg = Content.Load<Texture2D>("sandsea");

            Floor.Initialize(Content.Load<Texture2D>("bgMR2"), -5, W, H);
            //pipe_top.Initialize(Content.Load<Texture2D>("pipe_top"), -1, W, H);
            //pipe_bot.Initialize(Content.Load<Texture2D>("pipe_bot"), -1, W, H);
            font = Content.Load<SpriteFont>("gameFont");
            //Sound
            GameSong = Content.Load<Song>("Easy_Breezy");
            GameSong2 = Content.Load<Song>("Garage");

            fly = Content.Load<SoundEffect>("beep-02-a");
            fall = Content.Load<SoundEffect>("fall");
            smash = Content.Load<SoundEffect>("smash");


            MediaPlayer.Play(GameSong2);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.6f;

            player.pic = bird_fall;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            //keyboard

            currenKey = Keyboard.GetState();

            if (currenKey.IsKeyDown(Keys.Space) && isOverGame == false)// && player.lives > 0)
            {
                player.y = (int)(player.y - player.speed);
                player.pic = bird;
                fly.Play();
            }
            else
            {
                player.y = (int)(player.y + 2);
                player.pic = bird_fall;
                //fall.Play();
            }

            //player.x = MathHelper.Clamp(player.x, 0, W - player.pic.Width);
            player.y = MathHelper.Clamp(player.y, 0, H - player.pic.Height );

            if (player.y > H - player.pic.Height - 60 && player.lives != 0)
            {
                player.x = 15;
                player.y = H / 2 - 50;
                player.lives--;
                smash.Play();

            }



            if (player.lives == 0)
            {
                //MediaPlayer.Stop();
                //MediaPlayer.Play(GameSong2);
                //player.speed = 0;
                //player.y = H / 2 - 50;
                //player.pic = bird;
                isOverGame = true;
                //player.time = 0;
                //Floor.Speed = 0;
                //pillar.speed = 0;
                

            }



            foreach (Vector2 pos in pillar.Stonelist)
            {
                if (isCollide(player, pos, pillar.pic) )
                {
                    
                    player.x = 15;
                    player.y = H / 2 -20;
                    player.lives --;
                    //player.time = 0;
                    player.score--;
                    smash.Play();
                    //if (player.lives <= 0)
                    //{
                        
                    //    player.lives = 10;


                    //    //pillar.speed = 0;
                    //    //player.time = 0;
                    //    //break;

                    //}

                }


            }


            putStone += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (putStone >= 1500 && player.lives> 0 )
            {
                putStone = 0;
                player.score++;
            }


            if (isOverGame)
            {

                player.speed = 0;
                player.y = H / 2 - 50;
                player.pic = bird;
                player.time = 0;
                Floor.Speed = 0;
                pillar.speed = 0;
                
                if (currenKey.IsKeyDown(Keys.Enter)&& isOverGame)
                {
                    player.speed = 4;
                    //player.time = 
                    player.lives = 2;
                    player.x = 15;
                    player.y = H / 2;
                    player.score = 0;
                    Floor.Speed = -5;
                    pillar.speed = 5;
                    isOverGame = false;
                    MediaPlayer.Play(GameSong2);
                }
                

                    
            }

            pillar.Update(gameTime);
            Floor.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            sb.Begin();
            sb.Draw(bg, bg1, Color.White);
            sb.Draw(player.pic, new Vector2(player.x, player.y), Color.White);

            //pipe_top.Draw(sb);
            //pipe_bot.Draw(sb);
            pillar.Draw(sb);
            Floor.Draw(sb);
            sb.DrawString(font, "Lives: " + player.lives, new Vector2(10, 10), Color.White);
            sb.DrawString(font, "Score: " + player.score, new Vector2(W / 2, 10), Color.White);

            sb.End();

            base.Draw(gameTime);
        }

        static bool isCollide(Unit unit1, Vector2 unit2, Texture2D unit2_pic)
        {
            Rectangle rect1 = new Rectangle(unit1.x, unit1.y, unit1.pic.Width, unit1.pic.Height);
            Rectangle rect2 = new Rectangle((int)unit2.X, (int)unit2.Y, unit2_pic.Width, unit2_pic.Height);

            if (rect1.Intersects(rect2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
