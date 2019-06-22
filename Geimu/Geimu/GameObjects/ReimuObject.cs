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
        public static float JumpSpeed = -7;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float Gravity = 0.3f;
        public static float IdleMaxSpeed = 3;
        public static float AirMinSpeed = 2;

        private int jumpsRemaining = 2;
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mouseState;
        private MouseState prevMouseState;
        private bool isJumping;
        private bool facingRight;
        private Texture2D[] idleSprite, moveSprite, jumpSprite, airSprite;
        public ReimuObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
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
            airSprite = null;
            SpriteManager.RequestTexture("reimuIdle", (frames) =>
            {
                idleSprite = frames;
            });
            SpriteManager.RequestTexture("reimuRun", (frames) =>
            {
                moveSprite = frames;
            });
            SpriteManager.RequestTexture("reimuJump", (frames) =>
            {
                jumpSprite = frames;
            });
            SpriteManager.RequestTexture("reimuFall", (frames) =>
            {
                airSprite = frames;
            });
        }
        public void SwitchMode(string mode)
        {
            switch(mode)
            {
                case "idle":
                    Sprite.Change(idleSprite);
                    Sprite.Speed = 1f / 10;
                    Sprite.Size = new Vector2(64, 64);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
                case "move":
                    Sprite.Change(moveSprite);
                    Sprite.Speed = 1f / 5;
                    Sprite.Size = new Vector2(64, 64);
                    Sprite.Offset = new Vector2(0, 0);
                    break;
                case "fall":
                    Sprite.Change(airSprite);
                    Sprite.Size = new Vector2(64, 96);
                    Sprite.Offset = new Vector2(0, 32);
                    break;
                case "jump":
                    Sprite.Change(jumpSprite);
                    Sprite.Size = new Vector2(64, 96);
                    Sprite.Offset = new Vector2(0, 32);
                    break;
            }
        }
        public override void Update()
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
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
            if(!isJumping)
            {
                if(Math.Abs(vel.X) < IdleMaxSpeed)
                {
                    SwitchMode("idle");
                }
                else
                {
                    SwitchMode("move");
                }
            }
            if (vel.Y > AirMinSpeed)
            {
                SwitchMode("fall");
            }
            else if (vel.Y < -AirMinSpeed)
            {
                SwitchMode("jump");
            }
            else
            {
                if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(0, 1))))
                {
                    jumpsRemaining = 2;
                    isJumping = false;
                }
                else
                {
                    SwitchMode("idle");
                }
            }
            if (keyState.IsKeyDown(Settings.Binds.Jump) && prevKeyState.IsKeyUp(Settings.Binds.Jump) && jumpsRemaining > 0)
            {
                vel.Y = JumpSpeed;
                isJumping = true;
                jumpsRemaining--;
            }
            if (Math.Abs(vel.X) > MaxVelocity.X)
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
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 playerPos = Position + (Size / 2);
                Vector2 playerOnScreenPos = playerPos - Room.ViewOffset;
                Vector2 mouseRelative = new Vector2(mouseState.X, mouseState.Y) - playerOnScreenPos;
                Room.GameObjectList.Add(new BulletObject(Room, playerPos, (float)Math.Atan2(mouseRelative.Y, mouseRelative.X)));
            }

            Velocity = vel;

            prevKeyState = keyState;
            prevMouseState = mouseState;
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
