using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Serialization;
using FirstFloor.ModernUI.Presentation;
using WPFSpeaker.Extensions;

namespace WPFSpeaker
{
	[XmlRootAttribute("HotkeyManager", IsNullable = false)]
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

		public HotKey AddKey() {
			var key = new HotKey(RemoveHotkey);
			_hotkeys.Add(key);
			return key;
		}

		public void RemoveHotkey(HotKey hotKey) {
			_hotkeys.Remove(hotKey);
		}

		//Settings

		public ICommand SaveHotkeysCommand { get { return new RelayCommand(param => SaveSettings()); } }
		public ICommand LoadHotkeysCommand { get { return new RelayCommand(param => LoadSettings()); } }

		public void SaveSettings() {
			FileManager.Instance.Serialize(typeof(HotkeysViewModel), Instance);
		}

		public void LoadSettings() {
			var keys = FileManager.Instance.Deserialize(typeof(HotkeysViewModel)) as HotkeysViewModel;
			if (keys == null) return;
			//Toggle = keys.Toggle
			foreach (var key in keys.Hotkeys) {
				if (!HotKey.ValidateValue(key.Type, key.Value)) continue;
				var newKey = AddKey();
				newKey.Type = key.Type;
				newKey.Value = key.Value;
				newKey.Option = key.Option;
				newKey.Key = key.Key;
				newKey.KeyModifier = key.KeyModifier;
			}
		}
	}
}
