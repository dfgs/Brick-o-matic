using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class TileMap : Primitive,ITileMap
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
		public Size TileSize
		{
			get;
			set;
		}
		

		public TileMap()
		{
			items = new List<IPrimitive>();
			TileSize = new Size(16,16,16);
		}
		public TileMap(Position Position):base(Position)
		{
			items = new List<IPrimitive>();
			TileSize = new Size(16, 16, 16);
		}
		public void Add(IPrimitive Child)
		{
			if (Child == null) throw new ArgumentNullException(nameof(Child));
			items.Add(Child);
		}

		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			int minX=int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (Count == 0) return new Box(Position, new Size());
			
			foreach (IPrimitive item in this.items)
			{
				childBox = item.GetBoundingBox(ResourceProvider);
				minX = System.Math.Min(minX, item.Position.X * TileSize.X);
				minY = System.Math.Min(minY, item.Position.Y * TileSize.Y);
				minZ = System.Math.Min(minZ, item.Position.Z * TileSize.Z);

				maxX = System.Math.Max(maxX, item.Position.X * TileSize.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, item.Position.Y * TileSize.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, item.Position.Z * TileSize.Z + childBox.Size.Z);
			}

			return new Box(Position + new Position(minX, minY , minZ ), new Size(maxX-minX,maxY-minY,maxZ-minZ));

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			Position childPosition;


			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			
			foreach (IPrimitive item in this.items)
			{
				childPosition = new Position(item.Position.X * TileSize.X, item.Position.Y * TileSize.Y, item.Position.Z * TileSize.Z);
				foreach (Brick brick in item.Build(ResourceProvider))
				{
					yield return new Brick(this.Position + brick.Position - item.Position+childPosition, brick.Size,brick.Color);
				}
			}

		}

		

	}
}
