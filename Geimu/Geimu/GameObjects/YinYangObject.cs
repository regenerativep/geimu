using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    public class YinYangObject : GameObject
    {
        private Texture2D[] yinyangSprite;
        private MouseState mouseState;
        public YinYangObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(16, 16))
        {
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(16, 16);
            Sprite.Layer = 1;
            Sprite.Speed = 0.2f;
            yinyangSprite = null;
            AssetManager.RequestTexture("yinYang", (frames) =>
            {
                yinyangSprite = frames;
                Sprite.Change(yinyangSprite);
            });

            Sprite.Offset = new Vector2(4, 4);
        }

        public void UpdatePos(Vector2 pos)
        {
            mouseState = Mouse.GetState();
            Vector2 playerPos = pos;
            Vector2 playerOnScreenPos = playerPos - Room.ViewOffset;
            Vector2 mouseRelative = new Vector2(mouseState.X, mouseState.Y) - playerOnScreenPos;
            float dir = (float)Math.Atan2(mouseRelative.Y, mouseRelative.X);
            Vector2 DirectionVector = new Vector2((float)Math.Cos(dir), (float)Math.Sin(dir));
            Position = playerPos + (DirectionVector*25);
            base.Update();
        }
    }
}
