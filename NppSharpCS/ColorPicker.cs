// NppSharp - C#/.NET Scripting Plugin for Notepad++
// Copyright (C) 2012  Chris Mrazek
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NppSharp
{
	internal partial class ColorPicker : UserControl
	{
		private Color? _color = null;
		private Pen _nonePen = null;
		private SolidBrush _colorBrush = null;

		public event EventHandler ColorChanged;

		public ColorPicker()
		{
			InitializeComponent();
		}

		private void ColorPicker_Load(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void PickColor()
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = _color != null ? (Color)_color : System.Drawing.Color.Black;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				if (dlg.Color != _color)
				{
					_color = dlg.Color;
					Invalidate();

					EventHandler ev = ColorChanged;
					if (ev != null) ev(this, new EventArgs());
				}
			}
		}

		private void ColorPicker_Click(object sender, EventArgs e)
		{
			try
			{
				PickColor();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void ShowError(Exception ex)
		{
			ErrorForm form = new ErrorForm(ex.Message, ex.ToString());
			form.ShowDialog();
		}

		public Color? Color
		{
			get { return _color; }
			set
			{
				if (_color != value)
				{
					_color = value;
					Invalidate();
				}
			}
		}

		private void ColorPicker_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				Graphics g = e.Graphics;

				if (_color == null)
				{
					if (_nonePen == null) _nonePen = new Pen(SystemColors.ControlDark, 2.0f);
					Rectangle clientRect = ClientRectangle;
					g.FillRectangle(SystemBrushes.Control, clientRect);
					g.DrawLine(_nonePen, new Point(0, 0), new Point(clientRect.Right, clientRect.Bottom));
					g.DrawLine(_nonePen, new Point(0, clientRect.Bottom), new Point(clientRect.Right, 0));
				}
				else
				{
					if (_colorBrush == null || _colorBrush.Color != _color) _colorBrush = new SolidBrush((Color)_color);
					g.FillRectangle(_colorBrush, ClientRectangle);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void ciPick_Click(object sender, EventArgs e)
		{
			try
			{
				PickColor();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void ciNone_Click(object sender, EventArgs e)
		{
			try
			{
				_color = null;
				Invalidate();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
	}
}
