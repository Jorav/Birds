using Birds.src.BVH;
using Birds.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using Birds.src.controllers;
using Birds.src.utility;
using Birds.src.menu;
using Birds.src.factories;

namespace Birds.src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //private PerformanceMeasurer performanceMeasurer;
        //private MeanSquareError meanSquareError;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static float GRAVITY = 10;
        public static SpriteFont font;
        public static float timeStep = (1f / 60f);
        private State currentState;
        private State nextState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Add your initialization logic here
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            _graphics.PreferMultiSampling = true;
            base.Initialize();
        }

        protected override void LoadContent()
        { // use this and Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.ApplyChanges();
            Input input = new Input()
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Pause = Keys.Escape,
                Build = Keys.B,
                Enter = Keys.Enter,
            };
            GRAVITY = 10;

            currentState = new MainMenu(this, GraphicsDevice, Content, input);

            EntityFactory.rectangularHull = Content.Load<Texture2D>("RotatingHull");
            EntityFactory.cloud = Content.Load<Texture2D>("cloud");
            EntityFactory.sun = Content.Load<Texture2D>("solar");
            SpriteFactory.rectangularHull = Content.Load<Texture2D>("RotatingHull");
            SpriteFactory.cloud = Content.Load<Texture2D>("cloud");
            SpriteFactory.sun = Content.Load<Texture2D>("solar");
            font = Content.Load<SpriteFont>("font");
        }
        public void ChangeState(State state)
        {
            nextState = state;
        }
        public State GetNextState()
        {
            if (nextState == null)
                return currentState;
            return nextState;
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

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime);
            currentState.PostUpdate();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}
