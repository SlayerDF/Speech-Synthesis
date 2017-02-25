using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;

namespace SpeechSynthesis
{
	internal class Player
	{
		private const int Interval = 500;

		#region Private fields
		
		private readonly Timer _newPhrasesTimer = new Timer();
		private readonly Queue<MemoryStream> _phrases = new Queue<MemoryStream>();

		private bool _playing = false;
		private int _device;

		#endregion

		#region  Public properties

		public int Device {
			get { return _device; }

			set {
				if (value < 0 || value > WaveOut.DeviceCount) throw new Exception("номер устройства воспроизведения должен быть в промежутке от 0 до " + WaveOut.DeviceCount);
				_device = value;
			}
		}

		#endregion

		#region  Constructor

		public Player(int deviceNumber = 0) {
			Device = deviceNumber;

			//Timer Init
			_newPhrasesTimer.Interval = Interval;
			_newPhrasesTimer.Tick += new EventHandler(CheckNewPhrases);
			_newPhrasesTimer.Start();
		}

		#endregion

		#region Public methods

		//Получить список устройств воспроизведения
		public static List<string> DeviceList() {
			List<string> devices = new List<string>();
			for (int i = 0, d = WaveOut.DeviceCount; i < d; i++)
				devices.Add(WaveOut.GetCapabilities(i).ProductName);
			return devices;
		}

		//Добавление фразы в очередь
		public void AddPhrase(Synthesis synth, string text) {
			synth.GetVoiceStream(text, GetPhrase);
		}

		#endregion

		#region  Private methods

		//Фраза добавлена
		private void GetPhrase(MemoryStream stream) {
			_phrases.Enqueue(stream);
		}

		//Проверка новых фраз
		private void CheckNewPhrases(object sender, EventArgs e) {
			if (_playing) return;
			if (_phrases.Count == 0) return;
			_playing = true;
			PlayPhrase(_phrases.Dequeue());
		}

		//Проиграть фразу
		private void PlayPhrase(MemoryStream stream) {
			var waveStream = PrepareStream(stream);
			var reader = new NAudio.Wave.WaveFileReader(waveStream);

			var output = new WaveOut() {DeviceNumber = Device};
			output.PlaybackStopped += (sender, args) => {
				stream.Dispose();
				waveStream.Dispose();
				reader.Dispose();
				output.Dispose();
				_playing = false;
			};

			output.Init(reader);
			output.Play();
		}

		//Преобразование потока в WAV формат
		private static WaveStream PrepareStream(MemoryStream inputStream) {
			inputStream.Position = 0;
			var outputStream = new RawSourceWaveStream(inputStream, new WaveFormat(8000, 1));
			outputStream.Position = 0;

			return outputStream;
		}

		#endregion

	}
}
