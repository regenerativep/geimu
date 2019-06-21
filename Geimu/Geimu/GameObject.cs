using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public SpriteData Sprite { get; set; }
        /// <summary>
        /// hitbox, relative to Position
        /// </summary>
        public Rectangle Hitbox { get; set; }
        public Room Room { get; set; }
        public bool Solid { get; set; }
        public GameObject(Room room, Vector2 pos, Vector2 vel, Vector2 size)
        {
            Solid = false;
            Room = room;
            Position = pos;
            Velocity = vel;
            Size = size;
            Sprite = null;
            Hitbox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
        }
        public virtual void Update()
        {
            Position += Velocity;
            Sprite?.Update();
        }
        public virtual void Draw(SpriteBatch batch)
        {
            Sprite?.Draw(batch, Position);
        }
        public static Type GetObjectFromName(string name)
        {
            switch(name)
            {
                case "reimu":
                    return typeof(ReimuObject);
                case "block":
                    return typeof(BlockObject);
            }
            return null;
        }
    }
}
