using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geimu
{
    public static class SpriteManager
    {
        private static Dictionary<string, Texture2D[]> data = new Dictionary<string, Texture2D[]>();
        private static List<KeyValuePair<string, Action<Texture2D[]>>> requests = new List<KeyValuePair<string, Action<Texture2D[]>>>();
        public static Texture2D[] Load(string name, string foldername, int frameCount, ContentManager contentManager)
        {
            Texture2D[] frames = new Texture2D[frameCount];
            if (foldername[foldername.Length - 1] != '\\')
            {
                foldername += '\\';
            }
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = contentManager.Load<Texture2D>(foldername + i.ToString());
            }
            for(int i = requests.Count - 1; i >= 0; i--)
            {
                KeyValuePair<string, Action<Texture2D[]>> req = requests[i];
                if(req.Key.Equals(name))
                {
                    req.Value.Invoke(frames);
                    requests.RemoveAt(i);
                }
            }
            data[name] = frames;
            return frames;
        }
        public static void RequestTexture(string name, Action<Texture2D[]> callback)
        {
            if (data.ContainsKey(name))
            {
                callback.Invoke(data[name]);
            }
            else
            {
                requests.Add(new KeyValuePair<string, Action<Texture2D[]>>(name, callback));
            }
        }
    }
}
