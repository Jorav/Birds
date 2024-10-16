﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Birds.src.menu;

namespace Birds.src
{
    public class Sprite : IComponent
    {
        public Texture2D texture;
        public float Rotation { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public int Height { get { return (int)texture.Height; } }
        public int Width { get { return (int)texture.Width; } }
        public Color Color { get; set; }
        public bool isVisible = true;
        
        public float Scale { get; set; }

        public Sprite(Texture2D texture, float scale = 1f)
        {
            this.Scale = scale;
            this.texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Color = Color.White;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch sb)
        {
            if (isVisible)
                sb.Draw(texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
