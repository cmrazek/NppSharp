NppSharp
Notepad++ plugin to run scripts written in C#/.NET
https://nppsharp.codeplex.com
Current Version: 1.2


Requirements:
	Visual C++ 2010 Runtime: http://www.microsoft.com/en-ca/download/details.aspx?id=5555
	.NET Framework 4:        http://www.microsoft.com/en-us/download/details.aspx?id=17718

Contact:
	Chris Mrazek (cmrazek)
	Email: chrismrazek@gmail.com

ChangeLog:

Version 1.2 - 2013-03-09:
- Migrated to CodePlex
- Added Modification event.
- Improved folding support.
- Fixed problem with toolbar icons displaying as black when bitmap is incompatible with screen format.

Version 1.1 - 2012-07-21:
- Document text positions have changed from a basic byte-offset to a TextLocation class.
  This allows easier manipulation when dealing with multi-byte characters.
  Unfortunately, this will break some existing scripts.
- Scripts can now trigger auto-completion with their own word lists.
- DockWindow function now returns an object which can be used to show/hide the window.
- Improved lexer performance (less memory allocations)
- Added new events for char-added and double-click.
- Misc fixes/improvements.

Version 1.0.3 - 2012-07-08:
- Upgraded to .NET Framework 4 / Visual Studio 2010.
- Added ability to create lexers in C#.

Version 1.0.2 - 2012-06-26:
- Fixed serious error occurring when the toolbar icon property throws an exception.
- Added built-in ability to load toolbar icons from .ico files through the 'FileName' property.
- Added NppScript.ScriptDirectory property.
- Added NppScript.NppWindow property.
- Added support for docking windows.
- Added more documentation.

Version 1.0.1 - 2012-06-21:
- Added API documentation.
