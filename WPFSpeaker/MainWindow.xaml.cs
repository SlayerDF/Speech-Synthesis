using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

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
            AppearanceManager.Current.AccentColor = Colors.OrangeRed;

            var VM = ViewModel.Instance;
			//VM.Device = 1;
			//VM.Voice = 1;
			//VM.Dub = true;

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
