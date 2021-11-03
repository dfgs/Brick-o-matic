﻿using Brick_o_matic.Math;
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
            Parse.AnyOf('-','_').Or(Parse.AnyInRange('0','9')).Or(Parse.AnyInRange('A', 'z')).OneOrMoreTimes());

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

      


        public static IParser<IPrimitive> Primitive = Brick.Or<IPrimitive>(Part);
        public static IParser<IEnumerable<IPrimitive>> Primitives = Primitive.OneOrMoreTimes();

        public static IParser<ISceneObject> SceneObject = StaticColor.Or<ISceneObject>(Primitive);

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
 