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
        public Color BackgroundColor { get; set; }
        public float LightingDifficulty { get; set; }
        public float LightingOpacity { get; set; }
        private float[,] lightLevels;
        private Texture2D whiteChunk;
        public LightingSystem(Room room, int chunkSize)
        {
            ChunkSize = chunkSize;
            whiteChunk = null;
            LightingDifficulty = 1f;
            LightingOpacity = 0.5f;
            AssetManager.RequestTexture("whiteChunk", (frames) =>
            {
                whiteChunk = frames[0];
            });
            Room = room;
            DarknessColor = Color.Indigo;
            BackgroundColor = Color.CornflowerBlue;
            ResetLighting(LightingDifficulty);
        }
        public void ResetLighting(float baseLight)
        {
            lightLevels = new float[Room.Width / ChunkSize, Room.Height / ChunkSize];
            for (int i = 0; i < lightLevels.GetLength(0); i++)
            {
                for (int j = 0; j < lightLevels.GetLength(1); j++)
                {
                    lightLevels[i, j] = baseLight;
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
                    batch.Draw(whiteChunk, new Rectangle(i * ChunkSize - (int)offset.X, j * ChunkSize - (int)offset.Y, ChunkSize, ChunkSize), null, DarknessColor * ((Math.Min(lightLevels[i, j], 1f) / LightingDifficulty) * LightingOpacity), 0, Vector2.Zero, SpriteEffects.None, 0.9f);
                }
            }
        }
        public void UpdateLighting(LightData[] lights)
        {
            ResetLighting(LightingDifficulty);
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
