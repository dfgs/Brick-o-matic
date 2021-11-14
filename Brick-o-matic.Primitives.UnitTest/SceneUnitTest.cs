using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class SceneUnitTest
	{
		[TestMethod]
		public void ShouldAddChildItem()
		{
			Scene scene;
			Brick b;

			scene = new Scene();
			Assert.IsNotNull(scene);
			Assert.AreEqual(0, scene.ItemsCount);
			b = new Brick();
			scene.Add(b);
			Assert.AreEqual(1, scene.ItemsCount);

		}
		[TestMethod]
		public void ShouldNotAddNullChildItem()
		{
			Scene scene;

			scene = new Scene();
			Assert.IsNotNull(scene);
			Assert.AreEqual(0, scene.ItemsCount);
			Assert.ThrowsException<ArgumentNullException>(() => scene.Add(null));

		}

		[TestMethod]
		public void ShouldNotAddInvalidResource()
		{
			Scene scene;

			scene = new Scene();
			Assert.ThrowsException<ArgumentNullException>(() => scene.AddResource(null, new Color()));
			Assert.ThrowsException<ArgumentNullException>(() => scene.AddResource("Name", null));
		}
		[TestMethod]
		public void ShouldNotAddDuplicateResource()
		{
			Scene scene;

			scene = new Scene();
			scene.AddResource("Name", new Color());
			Assert.ThrowsException<InvalidOperationException>(() => scene.AddResource("Name", new Color()));
		}

		[TestMethod]
		public void ShouldNotGetResource()
		{
			Scene scene;
			Brick b;
			bool result;

			scene = new Scene();
			result = scene.TryGetResource("Name",  out b);
			Assert.IsFalse(result);
			Assert.ThrowsException<ArgumentNullException>(()=> scene.TryGetResource(null,  out b));
		}
		[TestMethod]
		public void ShouldGetResource()
		{
			Scene scene;
			Color a, b;
			bool result;

			a = new Color(255, 0, 0);
			scene = new Scene();
			scene.AddResource("color", a);
			result = scene.TryGetResource("color",  out b);
			Assert.IsTrue(result);
			Assert.AreEqual(b, a);
		}
		[TestMethod]
		public void ShouldGetResources()
		{
			Scene scene;
			Color a;
			ISceneObject[] resources;

			a = new Color(255, 0, 0);
			scene = new Scene();
			scene.AddResource("color", a);
			resources = scene.Resources.Select(item=>item.Object).ToArray();
			Assert.AreEqual(1, resources.Length);
		}

		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoItems()
		{
			Box box;
			Scene scene;

			scene = new Scene();
			Assert.IsNotNull(scene);
			box = scene.GetBoundingBox(null);
			Assert.AreEqual(0, box.Position.X);
			Assert.AreEqual(0, box.Position.Y);
			Assert.AreEqual(0, box.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Scene scene;
			Box box;
			Brick b;

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			box = scene.GetBoundingBox(null);
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			box = scene.GetBoundingBox(null);
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(1, 1, 1));
			scene.Add(b);
			box = scene.GetBoundingBox(null);
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(3, 5, 7), box.Size);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(2, 2, 2));
			scene.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(2, 2, 2));
			scene.Add(b);
			box = scene.GetBoundingBox(null);
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(4, 6, 8), box.Size);

		}



		[TestMethod]
		public void ShouldReturnFlatICSGNodeWhenHasNoItems()
		{
			ICSGNode node;
			Scene scene;

			scene = new Scene();
			Assert.IsNotNull(scene);
			node = scene.BuildCSGNode(null);
			Assert.AreEqual(0, node.BoundingBox.Position.X);
			Assert.AreEqual(0, node.BoundingBox.Position.Y);
			Assert.AreEqual(0, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(null, node.Primitive);

		}

		[TestMethod]
		public void ShouldReturnBoundingICSGNode()
		{
			Scene scene;
			ICSGNode node;
			Brick b;

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			node = scene.BuildCSGNode(null);
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(null, node.Primitive);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			node = scene.BuildCSGNode(null);
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(null, node.Primitive);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(1, 1, 1));
			scene.Add(b);
			node = scene.BuildCSGNode(null);
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(3, 5, 7), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(null, node.Primitive);

			scene = new Scene();
			Assert.IsNotNull(scene);
			b = new Brick(new Position(-1, -2, -3), new Size(2, 2, 2));
			scene.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(2, 2, 2));
			scene.Add(b);
			node = scene.BuildCSGNode(null);
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(4, 6, 8), node.BoundingBox.Size);
			Assert.AreEqual("Scene", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(null, node.Primitive);

		}
	}
}
