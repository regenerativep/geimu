using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class LightData
    {
        public Vector2 Position { get; set; }
        public float Brightness { get; set; }
        public LightData()
        {
            Position = new Vector2(0, 0);
            Brightness = 1;
        }
    }
}
