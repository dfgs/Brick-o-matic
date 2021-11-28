using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Brick_o_matic.Primitives;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Scenes;
using SharpGLTF.Schema2;


namespace Brick_o_matic.BuildToGLTF
{
	using VERTEX = SharpGLTF.Geometry.VertexTypes.VertexPosition;

	class Program
	{

		static void Main(string[] args)
		{
			Brick_o_matic.Primitives.Scene scene;

			if (args.Length == 0)
			{
				Console.WriteLine("No file specified");
				return;
			}
			try
			{
				scene = Parsing.SceneReader.ReadFromFile(args[0]);
				scene.Validate();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			CreateGLTF(scene, Path.GetFileNameWithoutExtension(args[0])); ;
		}

		private static void CreateGLTF(Brick_o_matic.Primitives.Scene Scene, string FileName)
		{
			byte r, g, b;
			Brick[] bricks;
			VERTEX[] vertices;

			Directory.CreateDirectory(global::Brick_o_matic.BuildToGLTF.Properties.Settings.Default.OutFolder);

			try
			{
				bricks = Scene.Build(null).ToArray();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}


			// create a scene
			var gltfScene = new SharpGLTF.Scenes.SceneBuilder();
			
			foreach (Brick brick in bricks)
			{
				brick.Color.GetComponents(Scene, out r, out g, out b);

				var material = new MaterialBuilder()
					.WithDoubleSide(false)
					.WithBaseColor(new Vector4(r/255.0f, g / 255.0f, b / 255.0f, 1));

				// create a mesh with two primitives, one for each material
				var meshBuilder = new MeshBuilder<VERTEX>("mesh");
				var primitive = meshBuilder.UsePrimitive(material);

				vertices = new VERTEX[8];
				// front face
				vertices[0] = new VERTEX(brick.Position.X, brick.Position.Y, brick.Position.Z + brick.Size.Z);
				vertices[1] = new VERTEX(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z + brick.Size.Z);
				vertices[2] = new VERTEX(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z);
				vertices[3] = new VERTEX(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z);
				// back face
				vertices[4] = new VERTEX(brick.Position.X, brick.Position.Y, brick.Position.Z);
				vertices[5] = new VERTEX(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z );
				vertices[6] = new VERTEX(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z );
				vertices[7] = new VERTEX(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z );


				// front face
				primitive.AddTriangle(vertices[0], vertices[1], vertices[2]);
				primitive.AddTriangle(vertices[0], vertices[2], vertices[3]);
				// back face
				primitive.AddTriangle(vertices[4], vertices[7], vertices[6]);
				primitive.AddTriangle(vertices[4], vertices[6], vertices[5]);
				// left face
				primitive.AddTriangle(vertices[0], vertices[3], vertices[7]);
				primitive.AddTriangle(vertices[0], vertices[7], vertices[4]);
				// right face
				primitive.AddTriangle(vertices[1], vertices[5], vertices[2]);
				primitive.AddTriangle(vertices[5], vertices[6], vertices[2]);
				// top face
				primitive.AddTriangle(vertices[3], vertices[2], vertices[6]);
				primitive.AddTriangle(vertices[3], vertices[6], vertices[7]);
				// bottom face
				primitive.AddTriangle(vertices[0], vertices[4], vertices[1]);
				primitive.AddTriangle(vertices[1], vertices[4], vertices[5]);


				gltfScene.AddRigidMesh(meshBuilder, Matrix4x4.Identity);
			}

			// save the model in different formats

			var model = gltfScene.ToGltf2();

			string outFileName = Path.ChangeExtension(Path.Combine(global::Brick_o_matic.BuildToGLTF.Properties.Settings.Default.OutFolder, FileName), "gltf");
			WriteSettings settings;

			settings = new WriteSettings();
			settings.JsonIndented = true;
			
			model.SaveGLTF(outFileName,settings) ;
			
		}


	}
}
