using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2F
{
	class DebugDrawer : drawableComponent
	{

		public DebugDrawer() : base() {}

		public void DrawLine(Vector2 startPoint, Vector2 endPoint, float thickness, Color color)
		{
			// Setup our values for drawing the line
			Vector2 currentLoc = new Vector2(startPoint.X, startPoint.Y);
			Vector2 distance;

			// Clamp thickness to our min range
			thickness = Math.Max(thickness, 2);
			int halfThickness = (int)Math.Round(thickness / 2);

			Rectangle renderBox;

			// Make sure we draw the first rectangle at startPoint
			renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), halfThickness, halfThickness);
			spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);

			do
			{
				// Calculate our current distance to ednpoint
				distance = new Vector2(endPoint.X - currentLoc.X, endPoint.Y - currentLoc.Y);
				Vector2 direction = distance;
				// Get just the direction toward endpoint
				direction.Normalize();
				// Move our location toward the endpoint
				currentLoc.X = direction.X + currentLoc.X;
				currentLoc.Y = direction.Y + currentLoc.Y;

				renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), halfThickness, halfThickness);
				spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);

			} while (distance.Length() > 0.5f);

			// Make sure we draw the last rectangle at endpoint
			currentLoc = new Vector2(endPoint.X, endPoint.Y);
			renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), halfThickness, halfThickness);
			spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);
		}

		public void DrawCircle(Vector2 centerPoint, float radius, float thickness, Color color)
		{

			double theta = 0;
			thickness = Math.Max(thickness, 2);
			int halfThickness = (int)Math.Round(thickness / 2);

			Rectangle renderBox;

			while (theta <= Math.PI*2)
			{
				theta += (Math.PI * 2) / 800;

				double y = (Math.Sin(theta) * radius) + centerPoint.Y;
				double x = (Math.Cos(theta) * radius) + centerPoint.X;

				renderBox = new Rectangle((int)(x - halfThickness), (int)(y - halfThickness), halfThickness, halfThickness);
				spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);
			}

		}
	}// End Clas
}
