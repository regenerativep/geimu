using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    public abstract class FairyObject : GameObject
    {
        public static float MoveSpeed = 1.5f;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float Gravity = 0.3f;
        public static float IdleMaxSpeed = 3;

        private bool facingRight;

        public FairyObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            facingRight = true;
        }
    }
}
