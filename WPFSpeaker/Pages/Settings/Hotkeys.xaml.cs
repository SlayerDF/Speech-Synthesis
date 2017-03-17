using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WPFSpeaker.Pages.Settings
{
	/// <summary>z
	/// Interaction logic for Hotkeys.xaml
	/// </summary>
	public partial class Hotkeys : UserControl
	{
	    public Hotkeys() {
			InitializeComponent();
		    DataContext = HotkeysViewModel.Instance;
		}

        private void Slider_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            var slider = (Slider)sender;
            slider.Value = Convert.ToInt32(slider.Tag);
        }

        //Set hotkey
        private void Hotkey_KeyDown(object sender, KeyEventArgs e) {
            var textBox = sender as TextBox;
            if (textBox == null) return;

	        if (e.Key == Key.Escape) {
		        textBox.Text = "";
		        return;
	        }

	        Key key;
	        if (e.Key == Key.System && e.KeyboardDevice.Modifiers == ModifierKeys.Alt) key = e.SystemKey;
	        else key = e.Key;

			var keyCode = Convert.ToInt32(key);     
            if (keyCode > 115 && keyCode < 122 || keyCode == 156) return;

	        var mod = e.KeyboardDevice.Modifiers;
            textBox.Text = $"{(mod != ModifierKeys.None ? mod.ToString() + "+" : "")}{key.ToString()}";
        }
	}
}
