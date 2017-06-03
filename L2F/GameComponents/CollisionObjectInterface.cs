using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2F
{
	public enum CollisionTypes
	{
		none = -1,
		noCollision,
		playerCollide
	}

	class CollisionBoundsBase
	{
		public Vector2 WorldPosition;
		public float X
		{
			get { return WorldPosition.X; }
			set { WorldPosition.X = value; }
		}

		public float Y
		{
			get { return WorldPosition.Y; }
			set { WorldPosition.Y = value; }
		}

		public static explicit operator CollisionBoundsBase(SphereCollisionObject v)
		{
			//return SphereCollisionObject
			throw new NotImplementedException();
		}
	}

	interface CollisionObjectInterface
	{
		bool CheckOverlap(CollisionBoundsBase bounds);
	}
}
