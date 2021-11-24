using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing
{
	public static class SceneReader
	{
		public static Scene Read(string Text)
		{
			ParserLib.StringReader reader;

			reader = new ParserLib.StringReader(Text, ' ', '\t', '\r', '\n');
			return Grammar.Scene.Parse(reader);
		}
		public static Scene ReadFromFile(string FileName)
		{
			ParserLib.StreamReader reader;

			try
			{
				using (FileStream stream = new FileStream(FileName, FileMode.Open))
				{
					reader = new ParserLib.StreamReader(stream, ' ', '\t', '\r', '\n');
					return Grammar.Scene.Parse(reader);
				}
			}
			catch(Exception ex)
			{
				throw new Exception($"{Path.GetFileName(FileName)}:1:1: error: {ex.Message}");
			}
		}
	}
}
