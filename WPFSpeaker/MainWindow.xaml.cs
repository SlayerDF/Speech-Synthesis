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
			AppearanceManager.Current.AccentColor = Colors.OrangeRed;

			ViewModel.Instance.LoadSettings();
			HotkeysViewModel.Instance.LoadSettings();

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
			if (Pages.Synthesis.InputBox == null) return;
			Keyboard.Focus(Pages.Synthesis.InputBox);
		}
	}
}
