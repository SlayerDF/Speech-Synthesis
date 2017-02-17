using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace Speaker
{
	public class Player
	{
		Timer NewPhrasesTimer = new Timer();
		Queue<WaveStream> phrases = new Queue<WaveStream>();

		WaveOut output;
		WaveFileReader reader;
		Stream stream;

		public Player(int Interval, int DeviceNumber = 0) {
			if (Interval < 100 || Interval > 5000) throw new Exception("интервал проверки фраз должен быть в промежутке от 100 до 5000");
			if (DeviceNumber < 0 || DeviceNumber > WaveOut.DeviceCount) throw new Exception("номер устройства воспроизведения должен быть в промежутке от 0 до " + WaveOut.DeviceCount);

			//Output Init
			output = new WaveOut();
			output.DeviceNumber = DeviceNumber;
			output.PlaybackStopped += output_PlaybackStopped;

			//Timer Init
			NewPhrasesTimer.Interval = 500;
			NewPhrasesTimer.Tick += new EventHandler(CheckNewPhrases);
			NewPhrasesTimer.Start();
		}

		//Получить список устройств воспроизведения
		public static List<string> DeviceList() {
			List<string> devices = new List<string>();
			for (int i = 0, d = WaveOut.DeviceCount; i < d; i++)
				devices.Add(WaveOut.GetCapabilities(i).ProductName);
			return devices;
		}

		//Установить источник вывода звука
		public void SetDevice(int DeviceNumber) {
			if (DeviceNumber < 0 || DeviceNumber > WaveOut.DeviceCount) throw new Exception("номер устройства воспроизведения должен быть в промежутке от 0 до " + WaveOut.DeviceCount);
			output.DeviceNumber = DeviceNumber;
		}

		//Добавление фразы в очередь
		public void AddPhrase(Synthesis synth, string text) {
			phrases.Enqueue(synth.GetVoiceStream(text));
		}

		//Проигрывание остановилось
		void output_PlaybackStopped(object sender, StoppedEventArgs e) {
			stream.Dispose();
			reader.Dispose();
		}

		//Проверка новых фраз
		void CheckNewPhrases(object sender, EventArgs e) {
			if (output.PlaybackState == PlaybackState.Playing) return;
			if (phrases.Count == 0) return;
			PlayPhrase(phrases.Dequeue());
		}

		//Проиграть фразу
		void PlayPhrase(WaveStream InputStream) {
			stream = InputStream;
			reader = new NAudio.Wave.WaveFileReader(stream);

			output.Init(reader);
			output.Play();
		}
	}
}
