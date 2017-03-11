using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFSpeaker
{
	public class HotkeysViewModel
	{
		//Singleton lazy realization
		private static HotkeysViewModel _instance;
		public static HotkeysViewModel Instance => _instance ?? (_instance = new HotkeysViewModel());

		//Use static property 'Instance' to access HotkeyManager instead
	    private HotkeysViewModel() {
            _hotkeys.Add(new HotKey() {
                Type = KeyType.Dub,
                Key = Key.J,
                KeyModifiers = KeyModifier.Alt,
                BoolValue = false
            });
            _hotkeys.Add(new HotKey() {
                Type = KeyType.Dub,
                    Key = Key.K,
                    KeyModifiers = KeyModifier.Alt,
                    BoolValue = true
            });
	    }

        public ObservableCollection<HotKey> _hotkeys = new ObservableCollection<HotKey>();
        public ObservableCollection<HotKey> Hotkeys { get { return _hotkeys; } }

	}
}
