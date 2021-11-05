using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public struct Position:IEquatable<Position>
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
		
		public Position(int X,int Y,int Z)
		{
			this.X = X;this.Y = Y;this.Z = Z;
		}

		public override string ToString()
		{
			return $"({X},{Y},{Z})";
		}
		public override bool Equals(object obj)
		{
			if (obj is Position otherPosition) return ((otherPosition.X == X) && (otherPosition.Y == Y) && (otherPosition.Z == Z));
			return false;
		}
		public bool Equals(Position OtherPosition)
		{
			return ((OtherPosition.X == X) && (OtherPosition.Y == Y) && (OtherPosition.Z == Z));
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public static bool operator ==(Position Position1, Position Position2)
		{
			return ((Position1.X == Position2.X) && (Position1.Y == Position2.Y) && (Position1.Z == Position2.Z));
		}
		public static bool operator !=(Position Position1, Position Position2)
		{
			return ((Position1.X != Position2.X) || (Position1.Y != Position2.Y) || (Position1.Z != Position2.Z));
		}

		public static Position operator +(Position Position1, Position Position2)
		{
			return new Position(Position1.X + Position2.X, Position1.Y + Position2.Y, Position1.Z + Position2.Z);
		}
		public static Position operator -(Position Position1, Position Position2)
		{
			return new Position(Position1.X - Position2.X, Position1.Y - Position2.Y, Position1.Z - Position2.Z);
		}


		public static Position operator +(Position Position1, Size Size)
		{
			return new Position(Position1.X + Size.X, Position1.Y + Size.Y, Position1.Z + Size.Z);
		}
		public static Position operator -(Position Position1, Size Size)
		{
			return new Position(Position1.X - Size.X, Position1.Y - Size.Y, Position1.Z - Size.Z);
		}



		public Position RotateX(int Count)
		{
			int modulo;
			Position newPosition;

			newPosition = this;

			modulo = Count % 4;
			if (modulo < 0) modulo += 4;

			for (int t = 0; t < modulo; t++)
			{
				newPosition = new Position(newPosition.X, -(newPosition.Z+1), newPosition.Y);
			}

			return newPosition;
		}
		public Position RotateY(int Count)
		{
			int modulo;
			Position newPosition;

			newPosition = this;

			modulo = Count % 4;
			if (modulo < 0) modulo += 4;

			for (int t = 0; t < modulo; t++)
			{
				newPosition = new Position(newPosition.Z, newPosition.Y, -(newPosition.X+1));
			}

			return newPosition;
		}
		public Position RotateZ(int Count)
		{
			int modulo;
			Position newPosition;

			newPosition = this;

			modulo = Count % 4;
			if (modulo < 0) modulo += 4;

			for (int t = 0; t < modulo; t++)
			{
				newPosition = new Position(-(newPosition.Y+1), newPosition.X, newPosition.Z);
			}

			return newPosition;
		}

	}
}
