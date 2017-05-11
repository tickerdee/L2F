using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2F
{
	/// <summary>
	/// This will be our base component for something that will draw to the screen
	/// </summary>
	class drawableComponent : Microsoft.Xna.Framework.DrawableGameComponent
	{
		protected ContentManager Content;
		protected GraphicsDevice graphics;

		protected Game1 game;

		public static Rectangle drawArea = new Rectangle(0,0, 1280, 720);

		public drawableComponent()
			: base(Program.game)
		{
			this.game = Program.game;

			spriteBatch =
				(SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
			Content =
				(ContentManager)Game.Services.GetService(typeof(ContentManager));

			graphics = (GraphicsDevice)game.Services.GetService(typeof(GraphicsDevice));
		}

		public SpriteBatch spriteBatch { get; set; }
	}
}
