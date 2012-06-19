@echo off
del /Q /F /AH NppSharp.suo
del /Q /F NppSharp.ncb
del /Q /F /S *.exp
del /Q /F /S *.lib
del /Q /F /S *.pdb
del /Q /F /S *.ilk
del /Q /F NppSharp\Release\*.*
del /Q /F NppSharp\Debug\*.*
del /Q /F NppSharpCS\bin\Debug\*.*
del /Q /F NppSharpCS\bin\Release\*.*
