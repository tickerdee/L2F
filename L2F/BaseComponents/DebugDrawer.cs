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
			thickness = Math.Max(thickness, 1);
			int halfThickness = (int)Math.Round(thickness / 2);

			Rectangle renderBox;
			
			// do while ensures we will draw at least one point (our start point)
			do
			{
				// Calculate our current distance to ednpoint
				distance = new Vector2(endPoint.X - currentLoc.X, endPoint.Y - currentLoc.Y);
				Vector2 direction = distance;
				// Get just the direction toward endpoint
				direction.Normalize();

				renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), (int)thickness, (int)thickness);
				spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);

				// Move our location toward the endpoint
				currentLoc.X = direction.X + currentLoc.X;
				currentLoc.Y = direction.Y + currentLoc.Y;

			// Using an epsilon of 0.5 inacurate yes... this suggests an issue with the Move currentLoc formula
			} while (distance.Length() > 0.5f);

			// Make sure we draw the last rectangle at endpoint
			currentLoc = new Vector2(endPoint.X, endPoint.Y);
			renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), (int)thickness, (int)thickness);
			spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);
		}

		public void DrawCircle(Vector2 centerPoint, float radius, float thickness, Color color)
		{

			double theta = 0;

			// Clamp thickness to our min range
			thickness = Math.Max(thickness, 1);
			float halfThickness = thickness / 2;

			Rectangle renderBox;

			while (theta <= Math.PI*2)
			{
				// Keep our ammount of steps in ratio with how large the circle is
				theta += (Math.PI * 2) / (radius * 8);

				// Sin(t)= y/r so y= Sin(t)*r and then we add our center point
				double y = (Math.Sin(theta) * radius) + centerPoint.Y;
				// Cos(t)= x/r so x= Cos(t)*r and then we add our center point
				double x = (Math.Cos(theta) * radius) + centerPoint.X;

				renderBox = new Rectangle((int)(x - halfThickness), (int)(y - halfThickness), (int)thickness, (int)thickness);
				spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);
			}
		}
	}// End Clas
}
