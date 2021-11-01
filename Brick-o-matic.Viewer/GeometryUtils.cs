using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Brick_o_matic.Viewer
{
	public static class GeometryUtils
	{
		public static GeometryModel3D CreateGeometryModel(BoxGeometry Item)
		{
			DiffuseMaterial material;
			GeometryModel3D model;
			MeshGeometry3D mesh;

			material = new DiffuseMaterial();
			material.Brush = new SolidColorBrush(Color.FromArgb(255,Item.Color.R,Item.Color.G,Item.Color.B));


			mesh = new MeshGeometry3D();

			// front face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, Item.Size.Z));
			// back face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, 0));
			// left face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			// right face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, 0));
			// top face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, Item.Size.Y, Item.Size.Z));
			// bottom face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(Item.Size.X, 0, Item.Size.Z));

			// front face
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			// back face
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			// left face
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			// right face
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			// top face
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			// bottom face
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));


			// front face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
			// back face
			mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(6);
			// left face
			mesh.TriangleIndices.Add(8); mesh.TriangleIndices.Add(9); mesh.TriangleIndices.Add(11);
			mesh.TriangleIndices.Add(9); mesh.TriangleIndices.Add(10); mesh.TriangleIndices.Add(11);
			// right face
			mesh.TriangleIndices.Add(12); mesh.TriangleIndices.Add(15); mesh.TriangleIndices.Add(13);
			mesh.TriangleIndices.Add(13); mesh.TriangleIndices.Add(15); mesh.TriangleIndices.Add(14);
			// top face
			mesh.TriangleIndices.Add(16); mesh.TriangleIndices.Add(18); mesh.TriangleIndices.Add(17);
			mesh.TriangleIndices.Add(16); mesh.TriangleIndices.Add(19); mesh.TriangleIndices.Add(18);
			// bottom face
			mesh.TriangleIndices.Add(20); mesh.TriangleIndices.Add(21); mesh.TriangleIndices.Add(23);
			mesh.TriangleIndices.Add(21); mesh.TriangleIndices.Add(22); mesh.TriangleIndices.Add(23);

			model = new GeometryModel3D();
			model.Material = material;
			model.Geometry = mesh;

			return model;
		}
		public static IEnumerable<GeometryModel3D> CreateGeometryModels(Model Model)
		{
			foreach (BoxGeometry item in Model.Items)
			{
				yield return CreateGeometryModel(item);
			}
		}
		public static ModelVisual3D CreateModelVisual(Model Model)
		{
			ModelVisual3D modelVisual;
			Model3DGroup group;
			DirectionalLight light;

			light = new DirectionalLight();
			light.Color = Colors.White;
			light.Direction = new Vector3D(0.5, -1, -0.30);

			group = new Model3DGroup();
			group.Children.Add(light);

			foreach (GeometryModel3D model in CreateGeometryModels(Model))
			{
				group.Children.Add(model);
			}


			modelVisual = new ModelVisual3D();
			modelVisual.Content = group;

			return modelVisual;
		}


	}
}
