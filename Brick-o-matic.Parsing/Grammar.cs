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

        public static IParser<Vector> Vector =
            from _ in Parse.Char('(')
            from x in Parse.Int()
            from __ in Parse.Char(',')
            from y in Parse.Int()
            from ___ in Parse.Char(',')
            from z in Parse.Int()
            from ____ in Parse.Char(')')
            select new Vector(x, y, z);

        // Brick setters
        public static IParser<BrickPositionSetter> BrickPositionSetter =
            from _ in Parse.String("Position:")
            from value in Vector
            select new BrickPositionSetter(value);

        public static IParser<BrickSizeSetter> BrickSizeSetter =
            from _ in Parse.String("Size:")
            from value in Vector
            select new BrickSizeSetter(value);

        public static IParser<IBrickSetter> BrickSetter = BrickPositionSetter.Or<IBrickSetter>(BrickSizeSetter);
        public static IParser<IEnumerable<IBrickSetter>> BrickSetters = BrickSetter.ZeroOrMoreTimes();

        // Part setters
        public static IParser<PartPositionSetter> PartPositionSetter =
            from _ in Parse.String("Position:")
            from value in Vector
            select new PartPositionSetter(value);

        public static IParser<IPartSetter> PartSetter = PartPositionSetter;
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

    }
}
 