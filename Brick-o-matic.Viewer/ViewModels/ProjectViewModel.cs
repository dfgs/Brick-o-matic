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
using System.Windows.Media.Media3D;

namespace Brick_o_matic.Viewer.ViewModels
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



		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ProjectViewModel), new PropertyMetadata(null));
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(ProjectViewModel), new PropertyMetadata(null));
		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}



		public static readonly DependencyProperty ErrorVisibilityProperty = DependencyProperty.Register("ErrorVisibility", typeof(Visibility), typeof(ProjectViewModel), new PropertyMetadata(Visibility.Collapsed));
		public Visibility ErrorVisibility
		{
			get { return (Visibility)GetValue(ErrorVisibilityProperty); }
			set { SetValue(ErrorVisibilityProperty, value); }
		}




		public static readonly DependencyProperty CameraProperty = DependencyProperty.Register("Camera", typeof(Camera), typeof(ProjectViewModel), new PropertyMetadata(null));
		public Camera Camera
		{
			get { return (Camera)GetValue(CameraProperty); }
			set { SetValue(CameraProperty, value); }
		}

		public static readonly DependencyProperty ModelVisualProperty = DependencyProperty.Register("ModelVisual", typeof(ModelVisual3D), typeof(ProjectViewModel), new PropertyMetadata(null));
		public ModelVisual3D ModelVisual
		{
			get { return (ModelVisual3D)GetValue(ModelVisualProperty); }
			set { SetValue(ModelVisualProperty, value); }
		}

		






		public ProjectViewModel()
		{
			
		}

	
		public void SetError(string Message)
		{
			ErrorMessage = Message;
			ErrorVisibility = Message == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public void Save()
		{
			if (FileName == null) throw new Exception("FileName not specified");
			File.WriteAllText(FileName, Text);
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);
			File.WriteAllText(FileName, Text);
		}

		public void LoadFromFile(string FileName)
		{

			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);
			Text = File.OpenText(FileName).ReadToEnd();
		}
		public void Build()
		{
			PerspectiveCamera camera;
			
			Vector3D center,direction;
			int zoom;
			Box boundingBox;
			Scene scene;

			if (Text==null)
			{
				ModelVisual = null;
				return;
			}


			scene = new Scene();
			try
			{
				scene=SceneReader.Read(Text, ' ', '\t', '\r', '\n');
			}
			catch(Exception ex)
			{
				SetError(ex.Message);
				return;
			}


			boundingBox = scene.GetBoundingBox();
			center=new Vector3D(boundingBox.Position.X + boundingBox.Size.X * 0.5f, boundingBox.Position.Y + boundingBox.Size.Y * 0.5f, boundingBox.Position.Z + boundingBox.Size.Z * 0.5f);
			zoom = System.Math.Max(System.Math.Max(boundingBox.Size.X, boundingBox.Size.Y), boundingBox.Size.Z)*5;
			direction = new Vector3D(zoom, -zoom, -zoom);
			
			this.ModelVisual = GeometryUtils.CreateModelVisual(scene);

			camera = new PerspectiveCamera();
			camera.LookDirection = direction;
			camera.Position = new Point3D(center.X - direction.X, center.Y - direction.Y, center.Z - direction.Z);
			this.Camera = camera;

			SetError(null);
		}


	}
}