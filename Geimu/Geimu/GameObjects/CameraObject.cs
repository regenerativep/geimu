using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Geimu
{
    public class CameraObject : GameObject
    {
        public GameObject Target { get; set; }
        public float TargetPreference { get; set; }
        public float SelfPreference { get; set; }
        public CameraObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(0, 0))
        {
            Target = null;
            TargetPreference = 1;
            SelfPreference = 50;
        }
        public override void Update()
        {
            if (Target == null) return;
            Vector2 pos = Position;
            pos.X = ((TargetPreference * Target.Position.X) + (SelfPreference * pos.X)) / (TargetPreference + SelfPreference);
            pos.Y = ((TargetPreference * Target.Position.Y) + (SelfPreference * pos.Y)) / (TargetPreference + SelfPreference);
            Position = pos;
            Room.ViewOffset = Position - (new Vector2(Room.Width, Room.Height) / 2);
        }
    }
}
