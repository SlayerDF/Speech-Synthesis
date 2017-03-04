using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;

namespace SpeechSynthesis
{
	internal class Synthesis
	{
		#region Private fields

		private int _voiceIndex;
		private string _voice;

		#endregion

		#region Public properties

		public int Voice {
			get { return _voiceIndex; }
			set {
				var voices = GetInstalledVoices();
				if (value < 0 || value >= voices.Count) _voiceIndex = 0;
				else _voiceIndex = value;
				_voice = voices[value].VoiceInfo.Name;
			}
		}

		[Range(0, 100, ErrorMessage = "Громкость должна быть в промежутке от {1} до {2}.")]
		public int Volume { get; set; }

		[Range(-10, 10, ErrorMessage = "Скорость должна быть в промежутке от {1} до {2}.")]
		public int Rate { get; set; }

		#endregion

		#region Constructor

		public Synthesis(int volume = 100, int rate = 0, int voice = 0) {
			Volume = volume;
			Rate = rate;
			Voice = voice;
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
				callback(stream);
			};

			ss.SetOutputToWaveStream(stream);
			ss.SpeakAsync(text);
		}

		#endregion
	}
}
