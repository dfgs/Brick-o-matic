using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Brick : Primitive, IBox
	{
		
		
		public Size Size
		{
			get;
			set;
		}
		public IColor Color
		{
			get;
			set;
		}

		public Brick()
		{
			this.Size = new Size(1, 1, 1);this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position):base(Position)
		{
			this.Size = new Size(1, 1, 1); this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position, Size Size) : base(Position)
		{
			this.Size = Size; this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position, Size Size,IColor Color) : base(Position)
		{
			this.Size = Size; this.Color = Color;
		}

		public override Box GetBoundingBox(IScene Scene)
		{
			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			return new Box(Position, Size);
		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			Color color;
			byte R, G, B;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			
			// We must replace colorref by color, in order to prevent resource not found in import
			this.Color.GetComponents(Scene, out R, out G, out B);
			color = new Color(R, G, B);
			yield return new Brick(Position, Size, color); ;
		}
	}
}
