using Brick_o_matic.Math;
using Brick_o_matic.Parsing;
using Brick_o_matic.Primitives;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml;

namespace Brick_o_matic.Editor.ViewModels
{
	public class ProjectViewModel : DependencyObject
	{


		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProjectViewModel), new PropertyMetadata("New project"));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(ProjectViewModel), new PropertyMetadata(null));
		public string FileName
		{
			get { return (string)GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}



		public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register("IsModified", typeof(bool), typeof(ProjectViewModel), new PropertyMetadata(false));
		public bool IsModified
		{
			get { return (bool)GetValue(IsModifiedProperty); }
			set { SetValue(IsModifiedProperty, value); }
		}
		

		public static readonly DependencyProperty CameraProperty = DependencyProperty.Register("Camera", typeof(PerspectiveCamera), typeof(ProjectViewModel), new PropertyMetadata(null));
		public PerspectiveCamera Camera
		{
			get { return (PerspectiveCamera)GetValue(CameraProperty); }
			set { SetValue(CameraProperty, value); }
		}



		public static readonly DependencyProperty LightProperty = DependencyProperty.Register("Light", typeof(DirectionalLight), typeof(ProjectViewModel), new PropertyMetadata(null));
		public DirectionalLight Light
		{
			get { return (DirectionalLight)GetValue(LightProperty); }
			set { SetValue(LightProperty, value); }
		}



		public static readonly DependencyProperty ModelVisualProperty = DependencyProperty.Register("ModelVisual", typeof(ModelVisual3D), typeof(ProjectViewModel), new PropertyMetadata(null));
		public ModelVisual3D ModelVisual
		{
			get { return (ModelVisual3D)GetValue(ModelVisualProperty); }
			set { SetValue(ModelVisualProperty, value); }
		}





		public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(int), typeof(ProjectViewModel), new FrameworkPropertyMetadata(5,AnglePropertyChanged));
		public int Zoom
		{
			get { return (int)GetValue(ZoomProperty); }
			set { SetValue(ZoomProperty, value); }
		}


		public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(int), typeof(ProjectViewModel), new FrameworkPropertyMetadata(AnglePropertyChanged));
		public int Angle
		{
			get { return (int)GetValue(AngleProperty); }
			set { SetValue(AngleProperty, value); }
		}
		public static readonly DependencyProperty ElevationProperty = DependencyProperty.Register("Elevation", typeof(int), typeof(ProjectViewModel), new FrameworkPropertyMetadata(90,AnglePropertyChanged));
		public int Elevation
		{
			get { return (int)GetValue(ElevationProperty); }
			set { SetValue(ElevationProperty, value); }
		}



		public static readonly DependencyProperty SceneProperty = DependencyProperty.Register("Scene", typeof(SceneViewModel), typeof(ProjectViewModel), new PropertyMetadata(null));
		public SceneViewModel Scene
		{
			get { return (SceneViewModel)GetValue(SceneProperty); }
			set { SetValue(SceneProperty, value); }
		}








		public ProjectViewModel()
		{
			Scene = new SceneViewModel();
			UpdateCamera();
		}


		

		public void Save()
		{
			if (FileName == null) throw new Exception("FileName not specified");
			//File.WriteAllText(FileName,Document.Text);
			IsModified = false;
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);
			//File.WriteAllText(FileName, Document.Text);
			IsModified = false;
		}

		public async Task LoadFromFileAsync(string FileName)
		{
			Scene scene;

			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);

			
			IsModified = false;
			scene = SceneReader.ReadFromFile(FileName);
			await Scene.LoadAsync(scene);
		}

		private static void AnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectViewModel vm;
			vm = d as ProjectViewModel;
			if (vm != null) vm.UpdateCamera();
		}

		private void UpdateCamera()
		{
			/*Vector3D center, direction;
			Point3D position;
			PerspectiveCamera camera;
			int cameraLength;
			Box boundingBox;

			if (scene == null) boundingBox = new Box();
			else boundingBox = scene.GetBoundingBox(null);

			center = new Vector3D(boundingBox.Position.X + boundingBox.Size.X * 0.5f, boundingBox.Position.Y + boundingBox.Size.Y * 0.5f, boundingBox.Position.Z + boundingBox.Size.Z * 0.5f);
			cameraLength = System.Math.Max(System.Math.Max(boundingBox.Size.X, boundingBox.Size.Y), boundingBox.Size.Z) * Zoom;

			position = new Point3D(
				System.Math.Sin(Angle * System.Math.PI / 180.0f) * cameraLength,
				System.Math.Sin(Elevation * System.Math.PI / 180.0f) * cameraLength, 
				System.Math.Cos(Angle * System.Math.PI / 180.0f)*cameraLength);
			
			direction = center - new Vector3D( position.X,position.Y,position.Z);
			direction.Normalize();

			camera = new PerspectiveCamera();
			camera.LookDirection = direction;
			camera.Position = position;
			this.Camera = camera;*/

		}
		public void Build()
		{
			/*DirectionalLight light;
			Model3DGroup group;
			ModelVisual3D modelVisual;

			if ((Content == null) || string.IsNullOrWhiteSpace(Document.Text))
			{
				ModelVisual = null;
				return;
			}

			scene = new Scene();
			try
			{
				scene=SceneReader.Read(Document.Text);
			}
			catch(ParserLib.UnexpectedCharException unexpected)
			{
				SetError(unexpected.Message);
				Position = (int)unexpected.Position;
				return;
			}
			catch (Exception ex)
			{
				SetError(ex.Message);
				return;
			}

			UpdateCamera();


			group = GeometryUtils.CreateModelGroup(scene);

			light = new DirectionalLight();
			light.Color = Colors.White;
			light.Direction = Camera.LookDirection;
			this.Light = light;
			group.Children.Add(light);


			modelVisual = new ModelVisual3D();
			modelVisual.Content = group;
			this.ModelVisual = modelVisual;

			SetError(null);//*/
		}


	}
}