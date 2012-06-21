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
using System.IO;

namespace NppSharp
{
	internal static class ScriptManager
	{
		private static List<Script> _scripts = new List<Script>();

		public static void Compile()
		{
			try
			{
				Plugin.Output.WriteLine(OutputStyle.NotImportant, "Compiling scripts...");

				_scripts.Clear();

				foreach (string dir in ScriptDirs)
				{
					if (!Directory.Exists(dir))
					{
						Plugin.Output.WriteLine(OutputStyle.Warning, "Script directory does not exist: {0}", dir);
					}
					else
					{
						CompileDir(dir);
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Output.Show();
				Plugin.Output.WriteLine("Exception when compiling scripts: " + ex.ToString());
			}
		}

		private static void CompileDir(string dir)
		{
			foreach (string fileName in Directory.GetFiles(dir))
			{
				switch (Path.GetExtension(fileName).ToLower())
				{
					case ".cs":
						CompileScript(fileName);
						break;
					case ".dll":
						LoadAssembly(fileName);
						break;
				}
			}
		}

		private static void CompileScript(string fileName)
		{
			Script script = Script.CompileScript(fileName);
			if (script != null) _scripts.Add(script);
		}

		private static void LoadAssembly(string fileName)
		{
			Script script = Script.LoadAssembly(fileName);
			if (script != null) _scripts.Add(script);
		}

		public static void AddCommands()
		{
			foreach (Script s in _scripts) s.AddCommand();
		}

		public static List<string> ScriptDirs
		{
			get
			{
				List<string> dirs = Settings.GetStringList(Res.Reg_ScriptDirs);
				if (dirs == null || dirs.Count == 0)
				{
					dirs = new List<string>();
					dirs.Add(Path.Combine(Plugin.NppIntf.NppDir, Res.ScriptDir));
				}
				return dirs;
			}
		}
	}
}
