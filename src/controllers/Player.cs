using Birds.src;
using Birds.src.utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Birds.src.controllers
{
    public class Player : CohesiveController
    {
        public Input Input { get; set; }
        public bool actionsLocked;
        
        
        public Player(List<IEntity> collidables, Input input) : base(collidables, IDs.TEAM_PLAYER)
        {
            this.Input = input;
            integrateSeperatedEntities = true;
        }
        public Player(Input input, [OptionalAttribute]Vector2 position) : base(position, IDs.TEAM_PLAYER)
        {
            this.Input = input;
            integrateSeperatedEntities = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!actionsLocked)
            {
                RotateTo(Input.MousePositionGameCoords);
                Accelerate();
                //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    //Shoot(gameTime);
            }
            base.Update(gameTime);
            /*
             * Rotate, calculate course, check collisions, update course, move, base.update
             */
        }
        /*
        public override void AddControllable(IControllable c)
        {
            base.AddControllable(c);
        }*/

        protected void Accelerate() //TODO(lowprio): remove vector 2 instanciation from angle calculation (inefficient, high computational req)
        {

            if (Input == null)
                return;
            Vector2 accelerationVector = Vector2.Zero;
            /*if (Keyboard.GetState().IsKeyDown(Input.Up) ^ Keyboard.GetState().IsKeyDown(Input.Down))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    //accelerationVector.X += (float)Math.Cos((double)MathHelper.ToRadians(90));
                    accelerationVector.Y += (float)Math.Sin((double)MathHelper.ToRadians(-90));
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                {
                    //accelerationVector.X += (float)Math.Cos((double)MathHelper.ToRadians(270));
                    accelerationVector.Y += (float)Math.Sin((double)MathHelper.ToRadians(-270));
                }
            }
            if (Keyboard.GetState().IsKeyDown(Input.Left) ^ Keyboard.GetState().IsKeyDown(Input.Right))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    accelerationVector.X += (float)Math.Cos((double)MathHelper.ToRadians(180));
                    //accelerationVector.Y += (float)Math.Sin((double)MathHelper.ToRadians(-180));
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    accelerationVector.X += (float)Math.Cos(0);
                    //accelerationVector.Y += (float)Math.Sin(0);
                }
            }
            if (!accelerationVector.Equals(Vector2.Zero))
            {
                accelerationVector.Normalize();
                foreach (IControllable c in Controllables)
                    c.Accelerate(accelerationVector);
            }*/

            if (Input.TouchPadActive)
            {
                Vector2 touchpadPosition = Input.TouchPadPositionGameCoords;
                RotateTo(touchpadPosition);
                accelerationVector = Vector2.Normalize(touchpadPosition - Position);
                Accelerate(accelerationVector);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                accelerationVector = Vector2.Normalize(Input.MousePositionGameCoords - Position);
                Accelerate(accelerationVector);
            }

        }
        public new static String GetName()
        {
            return "Player";
        }
    }
}