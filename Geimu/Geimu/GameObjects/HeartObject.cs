using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    class HeartObject : GameObject
    {
        private Texture2D[] heartSprite;
        public HeartObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(24, 24))
        {
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(24, 24);
            Sprite.Layer = 1;
            heartSprite = null;
            AssetManager.RequestTexture("heart", (frames) =>
            {
                heartSprite = frames;
                Sprite.Change(heartSprite);
            });
        }
    }
}
