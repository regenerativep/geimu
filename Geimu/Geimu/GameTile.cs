using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class GameTile
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData Sprite { get; set; }
        public GameTile(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
            Sprite = null;
        }
        public void Update()
        {
            Sprite?.Update();
        }
        public void Draw(SpriteBatch batch)
        {
            Sprite?.Draw(batch, Position);
        }
        public static Type GetObjectFromName(string name)
        {
            switch(name)
            {
                case "dirt":
                    return typeof(DirtTile);
                case "grass":
                    return typeof(GrassTile);
            }
            return null;
        }
    }
}
