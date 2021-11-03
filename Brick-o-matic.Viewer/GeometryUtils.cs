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
		public static IEnumerable<GeometryModel3D> CreateGeometryModels(BoxGeometry Item)
		{
			DiffuseMaterial positiveX,negativeX, positiveY, negativeY, positiveZ, negativeZ;
			GeometryModel3D model;
			MeshGeometry3D mesh;
			
			positiveX = new DiffuseMaterial();
			positiveX.Brush = new SolidColorBrush(Color.FromArgb(255, Item.PositiveX.Color.R, Item.PositiveX.Color.G, Item.PositiveX.Color.B));
			negativeX = new DiffuseMaterial();
			negativeX.Brush = new SolidColorBrush(Color.FromArgb(255, Item.NegativeX.Color.R, Item.NegativeX.Color.G, Item.NegativeX.Color.B));
			positiveY = new DiffuseMaterial();
			positiveY.Brush = new SolidColorBrush(Color.FromArgb(255, Item.PositiveY.Color.R, Item.PositiveY.Color.G, Item.PositiveY.Color.B));
			negativeY = new DiffuseMaterial();
			negativeY.Brush = new SolidColorBrush(Color.FromArgb(255, Item.NegativeY.Color.R, Item.NegativeY.Color.G, Item.NegativeY.Color.B));
			positiveZ = new DiffuseMaterial();
			positiveZ.Brush = new SolidColorBrush(Color.FromArgb(255, Item.PositiveZ.Color.R, Item.PositiveZ.Color.G, Item.PositiveZ.Color.B));
			negativeZ = new DiffuseMaterial();
			negativeZ.Brush = new SolidColorBrush(Color.FromArgb(255, Item.NegativeZ.Color.R, Item.NegativeZ.Color.G, Item.NegativeZ.Color.B));


			mesh = new MeshGeometry3D();
			// front face
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position,Item.NegativeY.Position,Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			// front face
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			mesh.Normals.Add(new Vector3D(0, 0, 1));
			// front face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);

			model = new GeometryModel3D();
			model.Material = positiveZ;
			model.Geometry = mesh;
			yield return model;


			mesh = new MeshGeometry3D();
			// back face
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			// back face
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			mesh.Normals.Add(new Vector3D(0, 0, -1));
			// back face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(2);

			model = new GeometryModel3D();
			model.Material = negativeZ;
			model.Geometry = mesh;
			yield return model;


			mesh = new MeshGeometry3D();
			// left face
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.NegativeY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			// left face
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			mesh.Normals.Add(new Vector3D(-1, 0, 0));
			// left face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
			model = new GeometryModel3D();
			model.Material = negativeX;
			model.Geometry = mesh;
			yield return model;


			mesh = new MeshGeometry3D();
			// right face
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			// right face
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			mesh.Normals.Add(new Vector3D(1, 0, 0));
			// right face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(2);
			model = new GeometryModel3D();
			model.Material = positiveX;
			model.Geometry = mesh;
			yield return model;



			mesh = new MeshGeometry3D();
			// top face
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.PositiveY.Position, Item.PositiveZ.Position));
			// top face
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			mesh.Normals.Add(new Vector3D(0, 1, 0));
			// top face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(2);
			model = new GeometryModel3D();
			model.Material = positiveY;
			model.Geometry = mesh;
			yield return model;

			mesh = new MeshGeometry3D();
			// bottom face
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.NegativeZ.Position));
			mesh.Positions.Add(new Point3D(Item.PositiveX.Position, Item.NegativeY.Position, Item.PositiveZ.Position));
			mesh.Positions.Add(new Point3D(Item.NegativeX.Position, Item.NegativeY.Position, Item.PositiveZ.Position));
			// bottom face
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			mesh.Normals.Add(new Vector3D(0, -1, 0));
			// bottom face
			mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
			model = new GeometryModel3D();
			model.Material = negativeY;
			model.Geometry = mesh;
			yield return model;

		}

		public static IEnumerable<GeometryModel3D> CreateGeometryModels(Model Model)
		{
			foreach (BoxGeometry item in Model.Items)
			{
				foreach (GeometryModel3D item2 in CreateGeometryModels(item))
				{
					yield return item2;
				}
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
