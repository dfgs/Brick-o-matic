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
			Part part;
			Model model;
			Vector3D center,direction;
			int zoom;

			if (Text==null)
			{
				ModelVisual = null;
				return;
			}

			try
			{
				part = new Part();
				foreach (IPrimitive primitive in PrimitiveReader.Read(Text, ' ', '\t', '\r', '\n'))
				{
					part.Add(primitive);
				}
			}
			catch(Exception ex)
			{
				SetError(ex.Message);
				return;
			}

			model = part.Build();

			
			center=new Vector3D(model.BoundingBox.Position.X + model.BoundingBox.Size.X * 0.5f, model.BoundingBox.Position.Y + model.BoundingBox.Size.Y * 0.5f, model.BoundingBox.Position.Z + model.BoundingBox.Size.Z * 0.5f);
			zoom = System.Math.Max(System.Math.Max(model.BoundingBox.Size.X, model.BoundingBox.Size.Y), model.BoundingBox.Size.Z)*10;
			direction = new Vector3D(zoom, -zoom, -zoom);
			
			this.ModelVisual = GeometryUtils.CreateModelVisual(model);

			camera = new PerspectiveCamera();
			camera.LookDirection = direction;
			camera.Position = new Point3D(center.X - direction.X, center.Y - direction.Y, center.Z - direction.Z);
			this.Camera = camera;

			SetError(null);
		}


	}
}