using System;
using NAudio.Wave;
using System.Speech.Synthesis;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Speaker
{
	public class Synthesis
	{
		#region Private fields

		private string _voice;

		#endregion

		#region Public properties

		public string Voice {
			get { return _voice; }
			set {
				if (value == "") {
					_voice = GetDefaultVoice();
					return;
				}

				if (!CheckVoice(value)) throw new Exception("голос не найден");
				_voice = value;
			}
		}

		[Range(0, 100, ErrorMessage = "Громкость должна быть в промежутке от {1} до {2}.")]
		public int Volume { get; set; }

		[Range(-10, 10, ErrorMessage = "Скорость должна быть в промежутке от {1} до {2}.")]
		public int Rate { get; set; }

		#endregion

		#region Constructor

		public Synthesis(int volume = 100, int rate = 0, string voice = "") {
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
			ss.SelectVoice(Voice);
			ss.SpeakCompleted += (sender, args) => {
				ss.SetOutputToNull();
				ss.Dispose();
				callback(stream);
			};

			ss.SetOutputToWaveStream(stream);
			ss.SpeakAsync(text);
		}

		#endregion

		#region Public methods


		//Проверка голоса
		private static bool CheckVoice(string voice) {
			var voices = GetInstalledVoices();
			var result = voices.FirstOrDefault(x => x.VoiceInfo.Name == voice);
			return result != null;
		}

		//Получить голос по-умолчанию
		private string GetDefaultVoice() {
			SpeechSynthesizer ssTemp = new SpeechSynthesizer();
			ssTemp.SelectVoiceByHints(VoiceGender.NotSet);
			var name = ssTemp.Voice.Name;
			ssTemp.Dispose();
			return name;
		}

		#endregion
	}
}
