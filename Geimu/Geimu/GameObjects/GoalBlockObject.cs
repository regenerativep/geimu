using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameObjects
{
    class GoalBlockObject : GameObject
    {
        private SoundEffect levelCompleteSound;
        public GoalBlockObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(32, 32))
        {
            AssetManager.RequestSound("levelComplete", (sound) =>
            {
                levelCompleteSound = sound;
            });
        }

        public override void Update()
        {
            GameObject coll = Room.FindCollision(AddVectorToRect(Hitbox, Position), "reimu");
            if (coll != null)
            {
                if (Room.FindObject("fairy") == null && Room.FindObject("clownpiece") == null)
                {
                    Room.Sounds.PlaySound(levelCompleteSound);
                    Room.NextRoom();
                }
                    
            }
            base.Update();
        }
    }
}
