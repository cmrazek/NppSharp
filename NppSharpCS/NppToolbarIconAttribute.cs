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
using System.Reflection;
using System.IO;

namespace NppSharp
{
	/// <summary>
	/// Specifies that a command is to be visible on the Notepad++ toolbar.
	/// </summary>
	public class NppToolbarIconAttribute : Attribute
	{
		private string _property = "";
		private string _fileName = "";

		/// <summary>
		/// Gets or sets the name of the property that will retrieve the toolbar icon.
		/// The property must be declared as:
		/// </summary>
		/// <remarks>The property must be declared as: public Bitmap <i>PropertyName</i> { get; }</remarks>
		public string Property
		{
			get { return _property; }
			set { _property = value; }
		}

		/// <summary>
		/// Gets or sets the file name for the icon or bitmap file that will be used as the toolbar icon.
		/// </summary>
		/// <remarks>The file name can either be an absolute path name or relative from the script location.</remarks>
		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		internal Bitmap LoadIcon(object instance, string scriptFileName)
		{
			if (!string.IsNullOrEmpty(_property))
			{
				foreach (PropertyInfo pi in instance.GetType().GetProperties())
				{
					if (pi.Name == _property &&
						pi.PropertyType == typeof(Bitmap) || pi.PropertyType.IsSubclassOf(typeof(Bitmap)))
					{
						try
						{
							return (Bitmap)pi.GetValue(instance, null);
						}
						catch (Exception ex)
						{
							Plugin.Output.Show();
							Plugin.Output.WriteLine(OutputStyle.Error, string.Format(Res.err_ToolbarIconProperty, ex));
						}
					}
				}
			}
			else if (!string.IsNullOrEmpty(_fileName))
			{
				try
				{
					string absoluteFileName;
					if (Path.IsPathRooted(_fileName)) absoluteFileName = _fileName;
					else absoluteFileName = Path.Combine(Path.GetDirectoryName(scriptFileName), _fileName);

					switch (Path.GetExtension(absoluteFileName).ToLower())
					{
						case ".ico":
							return new Icon(absoluteFileName).ToBitmap();
						default:
							return new Bitmap(absoluteFileName);
					}
				}
				catch (Exception ex)
				{
					Plugin.Output.Show();
					Plugin.Output.WriteLine(OutputStyle.Error, Res.err_LoadToolbarIconFile, _fileName, ex);
				}
			}

			return null;
		}
	}
}
