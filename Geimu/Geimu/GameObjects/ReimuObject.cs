using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Geimu
{
    public class ReimuObject : GameObject
    {
        public static float MoveSpeed = 1.5f;
        public static float JumpSpeed = -4;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float Gravity = 0.3f;
        public static float IdleMaxSpeed = 3;
        private int jumpsRemaining = 2;
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private bool isJumping;
        private bool facingRight;
        private Texture2D[] idleSprite, moveSprite, jumpSprite;
        public ReimuObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(128, 128))
        {
            isJumping = false;
            facingRight = true;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(64, 64);
            Sprite.Layer = Layer;
            Hitbox = new Rectangle(12, 4, 40, 59);
            idleSprite = null;
            moveSprite = null;
            jumpSprite = null;
            SpriteManager.RequestTexture("reimuIdle", (frames) =>
            {
                idleSprite = frames;
                jumpSprite = idleSprite; //todo remove when we get the other sprites
            });
            SpriteManager.RequestTexture("reimuRun", (frames) =>
            {
                moveSprite = frames;
            });
            SpriteManager.RequestTexture("reimuJump", (frames) =>
            {
                jumpSprite = frames;
            });
        }
        public override void Update()
        {
            keyState = Keyboard.GetState();
            Vector2 vel = Velocity; //don't know why i cant just use Velocity
            vel.Y += Gravity;
            bool moveKeyPressed = false;
            if (keyState.IsKeyDown(Settings.Binds.Left))
            {
                vel.X -= MoveSpeed;
                moveKeyPressed = true;
                facingRight = false;
            }
            if (keyState.IsKeyDown(Settings.Binds.Right))
            {
                vel.X += MoveSpeed;
                if (moveKeyPressed)
                {
                    facingRight = vel.X > 0;
                }
                else
                {
                    moveKeyPressed = true;
                    facingRight = true;
                }
            }
            if (keyState.IsKeyReleased(Settings.Binds.Jump) && jumpsRemaining > 0)
            {
                vel.Y += JumpSpeed;
                isJumping = true;
                jumpsRemaining--;
            }
            if(isJumping)
            {
                //check if there is something below us
                if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(0, 1))))
                {
                    jumpsRemaining = 2;
                    isJumping = false;
                }
                else
                {
                    Sprite.Change(jumpSprite);
                }
            }
            else
            {
                if(Math.Abs(vel.X) < IdleMaxSpeed)
                {
                    Sprite.Change(idleSprite);
                    Sprite.Speed = 1f / 10;
                }
                else
                {
                    Sprite.Change(moveSprite);
                    Sprite.Speed = 1f / 5;
                }
            }
            if(Math.Abs(vel.X) > MaxVelocity.X)
            {
                vel.X = Math.Sign(vel.X) * MaxVelocity.X;
            }
            if (Math.Abs(vel.Y) > MaxVelocity.Y)
            {
                vel.Y = Math.Sign(vel.Y) * MaxVelocity.Y;
            }
            if (!moveKeyPressed)
            {
                vel.X -= Math.Sign(vel.X) * HorizontalFriction;
            }
            Velocity = vel;

            prevKeyState = keyState;
            base.Update();
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if(facingRight)
            {
                Sprite.SpriteEffect = SpriteEffects.None;
            }
            else
            {
                Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            base.Draw(batch, offset);
        }
    }
}
