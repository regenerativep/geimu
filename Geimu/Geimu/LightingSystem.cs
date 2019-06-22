using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class LightingSystem
    {

        public Room Room { get; set; }
        public int ChunkSize { get; set; }
        public Color DarknessColor { get; set; }
        private float[,] lightLevels;
        private Texture2D whiteChunk;
        public LightingSystem(Room room)
        {
            whiteChunk = null;
            SpriteManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
            Room = room;
            ChunkSize = 32;
            DarknessColor = Color.Black;
            lightLevels = new float[room.Game.GraphicsDevice.Viewport.Width / ChunkSize, room.Game.GraphicsDevice.Viewport.Height / ChunkSize];
            ResetLighting();
        }
        public void ResetLighting()
        {
            for (int i = 0; i < lightLevels.GetLength(0); i++)
            {
                for (int j = 0; j < lightLevels.GetLength(1); j++)
                {
                    lightLevels[i, j] = 1;
                }
            }
        }
        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (whiteChunk == null) return;
            for (int i = 0; i < lightLevels.GetLength(0); i++)
            {
                for (int j = 0; j < lightLevels.GetLength(1); j++)
                {
                    batch.Draw(whiteChunk, new Rectangle(i * ChunkSize - (int)offset.X, j * ChunkSize - (int)offset.Y, ChunkSize, ChunkSize), null, DarknessColor  * lightLevels[i, j], 0, Vector2.Zero, SpriteEffects.None, 0.9f);
                }
            }
        }
        public void UpdateLighting(LightData[] lights)
        {
            ResetLighting();
            foreach(LightData light in lights)
            {
                for (int i = 0; i < lightLevels.GetLength(0); i++)
                {
                    for (int j = 0; j < lightLevels.GetLength(1); j++)
                    {
                        float distSqr = (float)(Math.Pow(i - (light.Position.X / ChunkSize), 2) + Math.Pow(j - (light.Position.Y / ChunkSize), 2));
                        lightLevels[i, j] = Math.Max(0f, lightLevels[i, j] - (light.Brightness / distSqr));
                    }
                }
            }
        }
    }
}
