using Birds.src;
using Birds.src.utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Birds.src.controllers
{
    public class Player : CohesiveController
    {
        public Input Input { get; set; }
        public bool actionsLocked;
        private bool wasPressed;
        private bool hasStartedMoving;

        public Player(List<IEntity> collidables, Input input) : base(collidables, IDs.TEAM_PLAYER)
        {
            this.Input = input;
            integrateSeperatedEntities = true;
        }
        public Player(Input input, [OptionalAttribute] Vector2 position) : base(position, IDs.TEAM_PLAYER)
        {
            this.Input = input;
            integrateSeperatedEntities = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!actionsLocked)
            {
                RotateTo(Input.PositionGameCoords);
                Accelerate();
                //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                //Shoot(gameTime);
            }
            base.Update(gameTime);
            /*
             * Rotate, calculate course, check collisions, update course, move, base.update
             */
        }

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
            if(Input.IsPressed && !wasPressed && !BoundingCircle.Contains(Input.PositionGameCoords))
                hasStartedMoving = true;
            if (Input.IsPressed && hasStartedMoving)
            {
                accelerationVector = Vector2.Normalize(Input.PositionGameCoords - Position);
                Accelerate(accelerationVector);
            }
            else
                hasStartedMoving = false;
            wasPressed = Input.IsPressed;
            /*
            if (Input.TouchPadActive)
            {
                Vector2 touchpadPosition = Input.PositionGameCoords;
                RotateTo(touchpadPosition);
                accelerationVector = Vector2.Normalize(touchpadPosition - Position);
                Accelerate(accelerationVector);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                accelerationVector = Vector2.Normalize(Input.PositionGameCoords - Position);
                Accelerate(accelerationVector);
            }*/

        }
        public new static String GetName()
        {
            return "Player";
        }
    }
}