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
            Parse.AnyOf('-','_','.').Or(Parse.AnyInRange('0','9')).Or(Parse.AnyInRange('A', 'z')).ZeroOrMoreTimes().ReaderIncludes(' ','\t','\r','\n'));

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

        public static IParser<string> EscapedChar =
            from _ in Parse.Char('\\')
            from value in Parse.Any()
            select value;
        public static IParser<string> Char = EscapedChar.Or(Parse.Except('"'));

        public static IParser<String> String =
            from _ in Parse.Char('"')
            from value in Char.OneOrMoreTimes()
            from __ in Parse.Char('"')
            select value;
        
        public static IParser<Variable> Variable =
             from value in String
             select new Variable(value);


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
        public static IParser<TileMapTileSizeSetter> TileMapTileSizeSetter =
            from _ in Parse.String("TileSize:")
            from value in Size
            select new TileMapTileSizeSetter(value);
        public static IParser<TileMapItemsSetter> TileMapItemsSetter =
             from _ in Parse.String("Items:")
             from value in Primitives
             select new TileMapItemsSetter(value);

        public static IParser<ITileMapSetter> TileMapSetter = TileMapPositionSetter.Or<ITileMapSetter>(TileMapTileSizeSetter).Or<ITileMapSetter>(TileMapItemsSetter);
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

        // ICSG setters
        public static IParser<ICSGPositionSetter> ICSGPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new ICSGPositionSetter(value);

        public static IParser<ICSGItemASetter> ICSGItemASetter =
             from _ in Parse.String("ItemA:")
             from value in Primitive
             select new ICSGItemASetter(value);

        public static IParser<ICSGItemBSetter> ICSGItemBSetter =
             from _ in Parse.String("ItemB:")
             from value in Primitive
             select new ICSGItemBSetter(value);

        public static IParser<ICSGSetter> ICSGSetter = ICSGPositionSetter.Or<ICSGSetter>(ICSGItemASetter).Or<ICSGSetter>(ICSGItemBSetter);
        public static IParser<IEnumerable<ICSGSetter>> ICSGSetters = ICSGSetter.ZeroOrMoreTimes();

        // When setters

        public static IParser<WhenValueSetter> WhenValueSetter =
            from _ in Parse.String("Value:")
            from value in String
            select new WhenValueSetter(value);
        public static IParser<WhenReturnSetter> WhenReturnSetter =
             from _ in Parse.String("Return:")
             from value in Primitive
             select new WhenReturnSetter(value);

        public static IParser<IWhenSetter> WhenSetter = WhenValueSetter.Or<IWhenSetter>(WhenReturnSetter);
        public static IParser<IEnumerable<IWhenSetter>> WhenSetters = WhenSetter.ZeroOrMoreTimes();



        // Switch setters

        public static IParser<SwitchPositionSetter> SwitchPositionSetter =
            from _ in Parse.String("Position:")
            from value in Position
            select new SwitchPositionSetter(value);
        public static IParser<SwitchVariableSetter> SwitchVariableSetter =
            from _ in Parse.String("Variable:")
            from value in Name
            select new SwitchVariableSetter(value);
        public static IParser<SwitchItemsSetter> SwitchItemsSetter =
             from _ in Parse.String("Items:")
             from value in Whens
             select new SwitchItemsSetter(value);

        public static IParser<ISwitchSetter> SwitchSetter = SwitchPositionSetter.Or<ISwitchSetter>(SwitchItemsSetter).Or<ISwitchSetter>(SwitchVariableSetter);
        public static IParser<IEnumerable<ISwitchSetter>> SwitchSetters = SwitchSetter.ZeroOrMoreTimes();


        // Whens
        public static IParser<When> When =
            from _ in Parse.String("When")
            from __ in Parse.Char('(')
            from setters in WhenSetters
            from ___ in Parse.Char(')')
            select new When().Set(setters);
        public static IParser<IEnumerable<IWhen>> Whens = When.ZeroOrMoreTimes();



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

        public static IParser<Difference> Difference =
             from _ in Parse.String("Difference")
             from __ in Parse.Char('(')
             from setters in ICSGSetters
             from ___ in Parse.Char(')')
             select new Difference().Set(setters) as Difference;

        public static IParser<Intersection> Intersection =
             from _ in Parse.String("Intersection")
             from __ in Parse.Char('(')
             from setters in ICSGSetters
             from ___ in Parse.Char(')')
             select new Intersection().Set(setters) as Intersection;

        public static IParser<Union> Union =
            from _ in Parse.String("Union")
            from __ in Parse.Char('(')
            from setters in ICSGSetters
            from ___ in Parse.Char(')')
            select new Union().Set(setters) as Union;

        public static IParser<Switch> Switch =
          from _ in Parse.String("Switch")
          from __ in Parse.Char('(')
          from setters in SwitchSetters
          from ___ in Parse.Char(')')
          select new Switch().Set(setters);


        // imported resources

        public static IParser<ImportedResources> ImportedResources =
           from _ in Parse.String("ImportResources")
           from __ in Parse.Char('(')
           from setters in ImportedResourcesSetters
           from ___ in Parse.Char(')')
           select new ImportedResources().Set(setters);


        public static IParser<IPrimitive> Primitive = Brick.Or<IPrimitive>(Part).Or<IPrimitive>(TileMap).Or<IPrimitive>(PrimitiveRef).Or<IPrimitive>(ImportedScene).Or<IPrimitive>(RotateX).Or<IPrimitive>(RotateY).Or<IPrimitive>(RotateZ).Or<IPrimitive>(FlipX).Or<IPrimitive>(FlipY).Or<IPrimitive>(FlipZ).Or<IPrimitive>(Difference).Or<IPrimitive>(Intersection).Or<IPrimitive>(Union).Or<IPrimitive>(Switch);
        public static IParser<IEnumerable<IPrimitive>> Primitives = Primitive.OneOrMoreTimes();



        // resources
        public static IParser<ISceneObject> SceneObject = Primitive.Or<ISceneObject>(ImportedResources).Or<ISceneObject>(Color).Or<ISceneObject>(Variable);

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
 