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
    }
}
