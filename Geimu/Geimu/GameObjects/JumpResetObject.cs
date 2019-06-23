using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    public class JumpResetObject : GameObject
    {
        private static int StepsUntilReset = 180;
        public bool IsActive { get; set; }
        private int stepsRemaning;
        public JumpResetObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {
            Hitbox = new Rectangle(8, 8, 16, 16);
            IsActive = true;
            stepsRemaning = 0;
            AssetManager.RequestTexture("jumpReset", (frames) =>
            {
                Sprite.Change(frames);
                Sprite.Speed = 0.5f;
            });
        }
        public override void Update()
        {
            if(!IsActive)
            {
                if(stepsRemaning > 0)
                {
                    stepsRemaning--;
                }
                else
                {
                    IsActive = true;
                }
            }
            base.Update();
        }
        public void Use()
        {
            IsActive = false;
            stepsRemaning = StepsUntilReset;
        }
    }
}
