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
        private int attackMode;
        public BossObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(64, 64))
        {
            attackMode = 0;
        }
        public override void Update()
        {
            switch(attackMode)
            {
                case 0: //no attack
                    break;
                case 1: //direct attack
                    break;
                case 2: //spray attack
                    break;
            }
        }

    }
}
