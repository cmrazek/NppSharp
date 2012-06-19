using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NppSharp
{
	/// <summary>
	/// Defines the keyboard shortcut for a command.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class NppShortcutAttribute : Attribute
	{
		private NppShortcut _shortcut;

		public NppShortcutAttribute(bool ctrl, bool alt, bool shift, Keys keyCode)
		{
			_shortcut = new NppShortcut(ctrl, alt, shift, keyCode);
		}

		/// <summary>
		/// Gets the shortcut object.
		/// </summary>
		public NppShortcut Shortcut
		{
			get { return _shortcut; }
		}
	}
}
