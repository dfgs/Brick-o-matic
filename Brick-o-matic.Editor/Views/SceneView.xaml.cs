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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModelLib;

namespace Brick_o_matic.Editor.Views
{
	/// <summary>
	/// Logique d'interaction pour SceneView.xaml
	/// </summary>
	public partial class SceneView : UserControl
	{


		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(IViewModel), typeof(SceneView), new PropertyMetadata(null));
		public IViewModel SelectedItem
		{
			get { return (IViewModel)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}



		public SceneView()
		{
			InitializeComponent();
		}

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			IViewModel oldItem;

			oldItem = e.OldValue as IViewModel;
			SelectedItem = e.NewValue as IViewModel;

			if (oldItem != null) oldItem.IsSelected = false;
			if (SelectedItem != null) SelectedItem.IsSelected = true;
		}
	}
}
