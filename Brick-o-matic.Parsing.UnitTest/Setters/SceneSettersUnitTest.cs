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
	public class SceneSettersUnitTest
	{
		

		
		

		// Scene Setters
		[TestMethod]
		public void ShouldParseSceneResourceSetter()
		{
			SceneResourcesSetter setter;

			setter = Grammar.SceneResourcesSetter.Parse("Resources:ResourceName=Brick() ResourceName2=Part()", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2, setter.Value.Count());
			Assert.AreEqual("ResourceName", setter.Value.First().Name);
		}
		[TestMethod]
		public void ShouldParseSceneItemsSetter()
		{
			SceneItemsSetter setter;

			setter = Grammar.SceneItemsSetter.Parse("Items: Brick() Part()", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2, setter.Value.Count());

		}




	}
}
