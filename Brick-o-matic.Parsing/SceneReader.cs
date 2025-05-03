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
		public static Scene Read(string FileName,string Text)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(Text)))
			{
				return Read(FileName, stream);
			}
		}
		public static Scene ReadFromFile(string FileName)
		{

			using (FileStream stream = new FileStream(FileName, FileMode.Open))
			{
				return Read(FileName,stream);
			}
		}

		private static Scene Read(string FileName,Stream Stream)
		{
			ParserLib.StreamReader reader;
			Scene scene;
			ParserLib.StreamPosConverter posConverter;
			int line, column;
			bool result;

			try
			{
				reader = new ParserLib.StreamReader(Stream, ' ', '\t', '\r', '\n');
				scene = Grammar.Scene.Parse(reader);
				scene.Validate();
				return scene;
			}
			catch (UnexpectedCharException ex)
			{
				try
				{
					posConverter = new ParserLib.StreamPosConverter(1024);
					result = posConverter.TryGetLineAndColumn(Stream, ex.Position, out line, out column);
				}
				catch
				{
					throw ex;
				}

				if (result)
				{
					throw new Exception($"{Path.GetFileName(FileName)}:{line}:{column}: error: {ex.Message}");
				}
				else
				{
					throw ex;
				}

			}
			
		}

	

	}
}
