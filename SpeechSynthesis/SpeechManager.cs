using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;

namespace SpeechSynthesis
{
    public class SpeechManager
    {
		private readonly Player _player;
		private readonly Player _player2;
		readonly Synthesis _synth;

	    public int Voice {
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

		public bool Dub { get; set; }

	    public IEnumerable<string> VoicesList {
		    get { return Synthesis.GetInstalledVoices().Select(x => x.VoiceInfo.Name); }
	    }

	    public IEnumerable<string> DeviceList {
		    get { return Player.DeviceList(); }
	    }

		public SpeechManager(int device = 0, int volume = 100, int rate = 0, string voice = "") {
			//Player Init
			_player = new Player(device);
			_player2 = new Player(0);

			//Synthesis Init
			_synth = new Synthesis();

		}

	    public void Synthesize(string text) {
			_player.AddPhrase(_synth, text);
			if (Dub) _player2.AddPhrase(_synth, text);
		}
    }
}
