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
        public static float AccelSpeed = 1;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 4);
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        public ReimuObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(128, 128))
        {
            SpriteManager.RequestTexture("reimu", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(64, 64);
                Sprite.Speed = 1f / 10;
            });
            Hitbox = new Rectangle(16, 4, 48, 63);
        }
        public override void Update()
        {
            keyState = Keyboard.GetState();
            Vector2 vel = Velocity; //don't know why i cant just use Velocity
            bool moveKeyPressed = false;
            if (keyState.IsKeyDown(Settings.Binds.Left))
            {
                vel -= new Vector2(AccelSpeed, 0);
                moveKeyPressed = true;
            }
            if (keyState.IsKeyDown(Settings.Binds.Right))
            {
                vel += new Vector2(AccelSpeed, 0);
                moveKeyPressed = true;
            }
            if(Math.Abs(vel.X) > MaxVelocity.X)
            {
                vel.X = Math.Sign(vel.X) * MaxVelocity.X;
            }
            if(!moveKeyPressed)
            {
                vel.X -= Math.Sign(vel.X) * HorizontalFriction;
            }
            for(int i = 0; i < Room.GameObjectList.Count; i++)
            {
                GameObject obj = Room.GameObjectList[i];
                if(obj.Solid)
                {
                    Rectangle fromRect = new Rectangle((int)Position.X + Hitbox.X + (int)vel.X, (int)Position.Y + Hitbox.Y + (int)vel.Y, Hitbox.X, Hitbox.Y);
                    Rectangle targetRect = new Rectangle((int)obj.Position.X + obj.Hitbox.X + (int)obj.Velocity.X, (int)obj.Position.Y + obj.Hitbox.Y + (int)obj.Velocity.Y, obj.Hitbox.X, obj.Hitbox.Y);
                    if(fromRect.Intersects(targetRect))
                    {
                        int hDist = Room.HorizRectDistance(fromRect, targetRect);
                        int vDist = Room.VertiRectDistance(fromRect, targetRect);
                        vel.X = hDist * Math.Sign(vel.X);
                        vel.Y = vDist * Math.Sign(vel.Y);
                    }
                }
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
