using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Represents a character position within the document.
	/// </summary>
	public struct TextLocation
	{
		#region Members Variables
		private int _line;
		private int _ch;

		/// <summary>
		/// Gets or sets the line number.
		/// </summary>
		public int Line
		{
			get { return _line + 1; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException(Res.err_TextLocation_LineNegative);
				_line = value - 1;
			}
		}

		/// <summary>
		/// Gets or sets the character position on the line.
		/// </summary>
		/// <remarks>The character position is equal to the number of characters from the start of the line.</remarks>
		public int CharPosition
		{
			get { return _ch + 1; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException(Res.err_TextLocation_PosNegative);
				_ch = value - 1;
			}
		}
		#endregion

		#region Static Instances
		/// <summary>
		/// A location representing the start of the document (line 1, character position 1).
		/// </summary>
		public static TextLocation Start;
		#endregion

		#region Construction
		/// <summary>
		/// Copies a text location object.
		/// </summary>
		/// <param name="c">The location to be copied.</param>
		public TextLocation(TextLocation c)
		{
			_line = c._line;
			_ch = c._ch;
		}

		/// <summary>
		/// Creates a text location object with a specific line and column.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="charPos">The one-based character position.</param>
		public TextLocation(int line, int charPos)
		{
			if (line <= 0) throw new ArgumentOutOfRangeException(Res.err_TextLocation_LineNegative);
			if (charPos <= 0) throw new ArgumentOutOfRangeException(Res.err_TextLocation_PosNegative);

			_line = line - 1;
			_ch = charPos - 1;
		}

		/// <summary>
		/// Returns a string representation of this location.
		/// </summary>
		/// <returns>A string.</returns>
		public override string ToString()
		{
			return string.Format("Ln {0}, Ch {1}", _line + 1, _ch + 1);
		}
		#endregion

		#region To/From Byte Offset
		/// <summary>
		/// Gets or sets the byte-offset.
		/// </summary>
		/// <remarks>The offset will not necessarily equal the number of characters into the file.
		/// If the file contains multi-byte characters, this value will differ.</remarks>
		public int ByteOffset
		{
			get { return Plugin.NppIntf.TextLocationToOffset(this); }
			set { this = Plugin.NppIntf.OffsetToTextLocation(value); }
		}

		/// <summary>
		/// Find the text location for a byte offset.
		/// </summary>
		/// <param name="offset">The zero-based byte offset to find.</param>
		/// <returns>The text location that represents the byte offset.</returns>
		/// <remarks>
		/// <para>
		/// If the offset happens to land in the middle of a multi-byte character, this function
		/// will return a text location pointing to the character that begins just prior to the
		/// offset.
		/// </para>
		/// <para>
		/// If the offset is before the start of the document (a negative number), then a location
		/// pointing to the start of the document will be returned.
		/// </para>
		/// <para>
		/// If the offset is after the end of the document, then a location pointing to the end
		/// of the document will be returned.
		/// </para>
		/// </remarks>
		public static TextLocation FromByteOffset(int offset)
		{
			return Plugin.NppIntf.OffsetToTextLocation(offset);
		}
		#endregion

		#region Operator Overloading
		/// <summary>
		/// Compares equality between two TextLocation objects.
		/// </summary>
		/// <param name="a">The first location to compare.</param>
		/// <param name="b">The second location to compare.</param>
		/// <returns>True if both objects point to the same line and column, otherwise false.</returns>
		public static bool operator ==(TextLocation a, TextLocation b)
		{
			return a._line == b._line && a._ch == b._ch;
		}

		/// <summary>
		/// Compares inequality between two TextLocation objects.
		/// </summary>
		/// <param name="a">The first location to compare.</param>
		/// <param name="b">The second location to compare.</param>
		/// <returns>True if the objects point to different lines and positions, otherwise false.</returns>
		public static bool operator !=(TextLocation a, TextLocation b)
		{
			return a._line != b._line || a._ch != b._ch;
		}

		/// <summary>
		/// Compares equality between this object and another TextLocation.
		/// </summary>
		/// <param name="obj">The object that will be compared.</param>
		/// <returns>True if the objects point to the same line and position, otherwise false.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null || !typeof(TextLocation).IsAssignableFrom(obj.GetType())) return false;
			return _line == ((TextLocation)obj)._line && _ch == ((TextLocation)obj)._ch;
		}

		/// <summary>
		/// Returns a hash code for this object.
		/// </summary>
		/// <returns>The calculated hash code.</returns>
		public override int GetHashCode()
		{
			return _line.GetHashCode() ^ _ch.GetHashCode();
		}

		/// <summary>
		/// Determines if 'a' is less than 'b'.
		/// </summary>
		/// <param name="a">The first text location to compare.</param>
		/// <param name="b">The second text location to compare.</param>
		/// <returns>True if 'a' is less than 'b', otherwise false.</returns>
		public static bool operator <(TextLocation a, TextLocation b)
		{
			if (a._line < b._line) return true;
			if (a._line == b._line && a._ch < b._ch) return true;
			return false;
		}

		/// <summary>
		/// Determines if 'a' is less than or equal to 'b'.
		/// </summary>
		/// <param name="a">The first text location to compare.</param>
		/// <param name="b">The second text location to compare.</param>
		/// <returns>True if 'a' is less or equal to than 'b', otherwise false.</returns>
		public static bool operator <=(TextLocation a, TextLocation b)
		{
			if (a._line < b._line) return true;
			if (a._line == b._line && a._ch <= b._ch) return true;
			return false;
		}

		/// <summary>
		/// Determines if 'a' is greater than 'b'.
		/// </summary>
		/// <param name="a">The first text location to compare.</param>
		/// <param name="b">The second text location to compare.</param>
		/// <returns>True if 'a' is greater than 'b', otherwise false.</returns>
		public static bool operator >(TextLocation a, TextLocation b)
		{
			if (a._line > b._line) return true;
			if (a._line == b._line && a._ch > b._ch) return true;
			return false;
		}

		/// <summary>
		/// Determines if 'a' is greater than or equal to 'b'.
		/// </summary>
		/// <param name="a">The first text location to compare.</param>
		/// <param name="b">The second text location to compare.</param>
		/// <returns>True if 'a' is greater or equal to than 'b', otherwise false.</returns>
		public static bool operator >=(TextLocation a, TextLocation b)
		{
			if (a._line > b._line) return true;
			if (a._line == b._line && a._ch >= b._ch) return true;
			return false;
		}

		/// <summary>
		/// Moves the text location by a specified number of characters.
		/// </summary>
		/// <param name="loc">The origin location.</param>
		/// <param name="dist">The number of characters to move.</param>
		/// <returns>A new location object representing the moved location.</returns>
		public static TextLocation operator +(TextLocation loc, int dist)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.MoveOffsetByChars(loc.ByteOffset, dist));
		}

		/// <summary>
		/// Moves the text location by a specified number of characters.
		/// </summary>
		/// <param name="loc">The origin location.</param>
		/// <param name="dist">The number of characters to move.</param>
		/// <returns>A new location object representing the moved location.</returns>
		public static TextLocation operator -(TextLocation loc, int dist)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.MoveOffsetByChars(loc.ByteOffset, -dist));
		}

		/// <summary>
		/// Increments the location by 1 character position.
		/// </summary>
		/// <param name="loc">The location object.</param>
		/// <returns>A new location object containing the incremented location.</returns>
		public static TextLocation operator ++(TextLocation loc)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.MoveOffsetByChars(loc.ByteOffset, 1));
		}

		/// <summary>
		/// Decrements the location by 1 character position.
		/// </summary>
		/// <param name="loc">The location object.</param>
		/// <returns>A new location object containing the decremented location.</returns>
		public static TextLocation operator --(TextLocation loc)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.MoveOffsetByChars(loc.ByteOffset, 1));
		}
		#endregion

		#region Text Manipulation
		/// <summary>
		/// Gets a location at the start of this line.
		/// </summary>
		public TextLocation LineStart
		{
			get { return new TextLocation(Line, 1); }
		}

		/// <summary>
		/// Gets a location at the end of this line (before line-end characters).
		/// </summary>
		public TextLocation LineEnd
		{
			get { return TextLocation.FromByteOffset(Plugin.NppIntf.GetLineEndPos(Line)); }
		}

		/// <summary>
		/// Gets the column number of this location, taking the width of tabs into account.
		/// </summary>
		public int Column
		{
			get { return Plugin.NppIntf.GetColumn(ByteOffset); }
		}
		#endregion

	}

}
