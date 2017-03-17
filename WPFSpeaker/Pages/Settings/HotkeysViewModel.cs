using System.Collections.ObjectModel;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace WPFSpeaker
{
	public class HotkeysViewModel
	{
		//Singleton lazy realization
		private static HotkeysViewModel _instance;
		public static HotkeysViewModel Instance => _instance ?? (_instance = new HotkeysViewModel());

		//Use static property 'Instance' to access HotkeyManager instead
	    private HotkeysViewModel() {}

		public bool Toggle { get { return HotKey.Toggle; } set { HotKey.Toggle = value; } }

        private ObservableCollection<HotKey> _hotkeys = new ObservableCollection<HotKey>();
        public ObservableCollection<HotKey> Hotkeys { get { return _hotkeys; } }

		public ICommand AddCommand { get { return new RelayCommand(param => AddKey()); } }

		public void AddKey() {
	        _hotkeys.Add(new HotKey(RemoveHotkey));
	    }

		public void RemoveHotkey(HotKey hotKey) {
			_hotkeys.Remove(hotKey);
		}

	}
}
