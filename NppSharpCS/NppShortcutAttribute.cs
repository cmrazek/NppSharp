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

		/// <summary>
		/// Creates a new NppShortcut attribute.
		/// </summary>
		/// <param name="ctrl">Control key required?</param>
		/// <param name="alt">Alt key required?</param>
		/// <param name="shift">Shift key required?</param>
		/// <param name="keyCode">The virtual key code.</param>
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
