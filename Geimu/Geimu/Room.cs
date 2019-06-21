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
        public int Width { get; set; }
        public int Height { get; set; }


        public Room()
        {
            GameObjectList = new List<GameObject>();
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
                    Type type = GameObject.GetObjectFromName(parts[1]);
                    Vector2 position = new Vector2(int.Parse(parts[2]), int.Parse(parts[3]));
                    GameObject obj = (GameObject)type.GetConstructor(new Type[] { typeof(Vector2) }).Invoke(new object[] { position });
                    GameObjectList.Add(obj);
                    break;
            }
        }
        public static Room Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            Room room = new Room();
            for(int i = 0; i < lines.Length; i++)
            {
                room.ProcessCommand(lines[i]);
            }
            return room;
        }
    }
}
