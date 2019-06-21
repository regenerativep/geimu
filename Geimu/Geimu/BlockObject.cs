using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu
{
    public class BlockObject : GameObject
    {
        public BlockObject(Vector2 pos) : base(pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            SpriteManager.RequestTexture("dirt", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(64, 64);
                Sprite.Speed = 1f / 10;
            });
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
