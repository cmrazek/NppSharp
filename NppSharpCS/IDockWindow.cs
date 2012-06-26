using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Interface for docked window created by scripts.
	/// </summary>
	public interface IDockWindow
	{
		/// <summary>
		/// Shows the docked window.
		/// </summary>
		void Show();

		/// <summary>
		/// Hides the docked window.
		/// </summary>
		void Hide();

		/// <summary>
		/// Gets or sets the visibility of the docked window.
		/// </summary>
		bool Visible { get; set; }

		/// <summary>
		/// Gets the ID number for this window.
		/// </summary>
		int Id { get; }
	}
}
