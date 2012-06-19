namespace NppSharp
{
	partial class SettingsForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabScriptDirs = new System.Windows.Forms.TabPage();
			this.lstScriptDirs = new System.Windows.Forms.ListBox();
			this.btnRemoveScriptDir = new System.Windows.Forms.Button();
			this.btnAddScriptDir = new System.Windows.Forms.Button();
			this.tabOutputStyles = new System.Windows.Forms.TabPage();
			this.clrBack = new NppSharp.ColorPicker();
			this.clrFore = new NppSharp.ColorPicker();
			this.lblBack = new System.Windows.Forms.Label();
			this.lblFore = new System.Windows.Forms.Label();
			this.chkUnderline = new System.Windows.Forms.CheckBox();
			this.chkItalic = new System.Windows.Forms.CheckBox();
			this.chkBold = new System.Windows.Forms.CheckBox();
			this.txtOutputSize = new System.Windows.Forms.TextBox();
			this.lblSize = new System.Windows.Forms.Label();
			this.cmbOutputFonts = new System.Windows.Forms.ComboBox();
			this.lblFont = new System.Windows.Forms.Label();
			this.lstOutputStyles = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabScriptDirs.SuspendLayout();
			this.tabOutputStyles.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 282);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(416, 44);
			this.panel1.TabIndex = 1;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(167, 9);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "&Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(329, 9);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(248, 9);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabScriptDirs);
			this.tabControl.Controls.Add(this.tabOutputStyles);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(416, 282);
			this.tabControl.TabIndex = 0;
			// 
			// tabScriptDirs
			// 
			this.tabScriptDirs.Controls.Add(this.lstScriptDirs);
			this.tabScriptDirs.Controls.Add(this.btnRemoveScriptDir);
			this.tabScriptDirs.Controls.Add(this.btnAddScriptDir);
			this.tabScriptDirs.Location = new System.Drawing.Point(4, 22);
			this.tabScriptDirs.Name = "tabScriptDirs";
			this.tabScriptDirs.Padding = new System.Windows.Forms.Padding(3);
			this.tabScriptDirs.Size = new System.Drawing.Size(408, 256);
			this.tabScriptDirs.TabIndex = 0;
			this.tabScriptDirs.Text = "Locations";
			this.tabScriptDirs.UseVisualStyleBackColor = true;
			// 
			// lstScriptDirs
			// 
			this.lstScriptDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstScriptDirs.FormattingEnabled = true;
			this.lstScriptDirs.Location = new System.Drawing.Point(8, 6);
			this.lstScriptDirs.Name = "lstScriptDirs";
			this.lstScriptDirs.Size = new System.Drawing.Size(311, 225);
			this.lstScriptDirs.TabIndex = 0;
			// 
			// btnRemoveScriptDir
			// 
			this.btnRemoveScriptDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveScriptDir.Location = new System.Drawing.Point(325, 35);
			this.btnRemoveScriptDir.Name = "btnRemoveScriptDir";
			this.btnRemoveScriptDir.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveScriptDir.TabIndex = 2;
			this.btnRemoveScriptDir.Text = "&Remove";
			this.btnRemoveScriptDir.UseVisualStyleBackColor = true;
			this.btnRemoveScriptDir.Click += new System.EventHandler(this.btnRemoveScriptDir_Click);
			// 
			// btnAddScriptDir
			// 
			this.btnAddScriptDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddScriptDir.Location = new System.Drawing.Point(325, 6);
			this.btnAddScriptDir.Name = "btnAddScriptDir";
			this.btnAddScriptDir.Size = new System.Drawing.Size(75, 23);
			this.btnAddScriptDir.TabIndex = 1;
			this.btnAddScriptDir.Text = "&Add";
			this.btnAddScriptDir.UseVisualStyleBackColor = true;
			this.btnAddScriptDir.Click += new System.EventHandler(this.btnAddScriptDir_Click);
			// 
			// tabOutputStyles
			// 
			this.tabOutputStyles.Controls.Add(this.clrBack);
			this.tabOutputStyles.Controls.Add(this.clrFore);
			this.tabOutputStyles.Controls.Add(this.lblBack);
			this.tabOutputStyles.Controls.Add(this.lblFore);
			this.tabOutputStyles.Controls.Add(this.chkUnderline);
			this.tabOutputStyles.Controls.Add(this.chkItalic);
			this.tabOutputStyles.Controls.Add(this.chkBold);
			this.tabOutputStyles.Controls.Add(this.txtOutputSize);
			this.tabOutputStyles.Controls.Add(this.lblSize);
			this.tabOutputStyles.Controls.Add(this.cmbOutputFonts);
			this.tabOutputStyles.Controls.Add(this.lblFont);
			this.tabOutputStyles.Controls.Add(this.lstOutputStyles);
			this.tabOutputStyles.Location = new System.Drawing.Point(4, 22);
			this.tabOutputStyles.Name = "tabOutputStyles";
			this.tabOutputStyles.Padding = new System.Windows.Forms.Padding(3);
			this.tabOutputStyles.Size = new System.Drawing.Size(408, 256);
			this.tabOutputStyles.TabIndex = 1;
			this.tabOutputStyles.Text = "Output Window";
			this.tabOutputStyles.UseVisualStyleBackColor = true;
			// 
			// clrBack
			// 
			this.clrBack.BackColor = System.Drawing.Color.Black;
			this.clrBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrBack.Color = null;
			this.clrBack.Location = new System.Drawing.Point(238, 100);
			this.clrBack.Name = "clrBack";
			this.clrBack.Size = new System.Drawing.Size(40, 20);
			this.clrBack.TabIndex = 12;
			this.clrBack.ColorChanged += new NppSharp.EventHandler(this.clrBack_ColorChanged);
			// 
			// clrFore
			// 
			this.clrFore.BackColor = System.Drawing.Color.Black;
			this.clrFore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clrFore.Color = null;
			this.clrFore.Location = new System.Drawing.Point(238, 71);
			this.clrFore.Name = "clrFore";
			this.clrFore.Size = new System.Drawing.Size(40, 20);
			this.clrFore.TabIndex = 11;
			this.clrFore.ColorChanged += new NppSharp.EventHandler(this.clrFore_ColorChanged);
			// 
			// lblBack
			// 
			this.lblBack.AutoSize = true;
			this.lblBack.Location = new System.Drawing.Point(164, 104);
			this.lblBack.Name = "lblBack";
			this.lblBack.Size = new System.Drawing.Size(68, 13);
			this.lblBack.TabIndex = 10;
			this.lblBack.Text = "Background:";
			// 
			// lblFore
			// 
			this.lblFore.AutoSize = true;
			this.lblFore.Location = new System.Drawing.Point(164, 75);
			this.lblFore.Name = "lblFore";
			this.lblFore.Size = new System.Drawing.Size(64, 13);
			this.lblFore.TabIndex = 9;
			this.lblFore.Text = "Foreground:";
			// 
			// chkUnderline
			// 
			this.chkUnderline.AutoSize = true;
			this.chkUnderline.Location = new System.Drawing.Point(325, 90);
			this.chkUnderline.Name = "chkUnderline";
			this.chkUnderline.Size = new System.Drawing.Size(71, 17);
			this.chkUnderline.TabIndex = 8;
			this.chkUnderline.Text = "Underline";
			this.chkUnderline.ThreeState = true;
			this.chkUnderline.UseVisualStyleBackColor = true;
			this.chkUnderline.CheckedChanged += new System.EventHandler(this.chkUnderline_CheckedChanged);
			// 
			// chkItalic
			// 
			this.chkItalic.AutoSize = true;
			this.chkItalic.Location = new System.Drawing.Point(325, 67);
			this.chkItalic.Name = "chkItalic";
			this.chkItalic.Size = new System.Drawing.Size(48, 17);
			this.chkItalic.TabIndex = 7;
			this.chkItalic.Text = "Italic";
			this.chkItalic.ThreeState = true;
			this.chkItalic.UseVisualStyleBackColor = true;
			this.chkItalic.CheckedChanged += new System.EventHandler(this.chkItalic_CheckedChanged);
			// 
			// chkBold
			// 
			this.chkBold.AutoSize = true;
			this.chkBold.Location = new System.Drawing.Point(325, 44);
			this.chkBold.Name = "chkBold";
			this.chkBold.Size = new System.Drawing.Size(47, 17);
			this.chkBold.TabIndex = 6;
			this.chkBold.Text = "Bold";
			this.chkBold.ThreeState = true;
			this.chkBold.UseVisualStyleBackColor = true;
			this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
			// 
			// txtOutputSize
			// 
			this.txtOutputSize.Location = new System.Drawing.Point(238, 44);
			this.txtOutputSize.Name = "txtOutputSize";
			this.txtOutputSize.Size = new System.Drawing.Size(40, 20);
			this.txtOutputSize.TabIndex = 5;
			this.txtOutputSize.TextChanged += new System.EventHandler(this.txtOutputSize_TextChanged);
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(164, 47);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(30, 13);
			this.lblSize.TabIndex = 4;
			this.lblSize.Text = "Size:";
			// 
			// cmbOutputFonts
			// 
			this.cmbOutputFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbOutputFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbOutputFonts.FormattingEnabled = true;
			this.cmbOutputFonts.Location = new System.Drawing.Point(238, 17);
			this.cmbOutputFonts.Name = "cmbOutputFonts";
			this.cmbOutputFonts.Size = new System.Drawing.Size(162, 21);
			this.cmbOutputFonts.TabIndex = 3;
			this.cmbOutputFonts.SelectedIndexChanged += new System.EventHandler(this.cmbOutputFonts_SelectedIndexChanged);
			// 
			// lblFont
			// 
			this.lblFont.AutoSize = true;
			this.lblFont.Location = new System.Drawing.Point(164, 20);
			this.lblFont.Name = "lblFont";
			this.lblFont.Size = new System.Drawing.Size(31, 13);
			this.lblFont.TabIndex = 2;
			this.lblFont.Text = "Font:";
			// 
			// lstOutputStyles
			// 
			this.lstOutputStyles.FormattingEnabled = true;
			this.lstOutputStyles.Location = new System.Drawing.Point(8, 6);
			this.lstOutputStyles.Name = "lstOutputStyles";
			this.lstOutputStyles.Size = new System.Drawing.Size(150, 238);
			this.lstOutputStyles.TabIndex = 0;
			this.lstOutputStyles.SelectedIndexChanged += new System.EventHandler(this.lstOutputStyles_SelectedIndexChanged);
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(416, 326);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(424, 360);
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NppSharp Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.panel1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabScriptDirs.ResumeLayout(false);
			this.tabOutputStyles.ResumeLayout(false);
			this.tabOutputStyles.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabScriptDirs;
		private System.Windows.Forms.Button btnRemoveScriptDir;
		private System.Windows.Forms.Button btnAddScriptDir;
		private System.Windows.Forms.ListBox lstScriptDirs;
		private System.Windows.Forms.TabPage tabOutputStyles;
		private System.Windows.Forms.ListBox lstOutputStyles;
		private System.Windows.Forms.ComboBox cmbOutputFonts;
		private System.Windows.Forms.Label lblFont;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.TextBox txtOutputSize;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.Label lblBack;
		private System.Windows.Forms.Label lblFore;
		private System.Windows.Forms.CheckBox chkUnderline;
		private System.Windows.Forms.CheckBox chkItalic;
		private System.Windows.Forms.CheckBox chkBold;
		private ColorPicker clrBack;
		private ColorPicker clrFore;
	}
}