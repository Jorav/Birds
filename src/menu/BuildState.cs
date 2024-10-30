using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Birds.src.controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Birds.src.utility;

namespace Birds.src.menu
{
    public class BuildState : MenuState
    {
        protected State previousState;
        //public MenuController menuController;
        //protected Controller controllerEdited;
        protected Controller controllerEdited;
        protected bool buildMode;
        public int previousScrollValue;
        public int currentScrollValue;
        private readonly Sprite overlay;
        protected Color originalColor;
        public BuildState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, State previousState, Input input, Controller controllerEdited/*, MenuController menuController = null*/) : base(game, graphicsDevice, content, input)
        {
            this.controllerEdited = controllerEdited;/*
            if (menuController == null)
                this.menuController = new MenuController(CopyEntitiesFromController(controllerEdited), input);
            else
                this.menuController = menuController;
                
            this.menuController.Camera.InBuildScreen = true;
            this.menuController.Camera.AutoAdjustZoom = true;
            
            this.menuController.Color = Color.White;
                */
            this.previousState = previousState;
            if(previousState is GameState)
                GameState.Player.Steering.actionsLocked = true;
            //currentScrollValue = input.ScrollValue;
            overlay = new Sprite(content.Load<Texture2D>("backgroundWhite"));
            overlay.Scale = overlay.Height / Game1.ScreenHeight;
            overlay.Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2);
            originalColor = controllerEdited.Color;
            components = new();
        }

        /*protected List<IEntity> CopyEntitiesFromController(Controller controller)
        {
            List<IEntity> collidables = new List<IEntity>();
            foreach (IEntity c in controller.Controllables)
                collidables.Add((IEntity)c.Clone());
            return collidables;
        }*/

        public override void Update(GameTime gameTime)
        {
            previousScrollValue = currentScrollValue;
            /*currentScrollValue = input.ScrollValue;
            if (previousScrollValue - currentScrollValue != 0)
            {
                menuController.Camera.Zoom /= (float)Math.Pow(0.999, (currentScrollValue - previousScrollValue));
                menuController.Camera.AutoAdjustZoom = false;
            }
            menuController.Update(gameTime);*/
            base.Update(gameTime);
            if(previousState is GameState gameS)
                gameS.RunGame(gameTime);
            if (input.PauseClicked)
                game.ChangeState(new PauseState(game, graphicsDevice, content, this, input));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            previousState.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            overlay.Draw(spriteBatch);
            spriteBatch.End();
            //spriteBatch.Begin(transformMatrix: menuController.Camera.Transform, samplerState: SamplerState.PointClamp);
            spriteBatch.Begin(transformMatrix: Input.Camera.Transform, samplerState: SamplerState.PointClamp);
            //menuController.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
    }
}
