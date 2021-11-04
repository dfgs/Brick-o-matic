using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Import : Primitive,IImport
	{


		public IScene Scene
		{
			get;
			set;
		}


		public Import()
		{
		}
		public Import(Position Position):base(Position)
		{
			
		}
		

		public override Box GetBoundingBox(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));

			if (this.Scene == null) childBox= new Box();
			else childBox = this.Scene.GetBoundingBox();
			
			return new Box(Position + childBox.Position, childBox.Size);

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			if (Scene == null) throw new ArgumentNullException(nameof(Scene));

			if (this.Scene == null) yield break;

			foreach (Brick brick in this.Scene.Build())
			{
				yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
			}
		}
		

	}
}
