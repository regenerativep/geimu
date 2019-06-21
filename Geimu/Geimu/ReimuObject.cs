using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu
{
    public class ReimuObject : GameObject
    {
        public ReimuObject(Vector2 pos) : base(pos, new Vector2(0, 0), new Vector2(128, 128))
        {
            SpriteManager.RequestTexture("reimu", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(128, 128);
                Sprite.Speed = 1f / 10;
                System.Diagnostics.Debug.WriteLine("got reimu :)");
            });
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
