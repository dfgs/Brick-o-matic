using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public enum PlaneDirections { NegativeX,PositiveX,NegativeY,PositiveY,NegativeZ,PositiveZ };
	public struct Plane
	{
		public Color Color
		{
			get;
			set;
		}

		public int Position
		{
			get;
			set;
		}

		public PlaneDirections Direction
		{
			get;
			set;
		}
		public Plane(int Position, PlaneDirections Direction,Color Color)
		{
			this.Position = Position;this.Direction = Direction; this.Color = Color;
		}

		public override string ToString()
		{
			return $"({Position},{Color})";
		}
	}
}
