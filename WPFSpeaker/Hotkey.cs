using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Runtime.CompilerServices;
using FirstFloor.ModernUI.Presentation;

namespace WPFSpeaker
{
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
	    private string _stringValue;
	    private int _intValue;
	    private bool _boolValue;
		private bool _disposed = false;
		private Action<HotKey> _removeCallback;

		public Key Key { get { return _key; } set { _key = value; Unregister(); Register(); } }
		public KeyModifier KeyModifier { get { return _keyModifier; } set { _keyModifier = value; Unregister(); Register(); } }
        public KeyType Type { get { return _type; } set { _type = value; Unregister(); Action = null; SetAction(); NotifyPropertyChanged(); } }
        public string StringValue { get { return _stringValue; } set { _stringValue = value; SetAction(); NotifyPropertyChanged(); } }
        public int IntValue { get { return _intValue; } set { _intValue = value; SetAction(); NotifyPropertyChanged(); } }
        public bool BoolValue { get { return _boolValue; } set { _boolValue = value; SetAction(); NotifyPropertyChanged(); } }
        public Action<HotKey> Action { get; private set; }
		public int Id { get; set; }

		private bool _active;
        public bool Active { get { return _active; } set { _active = value; NotifyPropertyChanged(); } }

		public ICommand DeleteCommand { get { return new RelayCommand(param => Dispose(true)); } }

        // ******************************************************************

	    public HotKey(Action<HotKey> removeCallback) {
            if (_dictHotKeyToCalBackProc == null) {
                _dictHotKeyToCalBackProc = new Dictionary<HotKey, int>();
                ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(ComponentDispatcherThreadFilterMessage);
            }
		    this._removeCallback = removeCallback;
	    }

		// ******************************************************************
        public void SetAction() {
            switch (Type) {
                case KeyType.Activate:
                    Action = hotKey => { (((MainWindow)System.Windows.Application.Current.MainWindow)).Activate(); };
                    break;
                case KeyType.Phrase:
                    if (string.IsNullOrEmpty(StringValue)) return;
                    Action = hotKey => { ViewModel.Instance.Synthesize(StringValue); };
                    break;
                case KeyType.Device:
                    Action = hotKey => { ViewModel.Instance.Device = IntValue; };
                    break;
                case KeyType.Voice:
                    Action = hotKey => { ViewModel.Instance.Voice = IntValue; };
                    break;
                case KeyType.Dub:
                    Action = hotKey => { ViewModel.Instance.Dub = BoolValue; };
                    break;
                case KeyType.Volume:
                    Action = hotKey => { ViewModel.Instance.Volume = IntValue; };
                    break;
                case KeyType.Rate:
                    Action = hotKey => { ViewModel.Instance.Rate = IntValue; };
                    break;
				case KeyType.None:
		            Action = null;
		            break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
            }
	        if (!Active) Register();
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
        Activate,
        Phrase,
        Voice,
        Device,
        Dub,
        Volume,
        Rate
    }
    // ******************************************************************
}
