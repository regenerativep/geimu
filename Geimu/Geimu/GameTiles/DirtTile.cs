using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class DirtTile : GameTile
    {
        private static Random randGen = new Random();
        public DirtTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            string[] options = new string[] { "dirt", "dirt2" };
            int optionInd = randGen.Next(options.Length);
            AssetManager.RequestTexture(options[optionInd], (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
                if (!Room.CheckTileAt(Position + new Vector2(Size.X, 0)))
                {
                    GameTile tl = new DirtSideRightTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position + new Vector2(0, Size.Y)))
                {
                    GameTile tl = new DirtSideBottomTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position - new Vector2(Size.X, 0)))
                {
                    GameTile tl = new DirtSideLeftTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position - new Vector2(0, Size.Y)))
                {
                    GameTile tl = new DirtSideTopTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
            });
            
        }
    }
}
