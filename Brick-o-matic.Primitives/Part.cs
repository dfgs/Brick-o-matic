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

			geometryItems = new List<BoxGeometry>();
			foreach(IPrimitive item in this.items)
			{
				foreach(BoxGeometry childGeometry in item.Build().Items)
				{
					geometryItems.Add(new BoxGeometry(childGeometry.Position + Position, childGeometry.Size, childGeometry.Color));
				}
			}

			return new Model(geometryItems.ToArray());
		}
		

	}
}
