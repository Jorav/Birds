using Microsoft.Xna.Framework;
using Birds.src.controllers;
using Birds.src.utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Birds.src.factories
{
    public class ControllerFactory
    {

        public static Controller Create(Vector2 position, IDs id = IDs.CONTROLLER_DEFAULT, int numberOfEntities = 1)
        {
            switch (id)
            {
                case IDs.CONTROLLER_DEFAULT: return new Controller(EntityFactory.CreateEntities(position, numberOfEntities, IDs.ENTITY_DEFAULT));
                //case IDs.CHASER_AI: return new ChaserAI(position);
                case IDs.PLAYER: return new Controller(EntityFactory.CreateEntities(position, numberOfEntities, IDs.ENTITY_DEFAULT));
                default:
                    throw new NotImplementedException();
            }
        }

        public static String GetName(IDs id)
        {
            switch (id)
            {
                case IDs.CONTROLLER_DEFAULT: return Controller.GetName();
                //case IDs.CHASER_AI: return ChaserAI.GetName();
                case IDs.PLAYER: return Player.GetName();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
