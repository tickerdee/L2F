using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace L2F
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        //camera Variables
        Camera camera;
        Vector2 BackgroundPostion= new Vector2(0,0);

        //projectile variables
        Projectile projectile; // declaration of Our Projectile

        // The input controller for all inputs
        InputController ic;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			
			// Setup the bounds of our screen
			graphics.PreferredBackBufferWidth = drawableComponent.drawArea.Width;
			graphics.PreferredBackBufferHeight = drawableComponent.drawArea.Height;

			// Set the mouse visible
			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
            // TODO: Add your initialization logic here

            camera = new Camera(GraphicsDevice.Viewport);

			ic = InputController.getInstance();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Services.AddService(typeof(SpriteBatch), spriteBatch);
			Services.AddService(typeof(ContentManager), Content);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{

			// Update input controller
			ic.doStateUpdate();
			ic.activates();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ic.activeAtAll(new inputObj((int)InputController.nonJoyTypes.keyboard, "Escape", 1)) > 0)
				Exit();

            camera.update(gameTime, new Vector2(Mouse.GetState().X - 400, Mouse.GetState().Y - 400), new Vector2(1,1));

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Our one and ONLY begin draw call
			//spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.transform);
		spriteBatch.Begin();
			// Debug print out all inputs
			spriteBatch.DrawString(Content.Load<SpriteFont>("Basic"), ic.activates(), new Vector2(0, 600), Color.White);
			SphereCollisionObject temp = new SphereCollisionObject(new Vector2(200, 200), 20);
			SphereCollisionObject temp2 = new SphereCollisionObject(new Vector2(120, 120), 20);
			temp.CheckOverlap((CollisionBoundsBase)(temp2.bounds));

			new DebugDrawer().DrawCircle(temp.GetWorldPosition(), temp.GetWideRadius(), 1, Color.Blue);
			new DebugDrawer().DrawCircle(temp2.GetWorldPosition(), temp2.GetWideRadius(), 1, Color.Red);

			//new DebugDrawer().DrawLine(new Vector2(10, 10), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, Color.Red);

			//new DebugDrawer().DrawCircle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 500, 1, Color.Blue);
			
			//TODO replace TestBackground with actual background image
			//spriteBatch.Draw(Content.Load<Texture2D>("TestBackground"), BackgroundPostion, Color.White);

		spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
