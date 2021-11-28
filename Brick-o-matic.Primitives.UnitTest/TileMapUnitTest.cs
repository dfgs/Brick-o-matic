using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{



	[TestClass]
	public class TileMapUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			TileMap TileMap;

			TileMap = new TileMap();
			Assert.IsNotNull(TileMap);
			Assert.AreEqual(new Position(), TileMap.Position);

			TileMap = new TileMap(new Position(1, 2, 3));
			Assert.IsNotNull(TileMap);
			Assert.AreEqual(new Position(1, 2, 3), TileMap.Position);
		}
		[TestMethod]
		public void ShouldAddChildItem()
		{
			TileMap TileMap;
			Brick b;

			TileMap = new TileMap();
			Assert.IsNotNull(TileMap);
			Assert.AreEqual(0, TileMap.Count);
			b = new Brick();
			TileMap.Add(b);
			Assert.AreEqual(1, TileMap.Count);

		}
		[TestMethod]
		public void ShouldNotAddNullChildItem()
		{
			TileMap TileMap;

			TileMap = new TileMap();
			Assert.IsNotNull(TileMap);
			Assert.AreEqual(0, TileMap.Count);
			Assert.ThrowsException<ArgumentNullException>(()=> TileMap.Add(null));

		}
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoItems()
		{
			TileMap TileMap;
			IBox box;

			TileMap = new TileMap(new Position(1,2,3));
			Assert.IsNotNull(TileMap);
			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(1, box.Position.X);
			Assert.AreEqual(2, box.Position.Y);
			Assert.AreEqual(3, box.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			TileMap TileMap;
			IBox box;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(20, box.Position.X);
			Assert.AreEqual(10, box.Position.Y);
			Assert.AreEqual(10, box.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), box.Size);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 1, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(10, box.Position.X);
			Assert.AreEqual(20, box.Position.Y);
			Assert.AreEqual(10, box.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), box.Size);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 0, 1), new Size(10, 10, 10));
			TileMap.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(10, box.Position.X);
			Assert.AreEqual(10, box.Position.Y);
			Assert.AreEqual(20, box.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), box.Size);


			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);
			b = new Brick(new Position(2, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(20, box.Position.X);
			Assert.AreEqual(10, box.Position.Y);
			Assert.AreEqual(10, box.Position.Z);
			Assert.AreEqual(new Size(20, 10, 10), box.Size);


		}


		[TestMethod]
		public void ShouldReturnNestedBoudingBox()
		{
			TileMap TileMap;
			IBox box;
			Part p;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0,0,0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(10, box.Position.X);
			Assert.AreEqual(10, box.Position.Y);
			Assert.AreEqual(10, box.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), box.Size);

		

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0, 0, 0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);
			p = new Part(new Position(1, 1, 1));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			box = TileMap.GetBoundingBox(new Scene());
			Assert.AreEqual(10, box.Position.X);
			Assert.AreEqual(10, box.Position.Y);
			Assert.AreEqual(10, box.Position.Z);
			Assert.AreEqual(new Size(20, 20, 20), box.Size);

		}


		[TestMethod]
		public void ShouldBuild()
		{
			TileMap TileMap;
			Brick[] bricks;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(20, bricks[0].Position.X);
			Assert.AreEqual(10, bricks[0].Position.Y);
			Assert.AreEqual(10, bricks[0].Position.Z);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 1, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(10, bricks[0].Position.X);
			Assert.AreEqual(20, bricks[0].Position.Y);
			Assert.AreEqual(10, bricks[0].Position.Z);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 0, 1), new Size(10, 10, 10));
			TileMap.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(10, bricks[0].Position.X);
			Assert.AreEqual(10, bricks[0].Position.Y);
			Assert.AreEqual(20, bricks[0].Position.Z);


			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);
			b = new Brick(new Position(2, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(2, bricks.Length);
			Assert.AreEqual(20, bricks[0].Position.X);
			Assert.AreEqual(10, bricks[0].Position.Y);
			Assert.AreEqual(10, bricks[0].Position.Z);


		}
		[TestMethod]
		public void ShouldBuildNestedItems()
		{
			TileMap TileMap;
			Brick[] bricks;
			Part p;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0, 0, 0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(2, bricks.Length);
			Assert.AreEqual(10, bricks[0].Position.X);
			Assert.AreEqual(10, bricks[0].Position.Y);
			Assert.AreEqual(10, bricks[0].Position.Z);
			Assert.AreEqual(19, bricks[1].Position.X);
			Assert.AreEqual(19, bricks[1].Position.Y);
			Assert.AreEqual(19, bricks[1].Position.Z);



			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0, 0, 0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);
			p = new Part(new Position(1, 1, 1));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			bricks = TileMap.Build(new Scene()).ToArray();
			Assert.AreEqual(4, bricks.Length);
			Assert.AreEqual(10, bricks[0].Position.X);
			Assert.AreEqual(10, bricks[0].Position.Y);
			Assert.AreEqual(10, bricks[0].Position.Z);
			Assert.AreEqual(19, bricks[1].Position.X);
			Assert.AreEqual(19, bricks[1].Position.Y);
			Assert.AreEqual(19, bricks[1].Position.Z);

			Assert.AreEqual(20, bricks[2].Position.X);
			Assert.AreEqual(20, bricks[2].Position.Y);
			Assert.AreEqual(20, bricks[2].Position.Z);
			Assert.AreEqual(29, bricks[3].Position.X);
			Assert.AreEqual(29, bricks[3].Position.Y);
			Assert.AreEqual(29, bricks[3].Position.Z);

		}



		/*[TestMethod]
		public void ShouldReturnFlatBoundingCSGNodeWhenHasNoItems()
		{
			TileMap TileMap;
			ICSGNode node;

			TileMap = new TileMap(new Position(1, 2, 3));
			Assert.IsNotNull(TileMap);
			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(1, node.BoundingBox.Position.X);
			Assert.AreEqual(2, node.BoundingBox.Position.Y);
			Assert.AreEqual(3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);
		}

		[TestMethod]
		public void ShouldReturnBoundingCSGNode()
		{
			TileMap TileMap;
			ICSGNode node;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(20, node.BoundingBox.Position.X);
			Assert.AreEqual(10, node.BoundingBox.Position.Y);
			Assert.AreEqual(10, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 1, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(10, node.BoundingBox.Position.X);
			Assert.AreEqual(20, node.BoundingBox.Position.Y);
			Assert.AreEqual(10, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);




			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(0, 0, 1), new Size(10, 10, 10));
			TileMap.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(10, node.BoundingBox.Position.X);
			Assert.AreEqual(10, node.BoundingBox.Position.Y);
			Assert.AreEqual(20, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);


			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			b = new Brick(new Position(1, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);
			b = new Brick(new Position(2, 0, 0), new Size(10, 10, 10));
			TileMap.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(20, node.BoundingBox.Position.X);
			Assert.AreEqual(10, node.BoundingBox.Position.Y);
			Assert.AreEqual(10, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(20, 10, 10), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);


		}


		[TestMethod]
		public void ShouldReturnNestedBoundingCSGNode()
		{
			TileMap TileMap;
			ICSGNode node;
			Part p;
			Brick b;

			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0, 0, 0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(10, node.BoundingBox.Position.X);
			Assert.AreEqual(10, node.BoundingBox.Position.Y);
			Assert.AreEqual(10, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(10, 10, 10), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);



			TileMap = new TileMap(new Position(10, 10, 10));
			Assert.IsNotNull(TileMap);
			TileMap.TileSize = new Size(10, 10, 10);
			p = new Part(new Position(0, 0, 0));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);
			p = new Part(new Position(1, 1, 1));
			TileMap.Add(p);
			b = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			p.Add(b);
			b = new Brick(new Position(9, 9, 9), new Size(1, 1, 1));
			p.Add(b);

			node = TileMap.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(10, node.BoundingBox.Position.X);
			Assert.AreEqual(10, node.BoundingBox.Position.Y);
			Assert.AreEqual(10, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(20, 20, 20), node.BoundingBox.Size);
			Assert.AreEqual("TileMap", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(TileMap, node.Primitive);

		}
		*/



	}
}
