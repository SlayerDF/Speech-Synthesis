using System;
using NAudio.Wave;
using System.Speech.Synthesis;
using System.IO;
using System.Collections.ObjectModel;

namespace Speaker
{
	public class Synthesis
	{
		SpeechSynthesizer ss = new SpeechSynthesizer();

		public Synthesis(int Volume, int Rate, string Voice = "") {
			if (Volume < 0 || Volume > 100) throw new Exception("громкость должна быть в промежутке от 0 до 100");
			if (Rate < -10 || Rate > 10) throw new Exception("скорость должна быть в промежутке от -10 до 10");

			//SpeechSynthesizer Init
			ss.Volume = Volume;    // от 0 до 100, def 100
			ss.Rate = Rate;        //от -10 до 10, def 0
			if (Voice != "") ss.SelectVoice(Voice);
			else ss.SelectVoiceByHints(VoiceGender.NotSet);

			string test = ss.Voice.Name;
		}

		//Получить коллекцию установленных голосов
		public static ReadOnlyCollection<InstalledVoice> GetInstalledVoices() {
			var ss_temp = new SpeechSynthesizer();
			var voices = ss_temp.GetInstalledVoices();
			ss_temp.Dispose();
			return voices;
		}

		//Сменить голос
		public void SetVoice(string Voice) {
			ss.SelectVoice(Voice);
		}

		//Текст в голосовой стрим
		public WaveStream GetVoiceStream(string text) {
			var stream = new MemoryStream();

			ss.SetOutputToWaveStream(stream);
			ss.Speak(text);
			ss.SetOutputToNull();

			return PrepareStream(stream);
		}

		//Преобразование стрима в WAV формат
		WaveStream PrepareStream(MemoryStream InputStream) {
			InputStream.Position = 0;
			var OutputStream = new RawSourceWaveStream(InputStream, new WaveFormat(8000, 1));
			OutputStream.Position = 0;

			return OutputStream;
		}
	}
}
