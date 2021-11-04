using Brick_o_matic.Math;
using Brick_o_matic.Parsing;
using Brick_o_matic.Primitives;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Xml;

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






		public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(TextDocument), typeof(ProjectViewModel), new PropertyMetadata(null));
		public TextDocument Document
		{
			get { return (TextDocument)GetValue(DocumentProperty); }
			set { SetValue(DocumentProperty, value); }
		}



		public static readonly DependencyProperty HighlightingDefinitionProperty = DependencyProperty.Register("HighlightingDefinition", typeof(IHighlightingDefinition), typeof(ProjectViewModel), new PropertyMetadata(null));
		public IHighlightingDefinition HighlightingDefinition
		{
			get { return (IHighlightingDefinition)GetValue(HighlightingDefinitionProperty); }
			set { SetValue(HighlightingDefinitionProperty, value); }
		}





		public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register("IsModified", typeof(bool), typeof(ProjectViewModel), new PropertyMetadata(false));
		public bool IsModified
		{
			get { return (bool)GetValue(IsModifiedProperty); }
			set { SetValue(IsModifiedProperty, value); }
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


		private Scene scene;





		public ProjectViewModel()
		{
			Document = new TextDocument();

			var hlManager = HighlightingManager.Instance;
			
			using (Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Brick_o_matic.Viewer.Highlighting.xshd"))
			{
				using (XmlTextReader reader = new XmlTextReader(s))
				{
					HighlightingDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
				}
			}

			UpdateCamera();
		}


		public void SetError(string Message)
		{
			ErrorMessage = Message;
			ErrorVisibility = Message == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public void Save()
		{
			if (FileName == null) throw new Exception("FileName not specified");
			File.WriteAllText(FileName,Document.Text);
			IsModified = false;
		}

		public void SaveAs(string FileName)
		{
			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);
			File.WriteAllText(FileName, Document.Text);
			IsModified = false;
		}

		public void LoadFromFile(string FileName)
		{

			this.FileName = FileName;
			this.Name = Path.GetFileNameWithoutExtension(FileName);

			Document = new TextDocument(File.ReadAllText(FileName));
			IsModified = false;

			try
			{
				Build();
			}
			catch
			{ }
		}

		private static void AnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectViewModel vm;
			vm = d as ProjectViewModel;
			if (vm != null) vm.UpdateCamera();
		}

		private void UpdateCamera()
		{
			Vector3D center, direction;
			Point3D position;
			PerspectiveCamera camera;
			int cameraLength;
			Box boundingBox;

			if (scene == null) boundingBox = new Box();
			else boundingBox = scene.GetBoundingBox();

			center = new Vector3D(boundingBox.Position.X + boundingBox.Size.X * 0.5f, boundingBox.Position.Y + boundingBox.Size.Y * 0.5f, boundingBox.Position.Z + boundingBox.Size.Z * 0.5f);
			cameraLength = System.Math.Max(System.Math.Max(boundingBox.Size.X, boundingBox.Size.Y), boundingBox.Size.Z) * Zoom;

			position = new Point3D(
				System.Math.Sin(Angle * System.Math.PI / 180.0f) * cameraLength,
				System.Math.Sin(Elevation * System.Math.PI / 180.0f) * cameraLength, 
				System.Math.Cos(Angle * System.Math.PI / 180.0f)*cameraLength);
			
			direction = center - new Vector3D( position.X,position.Y,position.Z);

			camera = new PerspectiveCamera();
			camera.LookDirection = direction;
			camera.Position = position;
			this.Camera = camera;
		}
		public void Build()
		{

			if ((Document==null) || string.IsNullOrWhiteSpace(Document.Text))
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
				return;
			}
			catch (Exception ex)
			{
				SetError(ex.Message);
				return;
			}
			UpdateCamera();
			this.ModelVisual = GeometryUtils.CreateModelVisual(scene);

			SetError(null);
		}


	}
}