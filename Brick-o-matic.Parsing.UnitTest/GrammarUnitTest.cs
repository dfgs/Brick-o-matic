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
	public class GrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseComment()
		{
			Assert.AreEqual("//Homer", Grammar.Comment.Parse("//Homer\rSecondLine", ' ','\r','\n'));
		}
		

		[TestMethod]
		public void ShouldParseName()
		{
			Assert.AreEqual("Homer", Grammar.Name.Parse("Homer", ' '));
			Assert.AreEqual("hOMEr", Grammar.Name.Parse("hOMEr", ' '));
			Assert.AreEqual("Homer1", Grammar.Name.Parse("Homer1", ' '));
			Assert.AreEqual("Homer-1", Grammar.Name.Parse("Homer-1", ' '));
			Assert.AreEqual("Homer_1", Grammar.Name.Parse("Homer_1", ' '));
			Assert.AreEqual("Homer_1A", Grammar.Name.Parse("Homer_1A", ' '));
			Assert.AreEqual("Homer_1.A", Grammar.Name.Parse("Homer_1.A", ' '));
		}


		[TestMethod]
		public void ShouldParsePosition()
		{
			Assert.AreEqual(new Position(1, 2, 3), Grammar.Position.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Position(1, -2, 3), Grammar.Position.Parse("(1,-2,3)", ' '));
			Assert.AreEqual(new Position(100, 200, -300), Grammar.Position.Parse(" (100, 200, -300)", ' '));
		}
		[TestMethod]
		public void ShouldParseSize()
		{
			Assert.AreEqual(new Size(1, 2, 3), Grammar.Size.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Size(0, 0, 0), Grammar.Size.Parse("(0,0,0)", ' '));
			Assert.AreEqual(new Size(100, 200, 300), Grammar.Size.Parse(" (100, 200, 300)", ' '));
		}

		[TestMethod]
		public void ShouldParseColorRef()
		{
			ColorRef color;

			color = Grammar.ColorRef.Parse("Red");
			Assert.IsNotNull(color);
			Assert.AreEqual("Red", color.Name);
		}

		[TestMethod]
		public void ShouldParseColor()
		{
			Assert.AreEqual(new Color(1, 2, 3), Grammar.StaticColor.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Color(255, 255, 255), Grammar.StaticColor.Parse(" (255, 255, 255)", ' '));
		}

		[TestMethod]
		public void ShouldNotParseInvalidColor()
		{
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.StaticColor.Parse("(256,2,3)", ' '));
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.StaticColor.Parse("(-1,2,3)", ' '));
		}

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

		// Brick Setters
		[TestMethod]
		public void ShouldParseBrickPositionSetter()
		{
			BrickPositionSetter setter;

			setter = Grammar.BrickPositionSetter.Parse("Position:(1, 2,3)", ' ') ;
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseBrickSizeSetter()
		{
			BrickSizeSetter setter;

			setter = Grammar.BrickSizeSetter.Parse("Size:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Size(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseBrickColorSetter()
		{
			BrickColorSetter setter;

			setter = Grammar.BrickColorSetter.Parse("Color:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Color(1, 2, 3), setter.Value);

			setter = Grammar.BrickColorSetter.Parse("Color:(128,127,126)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Color(128, 127, 126), setter.Value);

			setter = Grammar.BrickColorSetter.Parse("Color:Red", ' ');
			Assert.IsNotNull(setter);
		}


		// Part Setters
		[TestMethod]
		public void ShouldParsePartPositionSetter()
		{
			PartPositionSetter setter;

			setter = Grammar.PartPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);

		}
		[TestMethod]
		public void ShouldParsePartItemsSetter()
		{
			PartItemsSetter setter;

			setter = Grammar.PartItemsSetter.Parse("Items: Brick() Part()", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2,setter.Value.Count());

		}


		// PrimitiveRef Setters
		[TestMethod]
		public void ShouldParsePrimitiveRefPositionSetter()
		{
			PrimitiveRefPositionSetter setter;

			setter = Grammar.PrimitiveRefPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParsePrimitiveRefNameSetter()
		{
			PrimitiveRefNameSetter setter;

			setter = Grammar.PrimitiveRefNameSetter.Parse("Name: Homer123", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual("Homer123", setter.Value);
		}

		// ImportScene Setters
		[TestMethod]
		public void ShouldParseImportScenePositionSetter()
		{
			ImportedScenePositionSetter setter;

			setter = Grammar.ImportPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		

		// IRotate Setters
		[TestMethod]
		public void ShouldParseRotateXPositionSetter()
		{
			IRotatePositionSetter setter;

			setter = Grammar.IRotatePositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseRotateXCountSetter()
		{
			IRotateCountSetter setter;

			setter = Grammar.IRotateCountSetter.Parse("Count:15", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(15, setter.Value);
		}

		[TestMethod]
		public void ShouldParseIRotateItemSetter()
		{
			IRotateItemSetter setter;

			setter = Grammar.IRotateItemSetter.Parse("Item:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value,typeof(Brick));

		}

		// IFlip Setters
		[TestMethod]
		public void ShouldParseFlipXPositionSetter()
		{
			IFlipPositionSetter setter;

			setter = Grammar.IFlipPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		
		[TestMethod]
		public void ShouldParseIFlipItemSetter()
		{
			IFlipItemSetter setter;

			setter = Grammar.IFlipItemSetter.Parse("Item:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value, typeof(Brick));

		}

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

		}

		[TestMethod]
		public void ShouldParseImportScene()
		{
			ImportedScene b;

			b = Grammar.ImportedScene.Parse("Import()", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(), b.Position);

			b = Grammar.ImportedScene.Parse("Import(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

		}
		[TestMethod]
		public void ShouldParseImportResources()
		{
			ImportedResources b;

			b = Grammar.ImportedResources.Parse("Import()", ' ');
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

			item = Grammar.Primitive.Parse("Import( Position:(1,2,3) )", ' ');
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
		}


		[TestMethod]
		public void ShouldParsePrimitives()
		{
			IPrimitive[] items;

			items = Grammar.Primitives.Parse("Part()", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(1, items.Length);
			Assert.IsInstanceOfType(items[0], typeof(Part));

			items = Grammar.Primitives.Parse("Part() Brick(Position:(1,2,3)) Primitive(Name:homer) Import(Position:(1,2,3)) RotateX( Position:(1,2,3) Count:3 ) RotateY( Position:(1,2,3) Count:3 ) RotateZ( Position:(1,2,3) Count:3 ) FlipX( Position:(1,2,3)  ) FlipY( Position:(1,2,3) ) FlipZ( Position:(1,2,3)  )", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(10, items.Length);
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
		}


		[TestMethod]
		public void ShouldParseScene()
		{
			Scene scene;

			scene = Grammar.Scene.Parse("Scene()", ' ');
			Assert.IsNotNull(scene);
			scene = Grammar.Scene.Parse("Scene( Resources: b1 = Brick() Red = (255,0,0) Items: Brick() Primitive() Part() Import() RotateX() RotateY() RotateZ() FlipX() FlipY() FlipZ() )", ' ');
			Assert.IsNotNull(scene);
			Assert.AreEqual(2, scene.ResourcesCount);
			Assert.AreEqual(10, scene.ItemsCount);


			//Assert.AreEqual(new Position(), b.Position);
		}


	}
}
