using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Presentation;
using SpeechSynthesis;

namespace WPFSpeaker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ModernWindow
	{
		private Button _topMostButton;

		private Grid _window;

		public MainWindow() {
			InitializeComponent();

			var VM = ViewModel.Instance;
			//VM.Device = 1;
			//VM.Voice = 1;
			//VM.Dub = true;

			AppearanceManager.Current.AccentColor = Colors.OrangeRed;

			//FOR LOCALIZATION
			//FirstFloor.ModernUI.Resources.*;
		}

		//Override style
		public override void OnApplyTemplate() {
			base.OnApplyTemplate();

			var template = this.Template;

			_window = GetTemplateChild("LayoutRoot") as Grid;

			//Set topmost event
			_topMostButton = GetTemplateChild("Button_TopMost") as Button;
			_topMostButton.Click += (sender, args) => { Topmost = !Topmost; };
		}
	}
}
