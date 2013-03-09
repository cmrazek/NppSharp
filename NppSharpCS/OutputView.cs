using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// A class used to access to the output window.
	/// </summary>
	public class OutputView
	{
		#region Variables
		private int _showCommandIndex = 0;
		#endregion

		#region Construction
		internal int ShowViewCommandIndex
		{
			get { return _showCommandIndex; }
			set { _showCommandIndex = value; }
		}
		#endregion

		#region Visibility
		/// <summary>
		/// Shows the output window.
		/// </summary>
		public void Show()
		{
			Plugin.NppIntf.ShowOutputWindow();
		}

		/// <summary>
		/// Hides the output window.
		/// </summary>
		public void Hide()
		{
			Plugin.NppIntf.HideOutputWindow();
		}

		/// <summary>
		/// Gets or sets a value indicating if the output window is visible.
		/// </summary>
		public bool Visible
		{
			get { return Plugin.NppIntf.OutputWindowVisible; }
			set
			{
				if (value != Plugin.NppIntf.OutputWindowVisible)
				{
					if (value) Plugin.NppIntf.ShowOutputWindow();
					else Plugin.NppIntf.HideOutputWindow();
				}
			}
		}
		#endregion

		#region Writing
		/// <summary>
		/// Writes text to the output window using the current style.
		/// </summary>
		/// <param name="text">The text to be written.</param>
		public void Write(string text)
		{
			Plugin.NppIntf.WriteOutput(text);
		}

		/// <summary>
		/// Writes formatted text to the output window using the current style.
		/// </summary>
		/// <param name="format">The text format string.  See string.Format documentation for details.</param>
		/// <param name="args">Arguments to be used in the string formatting.</param>
		public void Write(string format, params object[] args)
		{
			Plugin.NppIntf.WriteOutput(string.Format(format, args));
		}

		/// <summary>
		/// Writes text to the output window using the specified style.
		/// </summary>
		/// <param name="style">The style for the text written.</param>
		/// <param name="text">The text to be written.</param>
		/// <remarks>After this function ends, the style will be remain what it was before this function was called.</remarks>
		public void Write(OutputStyle style, string text)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutput(text);
			Style = oldStyle;
		}

		/// <summary>
		/// Writes formatted text to the output window using the specified style.
		/// </summary>
		/// <param name="style">The style for the text written.</param>
		/// <param name="format">The text format string.  See string.Format documentation for details.</param>
		/// <param name="args">Arguments to be used in the string formatting.</param>
		/// <remarks>After this function ends, the style will be remain what it was before this function was called.</remarks>
		public void Write(OutputStyle style, string format, object[] args)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutput(string.Format(format, args));
			Style = oldStyle;
		}

		/// <summary>
		/// Writes text to the output window followed by a end-of-line, using the current style.
		/// </summary>
		/// <param name="text">The text to be written.</param>
		public void WriteLine(string text)
		{
			Plugin.NppIntf.WriteOutputLine(text);
		}

		/// <summary>
		/// Writes formatted text to the output window followed by a end-of-line, using the current style.
		/// </summary>
		/// <param name="format">The text format string.  See string.Format documentation for details.</param>
		/// <param name="args">Arguments to be used in the string formatting.</param>
		public void WriteLine(string format, params object[] args)
		{
			Plugin.NppIntf.WriteOutputLine(string.Format(format, args));
		}

		/// <summary>
		/// Writes text to the output window followed by a end-of-line, using the specified style.
		/// </summary>
		/// <param name="style">The style for the text written.</param>
		/// <param name="text">The text to be written.</param>
		/// <remarks>After this function ends, the style will be remain what it was before this function was called.</remarks>
		public void WriteLine(OutputStyle style, string text)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutputLine(text);
			Style = oldStyle;
		}

		/// <summary>
		/// Writes formatted text to the output window followed by a end-of-line, using the specified style.
		/// </summary>
		/// <param name="style">The style for the text written.</param>
		/// <param name="format">The text format string.  See string.Format documentation for details.</param>
		/// <param name="args">Arguments to be used in the string formatting.</param>
		/// <remarks>After this function ends, the style will be remain what it was before this function was called.</remarks>
		public void WriteLine(OutputStyle style, string format, params object[] args)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutputLine(string.Format(format, args));
			Style = oldStyle;
		}

		/// <summary>
		/// Clears all text from the output window.
		/// </summary>
		public void Clear()
		{
			Plugin.NppIntf.ClearOutputWindow();
		}
		#endregion

		#region Styles
		/// <summary>
		/// Gets or sets the current output window text style.
		/// </summary>
		public OutputStyle Style
		{
			get { return Plugin.NppIntf.OutputStyle; }
			set { Plugin.NppIntf.OutputStyle = value; }
		}
		#endregion

		#region Navigation
		/// <summary>
		/// Scrolls the output window to the top position.
		/// </summary>
		public void GoToTop()
		{
			Plugin.NppIntf.OutputWindowGoToTop();
		}

		/// <summary>
		/// Scrolls the output window to the bottom position.
		/// </summary>
		public void GoToBottom()
		{
			Plugin.NppIntf.OutputWindowGoToBottom();
		}
		#endregion
	}
}
