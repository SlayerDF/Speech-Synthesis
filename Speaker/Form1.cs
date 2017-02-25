using System;
using System.Windows.Forms;
using SpeechSynthesis;

namespace Speaker
{
	public partial class Form1 : Form
	{
		private SpeechManager _synth;

		public Form1() {
			InitializeComponent();

			//Form Init
			checkBox_options.Checked = false;

			_synth = new SpeechManager();

			//Fill Combo Box
			foreach (var voice in SpeechManager.GetInstalledVoices())
				comboBox_voices.Items.Add(voice.VoiceInfo.Name);
			comboBox_voices.SelectedIndex = 1;

			foreach (var device in SpeechManager.DeviceList())
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
			_synth.Device = comboBox_outputs.SelectedIndex;
		}

		//Нажата кнопка "Сказать"
		private void button_say_Click(object sender, EventArgs e) {
			_synth.Synthesize(textBox_text.Text, checkBox_repeat.Checked);

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
