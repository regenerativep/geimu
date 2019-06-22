using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    public class FairyObject : GameObject
    {
        public static float MoveSpeed = 1.5f;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float IdleMaxSpeed = 3;

        private bool facingRight;
        private Texture2D[] idleSprite, moveSprite;

        public FairyObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {
            System.Diagnostics.Debug.WriteLine("yee");
            facingRight = true;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(32, 32);
            Sprite.Layer = Layer;
            Hitbox = new Rectangle(12, 4, 40, 59);
            idleSprite = null;
            moveSprite = null;
            SpriteManager.RequestTexture("fairyIdle", (frames) =>
            {
                idleSprite = frames;
            });
            SpriteManager.RequestTexture("fairyRun", (frames) =>
            {
                moveSprite = frames;
            });
        }

        public void SwitchMode(string mode)
        {
            switch (mode)
            {
                case "idle":
                    Sprite.Change(idleSprite);
                    Sprite.Speed = 1f / 10;
                    Sprite.Size = new Vector2(32, 32);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
                case "move":
                    Sprite.Change(moveSprite);
                    Sprite.Speed = 1f / 5;
                    Sprite.Size = new Vector2(32, 32);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
            }
        }

        public override void Update()
        {
            if (Math.Abs(Velocity.X) < IdleMaxSpeed)
            {
                SwitchMode("idle");
            }
            else
            {
                SwitchMode("move");
            }
            System.Diagnostics.Debug.WriteLine(Position);
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (facingRight)
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }
            else
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            System.Diagnostics.Debug.WriteLine("Drawing to screen");
            base.Draw(batch, offset);
        }
    }
}
