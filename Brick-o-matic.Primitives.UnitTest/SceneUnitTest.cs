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

			scene = new Scene();
			Assert.ThrowsException<InvalidOperationException>(() => scene.GetResource("Name"));
		}
		[TestMethod]
		public void ShouldGetResource()
		{
			Scene scene;
			Color a, b;

			a = new Color(255, 0, 0);
			scene = new Scene();
			scene.AddResource("color", a);
			b = (Color)scene.GetResource("color");
			Assert.AreEqual(b, a);
		}

	}
}
