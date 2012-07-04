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