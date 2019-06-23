using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public abstract class GameTile
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData Sprite { get; set; }
        public Room Room { get; set; }
        private float layer;
        public float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value / 100;
                if (Sprite != null)
                {
                    Sprite.Layer = layer;
                }
            }
        }
        public GameTile(Room room, Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
            Sprite = null;
            Layer = 0;
            Room = room;
        }
        public void Update()
        {
            Sprite?.Update();
        }
        public virtual void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite?.Draw(batch, Position - offset);
        }
        public static Type GetObjectFromName(string name)
        {
            switch(name)
            {
                case "dirt":
                    return typeof(DirtTile);
                case "grass":
                    return typeof(GrassTile);
                case "grassTop":
                    return typeof(GrassTopTile);
                case "dirtSideRight":
                    return typeof(DirtSideRightTile);
                case "dirtSideBottom":
                    return typeof(DirtSideBottomTile);
                case "dirtSideLeft":
                    return typeof(DirtSideLeftTile);
                case "dirtSideTop":
                    return typeof(DirtSideTopTile);
            }
            return null;
        }
    }
}
