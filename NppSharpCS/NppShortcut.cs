using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NppSharp
{
	public class NppShortcut
	{
		private bool _ctrl = false;
		private bool _alt = false;
		private bool _shift = false;
		private Keys _keyCode = Keys.None;

		public NppShortcut(bool ctrl, bool alt, bool shift, Keys keyCode)
		{
			_ctrl = ctrl;
			_alt = alt;
			_shift = shift;
			_keyCode = keyCode;
		}

		public bool Control
		{
			get { return _ctrl; }
			set { _ctrl = value; }
		}

		public bool Alt
		{
			get { return _alt; }
			set { _alt = value; }
		}

		public bool Shift
		{
			get { return _shift; }
			set { _shift = value; }
		}

		public Keys KeyCode
		{
			get { return _keyCode; }
			set { _keyCode = value; }
		}
	}
}
