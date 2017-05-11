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

		public void DrawLine(Point startPoint, Point endPoint, float thickness, Color color)
		{
			Vector2 currentLoc = new Vector2(startPoint.X, startPoint.Y);

			Vector2 distance = new Vector2(currentLoc.X - endPoint.X, currentLoc.Y - endPoint.Y);

			do
			{
				distance = new Vector2(endPoint.X - currentLoc.X, endPoint.Y - currentLoc.Y);
				Vector2 direction = distance;
				direction.Normalize();
				currentLoc.X = direction.X + currentLoc.X;
				currentLoc.Y = direction.Y + currentLoc.Y;

				int halfThickness = (int)Math.Round(thickness / 2);

				Rectangle renderBox = new Rectangle((int)(currentLoc.X - halfThickness), (int)(currentLoc.Y - halfThickness), halfThickness, halfThickness);
				spriteBatch.Draw(Content.Load<Texture2D>("WhiteBox"), renderBox, color);

			} while (distance.Length() > 1);
			
		}
	}
}
