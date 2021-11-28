using Brick_o_matic.Parsing;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
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
				scene.Validate();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			CreateOBJ(scene, Path.GetFileNameWithoutExtension(args[0]) ); ;
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
			Writer.WriteLine($"v {Brick.Position.X} {Brick.Position.Y + Brick.Size.Y} {Brick.Position.Z }");

		}

		private static void CreateOBJ(Scene Scene,string FileName)
		{
			Brick[] bricks;
			int index;
			int brickIndex;
			string material;
			byte r, g, b;
			Dictionary<string, string> materials;

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
			
				using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(Path.Combine(global::Brick_o_matic.BuildToOBJ.Properties.Settings.Default.OutFolder, FileName),"obj"), false))
				{

					writer.WriteLine($"mtllib {Path.ChangeExtension(FileName, "mtl")}");
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

						brick.Color.GetComponents(Scene, out r, out g, out b);
						material = $"mat_{r}-{g}-{b}";
						writer.WriteLine($"usemtl {material}");

						WriteBrickVertices(writer, brick);
						// front face
						writer.WriteLine($"f {index + 1}//1 {index + 2}//1 {index + 3}//1");
						writer.WriteLine($"f {index + 1}//1 {index + 3}//1 {index + 4}//1");
						// back face
						writer.WriteLine($"f {index + 5}//2 {index + 8}//2 {index + 7}//2");
						writer.WriteLine($"f {index + 5}//2 {index + 7}//2 {index + 6}//2");
						// left face
						writer.WriteLine($"f {index + 1}//3 {index + 4}//3 {index + 8}//3");
						writer.WriteLine($"f {index + 1}//3 {index + 8}//3 {index + 5}//3");
						// right face
						writer.WriteLine($"f {index + 2}//4 {index + 6}//4 {index + 3}//4");
						writer.WriteLine($"f {index + 6}//4 {index + 7}//4 {index + 3}//4");
						// top face
						writer.WriteLine($"f {index + 4}//5 {index + 3}//5 {index + 7}//5");
						writer.WriteLine($"f {index + 4}//5 {index + 7}//5 {index + 8}//5");
						// bottom face
						writer.WriteLine($"f {index + 1}//6 {index + 5}//6 {index + 2}//6");
						writer.WriteLine($"f {index + 2}//6 {index + 5}//6 {index + 6}//6");

						index += 8; brickIndex++;
					}
					writer.Flush();
				}
				using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(Path.Combine(global::Brick_o_matic.BuildToOBJ.Properties.Settings.Default.OutFolder, FileName), "mtl"), false))
				{
					materials = new Dictionary<string, string>();
					foreach (Brick brick in bricks)
					{
						brick.Color.GetComponents(Scene, out r, out g, out b);
						material = $"mat_{r}-{g}-{b}";

						if (materials.ContainsKey(material)) continue;
						materials.Add(material, material);

						writer.WriteLine($"newmtl {material}");
						writer.WriteLine($"Ns 10");
						writer.WriteLine($"Ka 1 1 1");
						writer.WriteLine($"Kd {(r/255.0f).ToString("F",CultureInfo.InvariantCulture)} {(g / 255.0f).ToString("F", CultureInfo.InvariantCulture)} {(b / 255.0f).ToString("F", CultureInfo.InvariantCulture)}");
						writer.WriteLine($"Ks 0.5 0.5 0.5");
						writer.WriteLine($"Ke 0 0 0");
						writer.WriteLine($"Ni 1.000000");
						writer.WriteLine($"d 1.000000");
						writer.WriteLine();
					}
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
