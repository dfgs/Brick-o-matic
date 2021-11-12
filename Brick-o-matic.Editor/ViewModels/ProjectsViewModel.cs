using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Brick_o_matic.Editor.ViewModels
{
	public class ProjectsViewModel:DependencyObject
	{

		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<ProjectViewModel>), typeof(ProjectsViewModel), new PropertyMetadata(null));
		public ObservableCollection<ProjectViewModel> Items
		{
			get { return (ObservableCollection<ProjectViewModel>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}



		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(ProjectViewModel), typeof(ProjectsViewModel), new PropertyMetadata(null));
		public ProjectViewModel SelectedItem
		{
			get { return (ProjectViewModel)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}




		public ProjectsViewModel()
		{
			Items = new ObservableCollection<ProjectViewModel>();
		}

		public void AddNew()
		{
			ProjectViewModel project = new ProjectViewModel();
			Items.Add(project);
			SelectedItem = project;
		}
		public void CloseCurrent()
		{
			Items.Remove(SelectedItem);
			SelectedItem = Items.FirstOrDefault();
		}
		public async Task OpenAsync(string FileName)
		{
			ProjectViewModel project = new ProjectViewModel();
			
			await project.LoadFromFileAsync(FileName);
			Items.Add(project);
			SelectedItem = project;

		}

		
	}
}
