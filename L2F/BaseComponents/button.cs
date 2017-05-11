using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace L2F
{
	class button : drawableComponent
	{
		public short state;
		bool released, drawing, toggable, drawTooltip, usesMouse;

		Vector2 tooltipIndentation, backBoxSize;

		Texture2D sleep, hover;
		public Rectangle buttonBounds;
		Action clickedMethod;

		String tooltip;

		MouseState old;
		
		public button() : base() { }

		public button(int x, int y, String sleep, String hover, Action clickedMethod)
			: base()
		{

			this.sleep = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + sleep);
			this.hover = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + hover);

			buttonBounds = new Rectangle(x, y, this.sleep.Bounds.Width, this.sleep.Bounds.Height);

			state = 0;// set initial state to sleep
			drawing = true;
			this.clickedMethod = clickedMethod;
			
		}

		public button(Rectangle bounds, String sleep, String hover, Action clickedMethod)
			: base()
		{

			this.sleep = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + sleep);
			this.hover = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + hover);

			buttonBounds = bounds;

			state = 0;// set initial state to sleep
			drawing = true;
			this.clickedMethod = clickedMethod;
		}

		/// <summary>
		/// This will be used to make a toggale button
		/// </summary>
		/// <param name="bounds"></param>
		/// <param name="sleep">This is the buttons default false toggle state</param>
		/// <param name="hover">This is the buttons default true toggle state</param>
		/// <param name="clickedMethod"></param>
		/// <param name="game"></param>
		public button(Rectangle bounds, String sleep, String hover, Action clickedMethod, bool isToggable)
			: base()
		{

			this.sleep = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + sleep);
			this.hover = Content.Load<Texture2D>(@"Sprites/UI/Buttons/" + hover);

			buttonBounds = bounds;

			this.toggable = isToggable;

			tooltip = "ITS A BUTTON ";

			state = 0;// set initial state to sleep (false)
			drawing = true;
			this.clickedMethod = clickedMethod;
		}

		public void setTT(String tooltipMsg, Vector2 indent, Vector2 backBox)
		{

			backBoxSize = backBox;
			tooltipIndentation = indent;
			tooltip = tooltipMsg;
		}

		private Texture2D getPictureState()
		{

			switch (state)
			{
				case 0:
					return sleep;
				case 1:
					return hover;
				default:
					return sleep;
			}

		}

		private void clicked()
		{
			if (toggable)
				if (state == 1)
					state = 0;
				else
					state = 1;

			clickedMethod();
		}

		private void Update(){
			// Check if  the mouse is inside and or clicked

			if (buttonBounds.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
			{
				if (!toggable)
					state = 1;

				//drawTooltip = true;

				if (old.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
				{
					if (!toggable)
						state = 0;
					clicked();
					return;
				}
			}
			else
			{
				if (!toggable)
					state = 0;

				//drawTooltip = false;
			}
			
		}

		public void changePosition(int x, int y)
		{
			buttonBounds = new Rectangle(x, y, this.sleep.Bounds.Width, this.sleep.Bounds.Height);
		}

		public void removeFromDraw(List<button> screenButtonList)
		{
			screenButtonList.Remove(this);
			drawing = false;
		}

		public void addToDraw(List<button> screenButtonList)
		{
			screenButtonList.Add(this);
			drawing = true;
		}

		public void flipDrawing(List<button> screenButtonList)
		{
			if (drawing)
				removeFromDraw(screenButtonList);
			else
				addToDraw(screenButtonList);
		}

		public void Draw()
		{
			Update();

			spriteBatch.Draw(getPictureState(), buttonBounds, Color.White);

			old = Mouse.GetState();
		}

		public void DrawToolTip()
		{
			if (drawTooltip)
			{

				Vector2 pos = Mouse.GetState().Position.ToVector2();
				pos.Y -= tooltipIndentation.Y;
				pos.X -= tooltipIndentation.X;



				spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites/DEBUG/GreenBox"), new Rectangle((int)pos.X - 10, (int)pos.Y - 2, (int)backBoxSize.X, (int)backBoxSize.Y), Color.Black);
				spriteBatch.DrawString(Content.Load<SpriteFont>(@"Fonts/basicFont"), tooltip, pos, Color.White);
			}
		}//End drawtooltip
	}
}
