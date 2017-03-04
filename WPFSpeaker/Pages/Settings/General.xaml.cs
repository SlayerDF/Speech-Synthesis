﻿using System;
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
	/// <summary>
	/// Interaction logic for General.xaml
	/// </summary>
	public partial class General : UserControl
	{
		public General() {
			InitializeComponent();
			DataContext = ViewModel.Instance;		
		}

		//Set slider default value
		private void Slider_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
			var slider = (Slider)sender;
			slider.Value = Convert.ToInt32(slider.Tag);
		}
	}
}
