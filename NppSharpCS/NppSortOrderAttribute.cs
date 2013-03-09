using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Controls the sort order for the class or method.
	/// </summary>
	/// <remarks>Menu items are sorted by the SortOrder property, in ascending order.</remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class NppSortOrderAttribute : Attribute
	{
		private int _sortOrder = Int32.MaxValue;

		/// <summary>
		/// Creates the sort order attribute.
		/// </summary>
		/// <param name="sort">The sort order number</param>
		public NppSortOrderAttribute(int sort)
		{
			_sortOrder = sort;
		}

		/// <summary>
		/// Gets or sets the sort order value.
		/// </summary>
		public int SortOrder
		{
			get { return _sortOrder; }
			set { _sortOrder = value; }
		}
	}
}
