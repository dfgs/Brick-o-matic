using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Brick_o_matic.Editor
{
	public static class GeometryUtils
	{
		public static IEnumerable<GeometryModel3D> CreateGeometryModels(IScene Scene,Brick Brick)
		{
			DiffuseMaterial positiveX,negativeX, positiveY, negativeY, positiveZ, negativeZ;
			GeometryModel3D model;
			MeshGeometry3D mesh;
			
			positiveX = new DiffuseMaterial();
			positiveX.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));// new SolidColorBrush(Color.FromArgb(255, Brick.PositiveX.Color.R, Brick.PositiveX.Color.G, Brick.PositiveX.Color.B));
			negativeX = new DiffuseMaterial();
			negativeX.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));//new SolidColorBrush(Color.FromArgb(255, Brick.NegativeX.Color.R, Brick.NegativeX.Color.G, Brick.NegativeX.Color.B));
			positiveY = new DiffuseMaterial();
			positiveY.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));//new SolidColorBrush(Color.FromArgb(255, Brick.PositiveY.Color.R, Brick.PositiveY.Color.G, Brick.PositiveY.Color.B));
			negativeY = new DiffuseMaterial();
			negativeY.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));//new SolidColorBrush(Color.FromArgb(255, Brick.NegativeY.Color.R, Brick.NegativeY.Color.G, Brick.NegativeY.Color.B));
			positiveZ = new DiffuseMaterial();
			positiveZ.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));//new SolidColorBrush(Color.FromArgb(255, Brick.PositiveZ.Color.R, Brick.PositiveZ.Color.G, Brick.PositiveZ.Color.B));
			negativeZ = new DiffuseMaterial();
			negativeZ.Brush = new SolidColorBrush(Brick.Color.ToMediaColor(Scene));// new SolidColorBrush(Color.FromArgb(255, Brick.NegativeZ.Color.R, Brick.NegativeZ.Color.G, Brick.NegativeZ.Color.B));


			mesh = new MeshGeometry3D();
			// front face
			mesh.Positions.Add(new Point3D(Brick.Position.X,Brick.Position.Y,Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
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
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
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
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
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
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
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
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y+Brick.Size.Y, Brick.Position.Z+Brick.Size.Z));
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
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X+Brick.Size.X, Brick.Position.Y, Brick.Position.Z+Brick.Size.Z));
			mesh.Positions.Add(new Point3D(Brick.Position.X, Brick.Position.Y, Brick.Position.Z+Brick.Size.Z));
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

		

		public static Model3DGroup CreateModelGroup(IScene Scene)
		{
			Model3DGroup group;


			group = new Model3DGroup();

			foreach (Brick brick in Scene.Build(null))
			{
				foreach (GeometryModel3D model in CreateGeometryModels(Scene,brick))
				{
					group.Children.Add(model);
				}
			}


			return group;
		}


	}
}
