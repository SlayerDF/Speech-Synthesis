using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFSpeaker.Extensions
{
    class HotkeyKeyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var keyModifier = values[0] as KeyModifier? ?? KeyModifier.None;
            var key = values[1] as Key? ?? Key.None;
            return keyModifier != KeyModifier.None ? $"{keyModifier.ToString()}+{key.ToString()}" : key.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            var str = (string)value;
			if (str == "") return new object[] { KeyModifier.None, Key.None };
			object[] splitValues = new object[2];

            Key key;
            if (str.Contains("+")) {
                var strArr = str.Split('+');

                switch (strArr[0]) {
                    case "Alt":
                        splitValues[0] = KeyModifier.Alt;
                        break;
                    case "Control":
                        splitValues[0] = KeyModifier.Control;
                        break;
                    case "Shift":
                        splitValues[0] = KeyModifier.Shift;
                        break;
                }
                if(!Key.TryParse(strArr[1], false, out key)) throw new Exception("Hotkey conversion error");
                splitValues[1] = key;
                return splitValues;
            }
            else {
                splitValues[0] = KeyModifier.None;
                if(!Key.TryParse(str, false, out key)) throw new Exception("Hotkey conversion error");
                splitValues[1] = key;
                return splitValues;
            }
        }
    }
}
