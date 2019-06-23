using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    class DamageBlockObject : GameObject
    {
        public DamageBlockObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32,32))
        {

        }

        public void ChangeHitbox(Rectangle rect)
        {
            Hitbox = rect;
        }

        public override void Update()
        {
            GameObject coll = Room.FindCollision(AddVectorToRect(Hitbox, Position), "reimu");
            if (coll != null)
            {
                ((ReimuObject)coll).Damage();
            }
            base.Update();
        }
    }
}
