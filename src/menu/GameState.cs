using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Birds.src.controllers;
using Birds.src.utility;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Birds.src.menu
{
    public class GameState : State
    {
        protected GameController controller;
        protected List<Background> backgrounds;
        protected List<Background> foregrounds;
        protected State previousState;
        public List<IEntity> newEntities;
        public Player Player { get; set; }
        public Camera Camera { get; set; }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Input input, [OptionalAttribute]State previousState) : base(game, graphicsDevice, content, input)
        {
            controller = new GameController();
            backgrounds = new List<Background>();
            foregrounds = new List<Background>();
            this.previousState = previousState;
            newEntities = new List<IEntity>();
            if(Player == null)
            {
                Player = new Player(input);
                controller.Add(Player);
            }
            Camera = new Camera(Player);
            input.Camera = Camera;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.Transform, sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.AnisotropicClamp);
            graphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Background b in backgrounds)
            {
                b.Draw(spriteBatch);
            }
            controller.Draw(spriteBatch);
            foreach (Background f in foregrounds)
            {
                f.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void PostUpdate()
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            /*if (Player.Input.PauseClicked)
                game.ChangeState(new PauseState(game, graphicsDevice, content, this, input));*/
            /*else if (Player.Input.BuildClicked)
                if (Player.Entities != null && Player.Entities.Count>0)
                    game.ChangeState(new BuildOverviewState(game, graphicsDevice, content, this, input, Player));*/
            /*if (input.EnterClicked && previousState != null)
            {
                game.ChangeState(previousState);
                if (previousState is IPlayable p)
                    input.Camera = p.Camera;
            }*/
            RunGame(gameTime);
        }

        public void RunGame(GameTime gameTime)
        {

            //UPDATE
            controller.Update(gameTime);
            Camera.Update();

            //ADD NEW ENTITIES
            /*foreach (IEntity c in controllers)
                if (c is Controller cc)
                {
                    foreach (IEntity cSeperated in cc.ExtractAllSeperatedEntities())
                        newEntities.Add(cSeperated);
                    cc.SeperatedEntities.Clear();
                }
            foreach (IEntity c in newEntities)
                controllers.Add(c);
            newEntities.Clear();*/

            //INTERACT
            /*foreach (IEntity c in controllers)
                c.InteractWith(controllers);*/

            //BACKGROUNDS
            foreach (Background b in backgrounds)
                b.Update(gameTime);
            foreach (Background f in foregrounds)
                f.Update(gameTime);

        }
    }
}
