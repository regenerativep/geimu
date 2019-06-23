using Geimu.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public abstract class GameObject
    {
        public static float CollisionPrecision = 1;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Size { get; set; }
        public SpriteData Sprite { get; set; }
        private Texture2D hitboxSprite;
        public bool drawHitbox;
        /// <summary>
        /// hitbox, relative to Position
        /// </summary>
        public Rectangle Hitbox { get; set; }
        public Room Room { get; set; }
        public bool Solid { get; set; }
        public LightData Light { get; set; }
        private float layer;
        public float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value / 100;
                if (Sprite != null)
                {
                    Sprite.Layer = layer;
                }
            }
        }
        public GameObject(Room room, Vector2 pos, Vector2 vel, Vector2 size)
        {
            Light = null;
            Solid = false;
            Room = room;
            Position = pos;
            Velocity = vel;
            Size = size;
            Sprite = null;
            Layer = 0;
            Hitbox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            drawHitbox = false;
            AssetManager.RequestTexture("whiteChunk", (frames) =>
            {
                hitboxSprite = frames[0];
            });
        }
        public virtual void Update()
        {
            if (Velocity.X != 0 || Velocity.Y != 0)
            {
                Vector2 vel = Velocity;
                for (int i = 0; i < Room.GameObjectList.Count; i++)
                {
                    GameObject obj = Room.GameObjectList[i];
                    if (obj.Solid)
                    {
                        //feel like there should be a better way to do collisions but idk that way
                        Rectangle targetRect = AddVectorToRect(obj.Hitbox, obj.Position, VectorCeil(obj.Velocity));
                        Rectangle fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(0, vel.Y)));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.Y) < CollisionPrecision)
                            {
                                vel.Y = 0;
                                break;
                            }
                            else
                            {
                                vel.Y -= Math.Sign(vel.Y) * CollisionPrecision;
                                fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(0, vel.Y)));
                            }
                        }
                        fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(vel.X, 0)));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.X) < CollisionPrecision)
                            {
                                vel.X = 0;
                                break;
                            }
                            else
                            {
                                vel.X -= Math.Sign(vel.X) * CollisionPrecision;
                                fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(new Vector2(vel.X, 0)));
                            }
                        }
                        fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(vel));
                        while (RectangleInRectangle(fromRect, targetRect))
                        {
                            if (Math.Abs(vel.X) < CollisionPrecision)
                            {
                                vel.X = 0;
                                break;
                            }
                            else
                            {
                                vel.X -= Math.Sign(vel.X) * CollisionPrecision;
                            }
                            if (Math.Abs(vel.Y) < CollisionPrecision)
                            {
                                vel.Y = 0;
                                break;
                            }
                            else
                            {
                                vel.Y -= Math.Sign(vel.Y) * CollisionPrecision;
                            }
                            fromRect = AddVectorToRect(Hitbox, Position, VectorCeil(vel));
                        }
                    }
                }
                Velocity = vel;
            }
            Position += Velocity;
            Sprite?.Update();
        }
        public virtual void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite?.Draw(batch, Position - offset - Sprite.Offset);
            if (drawHitbox)
                batch.Draw(hitboxSprite, AddVectorToRect(Hitbox, Position - offset), null, Color.White, (float)0.0, new Vector2(0,0), SpriteEffects.None, (float)0.04);
        }
        public static Vector2 VectorCeil(Vector2 val)
        {
            return new Vector2((float)Math.Ceiling(Math.Abs(val.X)) * Math.Sign(val.X), (float)Math.Ceiling(Math.Abs(val.Y)) * Math.Sign(val.Y));
        }
        //https://stackoverflow.com/questions/306316/determine-if-two-rectangles-overlap-each-other
        public static bool RectangleInRectangle(Rectangle a, Rectangle b)
        {
            //return a.Left < b.Right && a.Right > b.Left && a.Top > b.Bottom && a.Bottom < b.Top;
            return a.X < b.X + b.Width && a.X + a.Width > b.X && a.Y < b.Y + b.Height && a.Y + a.Height > b.Y; //flipped comparisons for y since original code is for cartesian coords
        }
        public static Rectangle AddVectorToRect(Rectangle rect, params Vector2[] vecs)
        {
            Rectangle newRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            foreach(Vector2 vec in vecs)
            {
                newRect.X += (int)vec.X;
                newRect.Y += (int)vec.Y;
            }
            return newRect;
        }
        public static Type GetObjectFromName(string name)
        {
            switch(name)
            {
                case "reimu":
                    return typeof(ReimuObject);
                case "block":
                    return typeof(BlockObject);
                case "bullet":
                    return typeof(BulletObject);
                case "fairy":
                    return typeof(FairyObject);
                case "clownpiece":
                    return typeof(BossObject);
                case "jumpReset":
                    return typeof(JumpResetObject);
                case "touhouBall":
                    return typeof(CompressedTouhouBall);
                case "":
                    return typeof(GameObject);
            }
            return null;
        }
    }
}
