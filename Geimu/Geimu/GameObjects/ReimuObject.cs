using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geimu.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        public static float BaseJumpSpeed = -5;
        public static float PerStepJumpSpeed = -0.4f;
        public static int MaximumAfterJumpSteps = 9;
        public static int ShootCooldown = 8;

        private YinYangObject YinYang;
        private int jumpsRemaining = 2;
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mouseState;
        private bool isJumping;
        private bool facingRight;
        private Texture2D[] idleSprite, moveSprite, jumpSprite, airSprite;
        private int remainingJumpSteps;
        private SoundEffect throwCardSound;
        private SoundEffect jumpSound;
        private Vector2 spawnLoc;
        private int remainingShootCooldown;
        public ReimuObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            remainingShootCooldown = 0;
            spawnLoc = pos;
            isJumping = false;
            remainingJumpSteps = 0;
            facingRight = true;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(64, 64);
            Sprite.Layer = Layer;
            Hitbox = new Rectangle(16, 4, 32, 59);
            Light = new LightData();
            Light.Brightness = 128;
            Light.Position = Position + (Size / 2);
            YinYang = new YinYangObject(room, pos);
            idleSprite = null;
            moveSprite = null;
            jumpSprite = null;
            airSprite = null;
            jumpSound = null;
            throwCardSound = null;
            AssetManager.RequestTexture("reimuIdle", (frames) =>
            {
                idleSprite = frames;
            });
            AssetManager.RequestTexture("reimuRun", (frames) =>
            {
                moveSprite = frames;
            });
            AssetManager.RequestTexture("reimuJump", (frames) =>
            {
                jumpSprite = frames;
            });
            AssetManager.RequestTexture("reimuFall", (frames) =>
            {
                airSprite = frames;
            });
            AssetManager.RequestSound("reimuJump", (sound) =>
            {
                jumpSound = sound;
            });
            AssetManager.RequestSound("throwCard", (sound) =>
            {
                throwCardSound = sound;
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
                    Sprite.Offset = new Vector2(0, 16);
                    break;
                case "jump":
                    Sprite.Change(jumpSprite);
                    Sprite.Size = new Vector2(64, 96);
                    Sprite.Offset = new Vector2(0, 16);
                    break;
            }
        }
        public override void Update()
        {
            Light.Position = Position + (Size / 2);
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            Vector2 vel = Velocity; //don't know why i cant just use Velocity
            vel.Y += Gravity;
            bool moveKeyPressed = false;
            YinYang.UpdatePos(Position + (Size / 2));
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
            else
            {
                if(remainingJumpSteps > 0 && keyState.IsKeyDown(Settings.Binds.Jump))
                {
                    vel.Y += PerStepJumpSpeed;
                    remainingJumpSteps--;
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
            bool gotJumpReset = false;
            GameObject collidedObj = Room.FindCollision(AddVectorToRect(Hitbox, Position), "jumpReset");
            if (collidedObj != null)
            {
                JumpResetObject jumpObject = (JumpResetObject)collidedObj;
                if(jumpObject.IsActive)
                {
                    jumpObject.Use();
                    gotJumpReset = true;
                }
            }
            collidedObj = Room.FindCollision(AddVectorToRect(Hitbox, Position), "note");
            if(collidedObj != null)
            {
                NoteObject note = (NoteObject)collidedObj;
                note.ShowTextWindow = true;
            }
            else
            {
                GameObject foundObject = Room.FindObject("note");
                if(foundObject != null)
                {
                    ((NoteObject)foundObject).ShowTextWindow = false;
                }
            }
            if ((keyState.IsKeyDown(Settings.Binds.Jump) && prevKeyState.IsKeyUp(Settings.Binds.Jump) && jumpsRemaining > 0) || gotJumpReset)
            {
                Room.Sounds.PlaySound(jumpSound);
                vel.Y = BaseJumpSpeed;
                isJumping = true;
                remainingJumpSteps = MaximumAfterJumpSteps;
                jumpsRemaining--;
                Vector2 footPos = new Vector2(Position.X + (Size.X / 4), Position.Y + Size.Y);
                Room.GameObjectList.Add(new JumpParticleObject(Room, footPos));
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
            if(remainingShootCooldown == 0 && mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 playerPos = YinYang.Position + (YinYang.Size / 2);
                Vector2 playerOnScreenPos = playerPos - Room.ViewOffset;
                Vector2 mouseRelative = new Vector2(mouseState.X, mouseState.Y) - playerOnScreenPos;
                Room.GameObjectList.Add(new CardBulletObject(Room, playerPos, (float)Math.Atan2(mouseRelative.Y, mouseRelative.X)));
                remainingShootCooldown = ShootCooldown;
                Room.Sounds.PlaySound(throwCardSound);
            }
            if(remainingShootCooldown > 0)
            {
                remainingShootCooldown--;
            }

            if (Position.Y > 2000)
                Damage();
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
            YinYang.Draw(batch, offset);
            DrawLives(batch);
            base.Draw(batch, offset);
        }

        public void DrawLives(SpriteBatch batch)
        {
            int l = Room.Game.lives;
            Vector2 v = new Vector2(5, 5);
            for (int i = 0; i < l; i++)
            {
                HeartObject h = new HeartObject(Room, v);
                h.Draw(batch, new Vector2(0,0));
                v += new Vector2(32, 0);
            }
        }
        public void Damage()
        {
            Position = spawnLoc;
            if (Room.Game.lives == 0)
                Room.Game.Lose();
            else
                Room.Game.lives--;
        }
    }
}
