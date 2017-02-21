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

		private readonly SpeechSynthesizer _ss = new SpeechSynthesizer();

		#endregion

		#region Public properties

		public string Voice {
			get { return _ss.Voice.Name; }
			set {
				if (value == "") {
					_ss.SelectVoiceByHints(VoiceGender.NotSet);
					return;
				}

				if (!CheckVoice(value)) throw new Exception("голос не найден");
				_ss.SelectVoice(value);
			}
		}

		[Range(0, 100, ErrorMessage = "Громкость должна быть в промежутке от {1} до {2}.")]
		public int Volume {
			get { return _ss.Volume; }
			set { _ss.Volume = value; }
		}

		[Range(-10, 10, ErrorMessage = "Скорость должна быть в промежутке от {1} до {2}.")]
		public int Rate {
			get { return _ss.Rate; }
			set { _ss.Rate = value; }
		}

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
		public WaveStream GetVoiceStream(string text) {
			var stream = new MemoryStream();

			_ss.SetOutputToWaveStream(stream);
			_ss.Speak(text);
			_ss.SetOutputToNull();

			return PrepareStream(stream);
		}

		#endregion

		#region Public methods

		//Преобразование стрима в WAV формат
		private static WaveStream PrepareStream(MemoryStream inputStream) {
			inputStream.Position = 0;
			var outputStream = new RawSourceWaveStream(inputStream, new WaveFormat(8000, 1));
			outputStream.Position = 0;

			return outputStream;
		}

		//Проверка голоса
		private static bool CheckVoice(string voice) {
			var voices = GetInstalledVoices();
			var result = voices.FirstOrDefault(x => x.VoiceInfo.Name == voice);
			return result != null;
		}

		#endregion
	}
}
