using System;
using System.Windows.Forms;

namespace Speaker
{
	public partial class Form1 : Form
	{
		private readonly Player _player;
		private readonly Player _player2;
		readonly Synthesis _synth;

		public Form1() {
			InitializeComponent();

			var devices = Player.DeviceList();
			if (devices == null) throw new ArgumentNullException(nameof(devices));

			//Form Init
			checkBox_options.Checked = false;

			//Player Init
			_player = new Player(500, 1);
			_player2 = new Player(500, 0);

			//Synthesis Init
			_synth = new Synthesis(100, 0);

			//Fill Combo Box
			foreach (var voice in Synthesis.GetInstalledVoices())
				comboBox_voices.Items.Add(voice.VoiceInfo.Name);
			comboBox_voices.SelectedIndex = 1;

			foreach (var device in Player.DeviceList())
				comboBox_outputs.Items.Add(device);
			comboBox_outputs.SelectedIndex = 1;
		}

		//Выбор голоса
		private void comboBox_voices_SelectedValueChanged(object sender, EventArgs e) {
			_synth.Voice = comboBox_voices.SelectedItem.ToString();
		}

		//Выбор источника звука
		private void comboBox_outputs_SelectedIndexChanged(object sender, EventArgs e) {
			if (comboBox_outputs.SelectedIndex == -1) return;
			_player.Device = comboBox_outputs.SelectedIndex;
		}

		//Нажата кнопка "Сказать"
		private void button_say_Click(object sender, EventArgs e) {
			_player.AddPhrase(_synth, textBox_text.Text);

			if (checkBox_repeat.Checked) _player2.AddPhrase(_synth, textBox_text.Text);

			textBox_text.Text = "";
			textBox_text.Select(0, 0);
			textBox_text.Focus();
			textBox_text.ScrollToCaret();
		}

		//Нажатия в текстбоксе
		private void textBox_text_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == 13) {
				button_say_Click(null, null);
			}
		}

		//Открытие опций
		private void checkBox_options_CheckedChanged(object sender, EventArgs e) {
			var cb = (CheckBox)sender;
			tableLayoutPanel_top.Visible = cb.Checked;
		}

		//Изменение громкости
		private void trackBar_volume_ValueChanged(object sender, EventArgs e) {
			_synth.Volume = ((TrackBar) sender).Value;
		}

		//Изменение скорости
		private void trackBar_rate_ValueChanged(object sender, EventArgs e) {
			_synth.Rate = ((TrackBar)sender).Value;
		}
	}
}
