using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu.GameTiles
{
    class WinScreenTile : GameTile
    {

        public WinScreenTile(Room room, Vector2 pos) : base(room, pos, new Vector2(room.Game.GraphicsDevice.Viewport.Width, room.Game.GraphicsDevice.Viewport.Height))
        {
            AssetManager.RequestTexture("win", (frames) =>
            {
                Sprite = new SpriteData(frames);
                Sprite.Size = new Vector2(room.Game.GraphicsDevice.Viewport.Width, room.Game.GraphicsDevice.Viewport.Height);
                Sprite.Speed = 0;
                Sprite.Layer = Layer;
            });
        }
    }
}
