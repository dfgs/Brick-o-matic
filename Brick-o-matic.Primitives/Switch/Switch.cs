using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Switch : Primitive, ISwitch
	{
		public string Variable
		{
			get;
			set;
		}

		public int Count
		{
			get { return items.Count; }
		}

		private List<IWhen> items;
		public IEnumerable<IWhen> Items
		{
			get => items;
		}


		public Switch()
		{
			items = new List<IWhen>();
		}
		public Switch(Position Position) : base(Position)
		{
			items = new List<IWhen>();
		}
		public void Add(IWhen Item)
		{
			if (Item == null) throw new ArgumentNullException(nameof(Item));
			items.Add(Item);
		}
		public IPrimitive Select(IResourceProvider ResourceProvider)
		{
			Variable variable;

			if (Variable == null) return null;
			if (!ResourceProvider.TryGetResource<Variable>(Variable, out variable)) return null;

			foreach(IWhen item in items)
			{
				if (item.Value == variable.Value) return item.Return;
			}
			return null;
		}
		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;
			IPrimitive childItem;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			childItem = Select(ResourceProvider);
			if (childItem == null) return new Box(Position, new Size());
			childBox = childItem.GetBoundingBox(ResourceProvider);
			return new Box(Position + childBox.Position, childBox.Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			IPrimitive childItem;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			childItem = Select(ResourceProvider);
			if (childItem == null) yield break;

			foreach (Brick brick in childItem.Build(ResourceProvider))
			{
				yield return new Brick(this.Position + brick.Position, brick.Size, brick.Color);
			}
			
		}
		public override void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			foreach(When when in items)
			{
				when.Return?.Validate(ResourceProvider, Locker);
			}
		}


	}

}
