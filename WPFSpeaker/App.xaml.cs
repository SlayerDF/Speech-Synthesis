using System.Windows;

namespace WPFSpeaker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Exit(object sender, ExitEventArgs e) {
			ViewModel.Instance.SaveSettings();
			HotkeysViewModel.Instance.SaveSettings();
		}
	}
}
