using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Playing_With_Forces
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D circleTemplate;
        Random rand;
        Mover[] movers;
        bool canClick;

        public Game1()
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
            this.IsMouseVisible = true;
            canClick = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            rand = new Random();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            circleTemplate = Content.Load<Texture2D>("White Circle");
            movers = new Mover[15];
            for(int i = 0; i < movers.Length; i++)
            {
                Color color;
                int c = rand.Next(4);
                switch(c)
                {
                    case 0:
                        color = Color.Beige;
                        break;
                    case 1:
                        color = Color.Magenta;
                        break;
                    case 2:
                        color = Color.Maroon;
                        break;
                    default:
                        color = Color.Green;
                        break;
                }
                movers[i] = new Mover((float)rand.NextDouble(), rand.Next(100, GraphicsDevice.Viewport.Width - 100), rand.Next(100, GraphicsDevice.Viewport.Height - 100), spriteBatch, GraphicsDevice, circleTemplate, color);
                
            }
            

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

            // Click left mouse button top add a force
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && canClick)
            {
                Vector2 mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);



                foreach (Mover mover in movers)
                {
                    Vector2 v = (mover.GetPosition() - mouseLocation);
                    v = new Vector2(1 / v.X, 1 / v.Y) * 70;
                    mover.AddForce(v);
                }
                canClick = false;
            } else if(Mouse.GetState().LeftButton == ButtonState.Released)
            {
                canClick = true;
            }

            
            foreach (Mover mover in movers)
            {
                mover.FinalizeMovement();
                mover.ApplyFriction(.05f);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Mover mover in movers)
            {
                mover.Draw();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
