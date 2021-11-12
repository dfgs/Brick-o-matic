using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using ParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing
{
    public static class Grammar
    {
        public static IParser<string> Name = Parse.AnyInRange('A', 'z').Then(
            Parse.AnyOf('-','_','.').Or(Parse.AnyInRange('0','9')).Or(Parse.AnyInRange('A', 'z')).OneOrMoreTimes().ReaderIncludes(' ','\t','\r','\n'));

        public static IParser<string> FileName = Parse.Char('"').Then(Parse.Except('"').OneOrMoreTimes()).Then(Parse.Char('"'));

        public static IParser<string> Comment = Parse.String("//").Then(Parse.Except('\r').ZeroOrMoreTimes().ReaderIncludes('\r'));


        public static IParser<Position> Position =
            from _ in Parse.Char('(')
            from x in Parse.Int()
            from __ in Parse.Char(',')
            from y in Parse.Int()
            from ___ in Parse.Char(',')
            from z in Parse.Int()
            from ____ in Parse.Char(')')
            select new Position(x, y, z);
        
        public static IParser<Size> Size =
            from _ in Parse.Char('(')
            from x in Parse.Int()
            from __ in Parse.Char(',')
            from y in Parse.Int()
            from ___ in Parse.Char(',')
            from z in Parse.Int()
            from ____ in Parse.Char(')')
            select new Size(x, y, z);

        public static IParser<IColor> StaticColor =
            from _ in Parse.Char('(')
            from r in Parse.Byte()
            from __ in Parse.Char(',')
            from g in Parse.Byte()
            from ___ in Parse.Char(',')
            from b in Parse.Byte()
            from ____ in Parse.Char(')')
            select new Color(r, g, b) as IColor;    // added casting because struct is not compatible with variance

        public static IParser<ColorRef> ColorRef =
            from name in Name
            select new ColorRef(name);


        public static IParser<IColor> Color = StaticColor.Or(ColorRef);
        
        // Brick setters
        public static IParser<BrickPositionSetter> BrickPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new BrickPositionSetter(value);

        public static IParser<BrickSizeSetter> BrickSizeSetter =
            from _ in Parse.String("Size:")
            from value in Size
            select new BrickSizeSetter(value);

        public static IParser<BrickColorSetter> BrickColorSetter =
             from _ in Parse.String("Color:")
             from value in Color
             select new BrickColorSetter(value);

        public static IParser<IBrickSetter> BrickSetter = BrickPositionSetter.Or<IBrickSetter>(BrickSizeSetter).Or<IBrickSetter>(BrickColorSetter);
        public static IParser<IEnumerable<IBrickSetter>> BrickSetters = BrickSetter.ZeroOrMoreTimes();

        // Part setters
        public static IParser<PartPositionSetter> PartPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new PartPositionSetter(value);
        public static IParser<PartItemsSetter> PartItemsSetter =
             from _ in Parse.String("Items:")
             from value in Primitives
             select new PartItemsSetter(value);

        public static IParser<IPartSetter> PartSetter = PartPositionSetter.Or< IPartSetter>(PartItemsSetter);
        public static IParser<IEnumerable<IPartSetter>> PartSetters = PartSetter.ZeroOrMoreTimes();

        // TileMap setters
        public static IParser<TileMapPositionSetter> TileMapPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new TileMapPositionSetter(value);
        public static IParser<TileMapTileSizeXSetter> TileMapTileSizeXSetter =
            from _ in Parse.String("TileSizeX:")
            from value in Parse.Int()
            select new TileMapTileSizeXSetter(value);
        public static IParser<TileMapTileSizeYSetter> TileMapTileSizeYSetter =
            from _ in Parse.String("TileSizeY:")
            from value in Parse.Int()
            select new TileMapTileSizeYSetter(value);
        public static IParser<TileMapTileSizeZSetter> TileMapTileSizeZSetter =
            from _ in Parse.String("TileSizeZ:")
            from value in Parse.Int()
            select new TileMapTileSizeZSetter(value);
        public static IParser<TileMapItemsSetter> TileMapItemsSetter =
             from _ in Parse.String("Items:")
             from value in Primitives
             select new TileMapItemsSetter(value);

        public static IParser<ITileMapSetter> TileMapSetter = TileMapPositionSetter.Or<ITileMapSetter>(TileMapTileSizeXSetter).Or<ITileMapSetter>(TileMapTileSizeYSetter).Or<ITileMapSetter>(TileMapTileSizeZSetter).Or<ITileMapSetter>(TileMapItemsSetter);
        public static IParser<IEnumerable<ITileMapSetter>> TileMapSetters = TileMapSetter.ZeroOrMoreTimes();


        // PrimitiveRef setters
        public static IParser<PrimitiveRefPositionSetter> PrimitiveRefPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new PrimitiveRefPositionSetter(value);
        public static IParser<PrimitiveRefNameSetter> PrimitiveRefNameSetter =
            from _ in Parse.String("Name:")
            from value in Name
            select new PrimitiveRefNameSetter(value);
        public static IParser<PrimitiveRefResourcesSetter> PrimitiveRefResourcesSetter =
            from _ in Parse.String("Resources:")
            from value in Resources
            select new PrimitiveRefResourcesSetter(value);

        public static IParser<IPrimitiveRefSetter> PrimitiveRefSetter = PrimitiveRefPositionSetter.Or<IPrimitiveRefSetter>(PrimitiveRefNameSetter).Or<IPrimitiveRefSetter>(PrimitiveRefResourcesSetter);
        public static IParser<IEnumerable<IPrimitiveRefSetter>> PrimitiveRefSetters = PrimitiveRefSetter.ZeroOrMoreTimes();

        // ImportedScene setters
        public static IParser<ImportedScenePositionSetter> ImportedScenePositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new ImportedScenePositionSetter(value);
        public static IParser<ImportedSceneSetter> ImportedSceneSceneSetter =
             from _ in Parse.String("FileName:")
             from value in FileName
             select new ImportedSceneSetter(SceneReader.ReadFromFile(value.TrimStart('"').TrimEnd('"')));//*/

        public static IParser<IImportedSceneSetter> ImportedSceneSetter = ImportedScenePositionSetter.Or<IImportedSceneSetter>(ImportedSceneSceneSetter);
        public static IParser<IEnumerable<IImportedSceneSetter>> ImportedSceneSetters = ImportedSceneSetter.ZeroOrMoreTimes();

        // IRotate setters
        public static IParser<IRotatePositionSetter> IRotatePositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new IRotatePositionSetter(value);

        public static IParser<IRotateCountSetter> IRotateCountSetter =
            from _ in Parse.String("Count:")
            from value in Parse.Int()
            select new IRotateCountSetter(value);

        public static IParser<IRotateItemSetter> IRotateItemSetter =
             from _ in Parse.String("Item:")
             from value in Primitive
             select new IRotateItemSetter(value);

        public static IParser<IRotateSetter> IRotateSetter = IRotatePositionSetter.Or<IRotateSetter>(IRotateCountSetter).Or<IRotateSetter>(IRotateItemSetter);
        public static IParser<IEnumerable<IRotateSetter>> IRotateSetters = IRotateSetter.ZeroOrMoreTimes();

        // IFlip setters
        public static IParser<IFlipPositionSetter> IFlipPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new IFlipPositionSetter(value);

        public static IParser<IFlipItemSetter> IFlipItemSetter =
             from _ in Parse.String("Item:")
             from value in Primitive
             select new IFlipItemSetter(value);

        public static IParser<IFlipSetter> IFlipSetter = IFlipPositionSetter.Or<IFlipSetter>(IFlipItemSetter);
        public static IParser<IEnumerable<IFlipSetter>> IFlipSetters = IFlipSetter.ZeroOrMoreTimes();

        // ImportedResources setters
        public static IParser<ImportedResourcesSceneSetter> ImportedResourcesSceneSetter =
             from _ in Parse.String("FileName:")
             from value in FileName
             select new ImportedResourcesSceneSetter(SceneReader.ReadFromFile(value.TrimStart('"').TrimEnd('"')));//*/

        public static IParser<IImportedResourcesSetter> ImportedResourcesSetter = ImportedResourcesSceneSetter;//.Or<IImportedSceneSetter>(ImportSceneSetter);
        public static IParser<IEnumerable<IImportedResourcesSetter>> ImportedResourcesSetters = ImportedResourcesSetter.ZeroOrMoreTimes();


        // Primitives
        public static IParser<Brick> Brick =
            from _ in Parse.String("Brick")
            from __ in Parse.Char('(')
            from setters in BrickSetters
            from ___ in Parse.Char(')')
            select new Brick().Set(setters);

        public static IParser<Part> Part =
             from _ in Parse.String("Part")
             from __ in Parse.Char('(')
             from setters in PartSetters
             from ___ in Parse.Char(')')
             select new Part().Set(setters);

        public static IParser<TileMap> TileMap =
             from _ in Parse.String("TileMap")
             from __ in Parse.Char('(')
             from setters in TileMapSetters
             from ___ in Parse.Char(')')
             select new TileMap().Set(setters);

        public static IParser<PrimitiveRef> PrimitiveRef =
             from _ in Parse.String("Primitive")
             from __ in Parse.Char('(')
             from setters in PrimitiveRefSetters
             from ___ in Parse.Char(')')
             select new PrimitiveRef().Set(setters);

        public static IParser<ImportedScene> ImportedScene =
              from _ in Parse.String("ImportScene")
              from __ in Parse.Char('(')
              from setters in ImportedSceneSetters
              from ___ in Parse.Char(')')
              select new ImportedScene().Set(setters);

        public static IParser<RotateX> RotateX =
            from _ in Parse.String("RotateX")
            from __ in Parse.Char('(')
            from setters in IRotateSetters
            from ___ in Parse.Char(')')
            select new RotateX().Set(setters) as RotateX;

        public static IParser<RotateY> RotateY =
            from _ in Parse.String("RotateY")
            from __ in Parse.Char('(')
            from setters in IRotateSetters
            from ___ in Parse.Char(')')
            select new RotateY().Set(setters) as RotateY;

        public static IParser<RotateZ> RotateZ =
            from _ in Parse.String("RotateZ")
            from __ in Parse.Char('(')
            from setters in IRotateSetters
            from ___ in Parse.Char(')')
            select new RotateZ().Set(setters) as RotateZ;

        public static IParser<FlipX> FlipX =
            from _ in Parse.String("FlipX")
            from __ in Parse.Char('(')
            from setters in IFlipSetters
            from ___ in Parse.Char(')')
            select new FlipX().Set(setters) as FlipX;

        public static IParser<FlipY> FlipY =
            from _ in Parse.String("FlipY")
            from __ in Parse.Char('(')
            from setters in IFlipSetters
            from ___ in Parse.Char(')')
            select new FlipY().Set(setters) as FlipY;

        public static IParser<FlipZ> FlipZ =
            from _ in Parse.String("FlipZ")
            from __ in Parse.Char('(')
            from setters in IFlipSetters
            from ___ in Parse.Char(')')
            select new FlipZ().Set(setters) as FlipZ;


        // imported resources

        public static IParser<ImportedResources> ImportedResources =
           from _ in Parse.String("ImportResources")
           from __ in Parse.Char('(')
           from setters in ImportedResourcesSetters
           from ___ in Parse.Char(')')
           select new ImportedResources().Set(setters);


        public static IParser<IPrimitive> Primitive = Brick.Or<IPrimitive>(Part).Or<IPrimitive>(TileMap).Or<IPrimitive>(PrimitiveRef).Or<IPrimitive>(ImportedScene).Or<IPrimitive>(RotateX).Or<IPrimitive>(RotateY).Or<IPrimitive>(RotateZ).Or<IPrimitive>(FlipX).Or<IPrimitive>(FlipY).Or<IPrimitive>(FlipZ);
        public static IParser<IEnumerable<IPrimitive>> Primitives = Primitive.OneOrMoreTimes();



        // resources
        public static IParser<ISceneObject> SceneObject = Primitive.Or<ISceneObject>(ImportedResources).Or<ISceneObject>(Color);

        public static IParser<Resource> Resource =
            from name in Name
            from __ in Parse.Char('=')
            from obj in SceneObject
            select new Resource(name, obj);

        public static IParser<IEnumerable<Resource>> Resources = Resource.OneOrMoreTimes();

        // Scene setters
        public static IParser<SceneResourcesSetter> SceneResourcesSetter =
            from _ in Parse.String("Resources:")
            from value in Resources
            select new SceneResourcesSetter(value);

        public static IParser<SceneItemsSetter> SceneItemsSetter =
            from _ in Parse.String("Items:")
            from value in Primitives
            select new SceneItemsSetter(value);

        public static IParser<ISceneSetter> SceneSetter = SceneResourcesSetter.Or<ISceneSetter>(SceneItemsSetter);
        public static IParser<IEnumerable<ISceneSetter>> SceneSetters = SceneSetter.ZeroOrMoreTimes();

        
        // Scene
        public static IParser<Scene> Scene =
            from _ in Parse.String("Scene")
            from __ in Parse.Char('(')
            from setters in SceneSetters
            from ___ in Parse.Char(')')
            select new Scene().Set(setters);

    }
}
 