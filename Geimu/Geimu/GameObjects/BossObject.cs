using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu
{
    public class BossObject : GameObject
    {
        private static Random randNumGenerator = new Random();
        //private static int stepCooldownReset = 12;
        private static float minSprayDir = 0;
        private static float maxSprayDir = (float) Math.PI;
        private static float sprayDirChange = 0.04f;
        private static int stepsBeforeAttackChange = 300;
        private static int maxLife = 200;
        private static int healthbarTopPadding = 8;
        private int attackMode;
        private int stepCooldown;
        private float sprayDir;
        private int remainingStepsBeforeChange;
        private GameObject target;
        private int life;
        private SpriteData healthbar;
        private SpriteData healthbarFrame;
        private Vector2 drawHealthbarFrom;
        public BossObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            life = maxLife;
            stepCooldown = 0;
            sprayDir = minSprayDir;
            attackMode = 0;
            target = null;
            remainingStepsBeforeChange = stepsBeforeAttackChange;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(64, 64);
            Sprite.Layer = Layer;
            Sprite.Speed = 0.1f;
            Light = new LightData();
            Light.Brightness = 64;
            Light.Position = Position + (Size / 2);
            healthbar = new SpriteData();
            healthbar.Size = new Vector2(384, 32);
            healthbar.Source = new Rectangle(0, 0, (int)healthbar.Size.X, (int)healthbar.Size.Y);
            healthbar.Layer = 0.98f;
            healthbarFrame = new SpriteData();
            healthbarFrame.Size = new Vector2(384, 32);
            healthbarFrame.Layer = 0.99f;
            drawHealthbarFrom = new Vector2((Room.Game.GraphicsDevice.Viewport.Width - healthbarFrame.Size.X) / 2, healthbarTopPadding);
            AssetManager.RequestTexture("clownpiece", (frames) =>
            {
                Sprite.Change(frames);
            });
            AssetManager.RequestTexture("clownpieceHealthbar", (frames) =>
            {
                healthbar.Change(frames);
            });
            AssetManager.RequestTexture("clownpieceHealthbarFrame", (frames) =>
            {
                healthbarFrame.Change(frames);
            });
        }
        public override void Update()
        {
            if (target == null)
            {
                target = Room.FindObject("reimu");
            }
            else
            {
                switch (attackMode)
                {
                    case 0: //no attack
                        break;
                    case 1: //direct attack
                        {
                            float dir = (float)Math.Atan2(target.Position.Y - Position.Y, target.Position.X - Position.X);
                            fireBullet(dir, 30);
                            break;
                        }
                    case 2: //spray attack
                        {
                            sprayDir += sprayDirChange;
                            if(sprayDir > maxSprayDir)
                            {
                                sprayDir = minSprayDir;
                            }
                            fireBullet(sprayDir, 12);
                            break;
                        }
                }
            }
            if(stepCooldown > 0)
            {
                stepCooldown--;
            }
            if(remainingStepsBeforeChange == 0)
            {
                attackMode = randNumGenerator.Next(3);
                remainingStepsBeforeChange = stepsBeforeAttackChange;
            }
            else if(remainingStepsBeforeChange > 0)
            {
                remainingStepsBeforeChange--;
            }
            base.Update();
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            healthbar.Draw(batch, drawHealthbarFrom);
            healthbarFrame.Draw(batch, drawHealthbarFrom);
            base.Draw(batch, offset);
        }
        private void fireBullet(float dir, int cooldown)
        {
            if (stepCooldown == 0)
            {
                Room.GameObjectList.Add(new CompressedTouhouBall(Room, Position + (Size / 2), dir));
                stepCooldown = cooldown;
            }
        }

        public void Damage()
        {
            if (life <= 0)
            {
                Room.GameObjectList.Remove(this);
            }
            else
            {
                life--;
                int newWidth = (int)(((float)life / maxLife) * healthbarFrame.Size.X);
                healthbar.Source = new Rectangle(0, 0, newWidth, (int)healthbarFrame.Size.Y);
                healthbar.Size = new Vector2(newWidth, healthbar.Size.Y);
            }
        }
    }
}
