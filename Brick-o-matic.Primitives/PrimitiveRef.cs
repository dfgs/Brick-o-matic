using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class PrimitiveRef : Primitive,IPrimitiveRef
	{


		public string Name
		{
			get;
			set;
		}


		public PrimitiveRef()
		{
		}
		public PrimitiveRef(Position Position):base(Position)
		{
			
		}
		

		public override Box GetBoundingBox(IScene Scene)
		{
			IPrimitive referencedPrimitive;
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));

			referencedPrimitive = Scene.GetResource(Name) as IPrimitive;
			if (referencedPrimitive==null) throw new InvalidOperationException($"Reference to primitive {Name} was not found");

			childBox = referencedPrimitive.GetBoundingBox(Scene);

			return new Box(Position + childBox.Position, childBox.Size);

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			IPrimitive referencedPrimitive;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));

			referencedPrimitive = Scene.GetResource(Name) as IPrimitive;
			if (referencedPrimitive == null) throw new InvalidOperationException($"Reference to primitive {Name} was not found");

			foreach (Brick brick in referencedPrimitive.Build(Scene))
			{
				yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
			}
		}
		

	}
}
