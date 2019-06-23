using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Geimu
{
    public class BossObject : GameObject
    {
        private static Random randNumGenerator = new Random();
        private static int stepCooldownReset = 12;
        private static float minSprayDir = 0;
        private static float maxSprayDir = (float) Math.PI;
        private static float sprayDirChange = 0.04f;
        private static int stepsBeforeAttackChange = 300;
        private int attackMode;
        private int stepCooldown;
        private float sprayDir;
        private int remainingStepsBeforeChange;
        private GameObject target;
        private int life;
        public BossObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            life = 200;
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
            AssetManager.RequestTexture("clownpiece", (frames) =>
            {
                Sprite.Change(frames);
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
                            fireBullet(dir);
                            break;
                        }
                    case 2: //spray attack
                        {
                            sprayDir += sprayDirChange;
                            if(sprayDir > maxSprayDir)
                            {
                                sprayDir = minSprayDir;
                            }
                            fireBullet(sprayDir);
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
        private void fireBullet(float dir)
        {
            if (stepCooldown == 0)
            {
                Room.GameObjectList.Add(new CompressedTouhouBall(Room, Position + (Size / 2), dir));
                //maybe play a sound?
                stepCooldown = stepCooldownReset;
            }
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
