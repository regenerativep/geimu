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
            GameObject collFairy = Room.FindCollision(AddVectorToRect(Hitbox, Position), "fairy");
            GameObject collClown = Room.FindCollision(AddVectorToRect(Hitbox, Position), "clownpiece");
            if (collFairy != null)
            {
                ((FairyObject)collFairy).Damage();
                Room.GameObjectList.Remove(this);
            } else if (collClown != null)
            {
                ((BossObject)collClown).Damage();
                Room.GameObjectList.Remove(this);
            } else
                base.Update();
        }
    }
}
