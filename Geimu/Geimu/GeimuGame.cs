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
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();

            currentRoom = new Room(this);
            currentRoom.Load("test2.txt");
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
            SpriteManager.Load("reimuIdle", "sprites\\reimu", 3, Content);
            SpriteManager.Load("reimuRun", "sprites\\reimuRun", 8, Content);
            SpriteManager.Load("reimuJump", "sprites\\reimuJump", 1, Content);
            SpriteManager.Load("reimuFall", "sprites\\reimuFall", 1, Content);
            SpriteManager.Load("dirt", "sprites\\dirt", 1, Content);
            SpriteManager.Load("dirt2", "sprites\\dirt2", 1, Content);
            SpriteManager.Load("grass", "sprites\\grass", 1, Content);
            SpriteManager.Load("grass2", "sprites\\grass2", 1, Content);
            SpriteManager.Load("grassTop", "sprites\\grassTop", 1, Content);
            SpriteManager.Load("dirtSideRight", "sprites\\dirtSideRight", 1, Content);
            SpriteManager.Load("dirtSideBottom", "sprites\\dirtSideBottom", 1, Content);
            SpriteManager.Load("dirtSideLeft", "sprites\\dirtSideLeft", 1, Content);
            SpriteManager.Load("dirtSideTop", "sprites\\dirtSideTop", 1, Content);
            SpriteManager.Load("bullet", "sprites\\bullet", 1, Content);
            SpriteManager.Load("whiteChunk", "sprites\\whiteChunk", 1, Content);
            SpriteManager.Load("fairyIdle", "sprites\\fairy", 2, Content);
            SpriteManager.Load("fairyRun", "sprites\\fairy", 2, Content);
            SpriteManager.Load("woodedBackground", "sprites\\woodedBackground0", 1, Content);
            SpriteManager.Load("woodedBackground2", "sprites\\woodedBackground1", 1, Content);
            SpriteManager.Load("woodedBackground3", "sprites\\woodedBackground2", 1, Content);
            SpriteManager.Load("crosshair", "sprites\\crosshair", 3, Content);
            SpriteManager.Load("yinYang", "sprites\\yinYang", 4, Content);
            SpriteManager.Load("cardBullet", "sprites\\cardBullet", 1, Content);
            SpriteManager.Load("jumpParticle", "sprites\\jumpParticle", 3, Content);
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
