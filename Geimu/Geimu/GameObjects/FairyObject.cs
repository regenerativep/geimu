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
        private Texture2D[] fairySprite;
        private int animationindex;
        private bool goingUp;

        public FairyObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {
            Position -= new Vector2(0, 16);
            facingRight = true;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(32, 32);
            animationindex = -16;
            goingUp = true;
            Hitbox = new Rectangle(0, 0, 32, 48);
            Sprite.Layer = Layer;
            fairySprite = null;
            AssetManager.RequestTexture("fairy", (frames) =>
            {
                fairySprite = frames;
            });
        }

        public void SwitchMode(string mode)
        {
            switch (mode)
            {
                case "idle":
                    Sprite.Change(fairySprite);
                    Sprite.Speed = 1f / 10;
                    Sprite.Size = new Vector2(32, 32);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
                case "move":
                    Sprite.Change(fairySprite);
                    Sprite.Speed = 1f / 5;
                    Sprite.Size = new Vector2(32, 32);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
            }
        }

        public override void Update()
        {
            if (animationindex == -10)
                goingUp = true;
            if (animationindex == 14)
                goingUp = false;
            if (goingUp)
                animationindex++;
            else
                animationindex--;
            if (Math.Abs(Velocity.X) < IdleMaxSpeed)
            {
                SwitchMode("idle");
            }
            else
            {
                SwitchMode("move");
            }
            if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(0, 1))))
            {
                System.Diagnostics.Debug.WriteLine("fairy collide");
            }
            base.Update();
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if (facingRight)
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }
            Sprite.Offset = new Vector2(0, animationindex/2);
            /*if (animationindex < -15)
                Sprite.Offset = new Vector2(0, -16);
            else if (animationindex < 0)
                Sprite.Offset = new Vector2(0, -8);
            else if (animationindex < 15)
                Sprite.Offset = new Vector2(0, 0);
            else if (animationindex < 30)
                Sprite.Offset = new Vector2(0, 8);
            else
                Sprite.Offset = new Vector2(0, 16);*/
            base.Draw(batch, offset);
        }
    }
}
