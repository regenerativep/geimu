ArrayList<GameObject> objects;
ArrayList<GameObject> tiles;
int currentObject, currentTile;
int currentX, currentY;
int snapX, snapY;
int viewOffsetX, viewOffsetY;
int roomWidth, roomHeight;
int currentLayer;
String commandInput;
boolean doSpecialAction;
boolean showGrid;
boolean showDebug; //not actually used xd
boolean showTiles;
boolean showObjects;
boolean showCurrentLayer;
GameObjectType[] possibleTypes = new GameObjectType[] {
  null,
  new GameObjectType("reimu", 64, 64),
  new GameObjectType("block", 32, 32)
};
GameObjectType[] possibleTiles = new GameObjectType[] {
  null,
  new GameObjectType("dirt", 32, 32),
  new GameObjectType("grass", 32, 32),
  new GameObjectType("grassTop", 32, 32),
  new GameObjectType("dirtSideRight", 32, 32),
  new GameObjectType("dirtSideBottom", 32, 32),
  new GameObjectType("dirtSideLeft", 32, 32),
  new GameObjectType("dirtSideTop", 32, 32),
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
  tiles = new ArrayList<GameObject>();
  currentObject = 0;
  currentTile = 0;
  currentX = 0;
  currentY = 0;
  currentLayer = 0;
  snapX = 32;
  snapY = 32;
  viewOffsetX = 0;
  viewOffsetY = 0;
  roomWidth = 512;
  roomHeight = 512;
  doSpecialAction = false;
  showGrid = true;
  showDebug = true;
  showTiles = true;
  showObjects = true;
  showCurrentLayer = false;
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
  if(showGrid)
  {
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
  }
  if(showObjects)
  {
    for(GameObject obj : objects)
    {
      if(!showCurrentLayer || currentLayer == obj.layer)
      {
        drawObject(obj, 255);
      }
    }
  }
  if(showTiles)
  {
    for(GameObject obj : tiles)
    {
      if(!showCurrentLayer || currentLayer == obj.layer)
      {
        drawObject(obj, 128);
      }
    }
  }
  GameObject obj;
  if(showTiles)
  {
    obj = new GameObject(currentX, currentY, possibleTiles[currentTile], 0);
  }
  else
  {
    obj = new GameObject(currentX, currentY, possibleTypes[currentObject], 0);
  }
  drawObject(obj, 255 - (showTiles ? 127 : 0));
  popMatrix();
  noStroke();
  fill(0);
  rect(0, 0, textWidth(commandInput), 16);
  textAlign(LEFT, TOP);
  fill(255);
  noStroke();
  text(commandInput, 0, 0);
}
void drawObject(GameObject obj, int alpha)
{
  if(obj.type == null) return;
  stroke(0);
  fill(alpha, alpha);
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
  else if(keyCode == SHIFT)
  {
    doSpecialAction = true;
  }
  else
  {
    if(doSpecialAction)
    {
      if(key == 'G') //uppercase because we're also holding down shift
      {
        showGrid = !showGrid;
      }
      else if(key == 'T')
      {
        showTiles = !showTiles;
      }
      else if(key == 'O')
      {
        showObjects = !showObjects;
      }
      else if(key == 'D')
      {
        showDebug = !showDebug;
      }
      else if(key == 'S')
      {
        showCurrentLayer = !showCurrentLayer;
      }
    }
    else
    {
      commandInput += key;
    }
  }
}
void keyReleased()
{
  if(keyCode == SHIFT)
  {
    doSpecialAction = false;
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
        int layer = 0;
        try
        {
          x = parseInt(parts[2]);
          y = parseInt(parts[3]);
          layer = parseInt(parts[4]);
        }
        catch(Exception e)
        {
          println("failed to load data for type \"" + name + "\"");
        }
        objects.add(new GameObject(x, y, type, layer));
        break;
      }
      case "createtile":
      {
        String name = parts[1];
        GameObjectType type = null;
        for(int i = 0; i < possibleTiles.length; i++)
        {
          if(possibleTiles[i] != null && name.equals(possibleTiles[i].name))
          {
            type = possibleTiles[i];
          }
        }
        if(type == null)
        {
          println("failed to load type \"" + name + "\"");
          break;
        }
        int x = 0;
        int y = 0;
        int layer = 0;
        try
        {
          x = parseInt(parts[2]);
          y = parseInt(parts[3]);
          layer = parseInt(parts[4]);
        }
        catch(Exception e)
        {
          println("failed to load data for type \"" + name + "\"");
        }
        tiles.add(new GameObject(x, y, type, layer));
        break;
      }
      case "layer":
        currentLayer = parseInt(parts[1]);
        break;
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
    writer.println("createobject " + obj.type.name + " " + obj.x + " " + obj.y + " " + obj.layer);
  }
  for(int i = 0; i < tiles.size(); i++)
  {
    GameObject obj = tiles.get(i);
    writer.println("createtile " + obj.type.name + " " + obj.x + " " + obj.y + " " + obj.layer);
  }
  writer.flush();
  writer.close();
}
void mousePressed()
{
  if(mouseButton == LEFT)
  {
    GameObjectType type;
    if(showTiles)
    {
      type = possibleTiles[currentTile];
    }
    else
    {
      type = possibleTypes[currentObject];
    }
    GameObject obj;
    if(type != null)
    {
      obj = new GameObject(currentX, currentY, type, currentLayer);
      if(showTiles)
      {
        tiles.add(obj);
      }
      else
      {
        objects.add(obj);
      }
    }
  }
  else if(mouseButton == RIGHT)
  {
    ArrayList<GameObject> iterArray;
    if(showTiles)
    {
      iterArray = tiles;
    }
    else
    {
      iterArray = objects;
    }
    for(int i = 0; i < iterArray.size(); i++)
    {
      GameObject obj = iterArray.get(i);
      if(obj.x <= currentX && obj.y <= currentY && obj.x + obj.type.wid > currentX && obj.y + obj.type.hgt > currentY)
      {
        iterArray.remove(i);
        break;
      }
    }
  }
}
void mouseWheel(MouseEvent event)
{
  float ev = event.getCount();
  if(showTiles)
  {
    if(ev > 0)
    {
      currentTile++;
      if(currentTile >= possibleTiles.length)
      {
        currentTile = 0;
      }
    }
    else if(ev < 0)
    {
      currentTile--;
      if(currentTile < 0)
      {
        currentTile = possibleTiles.length - 1;
      }
    }
  }
  else
  {
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
}

class GameObject
{
  public int x;
  public int y;
  public GameObjectType type;
  public int layer;
  public GameObject(int x, int y, GameObjectType type, int layer)
  {
    this.layer = layer;
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