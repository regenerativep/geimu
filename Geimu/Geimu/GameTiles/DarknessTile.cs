using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu.GameTiles
{
    public class DarknessTile : GameTile
    {
        private Texture2D whiteChunk;
        private Rectangle drawRect;
        public DarknessTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            drawRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AssetManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (whiteChunk != null)
            {
                batch.Draw(whiteChunk, drawRect, null, Room.Lighting.DarknessColor * Room.Lighting.LightingOpacity, 0f, Vector2.Zero, SpriteEffects.None, 6f / 100);
            }
        }
    }
}
