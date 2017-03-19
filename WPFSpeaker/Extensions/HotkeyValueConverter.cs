using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFSpeaker.Extensions
{
	class HotkeyValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (targetType == typeof(int)) return value is int ? value : Binding.DoNothing;
			if (targetType == typeof(double)) return value is int ? value : Binding.DoNothing;
			if (targetType == typeof(string)) return value is string ? value : Binding.DoNothing;
			if (targetType == typeof(bool?)) return value is bool ? value : Binding.DoNothing;
			return Binding.DoNothing;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return value;
		}
	}
}
