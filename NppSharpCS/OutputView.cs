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
