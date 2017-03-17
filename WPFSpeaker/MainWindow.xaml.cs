using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Input;
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

		public MainWindow() {
			InitializeComponent();

			var VM = ViewModel.Instance;
			//VM.Device = 0;
			//VM.Voice = 1;
			//VM.Dub = false;

			AppearanceManager.Current.AccentColor = Colors.OrangeRed;

			//FOR LOCALIZATION
			//FirstFloor.ModernUI.Resources.*;
		}

		//Override style
		public override void OnApplyTemplate() {
			base.OnApplyTemplate();

			//Set topmost event
			_topMostButton = GetTemplateChild("Button_TopMost") as Button;
			if (_topMostButton != null) _topMostButton.Click += (sender, args) => { Topmost = !Topmost; };
		}

		//Focus textbox
		private void ModernWindow_Activated(object sender, EventArgs e) {
			Keyboard.Focus(Pages.Synthesis.InputBox);
		}
	}
}
