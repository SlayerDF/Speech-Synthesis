namespace Speaker
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.comboBox_voices = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel_bottom = new System.Windows.Forms.TableLayoutPanel();
			this.label_input = new System.Windows.Forms.Label();
			this.button_say = new System.Windows.Forms.Button();
			this.textBox_text = new System.Windows.Forms.TextBox();
			this.checkBox_options = new System.Windows.Forms.CheckBox();
			this.label_output = new System.Windows.Forms.Label();
			this.tableLayoutPanel_top = new System.Windows.Forms.TableLayoutPanel();
			this.trackBar_rate = new System.Windows.Forms.TrackBar();
			this.label_volume = new System.Windows.Forms.Label();
			this.comboBox_outputs = new System.Windows.Forms.ComboBox();
			this.label_voice = new System.Windows.Forms.Label();
			this.checkBox_repeat = new System.Windows.Forms.CheckBox();
			this.label_rate = new System.Windows.Forms.Label();
			this.trackBar_volume = new System.Windows.Forms.TrackBar();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel_bottom.SuspendLayout();
			this.tableLayoutPanel_top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox_voices
			// 
			resources.ApplyResources(this.comboBox_voices, "comboBox_voices");
			this.tableLayoutPanel_top.SetColumnSpan(this.comboBox_voices, 2);
			this.comboBox_voices.FormattingEnabled = true;
			this.comboBox_voices.Name = "comboBox_voices";
			this.comboBox_voices.TabStop = false;
			this.comboBox_voices.SelectedValueChanged += new System.EventHandler(this.comboBox_voices_SelectedValueChanged);
			// 
			// tableLayoutPanel_bottom
			// 
			resources.ApplyResources(this.tableLayoutPanel_bottom, "tableLayoutPanel_bottom");
			this.tableLayoutPanel_bottom.Controls.Add(this.label_input, 0, 0);
			this.tableLayoutPanel_bottom.Controls.Add(this.button_say, 0, 2);
			this.tableLayoutPanel_bottom.Controls.Add(this.textBox_text, 0, 1);
			this.tableLayoutPanel_bottom.Controls.Add(this.checkBox_options, 1, 0);
			this.tableLayoutPanel_bottom.Name = "tableLayoutPanel_bottom";
			// 
			// label_input
			// 
			resources.ApplyResources(this.label_input, "label_input");
			this.label_input.Name = "label_input";
			// 
			// button_say
			// 
			resources.ApplyResources(this.button_say, "button_say");
			this.button_say.Name = "button_say";
			this.button_say.UseVisualStyleBackColor = true;
			this.button_say.Click += new System.EventHandler(this.button_say_Click);
			// 
			// textBox_text
			// 
			resources.ApplyResources(this.textBox_text, "textBox_text");
			this.tableLayoutPanel_bottom.SetColumnSpan(this.textBox_text, 2);
			this.textBox_text.Name = "textBox_text";
			this.textBox_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_text_KeyPress);
			// 
			// checkBox_options
			// 
			resources.ApplyResources(this.checkBox_options, "checkBox_options");
			this.checkBox_options.Name = "checkBox_options";
			this.checkBox_options.TabStop = false;
			this.checkBox_options.UseVisualStyleBackColor = true;
			this.checkBox_options.CheckedChanged += new System.EventHandler(this.checkBox_options_CheckedChanged);
			// 
			// label_output
			// 
			resources.ApplyResources(this.label_output, "label_output");
			this.label_output.Name = "label_output";
			// 
			// tableLayoutPanel_top
			// 
			resources.ApplyResources(this.tableLayoutPanel_top, "tableLayoutPanel_top");
			this.tableLayoutPanel_top.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanel_top.Controls.Add(this.trackBar_rate, 0, 5);
			this.tableLayoutPanel_top.Controls.Add(this.label_volume, 0, 2);
			this.tableLayoutPanel_top.Controls.Add(this.comboBox_outputs, 0, 1);
			this.tableLayoutPanel_top.Controls.Add(this.label_output, 0, 0);
			this.tableLayoutPanel_top.Controls.Add(this.comboBox_voices, 0, 8);
			this.tableLayoutPanel_top.Controls.Add(this.label_voice, 0, 7);
			this.tableLayoutPanel_top.Controls.Add(this.checkBox_repeat, 0, 6);
			this.tableLayoutPanel_top.Controls.Add(this.label_rate, 0, 4);
			this.tableLayoutPanel_top.Controls.Add(this.trackBar_volume, 0, 3);
			this.tableLayoutPanel_top.Name = "tableLayoutPanel_top";
			// 
			// trackBar_rate
			// 
			resources.ApplyResources(this.trackBar_rate, "trackBar_rate");
			this.tableLayoutPanel_top.SetColumnSpan(this.trackBar_rate, 2);
			this.trackBar_rate.Minimum = -10;
			this.trackBar_rate.Name = "trackBar_rate";
			this.trackBar_rate.TabStop = false;
			this.trackBar_rate.TickFrequency = 5;
			this.trackBar_rate.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar_rate.ValueChanged += new System.EventHandler(this.trackBar_rate_ValueChanged);
			// 
			// label_volume
			// 
			resources.ApplyResources(this.label_volume, "label_volume");
			this.label_volume.Name = "label_volume";
			// 
			// comboBox_outputs
			// 
			resources.ApplyResources(this.comboBox_outputs, "comboBox_outputs");
			this.tableLayoutPanel_top.SetColumnSpan(this.comboBox_outputs, 2);
			this.comboBox_outputs.FormattingEnabled = true;
			this.comboBox_outputs.Name = "comboBox_outputs";
			this.comboBox_outputs.TabStop = false;
			this.comboBox_outputs.SelectedIndexChanged += new System.EventHandler(this.comboBox_outputs_SelectedIndexChanged);
			// 
			// label_voice
			// 
			resources.ApplyResources(this.label_voice, "label_voice");
			this.label_voice.Name = "label_voice";
			// 
			// checkBox_repeat
			// 
			resources.ApplyResources(this.checkBox_repeat, "checkBox_repeat");
			this.checkBox_repeat.Checked = true;
			this.checkBox_repeat.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel_top.SetColumnSpan(this.checkBox_repeat, 2);
			this.checkBox_repeat.Name = "checkBox_repeat";
			this.checkBox_repeat.TabStop = false;
			this.checkBox_repeat.UseVisualStyleBackColor = true;
			// 
			// label_rate
			// 
			resources.ApplyResources(this.label_rate, "label_rate");
			this.label_rate.Name = "label_rate";
			// 
			// trackBar_volume
			// 
			resources.ApplyResources(this.trackBar_volume, "trackBar_volume");
			this.tableLayoutPanel_top.SetColumnSpan(this.trackBar_volume, 2);
			this.trackBar_volume.LargeChange = 10;
			this.trackBar_volume.Maximum = 100;
			this.trackBar_volume.Name = "trackBar_volume";
			this.trackBar_volume.SmallChange = 5;
			this.trackBar_volume.TabStop = false;
			this.trackBar_volume.TickFrequency = 10;
			this.trackBar_volume.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar_volume.Value = 100;
			this.trackBar_volume.ValueChanged += new System.EventHandler(this.trackBar_volume_ValueChanged);
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel_bottom);
			this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel_top);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "Form1";
			this.tableLayoutPanel_bottom.ResumeLayout(false);
			this.tableLayoutPanel_bottom.PerformLayout();
			this.tableLayoutPanel_top.ResumeLayout(false);
			this.tableLayoutPanel_top.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox comboBox_voices;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_top;
		private System.Windows.Forms.ComboBox comboBox_outputs;
		private System.Windows.Forms.Label label_output;
		private System.Windows.Forms.CheckBox checkBox_repeat;
		private System.Windows.Forms.Label label_voice;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_bottom;
		private System.Windows.Forms.Label label_input;
		private System.Windows.Forms.Button button_say;
		private System.Windows.Forms.TextBox textBox_text;
		private System.Windows.Forms.CheckBox checkBox_options;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TrackBar trackBar_rate;
		private System.Windows.Forms.Label label_volume;
		private System.Windows.Forms.Label label_rate;
		private System.Windows.Forms.TrackBar trackBar_volume;
	}
}

