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
        public List<ParallaxBackground> Backgrounds { get; set; }
        public Room Room { get; set; }
        public bool ShowBackgroundOutsideRoom { get; set; }
        private Texture2D whiteChunk;
        public BackgroundSystem(Room room)
        {
            SpriteManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
            Room = room;
            ShowBackgroundOutsideRoom = false;
            Backgrounds = new List<ParallaxBackground>();
        }
        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (whiteChunk != null && !ShowBackgroundOutsideRoom)
            {
                List<Rectangle> rectanglesToDraw = new List<Rectangle>();
                int windowWidth = Room.Game.GraphicsDevice.Viewport.Width, windowHeight = Room.Game.GraphicsDevice.Viewport.Height;
                if (offset.X < 0)
                {
                    rectanglesToDraw.Add(new Rectangle(0, (int)(-offset.Y), (int)(-offset.X), Room.Height));
                }
                if (offset.Y < 0)
                {
                    rectanglesToDraw.Add(new Rectangle(0, 0, windowWidth, (int)(-offset.Y)));
                }
                if (offset.X > Room.Width - windowWidth)
                {
                    rectanglesToDraw.Add(new Rectangle((int)(Room.Width - offset.X), (int)(-offset.Y), (int)(windowWidth - (Room.Width - offset.X)), Room.Height));
                }
                if (offset.Y > Room.Height - windowHeight)
                {
                    rectanglesToDraw.Add(new Rectangle(0, (int)(Room.Height - offset.Y), windowWidth, (int)(windowHeight - (Room.Height - offset.Y))));
                }
                foreach (Rectangle rect in rectanglesToDraw)
                {
                    batch.Draw(whiteChunk, rect, null, Room.Lighting.DarknessColor * Room.Lighting.LightingOpacity, 0f, Vector2.Zero, SpriteEffects.None, 6f / 100);
                }
            }
            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].Draw(batch, offset);
            }
        }
    }
}
