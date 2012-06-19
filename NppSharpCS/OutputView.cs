using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
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
		public void Show()
		{
			Plugin.NppIntf.ShowOutputWindow();
		}

		public void Hide()
		{
			Plugin.NppIntf.HideOutputWindow();
		}

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
		public void Write(string text)
		{
			Plugin.NppIntf.WriteOutput(text);
		}

		public void Write(string format, params object[] args)
		{
			Plugin.NppIntf.WriteOutput(string.Format(format, args));
		}

		public void Write(OutputStyle style, string text)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutput(text);
			Style = oldStyle;
		}

		public void Write(OutputStyle style, string format, object[] args)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutput(string.Format(format, args));
			Style = oldStyle;
		}

		public void WriteLine(string text)
		{
			Plugin.NppIntf.WriteOutputLine(text);
		}

		public void WriteLine(string format, params object[] args)
		{
			Plugin.NppIntf.WriteOutputLine(string.Format(format, args));
		}

		public void WriteLine(OutputStyle style, string text)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutputLine(text);
			Style = oldStyle;
		}

		public void WriteLine(OutputStyle style, string format, params object[] args)
		{
			OutputStyle oldStyle = Style;
			Style = style;
			Plugin.NppIntf.WriteOutputLine(string.Format(format, args));
			Style = oldStyle;
		}

		public void Clear()
		{
			Plugin.NppIntf.ClearOutputWindow();
		}
		#endregion

		#region Styles
		public OutputStyle Style
		{
			get { return Plugin.NppIntf.OutputStyle; }
			set { Plugin.NppIntf.OutputStyle = value; }
		}
		#endregion

		#region Navigation
		public void GoToTop()
		{
			Plugin.NppIntf.OutputWindowGoToTop();
		}

		public void GoToBottom()
		{
			Plugin.NppIntf.OutputWindowGoToBottom();
		}
		#endregion
	}
}
