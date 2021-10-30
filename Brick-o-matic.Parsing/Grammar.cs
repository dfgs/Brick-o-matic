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

        public static Parser<Vector> Vector =
            from _ in Parse.Char('(')
            from x in Parse.Int()
            from __ in Parse.Char(',')
            from y in Parse.Int()
            from ___ in Parse.Char(',')
            from z in Parse.Int()
            from ____ in Parse.Char(')')
            select new Vector(x, y, z);

        public static Parser<ISetter<IPrimitive>> PositionSetter =
            from _ in Parse.String("Position:")
            from value in Vector
            select (ISetter<IPrimitive>)new PositionSetter(value);

        public static Parser<IPrimitive> Brick =
            from _ in Parse.String("Brick")
            from __ in Parse.Char('(')
            from setters in PositionSetter.ZeroOrMoreTimes()
            from ___ in Parse.Char(')')
            select new Brick().Set(setters);

    }
}
