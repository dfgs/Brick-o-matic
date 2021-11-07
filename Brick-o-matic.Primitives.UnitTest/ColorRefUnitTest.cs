using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class ColorRefUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			ColorRef color;

			color = new ColorRef("Red");
			Assert.IsNotNull( color);
		}

		[TestMethod]
		public void ShouldGetDirectRGB()
		{
			ColorRef color;
			Color red;
			Scene scene;
			byte R, G, B;

			red = new Color(255, 0, 0);
			color = new ColorRef("Red");

			scene = new Scene();
			scene.AddResource("Red",red);

			color.GetComponents(scene, out R, out G, out B);
			Assert.AreEqual(255, R);
			Assert.AreEqual(0, G);
			Assert.AreEqual(0, B);

		}

		[TestMethod]
		public void ShouldGetNestedRGB()
		{
			ColorRef color,redRef;
			Color red;
			Scene scene;
			byte R, G, B;

			red = new Color(255, 0, 0);
			redRef = new ColorRef("Red");
			color = new ColorRef("RedRef");

			scene = new Scene();
			scene.AddResource("Red", red);
			scene.AddResource("RedRef", redRef);

			color.GetComponents(scene, out R, out G, out B);
			Assert.AreEqual(255, R);
			Assert.AreEqual(0, G);
			Assert.AreEqual(0, B);

		}

		[TestMethod]
		public void ShouldDetectSelfReferencedColor()
		{
			ColorRef color, redRef;
			Scene scene;
			byte R, G, B;

			redRef = new ColorRef("Color");
			color = new ColorRef("RedRef");

			scene = new Scene();
			scene.AddResource("Color", color);
			scene.AddResource("RedRef", redRef);

			Assert.ThrowsException<InvalidOperationException>(()=>color.GetComponents(scene, out R, out G, out B));
			

		}

	}
}
