using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using System;
using System.Linq;

namespace Brick_o_matic.Parsing.UnitTest
{
	[TestClass]
	public class ResourceUnitTest
	{
		

		// resource
		[TestMethod]
		public void ShouldParseSceneObject()
		{
			ISceneObject item;

			item = Grammar.SceneObject.Parse("Part()", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Part));

			item = Grammar.SceneObject.Parse("Brick(Position:(1,2,3))", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Brick));
			
			item = Grammar.SceneObject.Parse("(1,2,3)", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Color));

			item = Grammar.SceneObject.Parse("Red", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(ColorRef));
		}

		[TestMethod]
		public void ShouldParseResource()
		{
			Resource resource;

			resource = Grammar.Resource.Parse("ResourceName=Brick()", ' ');
			Assert.IsNotNull(resource);
			Assert.AreEqual("ResourceName", resource.Name);
			Assert.IsInstanceOfType(resource.Object, typeof(Brick));
			
			resource = Grammar.Resource.Parse("Color=(255,255,0)", ' ');
			Assert.IsNotNull(resource);
			Assert.AreEqual("Color", resource.Name);
			Assert.IsInstanceOfType(resource.Object, typeof(Color));
		}

		

	}
}
