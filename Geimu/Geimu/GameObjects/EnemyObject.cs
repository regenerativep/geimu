using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    class EnemyObject : GameObject
    {
        public static float MoveSpeed = 1.5f;
        //public static float JumpSpeed = -6;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float Gravity = 0.3f;
        public static float IdleMaxSpeed = 3;

        public EnemyObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(128, 128))
        {

        }
    }
}
