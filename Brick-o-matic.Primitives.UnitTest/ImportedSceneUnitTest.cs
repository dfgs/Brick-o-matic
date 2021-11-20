using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Import_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class ImportedSceneUnitTest
	{
		
		

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Scene scene;
			ImportedScene import;
			Box box;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);

			import = new ImportedScene(new Position(0, 0, 0));
			import.Scene = scene;
			box = import.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(-1,-2,-3), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);


			import = new ImportedScene(new Position(1, 2, 3));
			import.Scene = scene;
			box = import.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(0, 0, 0), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

			import = new ImportedScene(new Position(1, 2, 3));
			import.Scene = null;
			box = import.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1, 2, 3), box.Position);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}



		[TestMethod]
		public void ShouldGetBricks()
		{
			Scene scene;
			ImportedScene import ;
			Brick[] bricks;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add( b);

			import = new ImportedScene(new Position(0,0, 0));
			import.Scene = scene;
			bricks = import.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(-1, -2, -3), bricks[0].Position);

			import = new ImportedScene(new Position(1, 2, 3));
			import.Scene = scene;
			bricks = import.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(0, 0, 0), bricks[0].Position);

		}


		/*[TestMethod]
		public void ShouldBuildICSGNode()
		{
			Scene scene;
			ImportedScene import;
			ICSGNode node;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);

			import = new ImportedScene(new Position(0, 0, 0));
			import.Scene = scene;
			node = import.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(-1, -2, -3), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(import, node.Primitive);


			import = new ImportedScene(new Position(1, 2, 3));
			import.Scene = scene;
			node = import.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(0, 0, 0), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(import, node.Primitive);

			import = new ImportedScene(new Position(1, 2, 3));
			import.Scene = null;
			node = import.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(1, 2, 3), node.BoundingBox.Position);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(import, node.Primitive);
		}*/

	}
}
