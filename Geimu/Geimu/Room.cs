using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Geimu
{
    public class Room
    {
        public List<GameObject> GameObjectList { get; set; }
        public List<GameTile> GameTileList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GeimuGame Game { get; set; }
        public Vector2 ViewOffset { get; set; }
        public LightingSystem Lighting { get; set; }
        /// <summary>
        /// frames/update
        /// </summary>
        public int LightingUpdateRate { get; set; }
        private int lightingUpdateRateCounter;
        public Room(GeimuGame game)
        {
            Game = game;
            GameObjectList = new List<GameObject>();
            GameTileList = new List<GameTile>();
            Width = 512;
            Height = 512;
            ViewOffset = new Vector2(0, 0);
            Lighting = new LightingSystem(this);
            LightingUpdateRate = 1;
            lightingUpdateRateCounter = 0;
        }
        public void Update()
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Update();
            }
            if(lightingUpdateRateCounter == 0)
            {
                List<LightData> lights = new List<LightData>();
                for (int i = 0; i < GameObjectList.Count; i++)
                {
                    GameObject obj = GameObjectList[i];
                    if(obj.Light != null)
                    {
                        lights.Add(obj.Light);
                    }
                }
                Lighting.UpdateLighting(lights.ToArray());
            }
            lightingUpdateRateCounter = (lightingUpdateRateCounter + 1) % LightingUpdateRate;
        }
        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Draw(batch, GameObject.VectorCeil(ViewOffset));
            }
            for (int i = 0; i < GameTileList.Count; i++)
            {
                GameTile tile = GameTileList[i];
                tile.Draw(batch, GameObject.VectorCeil(ViewOffset));
            }
            Lighting.Draw(batch, ViewOffset);
        }
        public void ProcessCommand(string cmd)
        {
            string[] parts = cmd.Split(' ');
            switch (parts[0])
            {
                case "width":
                    Width = int.Parse(parts[1]);
                    break;
                case "height":
                    Height = int.Parse(parts[1]);
                    break;
                case "createobject":
                    {
                        Type type = GameObject.GetObjectFromName(parts[1]);
                        Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                        GameObject obj = (GameObject)type.GetConstructor(new Type[] { typeof(Room), typeof(Vector2) }).Invoke(new object[] { this, position });
                        obj.Layer = int.Parse(parts[4]);
                        GameObjectList.Add(obj);
                        break;
                    }
                case "createtile":
                    {
                        Type type = GameTile.GetObjectFromName(parts[1]);
                        Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                        GameTile obj = (GameTile)type.GetConstructor(new Type[] { typeof(Vector2) }).Invoke(new object[] { position });
                        obj.Layer = int.Parse(parts[4]);
                        GameTileList.Add(obj);
                        break;
                    }
            }
        }
        public bool CheckCollision(Rectangle collider)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if (obj.Solid)
                {
                    Rectangle targetRect = GameObject.AddVectorToRect(obj.Hitbox, obj.Position);
                    if (GameObject.RectangleInRectangle(collider, targetRect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public GameObject FindObject(string name)
        {
            for(int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if(obj.GetType() == GameObject.GetObjectFromName(name))
                {
                    return obj;
                }
            }
            return null;
        }
        public GameObject FindCollision(Rectangle collider, string name)
        {
            for(int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                if(obj.GetType() == GameObject.GetObjectFromName(name))
                {
                    if(GameObject.RectangleInRectangle(collider, GameObject.AddVectorToRect(obj.Hitbox, obj.Position)))
                    {
                        return obj;
                    }
                }
            }
            return null;
        }
        public void Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            for(int i = 0; i < lines.Length; i++)
            {
                ProcessCommand(lines[i]);
            }
        }
        public static int HorizRectDistance(Rectangle a, Rectangle b) //not too sure where to put these
        {
            if (a.X > b.X) //a is to the right of b
            {
                return a.X - (b.X + b.Width);
            }
            else //a is to the left of b
            {
                return b.X - (a.X + a.Width);
            }
        }
        public static int VertiRectDistance(Rectangle a, Rectangle b)
        {
            if (a.Y > b.Y) //a is below b
            {
                return a.Y - (b.Y + b.Height);
            }
            else //a is above b
            {
                return b.Y - (a.Y + a.Height);
            }
        }
    }
}
