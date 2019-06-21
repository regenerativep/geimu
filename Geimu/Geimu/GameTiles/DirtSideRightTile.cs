using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class DirtSideRightTile : GameTile
    {
        public DirtSideRightTile(Vector2 pos) : base(pos, new Vector2(32, 32))
        {
            SpriteManager.RequestTexture("dirtSideRight", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}