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
        public Game1 game { get; set; }
        public Room(Game1 game)
        {
            GameObjectList = new List<GameObject>();
            GameTileList = new List<GameTile>();
            Width = 512;
            Height = 512;
        }
        public void Update()
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Update();
            }
        }
        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObject obj = GameObjectList[i];
                obj.Draw(batch);
            }
            for (int i = 0; i < GameTileList.Count; i++)
            {
                GameTile tile = GameTileList[i];
                tile.Draw(batch);
            }
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
                        GameObjectList.Add(obj);
                        break;
                    }
                case "createtile":
                    {
                        Type type = GameTile.GetObjectFromName(parts[1]);
                        Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                        GameTile obj = (GameTile)type.GetConstructor(new Type[] { typeof(Vector2) }).Invoke(new object[] { position });
                        GameTileList.Add(obj);
                        break;
                    }
            }
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
