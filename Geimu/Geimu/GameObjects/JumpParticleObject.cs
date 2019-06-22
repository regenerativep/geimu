using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    public class JumpParticleObject : GameObject
    {
        private Texture2D[] jumpSprite;
        private int lifetime;

        public JumpParticleObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(30, 6))
        {
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(30, 6);
            Sprite.Layer = 1;
            jumpSprite = null;
            lifetime = 20;
            AssetManager.RequestTexture("jumpParticle", (frames) =>
            {
                jumpSprite = frames;
                Sprite.Change(jumpSprite);
            });
            Sprite.Offset = new Vector2(0, 0);
        }

        public override void Update()
        {
            lifetime--;
            if (lifetime <= 0)
                Room.GameObjectList.Remove(this);
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            base.Draw(batch, offset);
        }
    }
}
