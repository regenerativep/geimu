﻿using System;
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
        public static float AccelSpeed = 1;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 4);
        public static float gravity = .03f;
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private bool isJumping = false;
        public ReimuObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(128, 128))
        {
            SpriteManager.RequestTexture("reimu", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(64, 64);
                Sprite.Speed = 1f / 10;
            });
            Hitbox = new Rectangle(16, 4, 32, 59);
        }
        public override void Update()
        {
            keyState = Keyboard.GetState();
            Vector2 vel = Velocity; //don't know why i cant just use Velocity
            vel.Y += gravity;
            bool moveKeyPressed = false;
            if (keyState.IsKeyDown(Settings.Binds.Left))
            {
                vel.X -= AccelSpeed;
                moveKeyPressed = true;
            }
            if (keyState.IsKeyDown(Settings.Binds.Right))
            {
                vel.X += AccelSpeed;
                moveKeyPressed = true;
            }
            if (keyState.IsKeyDown(Settings.Binds.Jump) && !isJumping)
            {
                vel.Y -= AccelSpeed * 4;
                isJumping = true;
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
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
