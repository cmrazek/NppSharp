@echo off
if /I [%1] == [debug] (
	set _debug_=true
) else if /I [%1] == [release] (
	set _debug_=false
) else (
	goto ShowUsage
)


if /I [%computername%] == [cdm01] (
	set _soldir_="d:\dev\NppSharp"
	set _nppdir_="C:\Program Files (x86)\Notepad++"
) else if /I [%computername%] == [dev18381] (
	set _soldir_="D:\Work\NppSharp"
	set _nppdir_="C:\Program Files\Notepad++"
) else if /I [%computername%] == [cdmxpdev] (
	set _soldir_="C:\Dev\NppSharp"
	set _nppdir_="C:\Program Files\Notepad++"
) else if /I [%computername%] == [555isd022w] (
	set _soldir_="C:\Dev\NppSharp"
	set _nppdir_="C:\Program Files (x86)\Notepad++"
) else (
	echo Error: Unknown environment
	goto :eof
)

call :deploy %_soldir_% %_nppdir_%
goto :eof


:deploy
:: %1 = _soldir_ (NppSharp solution dir)
:: %2 = _nppdir_ (Notepad++ dir)

if [%_debug_%] == [true] (
	echo NppSharp.dll
	copy "%~1\debug\NppSharp.dll" "%~2\plugins\"

	echo NppSharpCS.dll
	copy "%~1\debug\NppSharpInterface.dll" "%~2\"
) else (
	echo NppSharp.dll
	copy "%~1\release\NppSharp.dll" "%~2\plugins\"

	echo NppSharpCS.dll
	copy "%~1\release\NppSharpInterface.dll" "%~2\"
)

goto :eof


:ShowUsage
echo Usage:
echo   Deploy (debug/release)
echo.
goto :eof
