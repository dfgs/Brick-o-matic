using Brick_o_matic.Primitives;
using ParserLib;
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
			int line, column;
			ParserLib.StreamReader reader;
			ParserLib.StreamPosConverter posConverter;
			bool result;

			try
			{
				using (FileStream stream = new FileStream(FileName, FileMode.Open))
				{
					reader = new ParserLib.StreamReader(stream, ' ', '\t', '\r', '\n');
					return Grammar.Scene.Parse(reader);
				}
			}
			catch(UnexpectedCharException ex)
			{
				try
				{
					posConverter = new ParserLib.StreamPosConverter(1024);
					using (FileStream stream = new FileStream(FileName, FileMode.Open))
					{
						result = posConverter.TryGetLineAndColumn(stream, ex.Position, out line, out column);
					}
				}
				catch (Exception ex2)
				{
					throw new Exception($"{Path.GetFileName(FileName)}:1:1: error: {ex2.Message}");
				}
				if (result)
				{
					throw new Exception($"{Path.GetFileName(FileName)}:{line}:{column}: error: {ex.Message}");
				}
				else
				{
					throw new Exception($"{Path.GetFileName(FileName)}:1:1: error: {ex.Message}");
				}
				
			}
			catch (Exception ex2)
			{
				throw new Exception($"{Path.GetFileName(FileName)}:1:1: error: {ex2.Message}");
			}
		}





	}
}
