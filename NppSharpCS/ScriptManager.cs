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
				Plugin.Output.WriteLine(OutputStyle.NotImportant, Res.script_CompilingAllScripts);

				_scripts.Clear();

				foreach (string dir in ScriptDirs)
				{
					if (!Directory.Exists(dir))
					{
						Plugin.Output.WriteLine(OutputStyle.Warning, Res.err_script_DirMissing, dir);
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
				Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_CompileAll, ex);
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
