using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Speech.Synthesis;

namespace SpeechSynthesis
{
	internal class Synthesis
	{
		#region Private fields

		private int _voiceIndex;
		private string _voice;
		private int _volume;
		private int _rate;
		private int _emptyLength;

		#endregion

		#region Public properties

		public int Voice {
			get { return _voiceIndex; }
			set {
				var voices = GetInstalledVoices();
				if (value < 0 || value >= voices.Count) _voiceIndex = 0;
				else _voiceIndex = value;
				_voice = voices[_voiceIndex].VoiceInfo.Name;
			}
		}

		public int Volume {
			get { return _volume; }
			set {
				if (value < 0) _volume = 0;
				else if (value > 100) _volume = 100;
				else _volume = value;
			}
		}

		public int Rate
		{
			get { return _rate; }
			set
			{
				if (value < -10) _rate = 0;
				else if (value > 10) _rate = 100;
				else _rate = value;
			}
		}

		#endregion

		#region Constructor

		public Synthesis(int volume = 100, int rate = 0, int voice = 0) {
			Volume = volume;
			Rate = rate;
			Voice = voice;

			using (var stream = new MemoryStream())
			using (var ss = new SpeechSynthesizer() {Volume = volume, Rate = rate}) {
				ss.SetOutputToWaveStream(stream);
				ss.Speak("");
				_emptyLength = (int)stream.Length;
			}
		}

		#endregion

		#region Public methods

		//Получить коллекцию установленных голосов
		public static ReadOnlyCollection<InstalledVoice> GetInstalledVoices() {
			var ssTemp = new SpeechSynthesizer();
			var voices = ssTemp.GetInstalledVoices();
			ssTemp.Dispose();
			return voices;
		}

		//Текст в голосовой стрим
		public void GetVoiceStream(string text, Action<MemoryStream> callback) {
			var stream = new MemoryStream();

			var ss = new SpeechSynthesizer {
				Volume = Volume,
				Rate = Rate
			};
			ss.SelectVoice(_voice);
			ss.SpeakCompleted += (sender, args) => {
				ss.SetOutputToNull();
				ss.Dispose();
				if (stream.Length != _emptyLength)
					callback(stream);
			};

			ss.SetOutputToWaveStream(stream);
			ss.SpeakAsync(text);
		}

		#endregion
	}
}
