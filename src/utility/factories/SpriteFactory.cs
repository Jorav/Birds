using System;
using System.Collections.Generic;
using Birds.src.utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Birds.src.factories
{
    public static class SpriteFactory
    {
      
        public static Texture2D rectangularHull;
        public static Texture2D circularHull;
        public static Texture2D gun;
        public static Texture2D projectile;
        public static Texture2D cloud;
        public static Texture2D sun;
        public static Texture2D emptyLink;
        public static Texture2D spike;
        public static Texture2D entityButton;
        public static Texture2D linkHull;
        public static Texture2D triangularEqualLeggedHull;
        public static Texture2D triangular90AngleHull;
        public static Texture2D engine;
        public static Stack<Sprite> availableSprites = new();
        public static Sprite CreateSprite(Vector2 position, ID_ENTITY id, float scale = 1)
        {
            Vector2 defaultPosition = Vector2.Zero;
            switch (id)
            {
                case ID_ENTITY.DEFAULT: return new Sprite(rectangularHull){Position = position, Scale = scale};
                #region background
                case ID_ENTITY.CLOUD: return new Sprite(cloud){Position = position, Scale = scale};
                case ID_ENTITY.SUN: return new Sprite(sun){Position = position, Scale = scale};
                #endregion

                default:
                    throw new NotImplementedException();
            }
        }
    }
}