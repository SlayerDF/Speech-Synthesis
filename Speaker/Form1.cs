using System;
using System.Windows.Forms;
using System.Drawing;

namespace Speaker
{
	public partial class Form1 : Form
	{
		Player player;
		Player player2;
		Synthesis synth;

		public Form1() {
			InitializeComponent();

			var devices = Player.DeviceList();

			//Form Init
			checkBox_options.Checked = false;

			//Player Init
			player = new Player(500, 1);
			player2 = new Player(500, 0);

			//Synthesis Init
			synth = new Synthesis(100, 0);

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
			synth.SetVoice(comboBox_voices.SelectedItem.ToString());
		}

		//Выбор источника звука
		private void comboBox_outputs_SelectedIndexChanged(object sender, EventArgs e) {
			if (comboBox_outputs.SelectedIndex == -1) return;
			player.SetDevice(comboBox_outputs.SelectedIndex);
		}

		//Нажата кнопка "Сказать"
		void button_say_Click(object sender, EventArgs e) {
			player.AddPhrase(synth, textBox_text.Text);
			if (checkBox_repeat.Checked) player2.AddPhrase(synth, textBox_text.Text);

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
			if (cb.Checked) tableLayoutPanel_top.Visible = true;
			else tableLayoutPanel_top.Visible = false;
		}
	}
}
