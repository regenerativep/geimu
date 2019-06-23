using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu.GameObjects
{
    public class NoteObject : GameObject
    {
        private static int textBoxTopPadding = 16;
        private static Vector2 startTextFrom = new Vector2(13, 13);
        public bool ShowTextWindow { get; set; }
        private SpriteData textWindowSprite;
        private Vector2 drawTextWindowFrom;
        public NoteObject(Room room, Vector2 pos) : base(room, pos, new Vector2(0, 0), new Vector2(24, 24))
        {
            ShowTextWindow = false;
            Sprite = new SpriteData();
            Sprite.Size = new Vector2(24, 24);
            textWindowSprite = new SpriteData();
            textWindowSprite.Size = new Vector2(512, 128);
            textWindowSprite.Layer = 0.96f;
            drawTextWindowFrom = new Vector2((Room.Game.GraphicsDevice.Viewport.Width - textWindowSprite.Size.X) / 2, textBoxTopPadding);
            AssetManager.RequestTexture("note", (frames) =>
            {
                Sprite.Change(frames);
            });
            AssetManager.RequestTexture("textWindow", (frames) =>
            {
                textWindowSprite.Change(frames);
            });
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            if(ShowTextWindow)
            {
                textWindowSprite.Draw(batch, drawTextWindowFrom);
                batch.DrawString(Room.Game.MainFont, Room.NoteText, drawTextWindowFrom + startTextFrom, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.97f);
            }
            base.Draw(batch, offset);
        }
    }
}
