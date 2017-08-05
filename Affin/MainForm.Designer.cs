namespace Affin
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pbCanvas = new System.Windows.Forms.PictureBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
			this.SuspendLayout();
			// 
			// pbCanvas
			// 
			this.pbCanvas.BackColor = System.Drawing.Color.White;
			this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbCanvas.Location = new System.Drawing.Point(12, 12);
			this.pbCanvas.Margin = new System.Windows.Forms.Padding(6);
			this.pbCanvas.Name = "pbCanvas";
			this.pbCanvas.Size = new System.Drawing.Size(480, 480);
			this.pbCanvas.TabIndex = 0;
			this.pbCanvas.TabStop = false;
			this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbCanvas_MouseDown);
			this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
			this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(593, 27);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(593, 56);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 1;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(676, 504);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pbCanvas);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbCanvas;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
	}
}

