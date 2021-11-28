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

		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
	
			childBox = new Box(items.Select(item => item.GetBoundingBox(ResourceProvider)));

			return new Box(Position + childBox.Position, childBox.Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
	
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			foreach (IPrimitive item in this.items)
			{
				foreach(Brick brick in item.Build(ResourceProvider))
				{
					yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
				}
			}

		}

		public override void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			foreach (IPrimitive item in this.items)
			{
				item.Validate(ResourceProvider, Locker);
			}
		}



	}
}
