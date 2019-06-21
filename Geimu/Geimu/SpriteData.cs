using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class SpriteData
    {
        public int CurrentFrame
        {
            get
            {
                return (int)currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }
        private float currentFrame;
        public float Speed { get; set; }
        public Texture2D[] Frames { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData(Texture2D[] frames)
        {
            CurrentFrame = 0;
            Speed = 1;
            Frames = frames;
        }
        public void Update()
        {
            currentFrame += Speed;
            if (currentFrame < 0) currentFrame += Frames.Length;
            if (currentFrame >= Frames.Length) currentFrame -= Frames.Length;
        }
        public void Draw(SpriteBatch batch, Vector2 position)
        {
            batch.Draw(Frames[CurrentFrame], new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y), Color.White);
        }
    }
}
