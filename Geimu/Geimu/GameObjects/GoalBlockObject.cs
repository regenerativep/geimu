using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    class GoalBlockObject : GameObject
    {
        public GoalBlockObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {

        }

        public override void Update()
        {
            GameObject coll = Room.FindCollision(AddVectorToRect(Hitbox, Position), "reimu");
            if (coll != null)
            {
                if (Room.FindObject("fairy") == null && Room.FindObject("clownpiece") == null)
                {
                    Room.Game.NextLevel();
                }
                    
            }
            base.Update();
        }
    }
}
