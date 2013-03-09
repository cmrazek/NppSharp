using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace NppSharp
{
	internal partial class SettingsForm : Form
	{
		#region Core
		private bool _changed = false;

		public SettingsForm()
		{
			InitializeComponent();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			try
			{
				LoadScriptDirs();
				LoadOutputWindowStyles();
				Changed = false;
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidateForm(true))
				{
					Apply();
					DialogResult = DialogResult.OK;
					Close();
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidateForm(true))
				{
					Apply();
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void ShowError(Exception exception)
		{
			try
			{
				ErrorForm form = new ErrorForm(exception.Message, exception.ToString());
				form.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ShowError(string message)
		{
			try
			{
				ErrorForm form = new ErrorForm(message);
				form.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ControlChanged(object sender, EventArgs e)
		{
			try
			{
				Changed = true;
				EnableControls();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void EnableControls()
		{
			btnApply.Enabled = Changed;
			btnRemoveScriptDir.Enabled = lstScriptDirs.SelectedItem != null;
		}

		private bool Changed
		{
			get { return _changed; }
			set
			{
				if (_changed != value)
				{
					_changed = value;
					btnApply.Enabled = _changed;
				}
			}
		}
		#endregion

		#region Saving
		private void Apply()
		{
			// Script Dirs
			List<string> scriptDirs = new List<string>();
			foreach (string dir in lstScriptDirs.Items) scriptDirs.Add(dir);
			Settings.SetStringList(Res.Reg_ScriptDirs, scriptDirs);

			ApplyOutputWindowStyles();

			Changed = false;
		}

		private bool ValidateForm(bool showError)
		{
			if (!ValidateOutputWindowStyles(showError)) return false;
			return true;
		}
		#endregion

		#region Script Dirs
		private void LoadScriptDirs()
		{
			lstScriptDirs.Items.Clear();
			foreach (string dir in ScriptManager.ScriptDirs) lstScriptDirs.Items.Add(dir);
		}

		private void btnAddScriptDir_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string path = dlg.SelectedPath;

					foreach (string dir in lstScriptDirs.Items)
					{
						if (dir.Equals(path, StringComparison.OrdinalIgnoreCase)) return;
					}

					lstScriptDirs.Items.Add(path);
					ControlChanged(sender, e);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void btnRemoveScriptDir_Click(object sender, EventArgs e)
		{
			try
			{
				object selItem = lstScriptDirs.SelectedItem;
				if (selItem != null)
				{
					lstScriptDirs.Items.Remove(selItem);
					ControlChanged(sender, e);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		#endregion

		#region Output Window Styles
		private Dictionary<OutputStyle, OutputStyleDef> _styles = new Dictionary<OutputStyle, OutputStyleDef>();
		private OutputStyle _currentStyle = OutputStyle.Normal;
		private OutputStyleDef _currentStyleDef = null;
		private bool _styleChanging = false;

		private void LoadOutputWindowStyles()
		{
			_styleChanging = true;

			InstalledFontCollection ifc = new InstalledFontCollection();
			cmbOutputFonts.Items.Clear();
			cmbOutputFonts.Items.Add(Res.OutputWindowStyleOverride);
			foreach (FontFamily family in ifc.Families)
			{
				cmbOutputFonts.Items.Add(family.Name);
			}

			lstOutputStyles.Items.Clear();
			foreach (OutputStyle style in Enum.GetValues(typeof(OutputStyle)))
			{
				lstOutputStyles.Items.Add(style);
			}
			lstOutputStyles.SelectedIndex = 0;

			_styleChanging = false;
		}

		private void lstOutputStyles_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (lstOutputStyles.SelectedItem == null) return;

				_styleChanging = true;

				_currentStyle = (OutputStyle)Enum.Parse(typeof(OutputStyle), lstOutputStyles.SelectedItem.ToString());
				_currentStyleDef = GetWorkingOutputStyleDef(_currentStyle);

				// Refresh font list
				if (string.IsNullOrEmpty(_currentStyleDef.FontName))
				{
					cmbOutputFonts.SelectedIndex = 0;
				}
				else
				{
					bool found = false;
					foreach (string f in cmbOutputFonts.Items)
					{
						if (f.Equals(_currentStyleDef.FontName, StringComparison.InvariantCultureIgnoreCase))
						{
							cmbOutputFonts.SelectedItem = f;
							found = true;
							break;
						}
					}
					if (!found) cmbOutputFonts.SelectedIndex = 0;
				}

				// Refresh font size
				if (_currentStyleDef.Size.HasValue)
				{
					txtOutputSize.Text = _currentStyleDef.Size.Value.ToString();
				}
				else
				{
					txtOutputSize.Text = string.Empty;
				}

				// Font style
				switch (_currentStyleDef.Bold)
				{
					case true:
						chkBold.CheckState = CheckState.Checked;
						break;
					case false:
						chkBold.CheckState = CheckState.Unchecked;
						break;
					default:
						chkBold.CheckState = CheckState.Indeterminate;
						break;
				}

				switch (_currentStyleDef.Italic)
				{
					case true:
						chkItalic.CheckState = CheckState.Checked;
						break;
					case false:
						chkItalic.CheckState = CheckState.Unchecked;
						break;
					default:
						chkItalic.CheckState = CheckState.Indeterminate;
						break;
				}

				switch (_currentStyleDef.Underline)
				{
					case true:
						chkUnderline.CheckState = CheckState.Checked;
						break;
					case false:
						chkUnderline.CheckState = CheckState.Unchecked;
						break;
					default:
						chkUnderline.CheckState = CheckState.Indeterminate;
						break;
				}

				// Refresh colors
				clrFore.Color = _currentStyleDef.ForeColor;
				clrBack.Color = _currentStyleDef.BackColor;

				_styleChanging = false;
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private OutputStyleDef GetWorkingOutputStyleDef(OutputStyle style)
		{
			OutputStyleDef osd;
			if (_styles.TryGetValue(style, out osd)) return osd;

			osd = OutputStyleDef.GetStyleDef(style).Clone();
			_styles.Add(style, osd);
			return osd;
		}

		private void cmbOutputFonts_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (_currentStyleDef == null || _styleChanging) return;

				if (cmbOutputFonts.SelectedIndex <= 0) _currentStyleDef.FontName = null;
				else _currentStyleDef.FontName = (string)cmbOutputFonts.SelectedItem;

				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void ApplyOutputWindowStyles()
		{
			OutputStyleDef defaultOsd = GetWorkingOutputStyleDef(OutputStyle.Normal);
			defaultOsd.Apply();
			defaultOsd.SaveSettings();

			foreach (OutputStyle style in Enum.GetValues(typeof(OutputStyle)))
			{
				if (style == OutputStyle.Normal) continue;

				OutputStyleDef osd = GetWorkingOutputStyleDef(style);
				osd.Apply();
				osd.SaveSettings();
			}
		}

		private bool ValidateOutputWindowStyles(bool showErrors)
		{
			string str = txtOutputSize.Text;
			if (!string.IsNullOrEmpty(str))
			{
				int size;
				if (!Int32.TryParse(str, out size) || size < 1)
				{
					if (showErrors)
					{
						tabControl.SelectTab(tabOutputStyles);
						ShowError(Res.Settings_OutputSizeInvalid);
					}
					return false;
				}
			}

			return true;
		}

		private void txtOutputSize_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				int size;
				if (Int32.TryParse(txtOutputSize.Text, out size) && size > 0)
				{
					_currentStyleDef.Size = size;
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void clrFore_ColorChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				_currentStyleDef.ForeColor = clrFore.Color;
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void clrBack_ColorChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				_currentStyleDef.BackColor = clrBack.Color;
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void chkBold_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				switch (chkBold.CheckState)
				{
					case CheckState.Checked:
						_currentStyleDef.Bold = true;
						break;
					case CheckState.Unchecked:
						_currentStyleDef.Bold = false;
						break;
					default:
						_currentStyleDef.Bold = null;
						break;
				}
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void chkItalic_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				switch (chkItalic.CheckState)
				{
					case CheckState.Checked:
						_currentStyleDef.Italic = true;
						break;
					case CheckState.Unchecked:
						_currentStyleDef.Italic = false;
						break;
					default:
						_currentStyleDef.Italic = null;
						break;
				}
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void chkUnderline_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (_styleChanging) return;

				switch (chkUnderline.CheckState)
				{
					case CheckState.Checked:
						_currentStyleDef.Underline = true;
						break;
					case CheckState.Unchecked:
						_currentStyleDef.Underline = false;
						break;
					default:
						_currentStyleDef.Underline = null;
						break;
				}
				ControlChanged(sender, e);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		#endregion
	}
}