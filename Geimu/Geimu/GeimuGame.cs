using Geimu.GameTiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Geimu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GeimuGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont MainFont;

        Room currentRoom;
        private int currentLevel;
        public int lives;
        public GeimuGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            lives = 4;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            LoadLevel(1);
            currentLevel = 1;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainFont = Content.Load<SpriteFont>("fonts\\mainFont");
            AssetManager.Content = Content;
            AssetManager.LoadTexture("reimuIdle", "sprites\\reimu", 3);
            AssetManager.LoadTexture("reimuRun", "sprites\\reimuRun", 8);
            AssetManager.LoadTexture("reimuJump", "sprites\\reimuJump", 1);
            AssetManager.LoadTexture("reimuFall", "sprites\\reimuFall", 1);
            AssetManager.LoadTexture("dirt", "sprites\\dirt", 1);
            AssetManager.LoadTexture("dirt2", "sprites\\dirt2", 1);
            AssetManager.LoadTexture("grass", "sprites\\grass", 1);
            AssetManager.LoadTexture("grass2", "sprites\\grass2", 1);
            AssetManager.LoadTexture("grassTop", "sprites\\grassTop", 1);
            AssetManager.LoadTexture("dirtSideRight", "sprites\\dirtSideRight", 1);
            AssetManager.LoadTexture("dirtSideBottom", "sprites\\dirtSideBottom", 1);
            AssetManager.LoadTexture("dirtSideLeft", "sprites\\dirtSideLeft", 1);
            AssetManager.LoadTexture("dirtSideTop", "sprites\\dirtSideTop", 1);
            AssetManager.LoadTexture("stoneSideRight", "sprites\\stoneSideRight", 1);
            AssetManager.LoadTexture("stoneSideBottom", "sprites\\stoneSideBottom", 1);
            AssetManager.LoadTexture("stoneSideLeft", "sprites\\stoneSideLeft", 1);
            AssetManager.LoadTexture("stoneSideTop", "sprites\\stoneSideTop", 1);
            AssetManager.LoadTexture("stone", "sprites\\stone", 1);
            AssetManager.LoadTexture("bullet", "sprites\\bullet", 1);
            AssetManager.LoadTexture("whiteChunk", "sprites\\whiteChunk", 1);
            AssetManager.LoadTexture("fairy", "sprites\\fairy", 4);
            AssetManager.LoadTexture("fairyRun", "sprites\\fairy", 2);
            AssetManager.LoadTexture("woodedBackground", "sprites\\woodedBackground0", 1);
            AssetManager.LoadTexture("woodedBackground2", "sprites\\woodedBackground1", 1);
            AssetManager.LoadTexture("woodedBackground3", "sprites\\woodedBackground2", 1);
            AssetManager.LoadTexture("crosshair", "sprites\\crosshair", 3);
            AssetManager.LoadTexture("yinYang", "sprites\\yinYang", 4);
            AssetManager.LoadTexture("cardBullet", "sprites\\cardBullet", 1);
            AssetManager.LoadTexture("jumpParticle", "sprites\\jumpParticle", 3);
            AssetManager.LoadTexture("clownpiece", "sprites\\clownpiece", 3);
            AssetManager.LoadTexture("hakureiShrine", "sprites\\hakureiShrine", 1);
            AssetManager.LoadTexture("hakureiShrine2", "sprites\\hakureiShrine2", 1);
            AssetManager.LoadTexture("hakureiShrine3", "sprites\\hakureiShrine3", 1);
            AssetManager.LoadTexture("touhouBall", "sprites\\touhouBall", 1);
            AssetManager.LoadTexture("touhouBallOutline", "sprites\\touhouBallOutline", 1);
            AssetManager.LoadTexture("yokaiMountain0", "sprites\\yokaiMountain0", 1);
            AssetManager.LoadTexture("yokaiMountain1", "sprites\\yokaiMountain1", 1);
            AssetManager.LoadTexture("yokaiMountain2", "sprites\\yokaiMountain2", 1);
            AssetManager.LoadTexture("yokaiMountain3", "sprites\\yokaiMountain3", 1);
            AssetManager.LoadTexture("yokaiMountain4", "sprites\\yokaiMountain4", 1);
            AssetManager.LoadTexture("moriyaShrine0", "sprites\\moriyaShrine0", 1);
            AssetManager.LoadTexture("moriyaShrine1", "sprites\\moriyaShrine1", 1);
            AssetManager.LoadTexture("moriyaShrine2", "sprites\\moriyaShrine2", 1);
            AssetManager.LoadTexture("moriyaShrine3", "sprites\\moriyaShrine3", 1);
            AssetManager.LoadTexture("moriyaShrine4", "sprites\\moriyaShrine4", 1);
            AssetManager.LoadTexture("jumpReset", "sprites\\jumpReset", 5);
            AssetManager.LoadTexture("heart", "sprites\\gui\\life", 1);
            AssetManager.LoadTexture("humanVillage0", "sprites\\humanVillage0", 1);
            AssetManager.LoadTexture("humanVillage1", "sprites\\humanVillage1", 1);
            AssetManager.LoadTexture("humanVillage2", "sprites\\humanVillage2", 1);
            AssetManager.LoadTexture("myourenTemple0", "sprites\\myourenTemple0", 1);
            AssetManager.LoadTexture("myourenTemple1", "sprites\\myourenTemple1", 1);
            AssetManager.LoadTexture("myourenTemple2", "sprites\\myourenTemple2", 1);
            AssetManager.LoadTexture("myourenTemple3", "sprites\\myourenTemple3", 1);
            AssetManager.LoadTexture("myourenTemple4", "sprites\\myourenTemple4", 1);
            AssetManager.LoadTexture("spikeRight", "sprites\\spikeRight", 1);
            AssetManager.LoadTexture("spikeLeft", "sprites\\spikeLeft", 1);
            AssetManager.LoadTexture("spikeTop", "sprites\\spikeTop", 1);
            AssetManager.LoadTexture("spikeBottom", "sprites\\spikeBottom", 1);
            AssetManager.LoadTexture("clownpieceHealthbar", "sprites\\clownpieceHealthbar", 1);
            AssetManager.LoadTexture("clownpieceHealthbarFrame", "sprites\\clownpieceHealthbarFrame", 1);
            AssetManager.LoadTexture("textWindow", "sprites\\textWindow", 1);
            AssetManager.LoadTexture("note", "sprites\\note", 1);
            AssetManager.LoadTexture("win", "sprites\\win", 1);

            AssetManager.LoadSound("reimuJump", "sounds\\reimuJump");
            AssetManager.LoadSound("throwCard", "sounds\\throwCard");
            AssetManager.LoadSound("mainTheme", "sounds\\Theme");
            AssetManager.LoadSound("bossTheme", "sounds\\bossTheme");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        public void LoadLevel(int levelnum)
        {
            currentLevel = levelnum;
            currentRoom?.music?.Stop();
            currentRoom = null;

            currentRoom = new Room(this);
            //currentRoom.Load("test3.txt");
            if (currentLevel == 6)
            {
                currentRoom.Load("win.txt");
                currentRoom.GameTileList.Add(new WinScreenTile(currentRoom, new Vector2(0, 0)));
            }
            else
            {
                currentRoom.Load("level" + levelnum + ".txt");
                CameraObject camera = new CameraObject(currentRoom, new Vector2(0, 0));
                camera.Target = currentRoom.FindObject("reimu");
                currentRoom.GameObjectList.Add(camera);
            }
        }

        public void NextLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void Lose()
        {
            LoadLevel(1);
            lives = 4;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.H))
                currentRoom.DisplayHitbox();
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Lose();
            currentRoom.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            currentRoom.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
