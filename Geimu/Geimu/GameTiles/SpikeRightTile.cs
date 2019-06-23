using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class SpikeRightTile : GameTile
    {
        public SpikeRightTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            AssetManager.RequestTexture("spikeRight", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
