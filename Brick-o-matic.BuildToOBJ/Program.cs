using Brick_o_matic.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.BuildToOBJ
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("No file specified");
				return;
			}
			try
			{
				SceneReader.ReadFromFile(args[0]);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}
	}
}
