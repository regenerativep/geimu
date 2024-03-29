﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public class ParallaxBackground
    {
        public SpriteData Sprite { get; set; }
        public float DistanceSpeed { get; set; }
        public Room Room { get; set; }
        private Vector2 drawFrom;
        private Vector2 windowSize;
        private int arbitraryNumber; //yes its arbitray. idk why it works but it does
        public ParallaxBackground(Room room, string spriteName, float layer, float distSpeed)
        {
            arbitraryNumber = 32;
            Room = room;
            DistanceSpeed = distSpeed;
            Sprite = new SpriteData();
            Sprite.Layer = layer / 1000;
            windowSize = new Vector2(Room.Game.GraphicsDevice.Viewport.Width, Room.Game.GraphicsDevice.Viewport.Height);
            Sprite.Size = windowSize * 1.1f;
            AssetManager.RequestTexture(spriteName, (frame) =>
            {
                Sprite.Change(frame);
                drawFrom = windowSize - Sprite.Size;
                drawFrom.Y += arbitraryNumber;
            });
        }
        public void Draw(SpriteBatch batch, Vector2 offset)
        {
            Sprite.Draw(batch, drawFrom - (offset * DistanceSpeed));
        }
    }
}
