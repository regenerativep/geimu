using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class ParallaxBackground
    {
        public SpriteData Sprite { get; set; }
        public float DistanceSpeed { get; set; }
        public Room Room { get; set; }
        private Vector2 drawFrom;
        public ParallaxBackground(Room room, string spriteName, float layer, float distSpeed)
        {
            Room = room;
            DistanceSpeed = distSpeed;
            Sprite = new SpriteData();
            Sprite.Layer = layer;
            Sprite.Size = new Vector2(Room.Game.GraphicsDevice.Viewport.Width, Room.Game.GraphicsDevice.Viewport.Height);
            drawFrom = Vector2.Zero;
            AssetManager.RequestTexture(spriteName, (frame) =>
            {
                Sprite.Change(frame);
            });
        }
        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite.Draw(batch, drawFrom - (offset * DistanceSpeed));
        }
    }
}
