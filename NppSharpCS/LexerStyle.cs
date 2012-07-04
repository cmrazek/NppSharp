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
using System.Drawing;

namespace NppSharp
{
	/// <summary>
	/// Defines a style for the lexer.
	/// </summary>
	public class LexerStyle
	{
		/// <summary>
		/// Gets or sets the name of the style (e.g. "Comments" or "Operators")
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		private string _name;

		/// <summary>
		/// Gets or sets the foreground color.
		/// </summary>
		/// <remarks>Color.Transparent is used to indicate no color value.</remarks>
		public Color ForeColor
		{
			get { return _foreColor; }
			set { _foreColor = value; }
		}
		private Color _foreColor = Color.Transparent;

		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		/// <remarks>Color.Transparent is used to indicate no color value.</remarks>
		public Color BackColor
		{
			get { return _backColor; }
			set { _backColor = value; }
		}
		private Color _backColor = Color.Transparent;

		/// <summary>
		/// Gets or sets the name of the font.
		/// </summary>
		/// <remarks>If blank or null, the default font will be used.</remarks>
		public string FontName
		{
			get { return _fontName; }
			set { _fontName = value; }
		}
		private string _fontName = string.Empty;

		/// <summary>
		/// Gets or sets the font style.
		/// </summary>
		public FontStyle FontStyle
		{
			get { return _fontStyle; }
			set { _fontStyle = value; }
		}
		private FontStyle _fontStyle = 0;

		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		/// <remarks>If zero, the default font size will be used.</remarks>
		public int FontSize
		{
			get { return _fontSize; }
			set { _fontSize = value; }
		}
		private int _fontSize = 0;
	}
}
