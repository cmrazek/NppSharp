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
using System.Drawing.Text;

namespace NppSharp
{
	/// <summary>
	/// Defines elements of a style for output window text.
	/// </summary>
	public class OutputStyleDef
	{
		#region Static
		private static Dictionary<OutputStyle, OutputStyleDef> _styles = new Dictionary<OutputStyle, OutputStyleDef>();

		/// <summary>
		/// Gets the style definition object for the specified style number.
		/// </summary>
		/// <param name="style">The style number.</param>
		/// <returns>The style definition object.  If it does not exist yet, a new one will be created with default values.</returns>
		internal static OutputStyleDef GetStyleDef(OutputStyle style)
		{
			OutputStyleDef osd;
			if (_styles.TryGetValue(style, out osd)) return osd;

			osd = new OutputStyleDef(style);
			_styles.Add(style, osd);
			return osd;
		}

		/// <summary>
		/// Initializes default styles.
		/// </summary>
		internal static void InitStyles()
		{
			// Get the preferred font that is installed on the machine.
			string preferredFontName = "";
			string[] preferredFonts = new string[] { "Consolas", "Lucida Console", "Courier New", "Courier" };
			InstalledFontCollection ifc = new InstalledFontCollection();
			foreach (string fontName in preferredFonts)
			{
				bool found = false;
				foreach (FontFamily ff in ifc.Families)
				{
					if (ff.Name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase))
					{
						preferredFontName = ff.Name;
						found = true;
						break;
					}
				}
				if (found) break;
			}

			// Set standard styles (can be overridden by the user)
			OutputStyleDef osd = GetStyleDef(OutputStyle.Normal);
			osd.FontName = preferredFontName;
			osd.Size = 9;
			osd.Bold = false;
			osd.Italic = false;
			osd.Underline = false;
			osd.ForeColor = Color.Black;
			osd.BackColor = Color.White;

			osd = GetStyleDef(OutputStyle.Warning);
			osd.ForeColor = Color.DarkOrange;

			osd = GetStyleDef(OutputStyle.Error);
			osd.ForeColor = Color.Red;

			osd = GetStyleDef(OutputStyle.Comment);
			osd.ForeColor = Color.DarkGreen;

			osd = GetStyleDef(OutputStyle.Important);
			osd.ForeColor = Color.Blue;

			osd = GetStyleDef(OutputStyle.NotImportant);
			osd.ForeColor = Color.Gray;

			foreach (OutputStyleDef s in _styles.Values)
			{
				s.LoadSettings();
				s.Apply();
			}
		}
		#endregion

		#region Non-Static
		private OutputStyle _style;
		private string _fontName = null;
		private int? _size = null;
		private bool? _bold = null;
		private bool? _italic = null;
		private bool? _underline = null;
		private Color? _foreColor = null;
		private Color? _backColor = null;

		/// <summary>
		/// Constructs the style definition with default values.
		/// </summary>
		/// <param name="style">The style number.</param>
		public OutputStyleDef(OutputStyle style)
		{
			_style = style;
		}

		/// <summary>
		/// Creates a duplicate of this style definition.
		/// </summary>
		/// <returns>A new object with the same style elements.</returns>
		public OutputStyleDef Clone()
		{
			OutputStyleDef osd = new OutputStyleDef(_style);
			osd._fontName = _fontName;
			osd._size = _size;
			osd._bold = _bold;
			osd._italic = _italic;
			osd._underline = _underline;
			osd._foreColor = _foreColor;
			osd._backColor = _backColor;

			return osd;
		}

		/// <summary>
		/// Gets the style number.
		/// </summary>
		public OutputStyle Style
		{
			get { return _style; }
		}

		/// <summary>
		/// Gets or sets the font family name.
		/// (empty string for not defined)
		/// </summary>
		public string FontName
		{
			get { return _fontName; }
			set { _fontName = value; }
		}

		/// <summary>
		/// Gets or sets the font size.
		/// (null for not defined)
		/// </summary>
		public int? Size
		{
			get { return _size; }
			set { _size = value; }
		}

		/// <summary>
		/// Gets or sets the foreground color.
		/// (null for not defined)
		/// </summary>
		public Color? ForeColor
		{
			get { return _foreColor; }
			set { _foreColor = value; }
		}

		/// <summary>
		/// Gets or sets the background color.
		/// (null for not defined)
		/// </summary>
		public Color? BackColor
		{
			get { return _backColor; }
			set { _backColor = value; }
		}

		/// <summary>
		/// Gets or sets the bold flag.
		/// (null for not defined)
		/// </summary>
		public bool? Bold
		{
			get { return _bold; }
			set { _bold = value; }
		}

		/// <summary>
		/// Gets or sets the italic flag.
		/// (null for not defined)
		/// </summary>
		public bool? Italic
		{
			get { return _italic; }
			set { _italic = value; }
		}

		/// <summary>
		/// Gets or sets the underline flag.
		/// (null for not defined)
		/// </summary>
		public bool? Underline
		{
			get { return _underline; }
			set { _underline = value; }
		}

		/// <summary>
		/// Applies the style to the output window.
		/// </summary>
		internal void Apply()
		{
			OutputStyleDef osd = GetStyleDef(_style);
			osd._fontName = _fontName;
			osd._size = _size;
			osd._bold = _bold;
			osd._italic = _italic;
			osd._underline = _underline;
			osd._foreColor = _foreColor;
			osd._backColor = _backColor;

			OutputStyleDef defaultOsd = _style == OutputStyle.Normal ? null : GetStyleDef(OutputStyle.Normal);
			Plugin.NppIntf.SetOutputStyleDef(osd, defaultOsd);
		}

		/// <summary>
		/// Saves this style definition to the registry.
		/// </summary>
		internal void SaveSettings()
		{
			Settings.SetString(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleFontName), _fontName);
			Settings.SetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleSize), _size);
			Settings.SetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleBold), _bold);
			Settings.SetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleItalic), _italic);
			Settings.SetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleUnderline), _underline);
			Settings.SetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleForeColor), _foreColor.HasValue ? (int?)_foreColor.Value.ToArgb() : null);
			Settings.SetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleBackColor), _backColor.HasValue ? (int?)_backColor.Value.ToArgb() : null);
		}

		/// <summary>
		/// Loads style definition from the registry.
		/// </summary>
		internal void LoadSettings()
		{
			_fontName = Settings.GetString(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleFontName), _fontName);
			_size = Settings.GetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleSize), _size);
			_bold = Settings.GetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleBold), _bold);
			_italic = Settings.GetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleItalic), _italic);
			_underline = Settings.GetBoolOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleUnderline), _underline);

			int? color = Settings.GetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleForeColor), _foreColor.HasValue ? (int?)_foreColor.Value.ToArgb() : null);
			if (color.HasValue) _foreColor = Color.FromArgb(color.Value);

			color = Settings.GetIntOrNull(Settings.MakeOwsKeyPath(_style, Res.Reg_OutputStyleBackColor), _backColor.HasValue ? (int?)_backColor.Value.ToArgb() : null);
			if (color.HasValue) _backColor = Color.FromArgb(color.Value);
		}
		#endregion
	}
}
