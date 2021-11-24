using Brick_o_matic.Parsing;
using Brick_o_matic.Primitives;
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
			Scene scene;

			if (args.Length == 0)
			{
				Console.WriteLine("No file specified");
				return;
			}
			try
			{
				scene=SceneReader.ReadFromFile(args[0]);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			CreateOBJ(scene, Path.ChangeExtension(Path.GetFileName(args[0]), "obj")); ;
		}


		private static void WriteBrickVertices(StreamWriter Writer,Brick Brick)
		{
			// front face
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			// back face
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			// left face
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			// right face
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			// top face
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z + Brick.Size.Z}");
			// bottom face
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z}");
			Writer.WriteLine($"v {Brick.Position.X + Brick.Size.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y} {Brick.Position.Z + Brick.Size.Z}");
		}

		private static void CreateOBJ(Scene Scene,string FileName)
		{
			Brick[] bricks;
			int index;
			int brickIndex;

			Directory.CreateDirectory(global::Brick_o_matic.BuildToOBJ.Properties.Settings.Default.OutFolder);

			try
			{
				bricks = Scene.Build(null).ToArray();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			try
			{
			
				using (StreamWriter writer = new StreamWriter(Path.Combine(global::Brick_o_matic.BuildToOBJ.Properties.Settings.Default.OutFolder, FileName), false))
				{

					// front face
					writer.WriteLine("vn 0 0 1");
					// back face
					writer.WriteLine("vn 0 0 -1");
					// left face
					writer.WriteLine("vn -1 0 0");
					// right face
					writer.WriteLine("vn 1 0 1");
					// top face
					writer.WriteLine("vn 0 1 1");
					// bottom face
					writer.WriteLine("vn 0 -1 1");

					index = 0; brickIndex = 0;
					foreach (Brick brick in bricks)
					{
						writer.WriteLine($"o brick{brickIndex}");
						WriteBrickVertices(writer, brick);
						// front face
						writer.WriteLine($"f {index + 1}//1 {index + 2}//1 {index + 4}//1");
						writer.WriteLine($"f {index + 2}//1 {index + 3}//1 {index + 4}//1");
						// back face
						writer.WriteLine($"f {index + 5}//2 {index + 8}//2 {index + 6}//2");
						writer.WriteLine($"f {index + 6}//2 {index + 8}//2 {index + 7}//2");
						// left face
						writer.WriteLine($"f {index + 9}//3 {index + 10}//3 {index + 12}//3");
						writer.WriteLine($"f {index + 10}//3 {index + 11}//3 {index + 12}//3");
						// right face
						writer.WriteLine($"f {index + 13}//4 {index + 16}//4 {index + 14}//4");
						writer.WriteLine($"f {index + 14}//4 {index + 16}//4 {index + 15}//4");
						// top face
						writer.WriteLine($"f {index + 17}//5 {index + 20}//5 {index + 18}//5");
						writer.WriteLine($"f {index + 18}//5 {index + 20}//5 {index + 19}//5");
						// bottom face
						writer.WriteLine($"f {index + 21}//6 {index + 22}//6 {index + 24}//6");
						writer.WriteLine($"f {index + 22}//6 {index + 23}//6 {index + 24}//6");

						index += 24; brickIndex++;
					}
					writer.Flush();
				}

			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
		}

	}
}
