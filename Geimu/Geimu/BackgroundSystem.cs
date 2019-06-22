using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class BackgroundSystem
    {
        public ParallaxBackground[] Backgrounds { get; set; }
        public Room Room { get; set; }
        private Texture2D whiteChunk;
        public BackgroundSystem(Room room)
        {
            SpriteManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
            Room = room;
            Backgrounds = new ParallaxBackground[] { new ParallaxBackground(Room, "woodedBackground", 0, 0.5f) };
        }
        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (whiteChunk != null)
            {
                List<Rectangle> rectanglesToDraw = new List<Rectangle>();
                int windowWidth = Room.Game.GraphicsDevice.Viewport.Width, windowHeight = Room.Game.GraphicsDevice.Viewport.Height;
                if (offset.X < 0)
                {
                    rectanglesToDraw.Add(new Rectangle(0, 0, (int)(-offset.X), windowHeight));
                }
                if (offset.Y < 0)
                {
                    rectanglesToDraw.Add(new Rectangle(0, 0, windowWidth, (int)(-offset.Y)));
                }
                if (offset.X > Room.Width - windowWidth)
                {
                    rectanglesToDraw.Add(new Rectangle((int)(Room.Width - offset.X), 0, (int)(windowWidth - (Room.Width - offset.X)), windowHeight));
                }
                if (offset.Y > Room.Height - windowHeight)
                {
                    rectanglesToDraw.Add(new Rectangle(0, (int)(Room.Height - offset.Y), windowWidth, (int)(windowHeight - (Room.Height - offset.Y))));
                }
                foreach (Rectangle rect in rectanglesToDraw)
                {
                    batch.Draw(whiteChunk, rect, null, Room.Lighting.DarknessColor, 0f, Vector2.Zero, SpriteEffects.None, 1f / 100);
                }
            }
            for (int i = 0; i < Backgrounds.Length; i++)
            {
                Backgrounds[i].Draw(batch, offset);
            }
        }
    }
}
