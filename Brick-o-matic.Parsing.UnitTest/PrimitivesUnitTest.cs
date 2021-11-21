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
	public class PrimitivesUnitTest
	{
		



		// Primitives
		[TestMethod]
		public void ShouldParseBrick()
		{
			Brick b;

			b = Grammar.Brick.Parse("Brick()",' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(),b.Position);

			b = Grammar.Brick.Parse("Brick(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3))", ' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1) Color:(128,127,126) )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.AreEqual(new Color(128, 127, 126), b.Color);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1) Color:Red )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.Brick.Parse("Brick( Color:Red Size:(1,2,3) Position:(3,2,1)  )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
		}
		[TestMethod]
		public void ShouldParsePart()
		{
			Part p;

			p = Grammar.Part.Parse("Part()", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(), p.Position);

			p = Grammar.Part.Parse("Part(Position:(1,2,3))", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(1, 2, 3), p.Position);

			p = Grammar.Part.Parse("Part(Position:(1,2,3) Items: Brick() Brick() Part())", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(1, 2, 3), p.Position);
			Assert.AreEqual(3, p.Count);
		}
		[TestMethod]
		public void ShouldParseTileMap()
		{
			TileMap p;

			p = Grammar.TileMap.Parse("TileMap()", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(), p.Position);

			p = Grammar.TileMap.Parse("TileMap(Position:(1,2,3))", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(1, 2, 3), p.Position);

			p = Grammar.TileMap.Parse("TileMap(Position:(1,2,3) Items: Brick() Part() Primitive())", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(1, 2, 3), p.Position);
			Assert.AreEqual(3, p.Count);
		}
		[TestMethod]
		public void ShouldParsePrimitiveRef()
		{
			PrimitiveRef b;

			b = Grammar.PrimitiveRef.Parse("Primitive()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.PrimitiveRef.Parse("Primitive(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.PrimitiveRef.Parse("Primitive(Name:Homer)", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual("Homer", b.Name);

			b = Grammar.PrimitiveRef.Parse("Primitive(Position:(1,2,3) Name:Homer)", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);
			Assert.AreEqual("Homer", b.Name);

			b = Grammar.PrimitiveRef.Parse("Primitive(Name:Homer Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);
			Assert.AreEqual("Homer", b.Name);

			b = Grammar.PrimitiveRef.Parse("Primitive(Name:Homer Position:(1,2,3) Resources:Blue=(255,0,0))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);
			Assert.AreEqual("Homer", b.Name);
			Assert.AreEqual(1, b.ResourcesCount);
		}

		[TestMethod]
		public void ShouldParseImportScene()
		{
			ImportedScene b;

			b = Grammar.ImportedScene.Parse("ImportScene()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.ImportedScene.Parse("ImportScene(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

		}
		[TestMethod]
		public void ShouldParseImportResources()
		{
			ImportedResources b;

			b = Grammar.ImportedResources.Parse("ImportResources()", ' ');
			Assert.IsNotNull(b);

		
		}

		[TestMethod]
		public void ShouldParseRotateX()
		{
			RotateX b;

			b = Grammar.RotateX.Parse("RotateX()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.RotateX.Parse("RotateX(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.RotateX.Parse("RotateX(Count:-15)", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);

			b = Grammar.RotateX.Parse("RotateX(Count:-15 Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.RotateX.Parse("RotateX(Count:-15 Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item,typeof(Brick));
		}
		[TestMethod]
		public void ShouldParseRotateY()
		{
			RotateY b;

			b = Grammar.RotateY.Parse("RotateY()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.RotateY.Parse("RotateY(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.RotateY.Parse("RotateY(Count:-15)", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);

			b = Grammar.RotateY.Parse("RotateY(Count:-15 Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.RotateY.Parse("RotateY(Count:-15 Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item, typeof(Brick));
		}

		[TestMethod]
		public void ShouldParseRotateZ()
		{
			RotateZ b;

			b = Grammar.RotateZ.Parse("RotateZ()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.RotateZ.Parse("RotateZ(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.RotateZ.Parse("RotateZ(Count:-15)", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);

			b = Grammar.RotateZ.Parse("RotateZ(Count:-15 Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.RotateZ.Parse("RotateZ(Count:-15 Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(-15, b.Count);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item, typeof(Brick));
		}


		[TestMethod]
		public void ShouldParseFlipX()
		{
			FlipX b;

			b = Grammar.FlipX.Parse("FlipX()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.FlipX.Parse("FlipX(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.FlipX.Parse("FlipX(Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item, typeof(Brick));
		}
		[TestMethod]
		public void ShouldParseFlipY()
		{
			FlipY b;

			b = Grammar.FlipY.Parse("FlipY()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.FlipY.Parse("FlipY(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.FlipY.Parse("FlipY( Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item, typeof(Brick));
		}

		[TestMethod]
		public void ShouldParseFlipZ()
		{
			FlipZ b;

			b = Grammar.FlipZ.Parse("FlipZ()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.FlipZ.Parse("FlipZ(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.FlipZ.Parse("FlipZ( Position:(3,2,1) Item:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.Item, typeof(Brick));
		}

		[TestMethod]
		public void ShouldParseDifference()
		{
			Difference b;

			b = Grammar.Difference.Parse("Difference()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.Difference.Parse("Difference(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.Difference.Parse("Difference(Position:(3,2,1) ItemA:Brick() ItemB:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.ItemA, typeof(Brick));
			Assert.IsInstanceOfType(b.ItemB, typeof(Brick));
		}
		[TestMethod]
		public void ShouldParseIntersection()
		{
			Intersection b;

			b = Grammar.Intersection.Parse("Intersection()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.Intersection.Parse("Intersection(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.Intersection.Parse("Intersection(Position:(3,2,1) ItemA:Brick() ItemB:Brick() )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.IsInstanceOfType(b.ItemA, typeof(Brick));
			Assert.IsInstanceOfType(b.ItemB, typeof(Brick));
		}
		[TestMethod]
		public void ShouldParsePrimitive()
		{
			IPrimitive item;

			item = Grammar.Primitive.Parse("Part()", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Part));

			item = Grammar.Primitive.Parse("Brick(Position:(1,2,3))", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Brick));

			item = Grammar.Primitive.Parse("Primitive( Name:Homer Position:(1,2,3) )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(PrimitiveRef));

			item = Grammar.Primitive.Parse("ImportScene( Position:(1,2,3) )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(ImportedScene));

			item = Grammar.Primitive.Parse("RotateX( Position:(1,2,3) Count:3 )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(RotateX));

			item = Grammar.Primitive.Parse("RotateY( Position:(1,2,3) Count:3 )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(RotateY));


			item = Grammar.Primitive.Parse("RotateZ( Position:(1,2,3) Count:3 )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(RotateZ));

			
			item = Grammar.Primitive.Parse("FlipX( Position:(1,2,3)  )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(FlipX));

			item = Grammar.Primitive.Parse("FlipY( Position:(1,2,3) )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(FlipY));


			item = Grammar.Primitive.Parse("FlipZ( Position:(1,2,3) )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(FlipZ));
			
			item = Grammar.Primitive.Parse("TileMap( Position:(1,2,3) )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(TileMap));

			item = Grammar.Primitive.Parse("Difference(Position:(3,2,1) ItemA:Brick() ItemB:Brick() )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Difference));

			item = Grammar.Primitive.Parse("Intersection(Position:(3,2,1) ItemA:Brick() ItemB:Brick() )", ' ');
			Assert.IsNotNull(item);
			Assert.IsInstanceOfType(item, typeof(Intersection));
		}


		[TestMethod]
		public void ShouldParsePrimitives()
		{
			IPrimitive[] items;

			items = Grammar.Primitives.Parse("Part()", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(1, items.Length);
			Assert.IsInstanceOfType(items[0], typeof(Part));

			items = Grammar.Primitives.Parse("Part() Brick(Position:(1,2,3)) Primitive(Name:homer) ImportScene(Position:(1,2,3)) RotateX( Position:(1,2,3) Count:3 ) RotateY( Position:(1,2,3) Count:3 ) RotateZ( Position:(1,2,3) Count:3 ) FlipX( Position:(1,2,3)  ) FlipY( Position:(1,2,3) ) FlipZ( Position:(1,2,3) ) TileMap(Position:(1,2,3) ) Difference(Position:(3,2,1) ItemA:Brick() ItemB:Brick() ) Intersection(Position:(3,2,1) ItemA:Brick() ItemB:Brick() )        )", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(13, items.Length);
			Assert.IsInstanceOfType(items[0], typeof(Part));
			Assert.IsInstanceOfType(items[1], typeof(Brick));
			Assert.IsInstanceOfType(items[2], typeof(PrimitiveRef));
			Assert.IsInstanceOfType(items[3], typeof(ImportedScene));
			Assert.IsInstanceOfType(items[4], typeof(RotateX));
			Assert.IsInstanceOfType(items[5], typeof(RotateY));
			Assert.IsInstanceOfType(items[6], typeof(RotateZ));
			Assert.IsInstanceOfType(items[7], typeof(FlipX));
			Assert.IsInstanceOfType(items[8], typeof(FlipY));
			Assert.IsInstanceOfType(items[9], typeof(FlipZ));
			Assert.IsInstanceOfType(items[10], typeof(TileMap));
			Assert.IsInstanceOfType(items[11], typeof(Difference));
			Assert.IsInstanceOfType(items[12], typeof(Intersection));
		}


		[TestMethod]
		public void ShouldParseScene()
		{
			Scene scene;

			scene = Grammar.Scene.Parse("Scene()", ' ');
			Assert.IsNotNull(scene);
			scene = Grammar.Scene.Parse("Scene( Resources: b1 = Brick() Red = (255,0,0) Items: Brick() Primitive() Part() ImportScene() RotateX() RotateY() RotateZ() FlipX() FlipY() FlipZ() TileMap() Difference() Intersection() )", ' ');
			Assert.IsNotNull(scene);
			Assert.AreEqual(2, scene.ResourcesCount);
			Assert.AreEqual(13, scene.ItemsCount);


			//Assert.AreEqual(new Position(), b.Position);
		}


	}
}
