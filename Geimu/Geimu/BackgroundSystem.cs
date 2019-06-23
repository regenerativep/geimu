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
        public float BackgroundLayer { get; set; }
        public bool ShowBackgroundOutsideRoom { get; set; }
        private Texture2D whiteChunk;
        public BackgroundSystem(Room room)
        {
            AssetManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
            Room = room;
            ShowBackgroundOutsideRoom = false;
            Backgrounds = new List<ParallaxBackground>();
            BackgroundLayer = 2f / 100;
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
        public void LoadBackground(string name)
        {
            Backgrounds = new List<ParallaxBackground>();
            switch(name)
            {
                case "hakurei":
                    Room.Lighting.DarknessColor = new Color(42, 32, 68);
                    Backgrounds.Add(new ParallaxBackground(Room, "hakureiShrine", BackgroundLayer, 0.05f));
                    Backgrounds.Add(new ParallaxBackground(Room, "hakureiShrine2", BackgroundLayer, 0.07f));
                    Backgrounds.Add(new ParallaxBackground(Room, "hakureiShrine3", BackgroundLayer, 0.3f));
                    break;
                case "mountain":
                    Room.Lighting.DarknessColor = new Color(42, 32, 68);
                    Backgrounds.Add(new ParallaxBackground(Room, "yokaiMountain0", BackgroundLayer, 0.03f));
                    Backgrounds.Add(new ParallaxBackground(Room, "yokaiMountain1", BackgroundLayer, 0.05f));
                    Backgrounds.Add(new ParallaxBackground(Room, "yokaiMountain2", BackgroundLayer, 0.09f));
                    Backgrounds.Add(new ParallaxBackground(Room, "yokaiMountain3", BackgroundLayer, 0.1f));
                    Backgrounds.Add(new ParallaxBackground(Room, "yokaiMountain4", BackgroundLayer, 0.2f));
                    break;
                case "moriya":
                    Room.Lighting.DarknessColor = new Color(42, 32, 68);
                    Backgrounds.Add(new ParallaxBackground(Room, "moriyaShrine0", BackgroundLayer, 0.03f));
                    Backgrounds.Add(new ParallaxBackground(Room, "moriyaShrine1", BackgroundLayer, 0.05f));
                    Backgrounds.Add(new ParallaxBackground(Room, "moriyaShrine2", BackgroundLayer, 0.09f));
                    Backgrounds.Add(new ParallaxBackground(Room, "moriyaShrine3", BackgroundLayer, 0.1f));
                    Backgrounds.Add(new ParallaxBackground(Room, "moriyaShrine4", BackgroundLayer, 0.2f));
                    break;
                case "village":
                    break;
                case "myouren":
                    break;
            }
        }
    }
}
