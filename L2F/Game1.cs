﻿using Microsoft.Xna.Framework;
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
		spriteBatch.Begin();

			// Debug print out all inputs
			spriteBatch.DrawString(Content.Load<SpriteFont>("Basic"), ic.activates(), new Vector2(0, 600), Color.White);

		spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
