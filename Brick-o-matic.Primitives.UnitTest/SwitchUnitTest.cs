using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class SwitchUnitTest
	{
		[TestMethod]
		public void ShouldSelect()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			IPrimitive result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick();

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick();

			sw = new Switch();
			sw.Variable = "myVariable";
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.Select(scene);
			Assert.AreEqual(w1.Return, result);

			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value2"));
			result = sw.Select(scene);
			Assert.AreEqual(w2.Return, result);
		}

		[TestMethod]
		public void ShouldNotSelectIfVariableIsNotDefined()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			IPrimitive result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick();

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick();

			sw = new Switch();
			sw.Variable = null;// "myVariable";
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.Select(scene);
			Assert.AreEqual(null, result);

		}

		[TestMethod]
		public void ShouldSelectIfNoWhenFound()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			IPrimitive result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick();

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick();

			sw = new Switch();
			sw.Variable = "myVariable";
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value3"));
			result = sw.Select(scene);
			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void ShouldSelectIfNoWhenDefined()
		{
			Scene scene;
			Switch sw;
			IPrimitive result;

			

			sw = new Switch();
			sw.Variable = "myVariable";


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.Select(scene);
			Assert.AreEqual(null, result);
		}

		[TestMethod]
		public void ShouldGetBoundingBox()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			IBox result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick() { Position = new Math.Position(1, 1, 1), Size = new Size(1) };

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick() { Position = new Math.Position(2, 2, 2), Size = new Size(2) };

			sw = new Switch();
			sw.Position = new Position(10, 10, 10);
			sw.Variable = "myVariable";
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.GetBoundingBox(scene);
			Assert.AreEqual(new Position(11, 11, 11), result.Position);
			Assert.AreEqual(new Size(1, 1, 1), result.Size);

			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value2"));
			result = sw.GetBoundingBox(scene);
			Assert.AreEqual(new Position(12, 12, 12), result.Position);
			Assert.AreEqual(new Size(2, 2, 2), result.Size);
		}

		[TestMethod]
		public void ShouldNotGetBoundingBox()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			IBox result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick() { Position = new Math.Position(1, 1, 1),Size=new Size(1) };

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick() { Position = new Math.Position(2, 2, 2), Size = new Size(2) };

			sw = new Switch();
			sw.Position = new Position(10, 10, 10);
			sw.Variable = null;// Select should return null
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.GetBoundingBox(scene);
			Assert.AreEqual(new Position(10, 10, 10), result.Position);
			Assert.AreEqual(new Size(), result.Size);

			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value2"));
			result = sw.GetBoundingBox(scene);
			Assert.AreEqual(new Position(10, 10, 10), result.Position);
			Assert.AreEqual(new Size(), result.Size);
		}



		[TestMethod]
		public void ShouldBuild()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			Brick[] result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick() { Position = new Math.Position(1, 1, 1), Size = new Size(1) };

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick() { Position = new Math.Position(2, 2, 2), Size = new Size(2) };

			sw = new Switch();
			sw.Position = new Position(10, 10, 10);
			sw.Variable = "myVariable";
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.Build(scene).ToArray();
			Assert.AreEqual(1, result.Length);
			Assert.AreEqual(new Position(11, 11, 11), result[0].Position);
			Assert.AreEqual(new Size(1, 1, 1), result[0].Size);

			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value2"));
			result = sw.Build(scene).ToArray();
			Assert.AreEqual(1, result.Length);
			Assert.AreEqual(new Position(12, 12, 12), result[0].Position);
			Assert.AreEqual(new Size(2, 2, 2), result[0].Size);
		}

		[TestMethod]
		public void ShouldNotBuild()
		{
			Scene scene;
			Switch sw;
			When w1, w2;
			Brick[] result;

			w1 = new When();
			w1.Value = "value1";
			w1.Return = new Brick() { Position = new Math.Position(1, 1, 1), Size = new Size(1) };

			w2 = new When();
			w2.Value = "value2";
			w2.Return = new Brick() { Position = new Math.Position(2, 2, 2), Size = new Size(2) };

			sw = new Switch();
			sw.Position = new Position(10, 10, 10);
			sw.Variable = null;// Select should return null
			sw.Add(w1);
			sw.Add(w2);


			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value1"));
			result = sw.Build(scene).ToArray();
			Assert.AreEqual(0, result.Length);

			scene = new Scene();
			scene.AddResource("myVariable", new Variable("value2"));
			result = sw.Build(scene).ToArray();
			Assert.AreEqual(0, result.Length);
		}


	}
}
