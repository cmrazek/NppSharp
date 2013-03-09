using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// A style for use in the NppSharp output window.
	/// </summary>
	public enum OutputStyle
	{
		/// <summary>
		/// The normal or default style for text.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// A style associated with warnings.
		/// </summary>
		Warning = 1,

		/// <summary>
		/// A style associated with errors.
		/// </summary>
		Error = 2,

		/// <summary>
		/// A style associated with comment text.
		/// </summary>
		Comment = 3,

		/// <summary>
		/// A style associated with important or emphasized text.
		/// </summary>
		Important = 4,

		/// <summary>
		/// A style associated with not-important text.
		/// </summary>
		NotImportant = 5
	}
}
