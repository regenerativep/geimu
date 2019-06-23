using Geimu.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class CardBulletObject : BulletObject
    {
        public CardBulletObject(Room room, Vector2 pos, float dir) : base(room, pos, dir)
        {
            Speed = 6;
            Sprite.Angle = dir;
            Sprite.Size = new Vector2(16, 16);
            Sprite.Offset = Sprite.Size / 2;
            Hitbox = new Rectangle(-12, -12, 12, 12);
            Sprite.Layer = 5f / 100;
            AssetManager.RequestTexture("cardBullet", (frames) =>
            {
                Sprite.Change(frames);
            });
        }

        public override void Update()
        {
            GameObject coll = Room.FindCollision(AddVectorToRect(Hitbox, Position), "fairy");
            if (coll != null)
            {
                ((FairyObject)coll).Damage();
                Room.GameObjectList.Remove(this);
            } else
                base.Update();
        }
    }
}
