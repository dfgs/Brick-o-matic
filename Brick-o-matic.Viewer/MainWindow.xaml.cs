using Brick_o_matic.Viewer.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace Brick_o_matic.Viewer
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ProjectsViewModel projects;

		public MainWindow()
		{
			projects = new ProjectsViewModel();
			InitializeComponent();
			DataContext = projects;
		}

		private void Try(Action Action)
		{
			try
			{
				Action();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Error");
			}
		}


		private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true; e.Handled = true;
		}

		private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Try(() => projects.AddNew());
		}
		private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true; e.Handled = true;
		}

		private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dialog;

			dialog = new OpenFileDialog();
			dialog.Filter = "Brick files|*.brk|All files|*.*";
			dialog.Title = "Open project";
			dialog.DefaultExt = "brk";
			
			if (dialog.ShowDialog(this)??false)
			{
				Try(() => projects.Open(dialog.FileName));
			}
			
		}
		private void SaveAsCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (projects.SelectedItem!=null); e.Handled = true;
		}

		private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dialog;

			dialog = new SaveFileDialog();
			dialog.Filter = "Brick files|*.brk|All files|*.*";
			dialog.Title = "Save project as";
			dialog.DefaultExt = "brk";
			dialog.FileName = projects.SelectedItem.FileName;
			if (dialog.ShowDialog(this) ?? false)
			{
				Try(() => projects.SelectedItem.SaveAs(dialog.FileName));
			}
		}
		private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = ((projects.SelectedItem != null) && (projects.SelectedItem.FileName != null) && (projects.SelectedItem.IsModified )); e.Handled = true;
		}

		private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Try(() => projects.SelectedItem.Save());
		}

		private void BuildCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (projects.SelectedItem != null) ; e.Handled = true;
		}

		private void BuildCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Try(() => projects.SelectedItem.Build());
		}

		private void ZoomOutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (projects.SelectedItem != null); e.Handled = true;
		}

		private void ZoomOutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Try(() => projects.SelectedItem.Zoom+=1);
		}

		private void ZoomInCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (projects.SelectedItem != null) && (projects.SelectedItem.Zoom>1); e.Handled = true;
		}

		private void ZoomInCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Try(() => projects.SelectedItem.Zoom -= 1);
		}



	}
}
