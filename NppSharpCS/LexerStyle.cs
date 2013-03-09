using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NppSharp
{
	/// <summary>
	/// Defines a style for the lexer.
	/// </summary>
	/// <remarks>Notepad++ only allows a maximum of 32 styles per language to be defined.</remarks>
	public class LexerStyle
	{
		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		public LexerStyle(string name)
		{
			_name = name;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		public LexerStyle(string name, Color foreColor)
		{
			_name = name;
			_foreColor = foreColor;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="backColor">The background text color.</param>
		public LexerStyle(string name, Color foreColor, Color backColor)
		{
			_name = name;
			_foreColor = foreColor;
			_backColor = backColor;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, FontStyle fontStyle)
		{
			_name = name;
			_foreColor = foreColor;
			_fontStyle = fontStyle;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="backColor">The background text color.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, Color backColor, FontStyle fontStyle)
		{
			_name = name;
			_foreColor = foreColor;
			_backColor = backColor;
			_fontStyle = fontStyle;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="fontName">The name of the font family.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, string fontName, FontStyle fontStyle)
		{
			_name = name;
			_foreColor = foreColor;
			_fontName = fontName;
			_fontStyle = fontStyle;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="backColor">The background text color.</param>
		/// <param name="fontName">The name of the font family.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, Color backColor, string fontName, FontStyle fontStyle)
		{
			_name = name;
			_foreColor = foreColor;
			_backColor = backColor;
			_fontName = fontName;
			_fontStyle = fontStyle;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="fontName">The name of the font family.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <param name="fontSize">The size of the font.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, string fontName, FontStyle fontStyle, int fontSize)
		{
			_name = name;
			_foreColor = foreColor;
			_fontName = fontName;
			_fontStyle = fontStyle;
			_fontSize = fontSize;
		}

		/// <summary>
		/// Creates a new lexer style object.
		/// </summary>
		/// <param name="name">The name of the word style.</param>
		/// <param name="foreColor">The foreground text color.</param>
		/// <param name="backColor">The background text color.</param>
		/// <param name="fontName">The name of the font family.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <param name="fontSize">The size of the font.</param>
		/// <remarks>FontStyle.Strikeout is not functional within Notepad++</remarks>
		public LexerStyle(string name, Color foreColor, Color backColor, string fontName, FontStyle fontStyle, int fontSize)
		{
			_name = name;
			_foreColor = foreColor;
			_backColor = backColor;
			_fontName = fontName;
			_fontStyle = fontStyle;
			_fontSize = fontSize;
		}

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
		/// Gets or sets the index number for this style.
		/// </summary>
		/// <remarks>You do not need to set this value manually.
		/// The NppSharp plug-in will automatically assign this value as styles are added.</remarks>
		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}
		private int _index = 0;

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
