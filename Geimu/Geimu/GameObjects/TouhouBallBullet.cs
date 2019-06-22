using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu
{
    public class TouhouBallBullet : BulletObject
    {
        private static double hueChangePerStep = 4;
        private SpriteData outlineSprite;
        private double outlineHue;
        private Color outlineColor;
        public TouhouBallBullet(Room room, Vector2 pos, float dir) : base(room, pos, dir)
        {
            outlineHue = 0;
            outlineColor = new Color(255, 255, 255);
            outlineSprite = new SpriteData();
            outlineSprite.Layer = 4f / 100;
            outlineSprite.Size = new Vector2(32, 32);
            outlineSprite.Offset = outlineSprite.Size / 2;
            Speed = 3;
            Sprite.Size = new Vector2(24, 24);
            Sprite.Offset = Sprite.Size / 2;
            Hitbox = AddVectorToRect(new Rectangle(0, 0, (int)Sprite.Size.X, (int)Sprite.Size.Y), -Sprite.Offset * 2);
            Sprite.Layer = 5f / 100;
            AssetManager.RequestTexture("touhouBall", (frames) =>
            {
                Sprite.Change(frames);
            });
            AssetManager.RequestTexture("touhouBallOutline", (frames) =>
            {
                outlineSprite.Change(frames);
            });
        }
        public override void Update()
        {
            outlineSprite.Update();
            int r, g, b;
            HsvToRgb(outlineHue, 1d, 1d, out r, out g, out b);
            outlineColor = new Color(r, g ,b);
            outlineHue += hueChangePerStep;
            outlineHue %= 256;
            base.Update();
        }
        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            outlineSprite.Draw(batch, Position - offset - Sprite.Offset, outlineColor);
            base.Draw(batch, offset);
        }
        //http://www.splinter.com.au/converting-hsv-to-rgb-colour-using-c/
        private void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
