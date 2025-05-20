namespace MyProjectPLD
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			textBox1 = new TextBox();
			listBox1 = new ListBox();
			listBox2 = new ListBox();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(1, 3);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(508, 503);
			textBox1.TabIndex = 0;
			textBox1.TextChanged += textBox1_TextChanged;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.Location = new Point(515, 12);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(518, 244);
			listBox1.TabIndex = 1;
			// 
			// listBox2
			// 
			listBox2.FormattingEnabled = true;
			listBox2.Location = new Point(515, 262);
			listBox2.Name = "listBox2";
			listBox2.Size = new Size(518, 244);
			listBox2.TabIndex = 2;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1045, 518);
			Controls.Add(listBox2);
			Controls.Add(listBox1);
			Controls.Add(textBox1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private ListBox listBox1;
		private ListBox listBox2;
	}
}
