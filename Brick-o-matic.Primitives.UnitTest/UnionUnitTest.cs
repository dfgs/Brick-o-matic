using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;
using System.Collections.Generic;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class UnionUnitTest
	{

		private static Brick FindBrick(IEnumerable<Brick> Items, Position Position, Size Size)
		{
			return Items.FirstOrDefault(item => (item.Position == Position) && (item.Size == Size));
		}

		[TestMethod]
		public void ShouldInstanciate()
		{
			Union csg;

			csg = new Union();
			Assert.IsNotNull(csg);
			Assert.AreEqual(new Position(), csg.Position);

			csg = new Union(new Position(1, 2, 3));
			Assert.IsNotNull(csg);
			Assert.AreEqual(new Position(1, 2, 3), csg.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoItems()
		{
			Union csg;
			IBox box;

			csg = new Union(new Position(1,2,3));
			Assert.IsNotNull(csg);
			box = csg.GetBoundingBox(new Scene());
			Assert.AreEqual(1, box.Position.X);
			Assert.AreEqual(2, box.Position.Y);
			Assert.AreEqual(3, box.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}
		[TestMethod]
		public void ShouldReturnItemABoundingBoxWhenItemBNotSet()
		{
			Union csg;
			IBox box;

			csg = new Union(new Position(1, 2, 3));
			Assert.IsNotNull(csg);
			csg.ItemA = new Brick() { Size=new Size(2) };

			box = csg.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1,2,3), box.Position);
			Assert.AreEqual(new Size(2, 2, 2), box.Size);
		}
		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Union csg;
			Part part;
			Scene scene;
			IBox box;

			scene = new Scene();

			part = new Part();
			part.Add(new Brick(new Position(0, 0, 0), new Size(1)));
			part.Add(new Brick(new Position(2, 0, 0), new Size(1)));
			part.Add(new Brick(new Position(0, 2, 0), new Size(1)));
			part.Add(new Brick(new Position(2, 2, 0), new Size(1)));

			csg = new Union(); csg.Position = new Position(1, 2, 3);
			csg.ItemA = new Brick(new Position(0, 0, 0), new Size(3, 3, 1));
			csg.ItemB = part;

			box = csg.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1, 2, 3), box.Position);
			Assert.AreEqual(new Size(3, 3, 1), box.Size);
		}
		

		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			Union part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();

			part = new Union();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}
		[TestMethod]
		public void ShouldGetItemABricksWhenItembNotSet()
		{
			Union csg;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();

			csg = new Union(); csg.Position = new Position(1, 2, 3);
			csg.ItemA = new Brick(new Position(0, 0, 0), new Size(3, 3, 1));

			bricks = csg.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(1, 2, 3), bricks[0].Position);
			Assert.AreEqual(new Size(3, 3, 1), bricks[0].Size);

		}
		[TestMethod]
		public void ShouldGetBricks()
		{
			Union csg;
			Part part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();

			part = new Part();
			part.Add(new Brick(new Position(0, 0, 0), new Size(1)));
			part.Add(new Brick(new Position(2, 0, 0), new Size(1)));
			part.Add(new Brick(new Position(0, 2, 0), new Size(1)));
			part.Add(new Brick(new Position(2, 2, 0), new Size(1)));

			csg = new Union();csg.Position = new Position(1, 2, 3);
			csg.ItemA = new Brick(new Position(0,0,0),new Size(3,3,1));
			csg.ItemB = part;

			bricks = csg.Build(scene).ToArray();
			Assert.AreEqual(9, bricks.Length);
			Assert.IsNotNull(FindBrick(bricks, csg.Position + new Position(1, 0, 0), new Size(1)));
			Assert.IsNotNull(FindBrick(bricks, csg.Position + new Position(0, 1, 0), new Size(1)));
			Assert.IsNotNull(FindBrick(bricks, csg.Position + new Position(1, 1, 0), new Size(1)));
			Assert.IsNotNull(FindBrick(bricks, csg.Position + new Position(2, 1, 0), new Size(1)));
			Assert.IsNotNull(FindBrick(bricks, csg.Position + new Position(1, 2, 0), new Size(1)));
		}
		




	}
}
