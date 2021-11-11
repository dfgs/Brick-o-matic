using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Brick_o_matic.Viewer.Views
{
	public class TextEditorEx : TextEditor
	{


		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(TextEditorEx), new PropertyMetadata(0,PositionPropertyChanged));
		public int Position
		{
			get { return (int)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}



		public TextEditorEx()
		{
			TextArea.Caret.PositionChanged += Caret_PositionChanged;
		}

		private void Caret_PositionChanged(object sender, EventArgs e)
		{
			this.Position = CaretOffset;
		}

		private static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextEditorEx editor;

			editor = d as TextEditorEx;
			editor.CaretOffset = editor.Position;
		}



	}
}
