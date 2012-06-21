@echo off
setlocal enableextensions enabledelayedexpansion

if exist "C:\Program Files\NSIS\makensis.exe" (
	set makensis="C:\Program Files\NSIS\makensis.exe"
) else if exist "C:\Program Files (x86)\NSIS\makensis.exe" (
	set makensis="C:\Program Files (x86)\NSIS\makensis.exe"
) else (
	echo Error: makensis.exe could not be found
	goto :eof
)

if not exist bin mkdir bin
if not exist output mkdir output

copy ..\release\NppSharp.dll bin\
copy ..\release\NppSharpInterface.dll bin\
copy ..\NppSharpCS\bin\Release\NppSharpInterface.XML bin\
copy ..\Help\Output\NppSharpDoc.chm bin\

%makensis% NppSharpSetup.nsi
if errorlevel 1 (
	echo MakeNSIS Failed.
	goto :eof
)
