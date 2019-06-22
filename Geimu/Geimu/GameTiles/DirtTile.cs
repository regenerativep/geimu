using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class DirtTile : GameTile
    {
        private static Random randGen = new Random();
        public DirtTile(Vector2 pos) : base(pos, new Vector2(32, 32))
        {
            string[] options = new string[] { "dirt", "dirt2" };
            int optionInd = randGen.Next(options.Length);
            AssetManager.RequestTexture(options[optionInd], (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
