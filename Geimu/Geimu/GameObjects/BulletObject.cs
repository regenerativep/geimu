using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu
{
    public class BulletObject : GameObject
    {
        public Vector2 DirectionVector { get; set; }
        public float Speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <param name="pos"></param>
        /// <param name="dir">radians</param>
        public BulletObject(Room room, Vector2 pos, float dir) : base(room, pos, new Vector2(0, 0), new Vector2(16, 16))
        {
            DirectionVector = new Vector2((float)Math.Cos(dir), (float)Math.Sin(dir));
            Speed = 4;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(16, 16);
            Sprite.Offset = new Vector2(8, 8);
            Hitbox = new Rectangle(-12, -12, 12, 12);
            Sprite.Layer = 4f / 100;
            Sprite.Angle = dir;
            AssetManager.RequestTexture("cardBullet", (frames) =>
            {
                Sprite.Change(frames);
            });
        }
        public override void Update()
        {
            Sprite.Update();
            Position += DirectionVector * Speed;
            GameObject coll = Room.FindCollision(AddVectorToRect(Hitbox, Position), "block");
            if(coll != null)
            {
                Room.GameObjectList.Remove(this);
            }
        }
    }
}
