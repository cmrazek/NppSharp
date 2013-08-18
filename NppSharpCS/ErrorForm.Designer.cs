namespace NppSharp
{
	partial class ErrorForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabMessage = new System.Windows.Forms.TabPage();
			this.tabDetails = new System.Windows.Forms.TabPage();
			this.txtDetails = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl.SuspendLayout();
			this.tabMessage.SuspendLayout();
			this.tabDetails.SuspendLayout();
			this.panel1.SuspendLayout();
			this.cmTextBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.SystemColors.Window;
			this.txtMessage.ContextMenuStrip = this.cmTextBox;
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.Location = new System.Drawing.Point(3, 3);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(378, 102);
			this.txtMessage.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOk.Location = new System.Drawing.Point(314, 6);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabMessage);
			this.tabControl.Controls.Add(this.tabDetails);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(392, 134);
			this.tabControl.TabIndex = 1;
			// 
			// tabMessage
			// 
			this.tabMessage.Controls.Add(this.txtMessage);
			this.tabMessage.Location = new System.Drawing.Point(4, 22);
			this.tabMessage.Name = "tabMessage";
			this.tabMessage.Padding = new System.Windows.Forms.Padding(3);
			this.tabMessage.Size = new System.Drawing.Size(384, 108);
			this.tabMessage.TabIndex = 0;
			this.tabMessage.Text = "Message";
			this.tabMessage.UseVisualStyleBackColor = true;
			// 
			// tabDetails
			// 
			this.tabDetails.Controls.Add(this.txtDetails);
			this.tabDetails.Location = new System.Drawing.Point(4, 22);
			this.tabDetails.Name = "tabDetails";
			this.tabDetails.Padding = new System.Windows.Forms.Padding(3);
			this.tabDetails.Size = new System.Drawing.Size(384, 108);
			this.tabDetails.TabIndex = 1;
			this.tabDetails.Text = "Details";
			this.tabDetails.UseVisualStyleBackColor = true;
			// 
			// txtDetails
			// 
			this.txtDetails.BackColor = System.Drawing.SystemColors.Window;
			this.txtDetails.ContextMenuStrip = this.cmTextBox;
			this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDetails.Location = new System.Drawing.Point(3, 3);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.ReadOnly = true;
			this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDetails.Size = new System.Drawing.Size(378, 102);
			this.txtDetails.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 134);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 32);
			this.panel1.TabIndex = 0;
			// 
			// cmTextBox
			// 
			this.cmTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem});
			this.cmTextBox.Name = "cmTextBox";
			this.cmTextBox.Size = new System.Drawing.Size(168, 48);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// ErrorForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(392, 166);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 200);
			this.Name = "ErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Error";
			this.Load += new System.EventHandler(this.ErrorForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabMessage.ResumeLayout(false);
			this.tabMessage.PerformLayout();
			this.tabDetails.ResumeLayout(false);
			this.tabDetails.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.cmTextBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabMessage;
		private System.Windows.Forms.TabPage tabDetails;
		private System.Windows.Forms.TextBox txtDetails;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ContextMenuStrip cmTextBox;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
	}
}