using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Birds.src.utility;
using System;
using System.Collections.Generic;

namespace Birds.src.factories
{
    public static class EntityFactory
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

        public static WorldEntity Create(Vector2 position, IDs id, float scale = 1)
        {
            Vector2 defaultPosition = Vector2.Zero;
            switch (id)
            {
                case IDs.ENTITY_DEFAULT: return new WorldEntity(rectangularHull, position){Scale = scale};/*
                case IDs.EMPTY_LINK: return new RectangularComposite(new Sprite(emptyLink), position);
                case IDs.COMPOSITE: return new RectangularComposite(new Sprite(rectangularHull), position) { Mass = 2 };
                case IDs.CIRCULAR_COMPOSITE: return new CircularComposite(new Sprite(circularHull), position) { Mass = 2, Scale = 2 };
                case IDs.LINK_COMPOSITE: return new LinkComposite(new Sprite(linkHull), position) { Mass = 1f, Thrust = 0.5f };
                case IDs.TRIANGULAR_EQUAL_COMPOSITE: return new TriangularEqualLeggedComposite(new Sprite(triangularEqualLeggedHull), position) { Mass = 2 };
                case IDs.TRIANGULAR_90ANGLE_COMPOSITE: return new Triangular90AngleComposite(new Sprite(triangular90AngleHull), position) { Mass = 2 };

                case IDs.SHOOTER: return new Shooter(new Sprite(gun), position, (Projectile)Create(position, IDs.PROJECTILE))
                    { Thrust = 0, FireRatePerSecond = 10f, FiringStrength = 14, Mass = 0.5f };
                case IDs.PROJECTILE: return new Projectile(new Sprite(projectile), position)
                    { Mass = 0.4f, Friction = 0.03f, MaxLifeSpan = 3f, MinLifeSpan = 1f };
                case IDs.SPIKE: return new Spike(new Sprite(spike), position) { Thrust = 0, Mass = 0.5f };
                case IDs.ENGINE: return new WorldEntity(new Sprite(engine), position) {Mass = 0.5f, Thrust = 2f };
                //case (int)IDs.COMPOSITE: return new Composite(new Sprite(hull), position);*/
                #region background
                case IDs.CLOUD: return new WorldEntity(cloud, position, isCollidable: false){Scale = scale};
                case IDs.SUN: return new WorldEntity(sun, position, isCollidable:false){Scale = scale};
                #endregion

                default:
                    throw new NotImplementedException();
            }
        }
            
        public static List<IEntity> CreateEntities(Vector2 position, int numberOfEntities, IDs id)
        {
            List<IEntity> returnedList = new List<IEntity>();
            if (numberOfEntities == 1)
                returnedList.Add(EntityFactory.Create(position, id));
            if (numberOfEntities > 1)
            {
                Random rnd = new Random();
                for (int i = 0; i < numberOfEntities; i++)
                {
                    float rRadius = (float)(rnd.NextDouble() * 5 * numberOfEntities);
                    float rAngle = (float)(rnd.NextDouble() * 2 * Math.PI);
                    returnedList.Add(EntityFactory.Create(new Vector2((float)Math.Sin(rAngle), (float)Math.Cos(rAngle)) * rRadius+position, id));
                }
            }
            return returnedList;
        }
        
        /**
        public static void LoadTextures(Texture2D hull, Texture2D gun, Texture2D projectile, Texture2D cloud) //TODO - add support for multiple skins
        {
            this.hull = hull;
            this.gun = gun;
            this.projectile = projectile;
            this.cloud = cloud;
        }*/
    }
}
