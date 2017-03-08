using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFSpeaker
{
	public class HotkeyManager
	{
		//Singleton lazy realization
		private static HotkeyManager _instance;
		public static HotkeyManager Instance => _instance ?? (_instance = new HotkeyManager());

		//Use static property 'Instance' to get FakeRepository instead
		private HotkeyManager() { }

		private List<HotKey> _hotkeys = new List<HotKey>();

		public void AddPhraseHotkey(string key, string text, string modifier) {
			_hotkeys.Add(new HotKey(ParseKey(key), ParseModifier(modifier), hotKey => {
				
			}));
		}

		private Key ParseKey(string keyText) {
			Key key;
			if (!Key.TryParse(keyText, false, out key)) throw new Exception("Wrong key");
			return key;
		}

		private KeyModifier ParseModifier(string modifier) {
			switch (modifier) {
				case "None":
					return KeyModifier.None;
				case "Ctrl":
					return KeyModifier.Ctrl;
				case "Alt":
					return KeyModifier.Alt;
				case "Shift":
					return KeyModifier.Shift;
				default:
					throw new Exception("Wrong modifier");
			}
		}
	}
}
