namespace NppSharp
{
	partial class ColorPicker
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.cmColor = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ciPick = new System.Windows.Forms.ToolStripMenuItem();
			this.ciNone = new System.Windows.Forms.ToolStripMenuItem();
			this.cmColor.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmColor
			// 
			this.cmColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ciPick,
            this.ciNone});
			this.cmColor.Name = "cmColor";
			this.cmColor.Size = new System.Drawing.Size(111, 48);
			// 
			// ciPick
			// 
			this.ciPick.Name = "ciPick";
			this.ciPick.Size = new System.Drawing.Size(110, 22);
			this.ciPick.Text = "&Pick";
			this.ciPick.Click += new System.EventHandler(this.ciPick_Click);
			// 
			// ciNone
			// 
			this.ciNone.Name = "ciNone";
			this.ciNone.Size = new System.Drawing.Size(110, 22);
			this.ciNone.Text = "&None";
			this.ciNone.Click += new System.EventHandler(this.ciNone_Click);
			// 
			// ColorPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ContextMenuStrip = this.cmColor;
			this.DoubleBuffered = true;
			this.Name = "ColorPicker";
			this.Size = new System.Drawing.Size(38, 18);
			this.Load += new System.EventHandler(this.ColorPicker_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorPicker_Paint);
			this.Click += new System.EventHandler(this.ColorPicker_Click);
			this.cmColor.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmColor;
		private System.Windows.Forms.ToolStripMenuItem ciPick;
		private System.Windows.Forms.ToolStripMenuItem ciNone;
	}
}
