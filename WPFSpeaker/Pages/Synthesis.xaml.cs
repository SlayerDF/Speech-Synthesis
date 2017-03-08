using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
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
using FirstFloor.ModernUI.Presentation;

namespace WPFSpeaker.Pages
{
	/// <summary>
	/// Interaction logic for Synthesis.xaml
	/// </summary>
	public partial class Synthesis : UserControl
	{
		public static TextBox InputBox;

		private List<string> _story = new List<string>();
		private int _index;

		public Synthesis() {
			InitializeComponent();

			InputBox = SayTextBox;
		}

		private void Say_OnClick(object sender, RoutedEventArgs e) {
			ViewModel.Instance.Synthesize(SayTextBox.Text);
		}

		//System keys
		private void SayTextBox_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				if (SayTextBox.Text == "") return;

				if (_story.Count >= 5) _story.RemoveAt(0);
				_story.Add(SayTextBox.Text);
				_index = _story.Count;

				ViewModel.Instance.Synthesize(SayTextBox.Text);
				SayTextBox.Text = "";
			}
			else if (e.Key == Key.Up) {
				_index--;
				if (_index < 0) {
					_index++;
					return;
				}
				SayTextBox.Text = _story[_index];
			}
			else if (e.Key == Key.Down) {
				_index++;
				if (_index == _story.Count) {
					SayTextBox.Text = "";
					return;
				}
				if (_index > _story.Count) {
					_index--;
					return;
				}
				SayTextBox.Text = _story[_index];
			}
		}
	}
}
