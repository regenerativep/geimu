ArrayList<GameObject> objects;
int currentObject;
int currentX, currentY;
int snapX, snapY;
int viewOffsetX, viewOffsetY;
int roomWidth, roomHeight;
String commandInput;
GameObjectType[] possibleTypes = new GameObjectType[] {
  null,
  new GameObjectType("reimu", 128, 128),
  new GameObjectType("block", 64, 64)
};
void setup()
{
  size(1024, 768);
  commandInput = "";
  frameRate(60);
  reset();
}
void reset()
{
  objects = new ArrayList<GameObject>();
  currentObject = 0;
  currentX = 0;
  currentY = 0;
  snapX = 32;
  snapY = 32;
  viewOffsetX = 0;
  viewOffsetY = 0;
  roomWidth = 512;
  roomHeight = 512;
}
void draw()
{
  currentX = ((mouseX - viewOffsetX) / snapX) * snapX;
  currentY = ((mouseY - viewOffsetY) / snapX) * snapX;
  if(mousePressed && mouseButton == CENTER)
  {
    viewOffsetX += mouseX - pmouseX;
    viewOffsetY += mouseY - pmouseY;
  }
  
  background(255);
  pushMatrix();
  translate(viewOffsetX, viewOffsetY);
  stroke(0, 0, 255);
  noFill();
  rect(0, 0, roomWidth, roomHeight);
  stroke(0);
  for(int i = 0; i < roomWidth; i += snapX)
  {
    line(i, 0, i, roomHeight);
  }
  for(int i = 0; i < roomHeight; i += snapY)
  {
    line(0, i, roomWidth, i);
  }
  for(GameObject obj : objects)
  {
    drawObject(obj);
  }
  drawObject(new GameObject(currentX, currentY, possibleTypes[currentObject]));
  popMatrix();
  noStroke();
  fill(0);
  rect(0, 0, textWidth(commandInput), 16);
  textAlign(LEFT, TOP);
  fill(255);
  noStroke();
  text(commandInput, 0, 0);
}
void drawObject(GameObject obj)
{
  if(obj.type == null) return;
  stroke(0);
  fill(255);
  rect(obj.x, obj.y, obj.type.wid, obj.type.hgt);
  noStroke();
  fill(0);
  textAlign(LEFT, TOP);
  text(obj.type.name, obj.x, obj.y);
}
void keyPressed()
{
  if(keyCode == ENTER)
  {
    doCommand(commandInput);
    commandInput = "";
  }
  else if(keyCode == BACKSPACE)
  {
    if(commandInput.length() > 0)
    {
      commandInput = commandInput.substring(0, commandInput.length() - 1);
    }
  }
  else
  {
    commandInput += key;
  }
}
void doCommand(String inp)
{
  try
  {
    String[] parts = inp.split(" ");
    switch(parts[0])
    {
      case "width":
        roomWidth = parseInt(parts[1]);
        break;
      case "height":
        roomHeight = parseInt(parts[1]);
        break;
      case "snapx":
        snapX = parseInt(parts[1]);
        break;
      case "snapy":
        snapY = parseInt(parts[1]);
        break;
      case "snap":
        snapX = parseInt(parts[1]);
        snapY = parseInt(parts[1]);
        break;
      case "save":
        saveRoom(parts[1]);
        break;
      case "load":
        loadRoom(parts[1]);
        break;
      case "reset":
        reset();
        break;
      case "createobject":
      {
        String name = parts[1];
        GameObjectType type = null;
        for(int i = 0; i < possibleTypes.length; i++)
        {
          if(possibleTypes[i] != null && name.equals(possibleTypes[i].name))
          {
            type = possibleTypes[i];
          }
        }
        if(type == null)
        {
          println("failed to load type \"" + name + "\"");
          break;
        }
        int x = 0;
        int y = 0;
        try
        {
          x = parseInt(parts[2]);
          y = parseInt(parts[3]);
        }
        catch(Exception e)
        {
          println("failed to load positions for type \"" + name + "\"");
        }
        objects.add(new GameObject(x, y, type));
        break;
      }
      default:
        println("invalid command");
        break;
    }
  }
  catch(Exception e)
  {
    println("something went wrong with your command");
  }
}
void loadRoom(String filename)
{
  BufferedReader reader = createReader(filename);
  String line = null;
  try
  {
    while((line = reader.readLine()) != null)
    {
      doCommand(line);
    }
  }
  catch(Exception e)
  {
    e.printStackTrace();
  }
}
void saveRoom(String filename)
{
  PrintWriter writer = createWriter(filename);
  writer.println("width " + roomWidth);
  writer.println("height " + roomHeight);
  writer.println("snapx " + snapX);
  writer.println("snapy " + snapY);
  for(int i = 0; i < objects.size(); i++)
  {
    GameObject obj = objects.get(i);
    writer.println("createobject " + obj.type.name + " " + obj.x + " " + obj.y);
  }
  writer.flush();
  writer.close();
}
void mousePressed()
{
  if(mouseButton == LEFT)
  {
    GameObjectType type = possibleTypes[currentObject];
    if(type != null)
    {
      objects.add(new GameObject(currentX, currentY, type));
    }
  }
  else if(mouseButton == RIGHT)
  {
    for(int i = 0; i < objects.size(); i++)
    {
      GameObject obj = objects.get(i);
      if(obj.x <= currentX && obj.y <= currentY && obj.x + obj.type.wid > currentX && obj.y + obj.type.hgt > currentY)
      {
        objects.remove(i);
        break;
      }
    }
  }
}
void mouseWheel(MouseEvent event)
{
  float ev = event.getCount();
  if(ev > 0)
  {
    currentObject++;
    if(currentObject >= possibleTypes.length)
    {
      currentObject = 0;
    }
  }
  else if(ev < 0)
  {
    currentObject--;
    if(currentObject < 0)
    {
      currentObject = possibleTypes.length - 1;
    }
  }
}

class GameObject
{
  public int x;
  public int y;
  public GameObjectType type;
  public GameObject(int x, int y, GameObjectType type)
  {
    this.x = x;
    this.y = y;
    this.type = type;
  }
}
class GameObjectType
{
  public String name;
  public int wid, hgt;
  public GameObjectType(String name, int wid, int hgt)
  {
    this.name = name;
    this.wid = wid;
    this.hgt = hgt;
  }
  
}