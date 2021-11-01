using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing
{
	public static class PrimitiveReader
	{
		public static IEnumerable<IPrimitive> Read(string Text,params char[] IgnoredChards)
		{
			ParserLib.StringReader reader;

			reader = new ParserLib.StringReader(Text, IgnoredChards);
			return Grammar.Primitives.Parse(reader);
		}
	}
}
