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
        public static float MoveSpeed = .8f;
        public static float HorizontalFriction = 1;
        public static Vector2 MaxVelocity = new Vector2(4, 16);
        public static float IdleMaxSpeed = 3;

        private bool facingRight;
        private Texture2D[] fairySprite;
        private int animationindex;
        private bool goingUp;
        private bool moving;
        private int cooldown;
        private int life;

        public FairyObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {
            life = 3;
            cooldown = 60;
            moving = true;
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
                Sprite.Change(fairySprite);
                Sprite.Speed = 1f / 10;
            });

        }

        public override void Update()
        {
            if (cooldown > 0)
                cooldown--;
            if (animationindex == -10)
                goingUp = true;
            if (animationindex == 14)
                goingUp = false;
            if (goingUp)
                animationindex++;
            else
                animationindex--;
            Vector2 vel = Velocity;
            ReimuObject reimu = (ReimuObject)Room.FindObject("reimu");
            if (moving)
            {
                if (facingRight)
                    vel.X = MoveSpeed;
                else
                    vel.X = -MoveSpeed;
                if ((Math.Pow(Position.X - reimu.Position.X, 2) + Math.Pow(Position.Y - reimu.Position.Y, 2)) < (400 * 400))
                {
                    if (cooldown <= 0)
                        moving = false;
                }

            }
            else
            {


                Vector2 playerPos = reimu.Position + (reimu.Size / 2);
                Vector2 fairyPos = Position + (Size / 2);
                /*Vector2 fairyOnScreenPos = fairyPos - Room.ViewOffset;
                Vector2 playerRelativePos = playerPos - fairyOnScreenPos;*/
                float dir = (float)Math.Atan2(playerPos.Y - Position.Y, playerPos.X - Position.X);
                Room.GameObjectList.Add(new CompressedTouhouBall(Room, fairyPos, dir));
                moving = true;
                cooldown = 60;
            }
            Velocity = vel;
            if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(1, 0))))
            {
                facingRight = false;

            }
            if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(-1, 0))))
            {
                facingRight = true;
            }
            if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(0, 1))) && !Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(-1, 1))))
            {
                facingRight = true;
            }
            if (Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(0, 1))) && !Room.CheckCollision(AddVectorToRect(Hitbox, Position, new Vector2(1, 1))))
            {
                facingRight = false;
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
            base.Draw(batch, offset);
        }

        public void Damage()
        {
            if (life <= 0)
                Room.GameObjectList.Remove(this);
            else
                life--;
        }
    }
}
