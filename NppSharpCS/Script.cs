using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace NppSharp
{
	/// <summary>
	/// Instance of a single script or assembly.
	/// </summary>
	internal class Script
	{
		#region Variables
		private Assembly _assembly;
		private string _fileName;
		private List<CommandClass> _classes = new List<CommandClass>();
		private int _numLexersFound = 0;
		private List<string> _references = new List<string>();
		#endregion

		#region Internal Classes
		/// <summary>
		/// Stores information for a class that contains commands in an assembly.
		/// </summary>
		private class CommandClass
		{
			public Type classType = null;
			public NppScript instance = null;
			public List<CommandMethod> methods = new List<CommandMethod>();
		}

		/// <summary>
		/// Stores information for a method that will be treated like a command.
		/// </summary>
		private class CommandMethod
		{
			public CommandClass parentClass = null;
			public string name = "";
			public MethodInfo method = null;
			public int commandIndex = -1;

			/// <summary>
			/// Constructs the object.
			/// </summary>
			/// <param name="cmdClass">Parent class.</param>
			public CommandMethod(CommandClass cmdClass)
			{
				parentClass = cmdClass;
			}

			/// <summary>
			/// Gets the object instance for the class that owns this method.
			/// (This will be null for classes that only have static methods).
			/// </summary>
			public object Instance
			{
				get { return parentClass.instance; }
			}
		}
		#endregion

		#region Construction
		/// <summary>
		/// Constructs the script object.
		/// </summary>
		/// <param name="fileName">The script's file name.</param>
		private Script(string fileName)
		{
			_fileName = fileName;
		}
		#endregion

		#region Compilation
		/// <summary>
		/// Compiles a .cs script, and processes the assembly.
		/// </summary>
		/// <param name="fileName">The script's file name.</param>
		/// <returns>If the script could be successfully compiled, and contains command methods,
		/// then returns a new script object.  Null otherwise.</returns>
		public static Script CompileScript(string fileName)
		{
			try
			{
				Script script = new Script(fileName);
				if (script.Compile()) return script;
				return null;
			}
			catch (Exception ex)
			{
				Plugin.Output.Show();
				Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_Compile, fileName, ex.ToString());
				return null;
			}
		}

		/// <summary>
		/// Compiles a .cs script file, and processes the assembly.
		/// </summary>
		/// <returns>If the script could be compiled and contains command methods, then true.
		/// Otherwise false.</returns>
		private bool Compile()
		{
			Plugin.Output.WriteLine(OutputStyle.NotImportant, Res.script_Compiling, _fileName);

			string source = File.ReadAllText(_fileName);
			CodeParser parser = new CodeParser(source);
			if (!ProcessSource(parser)) return false;

			CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");
			CompilerParameters parameters = new CompilerParameters();
			parameters.GenerateExecutable = false;
			parameters.GenerateInMemory = true;
			parameters.WarningLevel = 3;
			parameters.TreatWarningsAsErrors = false;
			parameters.CompilerOptions = "/optimize";

			// References
			parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
			foreach (string r in _references) parameters.ReferencedAssemblies.Add(r);

			CompilerResults results = compiler.CompileAssemblyFromSource(parameters, source);
			if (results.Errors.Count > 0)
			{
				StringBuilder sbErr = new StringBuilder();
				bool failed = false;
				foreach (CompilerError err in results.Errors)
				{
					if (sbErr.Length != 0) sbErr.AppendLine();
					if (err.IsWarning)
					{
						sbErr.AppendFormat(Res.script_CompileWarning, err.Line, err.ErrorNumber, err.ErrorText);
					}
					else
					{
						sbErr.AppendFormat(Res.script_CompileError, err.Line, err.ErrorNumber, err.ErrorText);
						failed = true;
					}
				}

				OutputStyle outStyle;
				if (failed)
				{
					sbErr.Insert(0, string.Format(Res.script_CompileErrors + "\r\n", _fileName));
					outStyle = OutputStyle.Error;
				}
				else
				{
					sbErr.Insert(0, string.Format(Res.script_CompileWarnings + "\r\n", _fileName));
					outStyle = OutputStyle.Warning;
				}

				if (failed) Plugin.Output.Show();
				Plugin.Output.WriteLine(outStyle, sbErr.ToString());

				if (failed) return false;
			}

			//_plugin.Log("  Script compiled successfully.");

			_assembly = results.CompiledAssembly;
			if (!ProcessAssembly()) return false;

			return true;
		}

		/// <summary>
		/// Examines a source code file, looking for references in the comments at the top.
		/// </summary>
		/// <param name="cp">A CodeParser object used to parse the text.</param>
		/// <returns>True if successful; otherwise false.</returns>
		private bool ProcessSource(CodeParser cp)
		{
			Regex rxRef = new Regex(@"^//\s*\#ref(erence)?\:\s*(.+)$");
			Match match;
			string line;

			while (!cp.EndOfFile)
			{
				line = cp.ReadLine().Trim();

				if (!string.IsNullOrEmpty(line))
				{
					if ((match = rxRef.Match(line)).Success)
					{
						_references.Add(match.Groups[2].Value);
					}
					else if (!line.StartsWith("//")) break;
				}
			}

			return true;
		}
		#endregion

		#region Pre-compiled Assembly Loading
		/// <summary>
		/// Loads a pre-compiled assembly and processes it.
		/// </summary>
		/// <param name="fileName">The assembly's file name.</param>
		/// <returns>If the assembly could be loaded, and contains command methods, then it returns
		/// a new script object.  Null otherwise.</returns>
		public static Script LoadAssembly(string fileName)
		{
			try
			{
				Script script = new Script(fileName);
				if (script.LoadDll()) return script;
				return null;
			}
			catch (Exception ex)
			{
				Plugin.Output.Show();
				Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_LoadDll, fileName, ex.ToString());
				return null;
			}
		}

		/// <summary>
		/// Loads a pre-compiled assembly, and processes it.
		/// </summary>
		/// <returns>If the assembly could be loaded and contains command methods, then true.
		/// Otherwise false.</returns>
		private bool LoadDll()
		{
			Plugin.Output.WriteLine(OutputStyle.NotImportant, Res.script_LoadingDll, _fileName);
			_assembly = Assembly.LoadFile(_fileName);
			if (!ProcessAssembly()) return false;
			return true;
		}
		#endregion

		#region Assembly Processing
		/// <summary>
		/// Searches for classes and methods in the assembly to locate potential commands.
		/// Public methods that don't have any 'unsupported' parameters will be considered to be
		/// commands.
		/// </summary>
		/// <returns>If one or more commands were found, then true; otherwise false.</returns>
		private bool ProcessAssembly()
		{
			foreach (Type type in _assembly.GetTypes())
			{
				if (!type.IsPublic) continue;
				if (type.IsAbstract) continue;

				if (typeof(NppScript).IsAssignableFrom(type))
				{
					ProcessScriptClass(type);
				}

				if (typeof(ILexer).IsAssignableFrom(type))
				{
					ProcessLexerClass(type);
				}
			}

			if (_classes.Count == 0 && _numLexersFound == 0) return false;

			return true;
		}

		private void ProcessScriptClass(Type type)
		{
			CommandClass cmdClass = new CommandClass();
			cmdClass.classType = type;

			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static |
				BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly))
			{
				if (methodInfo.Name.StartsWith("get_") || methodInfo.Name.StartsWith("set_")) continue;

				CommandMethod cmdMethod = new CommandMethod(cmdClass);
				cmdMethod.method = methodInfo;

				bool supported = true;
				foreach (ParameterInfo pi in methodInfo.GetParameters())
				{
					if (!IsParameterSupported(pi))
					{
						supported = false;
						break;
					}
				}

				if (supported)
				{
					foreach (NppDisplayNameAttribute n in methodInfo.GetCustomAttributes(typeof(NppDisplayNameAttribute), false))
					{
						cmdMethod.name = n.DisplayName;
					}
					if (string.IsNullOrEmpty(cmdMethod.name)) cmdMethod.name = methodInfo.Name;

					cmdClass.methods.Add(cmdMethod);
				}
			}

			if (cmdClass.methods.Count > 0)
			{
				// Create an instance of this object.
				try
				{
					cmdClass.instance = (NppScript)Activator.CreateInstance(cmdClass.classType, null);
					cmdClass.instance.ScriptFileName = _fileName;
					cmdClass.instance.InitEvents();
					_classes.Add(cmdClass);
				}
				catch (Exception ex)
				{
					Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_CreateInstance,
						cmdClass.classType.Name, _fileName, ex.ToString());
					cmdClass.instance = null;
				}
			}
		}

		private void ProcessLexerClass(Type type)
		{
			try
			{
				string displayName = type.Name;
				foreach (NppDisplayNameAttribute attr in type.GetCustomAttributes(typeof(NppDisplayNameAttribute), false))
				{
					if (!string.IsNullOrEmpty(attr.DisplayName)) displayName = attr.DisplayName;
				}

				string desc = displayName;
				foreach (NppDescriptionAttribute attr in type.GetCustomAttributes(typeof(NppDescriptionAttribute), false))
				{
					if (!string.IsNullOrEmpty(attr.Description)) desc = attr.Description;
				}

				string blockCommentStart = string.Empty;
				string blockCommentEnd = string.Empty;
				string lineComment = string.Empty;
				foreach (LexerCommentsAttribute attr in type.GetCustomAttributes(typeof(LexerCommentsAttribute), true))
				{
					if (!string.IsNullOrEmpty(attr.BlockStart)) blockCommentStart = attr.BlockStart;
					if (!string.IsNullOrEmpty(attr.BlockEnd)) blockCommentEnd = attr.BlockEnd;
					if (!string.IsNullOrEmpty(attr.Line)) lineComment = attr.Line;
				}

				Plugin.AddLexer(type, displayName, desc, blockCommentStart, blockCommentEnd, lineComment);
				_numLexersFound++;
			}
			catch (Exception ex)
			{
				Plugin.Output.WriteLine(OutputStyle.Error, Res.err_lexer_CreateInstance,
					type.Name, _fileName, ex.ToString());
			}
		}
		#endregion

		#region Commands
		/// <summary>
		/// Adds a commands in the assembly to the plugin menu.
		/// </summary>
		public void AddCommand()
		{
			foreach (CommandClass cmdClass in _classes)
			{
				int classSortOrder = Int32.MaxValue;
				foreach (NppSortOrderAttribute so in cmdClass.classType.GetCustomAttributes(typeof(NppSortOrderAttribute), false))
				{
					classSortOrder = so.SortOrder;
				}

				foreach (CommandMethod cmdMethod in cmdClass.methods)
				{
					PluginCommand cmd = new PluginCommand(cmdMethod.name);
					cmd.Tag = cmdMethod;
					cmd.Execute += Execute;
					cmd.ClassSortOrder = classSortOrder;

					foreach (NppShortcutAttribute s in cmdMethod.method.GetCustomAttributes(typeof(NppShortcutAttribute), false))
					{
						cmd.Shortcut = s.Shortcut;
					}

					cmd.SortOrder = Int32.MaxValue;
					foreach (NppSortOrderAttribute so in cmdMethod.method.GetCustomAttributes(typeof(NppSortOrderAttribute), false))
					{
						cmd.SortOrder = so.SortOrder;
					}

					foreach (NppSeparatorAttribute s in cmdMethod.method.GetCustomAttributes(typeof(NppSeparatorAttribute), false))
					{
						cmd.Separator = true;
					}

					foreach (NppToolbarIconAttribute t in cmdMethod.method.GetCustomAttributes(typeof(NppToolbarIconAttribute), false))
					{
						cmd.ShowInToolbar = true;
						cmd.ToolbarIcon = t.LoadIcon(cmdClass.instance, _fileName);
						if (cmd.ToolbarIcon == null) cmd.ToolbarIcon = Res.DefaultToolbarIcon;
					}

					cmdMethod.commandIndex = Plugin.AddCommand(cmd);
				}
			}
		}

		/// <summary>
		/// Called when the user wants to execute a command.
		/// </summary>
		/// <param name="data">The 'tag' object added to the PluginCommand object.  For script
		/// commands, this is set to the CommandMethod object.</param>
		public void Execute(object data)
		{
			try
			{
				if (data == null || data.GetType() != typeof(CommandMethod)) throw new ArgumentException(Res.err_script_CmdDataMissing);
				CommandMethod cmdMethod = (CommandMethod)data;

				Plugin.NppIntf.OnCommandStart();

				List<object> args = new List<object>();

				foreach (ParameterInfo paramInfo in cmdMethod.method.GetParameters())
				{
					args.Add(GetSupportedParameter(paramInfo));
				}

				try
				{
					cmdMethod.method.Invoke(cmdMethod.Instance, args.ToArray());
				}
				catch (Exception ex)
				{
					Plugin.Output.Show();
					Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_Execute, cmdMethod.name, ex);
				}
				

				Plugin.NppIntf.OnCommandEnd();
			}
			catch (Exception ex)
			{
				Plugin.Output.Show();
				Plugin.Output.WriteLine(OutputStyle.Error, Res.err_script_Execute, Res.script_UnknownCommand, ex);
			}
		}

		/// <summary>
		/// Determines if a parameter can be populated by the plugin when executing a command method.
		/// Methods with unsupported parameters will not be eligible to be a command.
		/// </summary>
		/// <param name="paramInfo">The parameter information object.</param>
		/// <returns>True if the parameter is supported; otherwise false.</returns>
		private bool IsParameterSupported(ParameterInfo paramInfo)
		{
			return false;
		}

		/// <summary>
		/// Produces the value that will be used to populate a parameter for a command method.
		/// </summary>
		/// <param name="pi">The parameter information object.</param>
		/// <returns>The value that will be assigned to the parameter.</returns>
		private object GetSupportedParameter(ParameterInfo pi)
		{
			return null;
		}
		#endregion
	}
}
