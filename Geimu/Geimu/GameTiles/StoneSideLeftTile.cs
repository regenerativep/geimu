using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class StoneSideLeftTile : GameTile
    {
        public StoneSideLeftTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            AssetManager.RequestTexture("stoneSideLeft", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
