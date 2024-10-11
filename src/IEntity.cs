using Birds.src.bounding_areas;
using Birds.src.controllers;
using Birds.src.utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Birds.src
{
    public interface IEntity : ICollidable
    {
        public new Vector2 Position {get; set;}
        public IDs Team {get; set;}
        public Controller Manager {get; set;}
        public Color Color {get;set;}
        public Vector2 Velocity { get; }
        public void RotateTo(Vector2 position);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch sb);
        public void Accelerate(Vector2 directionalVector, float thrust);
        public void Accelerate(Vector2 directionalVector);
        public void AccelerateTo(Vector2 position, float thrust);
    }
}