using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.IO;
using System.Media;
using System.Reflection;

namespace WPFSpeaker
{
	public class HotKey : IDisposable
	{
		private static Dictionary<int, HotKey> _dictHotKeyToCalBackProc;

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, UInt32 fsModifiers, UInt32 vlc);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		public const int WmHotKey = 0x0312;

        public ViewModel ViewModelContext => ViewModel.Instance;

        private bool _disposed = false;

	    private Key _key;
	    private KeyModifier _keyModifier;
	    private KeyType _type;
	    private string _stringValue;
	    private int _intValue;
	    private bool _boolValue;
	    private bool _registered;

		public Key Key { get { return _key; } set { _key = value; Unregister(); Register(); } }
		public KeyModifier KeyModifiers { get { return _keyModifier; } set { _keyModifier = value; Unregister(); Register(); } }
        public KeyType Type { get { return _type; } set { _type = value; Unregister(); Action = null; } }
        public string StringValue { get { return _stringValue; } set { _stringValue = value; SetAction(); } }
        public int IntValue { get { return _intValue; } set { _intValue = value; SetAction(); } }
        public bool BoolValue { get { return _boolValue; } set { _boolValue = value; SetAction(); } }
        public Action<HotKey> Action { get; private set; }
		public int Id { get; set; }
        public bool Registered => _registered;

	    // ******************************************************************
	    public void SetAction() {
            if (Key == Key.None || Type == KeyType.None) return;

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
            }
	        if (!_registered) Register();
	    }

        // ******************************************************************
        public bool Register() {
		    if (Key == Key.None || Type == KeyType.None || Action == null)
		        return false;

			int virtualKeyCode = KeyInterop.VirtualKeyFromKey(Key);
			Id = virtualKeyCode + ((int)KeyModifiers * 0x10000);
            
			if (_dictHotKeyToCalBackProc == null) {
				_dictHotKeyToCalBackProc = new Dictionary<int, HotKey>();
				ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(ComponentDispatcherThreadFilterMessage);
			}

            //if (_dictHotKeyToCalBackProc.ContainsKey(Id)) return false;

            bool result = RegisterHotKey(IntPtr.Zero, Id, (UInt32)KeyModifiers, (UInt32)virtualKeyCode);

            if (!_dictHotKeyToCalBackProc.ContainsKey(Id)) _dictHotKeyToCalBackProc.Add(Id, this);

			Debug.Print(result.ToString() + ", " + Id + ", " + virtualKeyCode);
            _registered = result;
			return result;
		}

		// ******************************************************************
		public void Unregister() {
		    if (_dictHotKeyToCalBackProc == null) return;
			HotKey hotKey;
			if (_dictHotKeyToCalBackProc.TryGetValue(Id, out hotKey)) {
				UnregisterHotKey(IntPtr.Zero, Id);
			    _registered = false;
			}
		}

		// ******************************************************************
		private static void ComponentDispatcherThreadFilterMessage(ref MSG msg, ref bool handled) {
			if (!handled) {
				if (msg.message == WmHotKey) {
					HotKey hotKey;

					if (_dictHotKeyToCalBackProc.TryGetValue((int)msg.wParam, out hotKey)) {
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
			}
		}
	}

	// ******************************************************************
	[Flags]
	public enum KeyModifier
	{
		None = 0x0000,
		Alt = 0x0001,
		Ctrl = 0x0002,
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
