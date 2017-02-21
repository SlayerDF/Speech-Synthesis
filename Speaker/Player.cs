using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;

namespace Speaker
{
	public class Player
	{
		#region Private fields
		
		private readonly Timer _newPhrasesTimer = new Timer();
		private readonly Queue<WaveStream> _phrases = new Queue<WaveStream>();

		private readonly WaveOut _output;
		private WaveFileReader _reader;
		private Stream _stream;

		#endregion

		#region  Public properties

		public int Device {
			get { return _output.DeviceNumber; }

			set {
				if (value < 0 || value > WaveOut.DeviceCount) throw new Exception("номер устройства воспроизведения должен быть в промежутке от 0 до " + WaveOut.DeviceCount);
				_output.DeviceNumber = value;
			}
		}

		#endregion

		#region  Constructor

		public Player(int interval, int deviceNumber = 0) {
			if (interval < 100 || interval > 5000) throw new Exception("интервал проверки фраз должен быть в промежутке от 100 до 5000");
			if (deviceNumber < 0 || deviceNumber > WaveOut.DeviceCount) throw new Exception("номер устройства воспроизведения должен быть в промежутке от 0 до " + WaveOut.DeviceCount);

			//Output Init
			_output = new WaveOut {DeviceNumber = deviceNumber};
			_output.PlaybackStopped += output_PlaybackStopped;

			//Timer Init
			_newPhrasesTimer.Interval = 500;
			_newPhrasesTimer.Tick += new EventHandler(CheckNewPhrases);
			_newPhrasesTimer.Start();
		}

		#endregion

		#region Public methods

		//Добавление фразы в очередь
		public void AddPhrase(Synthesis synth, string text) {
			_phrases.Enqueue(synth.GetVoiceStream(text));
		}

		#endregion

		#region  Private methods

		//Получить список устройств воспроизведения
		public static List<string> DeviceList() {
			List<string> devices = new List<string>();
			for (int i = 0, d = WaveOut.DeviceCount; i < d; i++)
				devices.Add(WaveOut.GetCapabilities(i).ProductName);
			return devices;
		}


		//Проигрывание остановилось
		private void output_PlaybackStopped(object sender, StoppedEventArgs e) {
			_stream.Dispose();
			_reader.Dispose();
		}

		//Проверка новых фраз
		private void CheckNewPhrases(object sender, EventArgs e) {
			if (_output.PlaybackState == PlaybackState.Playing) return;
			if (_phrases.Count == 0) return;
			PlayPhrase(_phrases.Dequeue());
		}

		//Проиграть фразу
		private void PlayPhrase(WaveStream inputStream) {
			_stream = inputStream;
			_reader = new NAudio.Wave.WaveFileReader(_stream);

			_output.Init(_reader);
			_output.Play();
		}

		#endregion

	}
}
