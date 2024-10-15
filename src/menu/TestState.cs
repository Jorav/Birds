using Birds.src.utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Birds.src.controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Birds.src.factories;

namespace Birds.src.menu
{
    class TestState : GameState
    {
        public TestState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Input input) : base(game, graphicsDevice, content, input)
        {
            Player.SetEntities(EntityFactory.CreateEntities(Vector2.Zero,10,IDs.ENTITY_DEFAULT));
            controller.Add(ControllerFactory.Create(new Vector2(100,100),numberOfEntities: 3));
            controller.Add(ControllerFactory.Create(new Vector2(200,200),numberOfEntities: 1));
            controller.Add(ControllerFactory.Create(new Vector2(300,300),numberOfEntities: 7));
            controller.Add(ControllerFactory.Create(new Vector2(353,42)));

            List<IEntity> backgroundEntities = new List<IEntity>()
                    {
                        EntityFactory.Create(new Vector2(11, 11), IDs.CLOUD, 2),
                        EntityFactory.Create(new Vector2(51, 310), IDs.CLOUD, 3),
                        EntityFactory.Create(new Vector2(511, 4), IDs.CLOUD, 2.5f),
                        EntityFactory.Create(new Vector2(311, 456), IDs.CLOUD, 1.5f),
                        EntityFactory.Create(new Vector2(1311, 856), IDs.CLOUD, 3),
                        EntityFactory.Create(new Vector2(512, 712), IDs.CLOUD, 2),

                    };
            foregrounds.Add(new Background(backgroundEntities, 1.5f, Camera));
            backgrounds.Add(new Background(new List<IEntity> 
            { 
                EntityFactory.Create(new Vector2(0, 200), IDs.SUN, 4)
            }, 0.2f, Camera));
        }
    }
}
