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

        Room currentRoom;
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
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            currentRoom = new Room(this);
            currentRoom.Load("test4.txt");
            CameraObject camera = new CameraObject(currentRoom, new Vector2(0, 0));
            camera.Target = currentRoom.FindObject("reimu");
            currentRoom.GameObjectList.Add(camera);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            AssetManager.LoadSound("reimuJump", "sounds\\reimuJump");
            AssetManager.LoadSound("throwCard", "sounds\\throwCard");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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
