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
			this.comboBox_voices = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel_bottom = new System.Windows.Forms.TableLayoutPanel();
			this.label_input = new System.Windows.Forms.Label();
			this.button_say = new System.Windows.Forms.Button();
			this.textBox_text = new System.Windows.Forms.TextBox();
			this.checkBox_options = new System.Windows.Forms.CheckBox();
			this.label_output = new System.Windows.Forms.Label();
			this.tableLayoutPanel_top = new System.Windows.Forms.TableLayoutPanel();
			this.comboBox_outputs = new System.Windows.Forms.ComboBox();
			this.checkBox_repeat = new System.Windows.Forms.CheckBox();
			this.label_voice = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel_bottom.SuspendLayout();
			this.tableLayoutPanel_top.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox_voices
			// 
			this.tableLayoutPanel_top.SetColumnSpan(this.comboBox_voices, 2);
			this.comboBox_voices.FormattingEnabled = true;
			this.comboBox_voices.Location = new System.Drawing.Point(3, 190);
			this.comboBox_voices.Name = "comboBox_voices";
			this.comboBox_voices.Size = new System.Drawing.Size(205, 21);
			this.comboBox_voices.TabIndex = 3;
			this.comboBox_voices.SelectedValueChanged += new System.EventHandler(this.comboBox_voices_SelectedValueChanged);
			// 
			// tableLayoutPanel_bottom
			// 
			this.tableLayoutPanel_bottom.ColumnCount = 2;
			this.tableLayoutPanel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel_bottom.Controls.Add(this.label_input, 0, 0);
			this.tableLayoutPanel_bottom.Controls.Add(this.button_say, 0, 2);
			this.tableLayoutPanel_bottom.Controls.Add(this.textBox_text, 0, 1);
			this.tableLayoutPanel_bottom.Controls.Add(this.checkBox_options, 1, 0);
			this.tableLayoutPanel_bottom.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel_bottom.Name = "tableLayoutPanel_bottom";
			this.tableLayoutPanel_bottom.RowCount = 3;
			this.tableLayoutPanel_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66667F));
			this.tableLayoutPanel_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel_bottom.Size = new System.Drawing.Size(241, 236);
			this.tableLayoutPanel_bottom.TabIndex = 4;
			// 
			// label_input
			// 
			this.label_input.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label_input.AutoSize = true;
			this.label_input.Location = new System.Drawing.Point(3, 13);
			this.label_input.Name = "label_input";
			this.label_input.Size = new System.Drawing.Size(102, 13);
			this.label_input.TabIndex = 5;
			this.label_input.Text = "Текст для синтеза";
			this.label_input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button_say
			// 
			this.button_say.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button_say.Location = new System.Drawing.Point(22, 202);
			this.button_say.Name = "button_say";
			this.button_say.Size = new System.Drawing.Size(75, 27);
			this.button_say.TabIndex = 0;
			this.button_say.Text = "Сказать";
			this.button_say.UseVisualStyleBackColor = true;
			this.button_say.Click += new System.EventHandler(this.button_say_Click);
			// 
			// textBox_text
			// 
			this.tableLayoutPanel_bottom.SetColumnSpan(this.textBox_text, 2);
			this.textBox_text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox_text.Location = new System.Drawing.Point(3, 42);
			this.textBox_text.Multiline = true;
			this.textBox_text.Name = "textBox_text";
			this.textBox_text.Size = new System.Drawing.Size(235, 151);
			this.textBox_text.TabIndex = 2;
			this.textBox_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_text_KeyPress);
			// 
			// checkBox_options
			// 
			this.checkBox_options.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.checkBox_options.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox_options.AutoSize = true;
			this.checkBox_options.Location = new System.Drawing.Point(144, 8);
			this.checkBox_options.Name = "checkBox_options";
			this.checkBox_options.Size = new System.Drawing.Size(72, 23);
			this.checkBox_options.TabIndex = 6;
			this.checkBox_options.Text = "Настройки";
			this.checkBox_options.UseVisualStyleBackColor = true;
			this.checkBox_options.CheckedChanged += new System.EventHandler(this.checkBox_options_CheckedChanged);
			// 
			// label_output
			// 
			this.label_output.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label_output.AutoSize = true;
			this.label_output.Location = new System.Drawing.Point(3, 5);
			this.label_output.Name = "label_output";
			this.label_output.Size = new System.Drawing.Size(72, 13);
			this.label_output.TabIndex = 4;
			this.label_output.Text = "Вывод звука";
			this.label_output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel_top
			// 
			this.tableLayoutPanel_top.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanel_top.ColumnCount = 2;
			this.tableLayoutPanel_top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel_top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel_top.Controls.Add(this.comboBox_outputs, 0, 1);
			this.tableLayoutPanel_top.Controls.Add(this.label_output, 0, 0);
			this.tableLayoutPanel_top.Controls.Add(this.checkBox_repeat, 0, 2);
			this.tableLayoutPanel_top.Controls.Add(this.comboBox_voices, 0, 5);
			this.tableLayoutPanel_top.Controls.Add(this.label_voice, 0, 4);
			this.tableLayoutPanel_top.Location = new System.Drawing.Point(250, 3);
			this.tableLayoutPanel_top.Name = "tableLayoutPanel_top";
			this.tableLayoutPanel_top.RowCount = 6;
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_top.Size = new System.Drawing.Size(211, 236);
			this.tableLayoutPanel_top.TabIndex = 5;
			this.tableLayoutPanel_top.Visible = false;
			// 
			// comboBox_outputs
			// 
			this.tableLayoutPanel_top.SetColumnSpan(this.comboBox_outputs, 2);
			this.comboBox_outputs.FormattingEnabled = true;
			this.comboBox_outputs.Location = new System.Drawing.Point(3, 26);
			this.comboBox_outputs.Name = "comboBox_outputs";
			this.comboBox_outputs.Size = new System.Drawing.Size(205, 21);
			this.comboBox_outputs.TabIndex = 6;
			this.comboBox_outputs.SelectedIndexChanged += new System.EventHandler(this.comboBox_outputs_SelectedIndexChanged);
			// 
			// checkBox_repeat
			// 
			this.tableLayoutPanel_top.SetColumnSpan(this.checkBox_repeat, 2);
			this.checkBox_repeat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox_repeat.Location = new System.Drawing.Point(3, 85);
			this.checkBox_repeat.Name = "checkBox_repeat";
			this.checkBox_repeat.Size = new System.Drawing.Size(205, 41);
			this.checkBox_repeat.TabIndex = 7;
			this.checkBox_repeat.Text = "Дублирование звука в основной источник вывода";
			this.checkBox_repeat.UseVisualStyleBackColor = true;
			// 
			// label_voice
			// 
			this.label_voice.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label_voice.AutoSize = true;
			this.label_voice.Location = new System.Drawing.Point(3, 169);
			this.label_voice.Name = "label_voice";
			this.label_voice.Size = new System.Drawing.Size(37, 13);
			this.label_voice.TabIndex = 5;
			this.label_voice.Text = "Голос";
			this.label_voice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel_bottom);
			this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel_top);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(464, 242);
			this.flowLayoutPanel1.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(464, 242);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "Form1";
			this.Text = "Синтезатор голоса";
			this.tableLayoutPanel_bottom.ResumeLayout(false);
			this.tableLayoutPanel_bottom.PerformLayout();
			this.tableLayoutPanel_top.ResumeLayout(false);
			this.tableLayoutPanel_top.PerformLayout();
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
	}
}

