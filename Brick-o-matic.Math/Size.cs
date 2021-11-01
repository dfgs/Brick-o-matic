using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public struct Size:IEquatable<Size>
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
		
		public bool IsFlat
		{
			get => ((X == 0) || (Y == 0) || (Z == 0));
		}
		public Size(int XYZ)
		{
			if (XYZ < 0) throw new ArgumentException("Size cannot be negative");
			this.X = XYZ; this.Y = XYZ; this.Z = XYZ;
		}
		public Size(int X,int Y,int Z)
		{
			if (X < 0) throw new ArgumentException("Size X cannot be negative");
			if (Y < 0) throw new ArgumentException("Size X cannot be negative");
			if (Z < 0) throw new ArgumentException("Size X cannot be negative");
			this.X = X;this.Y = Y;this.Z = Z;
		}

		public override string ToString()
		{
			return $"({X},{Y},{Z})";
		}
		public override bool Equals(object obj)
		{
			if (obj is Size otherSize) return ((otherSize.X == X) && (otherSize.Y == Y) && (otherSize.Z == Z));
			return false;
		}
		public bool Equals(Size OtherSize)
		{
			return ((OtherSize.X == X) && (OtherSize.Y == Y) && (OtherSize.Z == Z));
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public static bool operator ==(Size Size1, Size Size2)
		{
			return ((Size1.X == Size2.X) && (Size1.Y == Size2.Y) && (Size1.Z == Size2.Z));
		}
		public static bool operator !=(Size Size1, Size Size2)
		{
			return ((Size1.X != Size2.X) || (Size1.Y != Size2.Y) || (Size1.Z != Size2.Z));
		}

		/*public static Size operator +(Size Size1, Size Size2)
		{
			return new Size(Size1.X + Size2.X, Size1.Y + Size2.Y, Size1.Z + Size2.Z);
		}
		public static Size operator -(Size Size1, Size Size2)
		{
			return new Size(Size1.X - Size2.X, Size1.Y - Size2.Y, Size1.Z - Size2.Z);
		}*/

	}
}
