using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Part : Primitive,IPart
	{

		IEnumerable<IPrimitive> IPart.Items => items;

		public int Count
		{
			get { return items.Count; }
		}

		private List<IPrimitive> items;
		public IEnumerable<IPrimitive> Items
		{
			get => items;
		}


		public Part()
		{
			items = new List<IPrimitive>();
		}
		public Part(Position Position):base(Position)
		{
			items = new List<IPrimitive>();
		}
		public void Add(IPrimitive Child)
		{
			if (Child == null) throw new ArgumentNullException(nameof(Child));
			items.Add(Child);
		}

		public override Model Build()
		{
			List<BoxGeometry> geometryItems;
			Plane negativeX;
			Plane positiveX;
			Plane negativeY;
			Plane positiveY;
			Plane negativeZ;
			Plane positiveZ;

			geometryItems = new List<BoxGeometry>();
			foreach(IPrimitive item in this.items)
			{
				foreach(BoxGeometry childGeometry in item.Build().Items)
				{
					negativeX = new Plane(childGeometry.NegativeX.Position + Position.X, childGeometry.NegativeX.Direction, childGeometry.NegativeX.Color);
					positiveX = new Plane(childGeometry.PositiveX.Position + Position.X, childGeometry.PositiveX.Direction, childGeometry.PositiveX.Color);
					negativeY = new Plane(childGeometry.NegativeY.Position + Position.Y, childGeometry.NegativeY.Direction, childGeometry.NegativeY.Color);
					positiveY = new Plane(childGeometry.PositiveY.Position + Position.Y, childGeometry.PositiveY.Direction, childGeometry.PositiveY.Color);
					negativeZ = new Plane(childGeometry.NegativeZ.Position + Position.Z, childGeometry.NegativeZ.Direction, childGeometry.NegativeZ.Color);
					positiveZ = new Plane(childGeometry.PositiveZ.Position + Position.Z, childGeometry.PositiveZ.Direction, childGeometry.PositiveZ.Color);

					geometryItems.Add(new BoxGeometry(negativeX,positiveX,negativeY,positiveY,negativeZ,positiveZ)) ;
				}
			}

			return new Model(geometryItems.ToArray());
		}
		

	}
}
