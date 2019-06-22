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
    class CrosshairObject : GameObject
    {
        private Texture2D[] crosshairSprite;
        private MouseState mouseState;
        public CrosshairObject(Room room) : base(room, new Vector2(0,0), new Vector2(0,0), new Vector2(24,24))
        {
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(24, 24);
            Sprite.Layer = 1;
            crosshairSprite = null;
            SpriteManager.RequestTexture("crosshair", (frames) =>
            {
                crosshairSprite = frames;
                Sprite.Change(crosshairSprite);
            });
            
            Sprite.Offset = new Vector2(12, 12);
        }

        public override void Update()
        {
            mouseState = Mouse.GetState();
            Position = new Vector2(mouseState.X, mouseState.Y);
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite.SpriteEffect = SpriteEffects.None;
            base.Draw(batch, Vector2.Zero);
        }
    }
}
