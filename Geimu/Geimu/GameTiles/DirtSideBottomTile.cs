using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class DirtSideBottomTile : GameTile
    {
        public DirtSideBottomTile(Vector2 pos) : base(pos, new Vector2(32, 32))
        {
            SpriteManager.RequestTexture("dirtSideBottom", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
