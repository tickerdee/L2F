using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2F
{

	class SphereCollisionBounds : CollisionBoundsBase
	{
		public float wideRadius;
	}

	class SphereCollisionObject : CollisionObjectInterface
	{
		public SphereCollisionBounds bounds;

		public SphereCollisionObject(Vector2 WorldPosition, float radius)
		{
			bounds = new SphereCollisionBounds();
			bounds.WorldPosition = WorldPosition;
			bounds.wideRadius = radius;
		}

		public Vector2 GetWorldPosition()
		{
			return bounds.WorldPosition;
		}

		public float GetWideRadius()
		{
			return bounds.wideRadius;
		}

		public bool CheckOverlap(CollisionBoundsBase checkBounds)
		{
			// TODO suport cross type collision

			if(checkBounds is SphereCollisionBounds)
			{
				//tempBounds = (SphereCollisionBounds)(checkBounds);

				double dist = Math.Sqrt(Math.Pow(bounds.X - checkBounds.X, 2) + Math.Pow(bounds.Y - checkBounds.Y, 2));

				return dist <= bounds.wideRadius ? true : false;
			}

			return false;
		}
	}
}
