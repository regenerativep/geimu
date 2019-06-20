using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Size { get; set; }
        public GameObject(Vector2 pos, Vector2 vel, Vector2 size)
        {
            Position = pos;
            Velocity = vel;
            Size = size;
        }
    }
}
