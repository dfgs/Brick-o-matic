using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public struct Vector:IEquatable<Vector>
	{
		public int X
		{
			get;
			private set;
		}
		public int Y
		{
			get;
			private set;
		}
		public int Z
		{
			get;
			private set;
		}
		
		public Vector(int X,int Y,int Z)
		{
			this.X = X;this.Y = Y;this.Z = Z;
		}

		public override string ToString()
		{
			return $"({X},{Y},{Z})";
		}
		public override bool Equals(object obj)
		{
			if (obj is Vector otherVector) return ((otherVector.X == X) && (otherVector.Y == Y) && (otherVector.Z == Z));
			return false;
		}
		public bool Equals(Vector OtherVector)
		{
			return ((OtherVector.X == X) && (OtherVector.Y == Y) && (OtherVector.Z == Z));
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public static bool operator ==(Vector Vector1, Vector Vector2)
		{
			return ((Vector1.X == Vector2.X) && (Vector1.Y == Vector2.Y) && (Vector1.Z == Vector2.Z));
		}
		public static bool operator !=(Vector Vector1, Vector Vector2)
		{
			return ((Vector1.X != Vector2.X) || (Vector1.Y != Vector2.Y) || (Vector1.Z != Vector2.Z));
		}

		public static Vector operator +(Vector Vector1, Vector Vector2)
		{
			return new Vector(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z);
		}
		public static Vector operator -(Vector Vector1, Vector Vector2)
		{
			return new Vector(Vector1.X - Vector2.X, Vector1.Y - Vector2.Y, Vector1.Z - Vector2.Z);
		}

	}
}
