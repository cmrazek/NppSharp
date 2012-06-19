using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NppSharp
{
	public partial class ErrorForm : Form
	{
		private string _message = "";
		private string _details = "";

		public ErrorForm()
		{
			InitializeComponent();
		}

		public ErrorForm(string message)
		{
			_message = message;
			InitializeComponent();
		}

		public ErrorForm(string message, string details)
		{
			_message = message;
			_details = details;
			InitializeComponent();
		}

		private void ErrorForm_Load(object sender, EventArgs e)
		{
			try
			{
				txtMessage.Font = new Font(txtMessage.Font, FontStyle.Bold);
				txtMessage.Text = _message;

				if (string.IsNullOrEmpty(_details))
				{
					tabControl.TabPages.Remove(tabDetails);
				}
				else
				{
					txtDetails.Text = _details;
				}
			}
			catch (Exception ex)
			{
				Inception(ex);
			}
		}

		private void Inception(Exception ex)
		{
			MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public string Details
		{
			get { return _details; }
			set { _details = value; }
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}