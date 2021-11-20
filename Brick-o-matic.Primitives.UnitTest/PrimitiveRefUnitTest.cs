using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace PrimitiveRef_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class PrimitiveRefUnitTest
	{

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Scene scene;
			PrimitiveRef primitive;
			IBox box;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.AddResource("b", b);

			primitive = new PrimitiveRef(new Position(0, 0, 0));
			primitive.Name = "b";

			box = primitive.GetBoundingBox(scene);
			Assert.AreEqual(new Position(-1, -2, -3), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);


			primitive = new PrimitiveRef(new Position(1, 2, 3));
			primitive.Name = "b";

			box = primitive.GetBoundingBox(scene);
			Assert.AreEqual(new Position(0, 0, 0), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

		}



		[TestMethod]
		public void ShouldNotGetBoudingBoxWhenPrimitiveIsSelfReferenced()
		{
			PrimitiveRef p1, p2;
			Scene scene;

			p1 = new PrimitiveRef() { Name = "p2" };
			p2 = new PrimitiveRef() { Name = "p1" };

			scene = new Scene();
			scene.AddResource("p1", p1);
			scene.AddResource("p2", p2);

			Assert.ThrowsException<InvalidOperationException>(() => p1.GetBoundingBox(scene));
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			Scene scene;
			PrimitiveRef primitive;
			Brick[] bricks;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.AddResource("b", b);

			primitive = new PrimitiveRef(new Position(0, 0, 0));
			primitive.Name = "b";

			bricks = primitive.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
		}

		[TestMethod]
		public void ShouldNotBuildWhenPrimitiveIsSelfReferenced()
		{
			PrimitiveRef p1, p2;
			Scene scene;

			p1 = new PrimitiveRef() { Name = "p2" };
			p2 = new PrimitiveRef() { Name = "p1" };

			scene = new Scene();
			scene.AddResource("p1", p1);
			scene.AddResource("p2", p2);

			Assert.ThrowsException<InvalidOperationException>(() => p1.Build(scene).ToArray());
		}



		/*[TestMethod]
		public void ShouldBuildICSGNode()
		{
			Scene scene;
			PrimitiveRef primitive;
			ICSGNode node;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.AddResource("b", b);

			primitive = new PrimitiveRef(new Position(0, 0, 0));
			primitive.Name = "b";

			node = primitive.BuildCSGNode(scene, new Position());
			Assert.AreEqual(new Position(-1, -2, -3), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("b", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(primitive, node.Primitive);


			primitive = new PrimitiveRef(new Position(1, 2, 3));
			primitive.Name = "b";

			node = primitive.BuildCSGNode(scene, new Position());
			Assert.AreEqual(new Position(0, 0, 0), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("b", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(primitive, node.Primitive);

		}



		[TestMethod]
		public void ShouldNotBuildICSGNodeWhenPrimitiveIsSelfReferenced()
		{
			PrimitiveRef p1, p2;
			Scene scene;

			p1 = new PrimitiveRef() { Name = "p2" };
			p2 = new PrimitiveRef() { Name = "p1" };

			scene = new Scene();
			scene.AddResource("p1", p1);
			scene.AddResource("p2", p2);

			Assert.ThrowsException<InvalidOperationException>(() => p1.BuildCSGNode(scene, new Position()));
		}*/
	}
}
