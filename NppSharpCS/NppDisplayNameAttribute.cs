using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Controls the display name for the Notepad++ command.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class NppDisplayNameAttribute : Attribute
	{
		private string _displayName = "";

		/// <summary>
		/// Constructs the display name attribute.
		/// </summary>
		/// <param name="displayName"></param>
		public NppDisplayNameAttribute(string displayName)
		{
			_displayName = displayName;
		}

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}
	}
}
