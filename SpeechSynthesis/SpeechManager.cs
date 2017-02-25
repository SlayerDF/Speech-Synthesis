using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Speech.Synthesis;

namespace SpeechSynthesis
{
    public class SpeechManager
    {
		private readonly Player _player;
		private readonly Player _player2;
		readonly Synthesis _synth;

	    public string Voice {
		    get { return _synth.Voice; } 
		    set { _synth.Voice = value; }
	    }

	    public int Volume {
			get { return _synth.Volume; }
			set { _synth.Volume = value; }
	    }

	    public int Rate {
			get { return _synth.Rate; }
			set { _synth.Rate = value; }
	    }

	    public int Device {
		    get { return _player.Device; }
			set { _player.Device = value; }
	    }

		public SpeechManager(int device = 0, int volume = 100, int rate = 0, string voice = "") {
			//Player Init
			_player = new Player(device);
			_player2 = new Player(0);

			//Synthesis Init
			_synth = new Synthesis();

		}

	    public void Synthesize(string text, bool dub = false) {
			_player.AddPhrase(_synth, text);
			if (dub) _player2.AddPhrase(_synth, text);
		}

	    public static ReadOnlyCollection<InstalledVoice> GetInstalledVoices() {
		    return Synthesis.GetInstalledVoices();
	    }

	    public static List<string> DeviceList() {
		    return Player.DeviceList();
	    }
    }
}
