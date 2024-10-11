using System.Collections.Generic;
using System.Linq;
using Birds.src.BVH;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Birds.src.controllers
{
  public class GameController{
    protected List<Controller> controllers;
    private AABBTree collisionManager = new();

    public GameController(List<Controller> controllers){
      this.controllers = controllers;
      collisionManager.UpdateTree(controllers.Cast<ICollidable>().ToList());
    }

    public void Update(GameTime gameTime){
      foreach(Controller c in controllers)
        c.Update(gameTime);
      collisionManager.Update(gameTime);
    }

    public void Draw(SpriteBatch sb){
      foreach(Controller c in controllers)
        c.Draw(sb);
    }
  }
}