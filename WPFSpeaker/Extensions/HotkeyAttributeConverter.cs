using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFSpeaker.Extensions
{
	class HotkeyAttributeConverter : IValueConverter
	{
		private string[] GetEnumValues(Enum enumObj) {
			FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

			object[] attribArray = fieldInfo.GetCustomAttributes(false);

			if (attribArray.Length == 0) {
				return new [] { enumObj.ToString(), "" };
			}
			else {
				HotkeyAttribute attrib = attribArray[0] as HotkeyAttribute;
				if (attrib == null) throw new Exception("Hotkey description conversion error");
				return new[] { attrib.Key, attrib.Value };
			}
		}

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			Enum myEnum = (Enum)value;
			string[] values = GetEnumValues(myEnum);

			var par = parameter as string;
			if (par == null) return "";
			if (par == "Name")
				return values[0];
			if (par == "Description")
				return values[1];
			else return "";
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return string.Empty;
		}
	}
}
