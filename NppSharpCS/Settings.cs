using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Xml;
using System.IO;

namespace NppSharp
{
	/// <summary>
	/// Manages settings stored in the registry.
	/// </summary>
	internal partial class Settings
	{
		/// <summary>
		/// Splits a key out into the sub-key and value portions.
		/// </summary>
		/// <param name="keyPath">The combined sub-key and value string.
		/// The text after the last '\' will be considered the value name.</param>
		/// <param name="keyOut">The sub-key string.</param>
		/// <param name="nameOut">The value name.</param>
		private static void SplitKey(string keyPath, out string keyOut, out string nameOut)
		{
			string key;
			string name;

			int index = keyPath.LastIndexOf('\\');
			if (index < 0)
			{
				key = "";
				name = keyPath;
			}
			else
			{
				key = keyPath.Substring(0, index);
				name = keyPath.Substring(index + 1);
			}

			if (string.IsNullOrEmpty(key)) keyOut = Res.Reg_Key;
			else keyOut = string.Concat(Res.Reg_Key, "\\", key);

			nameOut = name;
		}

		/// <summary>
		/// Gets a string value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="defaultValue">If the value does not exist, this will be returned instead.</param>
		/// <returns>The value in the registry, or defaultValue if it does not exist.</returns>
		public static string GetString(string keyPath, string defaultValue)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey))
				{
					if (key == null) return defaultValue;

					object val = key.GetValue(name);
					if (val == null) return defaultValue;
					return Convert.ToString(val);
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return defaultValue;
			}
		}

		/// <summary>
		/// Inserts a string value into the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="value">The value to be inserted.</param>
		public static void SetString(string keyPath, string value)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey))
				{
					if (value == null)
					{
						if (key.GetValue(name) != null) key.DeleteValue(name);
					}
					else
					{
						key.SetValue(name, value);
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}

		/// <summary>
		/// Gets a string list value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <returns>The value in the registry, or an empty list if it does not exist.</returns>
		public static List<string> GetStringList(string keyPath)
		{
			try
			{
				string xmlContent = GetString(keyPath, "");

				if (string.IsNullOrEmpty(xmlContent)) return new List<string>();

				List<string> list = new List<string>();

				StringReader reader = new StringReader(xmlContent);
				XmlTextReader xml = new XmlTextReader(reader);
				xml.MoveToContent();

				while (!xml.EOF)
				{
					if (xml.NodeType == XmlNodeType.Element)
					{
						switch (xml.Name)
						{
							case "list":
								xml.ReadStartElement();
								break;
							case "s":
								list.Add(xml.ReadElementContentAsString());
								break;
							default:
								xml.Skip();
								break;
						}
					}
					else xml.Skip();
				}

				return list;
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return null;
			}
		}

		/// <summary>
		/// Inserts a string list value into the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="list">The string list to be inserted.
		/// Will actually be saved as an XML string containing the list.</param>
		public static void SetStringList(string keyPath, IEnumerable<string> list)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				XmlWriterSettings xmlSettings = new XmlWriterSettings();
				xmlSettings.OmitXmlDeclaration = true;
				XmlWriter xml = XmlWriter.Create(sb, xmlSettings);

				xml.WriteStartDocument();
				xml.WriteStartElement("list");
				foreach (string str in list) xml.WriteElementString("s", str);
				xml.WriteEndElement();	// l
				xml.WriteEndDocument();
				xml.Close();

				SetString(keyPath, sb.ToString());
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}

		/// <summary>
		/// Creates a key-path for an output window style.
		/// The value will be placed under a sub-key for the style number.
		/// </summary>
		/// <param name="style">The style number.</param>
		/// <param name="name">The value name.</param>
		/// <returns>The resulting key-path.</returns>
		public static string MakeOwsKeyPath(OutputStyle style, string name)
		{
			return string.Concat(Res.Reg_OutputStylePrefix, style.ToString(), "\\", name);
		}

		/// <summary>
		/// Gets an integer value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="defaultValue">If the value does not exist, this will be returned instead.</param>
		/// <returns>The value in the registry, or defaultValue if it does not exist.</returns>
		public static int GetInt(string keyPath, int defaultValue)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey))
				{
					if (key == null) return defaultValue;
					return Convert.ToInt32(key.GetValue(name, defaultValue));
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return defaultValue;
			}
		}

		/// <summary>
		/// Inserts an integer value into the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="value">The value to be inserted.</param>
		public static void SetInt(string keyPath, int value)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey))
				{
					key.SetValue(name, value);
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}

		/// <summary>
		/// Gets an int? value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="defaultValue">If the value does not exist, this will be returned instead.</param>
		/// <returns>The value in the registry, or defaultValue if it does not exist.</returns>
		public static int? GetIntOrNull(string keyPath, int? defaultValue)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey))
				{
					if (key == null) return defaultValue;

					object value = key.GetValue(name, null);
					if (value == null) return defaultValue;

					return Convert.ToInt32(value);
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return defaultValue;
			}
		}

		/// <summary>
		/// Inserts an integer value into the registry.
		/// If 'value' is null, then the registry value will be deleted if it exists.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="value">The value to be inserted.</param>
		public static void SetIntOrNull(string keyPath, int? value)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey))
				{
					if (value == null)
					{
						if (key.GetValue(name) != null) key.DeleteValue(name);
					}
					else
					{
						key.SetValue(name, (int)value);
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}

		/// <summary>
		/// Gets a boolean value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="defaultValue">If the value does not exist, this will be returned instead.</param>
		/// <returns>The value in the registry, or defaultValue if it does not exist.</returns>
		public static bool GetBool(string keyPath, bool defaultValue)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey))
				{
					if (key == null) return defaultValue;
					return Convert.ToInt32(key.GetValue(name, defaultValue ? 1 : 0)) != 0;
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return defaultValue;
			}
		}

		/// <summary>
		/// Inserts a boolean value into the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="value">The value to be inserted.</param>
		public static void SetBool(string keyPath, bool value)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey))
				{
					key.SetValue(name, value ? 1 : 0);
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}

		/// <summary>
		/// Gets an bool? value from the registry.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="defaultValue">If the value does not exist, this will be returned instead.</param>
		/// <returns>The value in the registry, or defaultValue if it does not exist.</returns>
		public static bool? GetBoolOrNull(string keyPath, bool? defaultValue)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey))
				{
					if (key == null) return defaultValue;

					object value = key.GetValue(name, null);
					if (value == null) return defaultValue;

					return Convert.ToInt32(value) != 0;
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when getting registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
				return defaultValue;
			}
		}

		/// <summary>
		/// Inserts a bool? value into the registry.
		/// If 'value' is null, then the registry value will be deleted if it exists.
		/// </summary>
		/// <param name="keyPath">The key-path for the value.</param>
		/// <param name="value">The value to be inserted.</param>
		public static void SetBoolOrNull(string keyPath, bool? value)
		{
			try
			{
				string subKey, name;
				SplitKey(keyPath, out subKey, out name);
				using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey))
				{
					if (value == null)
					{
						if (key.GetValue(name) != null) key.DeleteValue(name);
					}
					else
					{
						key.SetValue(name, value == true ? 1 : 0);
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, "Exception when assigning registry setting '{0}':\r\n{1}", keyPath, ex.ToString());
			}
		}
	}
}
