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
