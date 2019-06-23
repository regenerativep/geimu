using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class GrassTile : GameTile
    {
        public GrassTile(Room room, Vector2 pos) : base(room, pos, new Vector2(32, 32))
        {
            AssetManager.RequestTexture("dirt", (frames) =>
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
                    GameTile tl = new GrassTopTile(Room, Position);
                    tl.Layer = Layer + 0.02f;
                    Room.GameTileList.Add(tl);
                }
            });
        }
    }
}
