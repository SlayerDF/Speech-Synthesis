using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using FirstFloor.ModernUI.Presentation;
using WPFSpeaker.Extensions;

namespace WPFSpeaker
{
	[XmlRoot("Hotkey", IsNullable = false)]
	public class HotKey : IDisposable, INotifyPropertyChanged
	{
		//INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// ******************************************************************

		private static Dictionary<HotKey, int> _dictHotKeyToCalBackProc;
		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, UInt32 fsModifiers, UInt32 vlc);
		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

	    private static bool _toggle;
	    public static bool Toggle {
		    get { return _toggle; }
		    set {
			    if (_dictHotKeyToCalBackProc == null) return;
			    _toggle = value;
			    if (value) foreach (var hotkey in _dictHotKeyToCalBackProc.Keys) hotkey.Register();
				else foreach (var hotkey in _dictHotKeyToCalBackProc.Keys) hotkey.Unregister();
			}
	    }

	    public const int WmHotKey = 0x0312;
		public ViewModel ViewModelContext => ViewModel.Instance;

	    private Key _key;
	    private KeyModifier _keyModifier;
	    private KeyType _type;
		private object _value;
		private bool _option = false;
		private bool _disposed = false;
		private Action<HotKey> _removeCallback;

		public Key Key { get { return _key; } set { _key = value; Unregister(); Register(); } }
		public KeyModifier KeyModifier { get { return _keyModifier; } set { _keyModifier = value; Unregister(); Register(); } }
        public KeyType Type { get { return _type; } set { _type = value; ClearValue(); Action = null; SetAction(); NotifyPropertyChanged(); } }
		public object Value { get { return _value; } set { _value = value; SetAction(); NotifyPropertyChanged(); } }
		public bool Option { get { return _option; } set { _option = value; NotifyPropertyChanged(); } }
		[XmlIgnore()]
		public Action<HotKey> Action { get; private set; }
		[XmlIgnore()]
		public int Id { get; set; }

		private bool _active;
		[XmlIgnore()]
		public bool Active { get { return _active; } set { _active = value; NotifyPropertyChanged(); } }

		public ICommand DeleteCommand { get { return new RelayCommand(param => Dispose(true)); } }

        // ******************************************************************

		public HotKey() { }

	    public HotKey(Action<HotKey> removeCallback) {
            if (_dictHotKeyToCalBackProc == null) {
                _dictHotKeyToCalBackProc = new Dictionary<HotKey, int>();
                ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(ComponentDispatcherThreadFilterMessage);
            }
		    this._removeCallback = removeCallback;
	    }

		// ******************************************************************
		private void ClearValue() {
			switch (Type) {
				case KeyType.None:
					Value = 0;
					break;
				case KeyType.Activate:
					Value = 0;
					break;
				case KeyType.Phrase:
					Value = "";
					break;
				case KeyType.Voice:
					Value = 0;
					break;
				case KeyType.Device:
					Value = 0;
					break;
				case KeyType.Replication:
					Value = false;
					break;
				case KeyType.Volume:
					Value = 100;
					break;
				case KeyType.Rate:
					Value = 0;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		// ******************************************************************
		private void SetAction() {
			int increment;
            switch (Type) {
                case KeyType.Activate:
                    Action = hotKey => {
						if (Application.Current.MainWindow.WindowState == WindowState.Minimized) Application.Current.MainWindow.WindowState = WindowState.Normal;
						(((MainWindow)System.Windows.Application.Current.MainWindow)).Activate();
					};
                    break;
                case KeyType.Phrase:
                    if (string.IsNullOrEmpty(Value.ToString())) return;
                    Action = hotKey => { ViewModel.Instance.Synthesize(Value.ToString()); };
                    break;
                case KeyType.Device:
                    Action = hotKey => { ViewModel.Instance.Device = Convert.ToInt32(Value); };
                    break;
                case KeyType.Voice:
                    Action = hotKey => { ViewModel.Instance.Voice = Convert.ToInt32(Value); };
                    break;
                case KeyType.Replication:
                    Action = hotKey => { ViewModel.Instance.Replication = Option ? !ViewModel.Instance.Replication : Convert.ToBoolean(Value); };
                    break;
                case KeyType.Volume:
		            increment = Convert.ToInt32(Value);
                    Action = hotKey => {
	                    if (Option) ViewModel.Instance.Volume += increment;
						else ViewModel.Instance.Volume = Convert.ToInt32(Value); 
                    };
                    break;
                case KeyType.Rate:
					increment = Convert.ToInt32(Value);
					Action = hotKey => {
	                    if (Option) ViewModel.Instance.Rate += increment;
						else ViewModel.Instance.Rate = Convert.ToInt32(Value); 
                    };
                    break;
				case KeyType.None:
		            Action = null;
		            break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
            }
	    }

		public static bool ValidateValue(KeyType type, object value) {
			int intParse;
			bool boolParse;
			if (type == KeyType.None || type == KeyType.Activate || type == KeyType.Phrase)
				return true;
			if (type == KeyType.Replication)
				return bool.TryParse(value.ToString(), out boolParse);
			if (type == KeyType.Voice || type == KeyType.Device || type == KeyType.Volume || type == KeyType.Rate)
				return int.TryParse(value.ToString(), out intParse);
			throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}

        // ******************************************************************
        public bool Register() {
	        bool result = false;
			int virtualKeyCode = KeyInterop.VirtualKeyFromKey(Key);
			Id = virtualKeyCode + ((int)KeyModifier * 0x10000);

			if (Key != Key.None && KeyModifier != KeyModifier.None && Type != KeyType.None && Action != null && !_dictHotKeyToCalBackProc.ContainsValue(Id)) {
				result = RegisterHotKey(IntPtr.Zero, Id, (UInt32) KeyModifier, (UInt32) virtualKeyCode);
				if (_dictHotKeyToCalBackProc.ContainsKey(this)) _dictHotKeyToCalBackProc[this] = Id;
				else _dictHotKeyToCalBackProc.Add(this, Id);
	        }

	        Debug.Print(result.ToString() + ", " + Id + ", " + virtualKeyCode);
			Active = result;
			return result;
		}

		// ******************************************************************
		public void Unregister() {
		    if (_dictHotKeyToCalBackProc == null) return;
			if (_dictHotKeyToCalBackProc.ContainsKey(this)) {
				UnregisterHotKey(IntPtr.Zero, Id);
				_dictHotKeyToCalBackProc[this] = -1;
				Active = false;
			}
		}

		// ******************************************************************
		private static void ComponentDispatcherThreadFilterMessage(ref MSG msg, ref bool handled) {
			if (!handled) {
				if (msg.message == WmHotKey) {
					HotKey hotKey;
				    int Id = (int)msg.wParam;
				    hotKey = _dictHotKeyToCalBackProc.FirstOrDefault(x => x.Value == Id).Key;
                    if (hotKey != null) {
						if (hotKey.Action != null) {
							hotKey.Action.Invoke(hotKey);
						}
						handled = true;
					}
				}
			}
		}

		// ******************************************************************
		// Implement IDisposable.
		// Do not make this method virtual.
		// A derived class should not be able to override this method.
		public void Dispose() {
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		// ******************************************************************
		// Dispose(bool disposing) executes in two distinct scenarios.
		// If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources
		// can be _disposed.
		// If disposing equals false, the method has been called by the
		// runtime from inside the finalizer and you should not reference
		// other objects. Only unmanaged resources can be _disposed.
		protected virtual void Dispose(bool disposing) {
			// Check to see if Dispose has already been called.
			if (!this._disposed) {
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing) {
					// Dispose managed resources.
					Unregister();
				}

				// Note disposing has been done.
				_disposed = true;
				_dictHotKeyToCalBackProc.Remove(this);
				_removeCallback(this);
			}
		}
	}

	// ******************************************************************
	[Flags]
	public enum KeyModifier
	{
		None = 0x0000,
		Alt = 0x0001,
		Control = 0x0002,
		NoRepeat = 0x4000,
		Shift = 0x0004,
		Win = 0x0008
	}

    [Flags]
    public enum KeyType
    {
        None,
		[Hotkey("Bring to the top", "Bring main window to the top")]
        Activate,
		[Hotkey("Synthesize phrase", "Synthesize entered phrase")]
        Phrase,
		[Hotkey("Set voice", "Change selected voice")]
        Voice,
		[Hotkey("Set device", "Change selected output device")]
        Device,
		[Hotkey("Set replication", "Change replication option")]
        Replication,
		[Hotkey("Set volume", "Set synthesis volume")]
        Volume,
		[Hotkey("Set rate", "Set synthesis rate")]
        Rate
    }
    // ******************************************************************
}
