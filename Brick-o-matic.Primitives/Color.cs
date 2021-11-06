using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public struct Color:IEquatable<Color>,IColor
	{
		public byte R
		{
			get;
			private set;
		}
		public byte G
		{
			get;
			private set;
		}
		public byte B
		{
			get;
			private set;
		}
		
		public Color(byte R, byte G, byte B)
		{
			this.R = R;this.G = G;this.B = B;
		}

		public override string ToString()
		{
			return $"({R},{G},{B})";
		}

		public void GetComponents(IResourceProvider ResourceProvider, out byte R, out byte G, out byte B)
		{
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			R = this.R;G = this.G;B = this.B;
		}

		public override bool Equals(object obj)
		{
			if (obj is Color otherColor) return ((otherColor.R == R) && (otherColor.G == G) && (otherColor.B == B));
			return false;
		}
		public bool Equals(Color OtherColor)
		{
			return ((OtherColor.R == R) && (OtherColor.G == G) && (OtherColor.B == B));
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		public static bool operator ==(Color Color1, Color Color2)
		{
			return ((Color1.R == Color2.R) && (Color1.G == Color2.G) && (Color1.B == Color2.B));
		}
		public static bool operator !=(Color Color1, Color Color2)
		{
			return ((Color1.R != Color2.R) || (Color1.G != Color2.G) || (Color1.B != Color2.B));
		}

		

	}
}
