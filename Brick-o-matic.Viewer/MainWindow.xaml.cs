using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brick_o_matic.Viewer
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private GeometryModel3D CreateGeometryModel(Brick Item)
		{
			DiffuseMaterial material;
			GeometryModel3D model;
			MeshGeometry3D mesh;

			material = new DiffuseMaterial();
			material.Brush = new SolidColorBrush(Colors.Red);


			mesh = new MeshGeometry3D();

			// front face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y,  Item.Size.Z));
			// back face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y, 0));
			// left face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			// right face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y, Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0, 0));
			// top face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0,  Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X,  Item.Size.Y,  Item.Size.Z));
			// bottom face
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0,  Item.Size.Z));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate(0, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0, 0));
			mesh.Positions.Add(Item.Position.ToPoint3D().Translate( Item.Size.X, 0,  Item.Size.Z));

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
		private IEnumerable<GeometryModel3D> CreateGeometryModels(IPrimitive Item)
		{
			foreach(Brick item in Item.GetBricks())
			{
				yield return CreateGeometryModel(item);
			}
		}
		private ModelVisual3D CreateModelVisual(IEnumerable<IPrimitive> Items)
		{
			ModelVisual3D modelVisual;
			Model3DGroup group;
			DirectionalLight light;

			light = new DirectionalLight();
			light.Color = Colors.White;
			light.Direction = new Vector3D(-0.5, -0.5, -1);

			group = new Model3DGroup();
			group.Children.Add(light);

			foreach (IPrimitive item in Items)
			{
				foreach(GeometryModel3D model in CreateGeometryModels(item))
				{
					group.Children.Add(model);
				}
			}


			modelVisual = new ModelVisual3D();
			modelVisual.Content = group;

			return modelVisual;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ModelVisual3D modelVisual;
			PerspectiveCamera camera;
			Brick b1;
			Brick b2;


			b1 = new Brick(new Math.Vector(-1, 0, 0), new Math.Vector(1, 1, 2));
			b2 = new Brick(new Math.Vector(0,0,0), new Math.Vector(1, 1, 1));

			camera = new PerspectiveCamera();
			camera.LookDirection = new Vector3D(0, 0, -1);
			camera.Position= new Point3D(0, 3, 20);

			modelVisual = CreateModelVisual(new IPrimitive[] {b1,b2 });
			viewport.Children.Clear();
			viewport.Camera = camera;
			viewport.Children.Add(modelVisual);
		}
	}
}
