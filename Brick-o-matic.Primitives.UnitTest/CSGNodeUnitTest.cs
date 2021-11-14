using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class CSGNodeUnitTest
	{
		[TestMethod]
		public void ShouldAdd()
		{
			CSGNode node;

			node = new CSGNode();
			Assert.IsNotNull(node);
			Assert.AreEqual(0, node.Count);

			node.Add(new CSGNode());
			Assert.AreEqual(1, node.Count);
			node.Add(new CSGNode());
			Assert.AreEqual(2, node.Count);
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			CSGNode node, a, b, c;
			ICSGNode[] items;

			node = new CSGNode();
			a = new CSGNode();
			b = new CSGNode();
			c = new CSGNode();
			Assert.AreEqual(0, node.Count);

			node.Add(a);
			node.Add(b);
			node.Add(c);
			Assert.AreEqual(3, node.Count);

			items = node.Nodes.ToArray();
			Assert.AreEqual(a, items[0]);
			Assert.AreEqual(b, items[1]);
			Assert.AreEqual(c, items[2]);

		}

		[TestMethod]
		public void ShouldNotGetIntersectionsWithBrick()
		{
			Brick brick;
			ICSGNode node;
			Box otherBox;
			ICSGNode[] intersectNodes;

			brick = new Brick(new Position(), new Size(10, 1, 1));
			otherBox = new Box(new Position(5, 2, 0), new Size(1, 1, 1));
			node = brick.BuildCSGNode(new Scene());

			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(0, intersectNodes.Length);


		}

		[TestMethod]
		public void ShouldGetIntersectionsWithBrick()
		{
			Brick brick;
			ICSGNode node;
			Box otherBox;
			ICSGNode[] intersectNodes;

			brick = new Brick(new Position(), new Size(10, 1, 1));
			otherBox = new Box(new Position(5, 0, 0),new Size(1,1,1));
			node = brick.BuildCSGNode(new Scene());

			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick, intersectNodes[0].Primitive);
		}

		[TestMethod]
		public void ShouldNotGetIntersectionsWithPart()
		{
			Part part;
			Brick brick1, brick2;
			ICSGNode node;
			Box otherBox;
			ICSGNode[] intersectNodes;

			brick1 = new Brick(new Position(), new Size(10, 1, 1));
			brick2 = new Brick(new Position(), new Size(1, 10, 1));
			part = new Part();
			part.Add(brick1); part.Add(brick2);

			node = part.BuildCSGNode(new Scene());

			otherBox = new Box(new Position(5, 5, 0), new Size(1, 1, 1));
			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(0, intersectNodes.Length);
			
		}

		[TestMethod]
		public void ShouldGetIntersectionsWithPart()
		{
			Part part;
			Brick brick1,brick2;
			ICSGNode node;
			Box otherBox;
			ICSGNode[] intersectNodes;

			brick1 = new Brick(new Position(), new Size(10, 1, 1));
			brick2 = new Brick(new Position(), new Size(1, 10, 1));
			part = new Part();
			part.Add(brick1);part.Add(brick2);

			node = part.BuildCSGNode(new Scene());

			otherBox = new Box(new Position(5, 0, 0), new Size(1, 1, 1));
			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick1, intersectNodes[0].Primitive);

			otherBox = new Box(new Position(0, 5, 0), new Size(1, 1, 1));
			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick2, intersectNodes[0].Primitive);
		}

	}
}
