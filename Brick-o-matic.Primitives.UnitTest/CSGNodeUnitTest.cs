using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class CSGNodeUnitTest
	{
		private static ICSGNode FindNode(IEnumerable<ICSGNode> Items, Position Position, Size Size)
		{
			return Items.FirstOrDefault(item => (item.BoundingBox.Position == Position) && (item.BoundingBox.Size == Size));
		}

		

		/*[TestMethod]
		public void ShouldAddBrick()
		{
			CSGNode node;

			node = new CSGNode();
			Assert.IsNotNull(node);
			Assert.AreEqual(0, node.BrickCount);

			node.Add(new Brick());
			Assert.AreEqual(1, node.BrickCount);
			node.Add(new Brick());
			Assert.AreEqual(2, node.BrickCount);
		}

		[TestMethod]
		public void ShouldEnumerateBricks()
		{
			CSGNode node;
			Brick a, b, c;
			Brick[] items;

			node = new CSGNode();
			a = new Brick();
			b = new Brick();
			c = new Brick();
			Assert.AreEqual(0, node.BrickCount);

			node.Add(a);
			node.Add(b);
			node.Add(c);
			Assert.AreEqual(3, node.BrickCount);

			items = node.Bricks.ToArray();
			Assert.AreEqual(a, items[0]);
			Assert.AreEqual(b, items[1]);
			Assert.AreEqual(c, items[2]);

		}//*/

		[TestMethod]
		public void ShouldBuild_SplitBoudingBox_OnXAxis()
		{
			CSGNode node;
			Part part;

			part = new Part();
			part.Add(new Brick(new Position(-1, 0, 0), new Size(1, 1, 1)));
			part.Add(new Brick(new Position(1, 0, 0), new Size(1, 1, 1)));

			node = new CSGNode();
			Assert.IsNull(node.Brick);
			node.Build( part.Build(new Scene()));

			Assert.IsNull(node.Brick);
			Assert.AreEqual(new Position(-1, 0, 0), node.BoundingBox.Position);
			Assert.AreEqual(new Size(3, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual(2, node.NodeCount);

			Assert.IsNotNull(node.Nodes.ElementAt(0).Brick);
			Assert.AreEqual(new Position(-1, 0, 0), node.Nodes.ElementAt(0).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(0).BoundingBox.Size);

			Assert.IsNotNull(node.Nodes.ElementAt(1).Brick);
			Assert.AreEqual(new Position(1, 0, 0), node.Nodes.ElementAt(1).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(1).BoundingBox.Size);
		}

		[TestMethod]
		public void ShouldBuild_SplitBoudingBox_OnYAxis()
		{
			CSGNode node;
			Part part;

			part = new Part();
			part.Add(new Brick(new Position(0, -1, 0), new Size(1, 1, 1)));
			part.Add(new Brick(new Position(0, 1, 0), new Size(1, 1, 1)));

			node = new CSGNode();
			Assert.IsNull(node.Brick);
			node.Build(part.Build(new Scene()));

			Assert.AreEqual(new Position(0, -1, 0), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 3, 1), node.BoundingBox.Size);
			Assert.AreEqual(2, node.NodeCount);

			Assert.IsNotNull(node.Nodes.ElementAt(0).Brick);
			Assert.AreEqual(new Position(0, -1, 0), node.Nodes.ElementAt(0).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(0).BoundingBox.Size);

			Assert.IsNotNull(node.Nodes.ElementAt(0).Brick);
			Assert.AreEqual(new Position(0, 1, 0), node.Nodes.ElementAt(1).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(1).BoundingBox.Size);
		}

		[TestMethod]
		public void ShouldBuild_SplitBoudingBox_OnZAxis()
		{
			CSGNode node;
			Part part;

			part = new Part();
			part.Add(new Brick(new Position(0,0, -1), new Size(1, 1, 1)));
			part.Add(new Brick(new Position(0, 0, 1), new Size(1, 1, 1)));

			node = new CSGNode();
			Assert.IsNull(node.Brick);
			node.Build(part.Build(new Scene()));

			Assert.AreEqual(new Position(0, 0, -1), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 3), node.BoundingBox.Size);
			Assert.AreEqual(2, node.NodeCount);

			Assert.IsNotNull(node.Nodes.ElementAt(0).Brick);
			Assert.AreEqual(new Position(0, 0, -1), node.Nodes.ElementAt(0).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(0).BoundingBox.Size);

			Assert.IsNotNull(node.Nodes.ElementAt(0).Brick);
			Assert.AreEqual(new Position(0, 0, 1), node.Nodes.ElementAt(1).BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.Nodes.ElementAt(1).BoundingBox.Size);
		}

		[TestMethod]
		public void ShouldBuild()
		{
			CSGNode node;
			Part part;

			part = new Part();
			for (int z = 0; z < 10; z++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int x = 0; x < 10; x++)
					{
						part.Add(new Brick(new Position(x, y, z), new Size(1, 1, 1)));
					}
				}
			}
			
			

			node = new CSGNode();
			Assert.IsNull(node.Brick);
			node.Build(part.Build(new Scene()));
			Assert.AreEqual(2, node.NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(0).NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(0).Nodes.ElementAt(0).NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(0).Nodes.ElementAt(0).Nodes.ElementAt(0).NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(0).Nodes.ElementAt(0).Nodes.ElementAt(1).NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(0).Nodes.ElementAt(1).NodeCount);
			Assert.AreEqual(2, node.Nodes.ElementAt(1).NodeCount);
		}
		[TestMethod]
		public void ShouldNotGoInInfiniteLoopWhileBuilding()
		{
			CSGNode node;
			Part part;

			part = new Part();
			part.Add(new Brick(new Position(1, 2, 3), new Size(2)));
			part.Add(new Brick(new Position(1, 2, 3), new Size(2)));
			part.Add(new Brick(new Position(1, 2, 3), new Size(2)));


			node = new CSGNode();
			Assert.IsNull(node.Brick);
			node.Build(part.Build(new Scene()));
			Assert.AreEqual(2, node.NodeCount);
			
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
			node = new CSGNode();
			node.Build(brick.Build(new Scene()));

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
			node = new CSGNode();
			node.Build(brick.Build(new Scene()));

			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick.Position, intersectNodes[0].BoundingBox.Position);
			Assert.AreEqual(brick.Size, intersectNodes[0].BoundingBox.Size);
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

			node = new CSGNode();
			node.Build(part.Build(new Scene()));

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

			node = new CSGNode();
			node.Build(part.Build(new Scene()));

			otherBox = new Box(new Position(5, 0, 0), new Size(1, 1, 1));
			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick1.Position, intersectNodes[0].BoundingBox.Position);
			Assert.AreEqual(brick1.Size, intersectNodes[0].BoundingBox.Size);

			otherBox = new Box(new Position(0, 5, 0), new Size(1, 1, 1));
			intersectNodes = node.GetIntersections(otherBox).ToArray();
			Assert.AreEqual(1, intersectNodes.Length);
			Assert.AreEqual(brick2.Position, intersectNodes[0].BoundingBox.Position);
			Assert.AreEqual(brick2.Size, intersectNodes[0].BoundingBox.Size);
		}


		
		[TestMethod]
		public void ShouldSplitWhenBricksDoesntIntersect()
		{
			Brick brick1, brick2;
			ICSGNode node1,node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(1, 1, 1));
			brick2 = new Brick(new Position(3, 3, 3), new Size(2, 2, 2));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			Assert.IsFalse(node1.SplitTag);

			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(0, node2.NodeCount);
			Assert.IsFalse(node1.SplitTag);

		}

		[TestMethod]
		public void ShouldSplitWhenBrickesIntersect_StairShape()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(2, 2, 2));
			brick2 = new Brick(new Position(1, 1, 1), new Size(2, 2, 2));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(8, node1.NodeCount);
			Assert.IsTrue(node1.SplitTag);
			// parts of box 1
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 0, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 1, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 1, 0), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 0, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 1, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsTrue(FindNode(node1.Nodes, new Position(1, 1, 1), new Size(1, 1, 1)).SplitTag);    // intersection with box2


			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(8, node2.NodeCount);
			Assert.IsTrue(node2.SplitTag);
			// parts of box 2
			Assert.IsTrue(FindNode(node2.Nodes, new Position(1, 1, 1), new Size(1, 1, 1)).SplitTag);    // intersection with box1
			Assert.IsFalse(FindNode(node2.Nodes, new Position(2, 1, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(1, 2, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(2, 2, 1), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node2.Nodes, new Position(1, 1, 2), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(2, 1, 2), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(1, 2, 2), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(2, 2, 2), new Size(1, 1, 1)).SplitTag);//*/

		}
		[TestMethod]
		public void ShouldSplitWhenBrickesIntersect_SameBrickes()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(2, 2, 2));
			brick2 = new Brick(new Position(0, 0, 0), new Size(2, 2, 2));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(1, node1.NodeCount);
			Assert.IsTrue(node1.SplitTag);
			Assert.IsTrue(node1.Nodes.ElementAt(0).SplitTag);

			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(1, node2.NodeCount);
			Assert.IsTrue(node2.SplitTag);
			Assert.IsTrue(node2.Nodes.ElementAt(0).SplitTag);

		}

		[TestMethod]
		public void ShouldSplitWhenBrickesIntersect_HorizontalShape()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(2, 2, 2));
			brick2 = new Brick(new Position(1, 0, 0), new Size(2, 2, 2));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(2, node1.NodeCount);
			Assert.IsTrue(node1.SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 0), new Size(1, 2, 2)).SplitTag);
			Assert.IsTrue(FindNode(node1.Nodes, new Position(1, 0, 0), new Size(1, 2, 2)).SplitTag);

			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(2, node2.NodeCount);
			Assert.IsTrue(node2.SplitTag);
			Assert.IsTrue(FindNode(node2.Nodes, new Position(1, 0, 0), new Size(1, 2, 2)).SplitTag);
			Assert.IsFalse(FindNode(node2.Nodes, new Position(2, 0, 0), new Size(1, 2, 2)).SplitTag);

		}


		[TestMethod]
		public void ShouldSplitWhenBrickesIntersect_Brick2IntoBrick1()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(3, 3, 3));
			brick2 = new Brick(new Position(1, 1, 1), new Size(1, 1, 1));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(27, node1.NodeCount);
			Assert.IsTrue(node1.SplitTag);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						if ((x==1) && (y==1) && (z==1)) Assert.IsTrue(FindNode(node1.Nodes, new Position(x, y, z), new Size(1, 1, 1)).SplitTag);
						else Assert.IsFalse(FindNode(node1.Nodes, new Position(x, y, z), new Size(1, 1, 1)).SplitTag);
					}
				}
			}

			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(1, node2.NodeCount);
			Assert.IsTrue(node2.SplitTag);
			Assert.IsTrue(FindNode(node2.Nodes, new Position(1, 1, 1), new Size(1, 1, 1)).SplitTag);



		}


		[TestMethod]
		public void ShouldSplitWhenBrickesIntersect_TShape()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;

			brick1 = new Brick(new Position(0, 0, 0), new Size(3, 2, 3));
			brick2 = new Brick(new Position(1, 1, 1), new Size(1, 2, 1));
			node1 = new CSGNode();
			node2 = new CSGNode();

			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(18, node1.NodeCount);
			Assert.IsTrue(node1.SplitTag);
			// parts of box 1
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 0, 2), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 0, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 0, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 0, 2), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 0, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 0, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 0, 2), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 1, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 1, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(0, 1, 2), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 1, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsTrue(FindNode(node1.Nodes, new Position(1, 1, 1), new Size(1, 1, 1)).SplitTag); // intersection with box2
			Assert.IsFalse(FindNode(node1.Nodes, new Position(1, 1, 2), new Size(1, 1, 1)).SplitTag);

			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 1, 0), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 1, 1), new Size(1, 1, 1)).SplitTag);
			Assert.IsFalse(FindNode(node1.Nodes, new Position(2, 1, 2), new Size(1, 1, 1)).SplitTag);



			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(2, node2.NodeCount);
			Assert.IsTrue(node2.SplitTag);
			// parts of box 2
			Assert.IsTrue(FindNode(node2.Nodes, new Position(1, 1, 1), new Size(1, 1, 1)).SplitTag);    // intersection with box1
			Assert.IsFalse(FindNode(node2.Nodes, new Position(1, 2, 1), new Size(1, 1, 1)).SplitTag);

		}


		[TestMethod]
		public void ShouldParseNodesFirstPredicate()
		{
			Brick brick1, brick2;
			ICSGNode node1, node2;
			ICSGNode[] nodes;

			brick1 = new Brick(new Position(0, 0, 0), new Size(2, 2, 2));
			brick2 = new Brick(new Position(1, 0, 0), new Size(2, 2, 2));
			node1 = new CSGNode();
			node2 = new CSGNode();


			node1.Build(brick1.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);
			node2.Build(brick2.Build(new Scene()));
			Assert.AreEqual(0, node1.NodeCount);

			node1.Split(brick2.GetBoundingBox(new Scene()));
			Assert.AreEqual(2, node1.NodeCount);

			nodes = node1.ParseNodes((node) => ParseActions.Yield).ToArray();
			Assert.AreEqual(3, nodes.Length);
			nodes = node1.ParseNodes((node) => ParseActions.Prune).ToArray();
			Assert.AreEqual(0, nodes.Length);

			node2.Split(brick1.GetBoundingBox(new Scene()));
			Assert.AreEqual(2, node2.NodeCount);

			nodes = node2.ParseNodes((node) => ParseActions.Yield).ToArray();
			Assert.AreEqual(3, nodes.Length);
			nodes = node2.ParseNodes((node) => ParseActions.Prune).ToArray();
			Assert.AreEqual(0, nodes.Length);

		}
		


	}
}
