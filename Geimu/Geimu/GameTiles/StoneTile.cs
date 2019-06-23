using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class StoneTile : GameTile
    {
        public StoneTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            AssetManager.RequestTexture("stone", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(32, 32);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
                if (!Room.CheckTileAt(Position + new Vector2(Size.X, 0)))
                {
                    GameTile tl = new StoneSideRightTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position + new Vector2(0, Size.Y)))
                {
                    GameTile tl = new StoneSideBottomTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position - new Vector2(Size.X, 0)))
                {
                    GameTile tl = new StoneSideLeftTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
                if (!Room.CheckTileAt(Position - new Vector2(0, Size.Y)))
                {
                    GameTile tl = new StoneSideTopTile(Room, Position);
                    tl.Layer = Layer + 0.01f;
                    Room.GameTileList.Add(tl);
                }
            });
        }
    }
}
