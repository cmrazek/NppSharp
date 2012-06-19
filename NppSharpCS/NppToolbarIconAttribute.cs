using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	public class NppToolbarIconAttribute : Attribute
	{
		private string _property = "";

		public string Property
		{
			get { return _property; }
			set { _property = value; }
		}
	}
}
