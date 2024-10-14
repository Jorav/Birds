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

namespace Birds.src//new
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private AABBTree controllerTree;
        private Camera camera;
        private Player player;
        private GameController gameController;
        private Background background1;
        private Background background2;
        //private PerformanceMeasurer performanceMeasurer;
        //private MeanSquareError meanSquareError;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static float GRAVITY = 10;
        public static SpriteFont font;
        public static float timeStep = (1f / 60f);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            _graphics.PreferMultiSampling = true;
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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
            // use this and Content to load your game content here
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            Texture2D textureParticle = Content.Load<Texture2D>("RotatingHull");
            Texture2D cloud = Content.Load<Texture2D>("cloud");
            Texture2D solar = Content.Load<Texture2D>("solar");
            font = Content.Load<SpriteFont>("font");
            //Sprite spriteParticle = new Sprite(textureParticle);
            controllerTree = new AABBTree();
            List<IEntity> returnedList = new List<IEntity>();
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(1134, 245)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(1124, 15)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(1124, 120)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(0, 0)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(3, 300)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(500, 5)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(110, 0)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(232, 300)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(342, 243)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(1111, 1111)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(110, 1112)));
            returnedList.Add(new WorldEntity(textureParticle, new Vector2(232, 535)));
            GRAVITY = 10;
            //string[] ConfigVar = EntityFactory.ReadConfig();
            //GRAVITY = float.Parse(ConfigVar[2]);
            //List<WorldEntity> returnedList = EntityFactory.EntFacImplementation(ConfigVar[0], ConfigVar[1], textureParticle);
            player = new Player(returnedList, input);

            camera = new Camera(player) { AutoAdjustZoom = true };
            input.Camera = camera;

            List<IEntity> list1 = new List<IEntity>();
            list1.Add(new WorldEntity(textureParticle, new Vector2(23, 245)));
            list1.Add(new WorldEntity(textureParticle, new Vector2(132, 265)));
            Controller AI1 = new Controller(list1);
            List<IEntity> list2 = new List<IEntity>();
            list2.Add(new WorldEntity(textureParticle, new Vector2(223, 245)));
            Controller AI2 = new Controller(list2);
            List<IEntity> list3 = new List<IEntity>();
            list3.Add(new WorldEntity(textureParticle, new Vector2(23, 45)));
            Controller AI3 = new Controller(list3);
            List<IEntity> list4 = new List<IEntity>();
            list4.Add(new WorldEntity(textureParticle, new Vector2(223, 45)));
            Controller AI4 = new Controller(list4);
            gameController = new GameController(new List<Controller> { player, AI1, AI2, AI3, AI4 });


            List<IEntity> backgroundEntities = new List<IEntity>()
                    {
                        new WorldEntity(cloud, new Vector2(11, 11)){Scale = 2},
                        new WorldEntity(cloud, new Vector2(51, 310)){Scale = 3},
                        new WorldEntity(cloud, new Vector2(511, 4)){Scale = 2.5f},
                        new WorldEntity(cloud, new Vector2(311, 456)){Scale = 1.5f},
                        new WorldEntity(cloud, new Vector2(1311, 856)){Scale = 3},
                        new WorldEntity(cloud, new Vector2(512, 712)){Scale = 2},

                    };
            background1 = new Background(backgroundEntities, 1.5f, camera);
            List<IEntity> backgrounds2 = new List<IEntity> { new WorldEntity(solar, new Vector2(512, 712)) { Scale = 4 } };
            background2 = new Background(backgrounds2, 0.2f, camera);
            // TODO: Use this.Content to load your game content here
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
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = default;
            try { gamePadState = GamePad.GetState(PlayerIndex.One); }
            catch (NotImplementedException) { /* ignore gamePadState */ }

            if (keyboardState.IsKeyDown(Keys.Escape) ||
                keyboardState.IsKeyDown(Keys.Back) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                try { Exit(); }
                catch (PlatformNotSupportedException) { /* ignore */ }
            }

            // TODO: Add your update logic here
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            gameController.Update(gameTime);
            camera.Update();
            background1.Update(gameTime);
            background2.Update(gameTime);

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
            _spriteBatch.Begin(transformMatrix: camera.Transform, sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.AnisotropicClamp);
            background2.Draw(_spriteBatch);
            gameController.Draw(_spriteBatch);
            background1.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
