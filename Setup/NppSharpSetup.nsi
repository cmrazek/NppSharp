;---------------------------------------------------------------------------------------------------
; NppSharpSetup.nsi
;---------------------------------------------------------------------------------------------------

Name "NppSharp"
OutFile "output\NppSharp Setup 1.0.exe"
InstallDir "$PROGRAMFILES32\Notepad++"

;---------------------------------------------------------------------------------------------------
Page directory
Page components
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;---------------------------------------------------------------------------------------------------
Section ""
	
	; Write program files
	SetOutPath "$INSTDIR\plugins"
	File "bin\NppSharp.dll"
	
	SetOutPath "$INSTDIR"
	File "bin\NppSharpInterface.dll"
	
	; Create plugin config directory which is the default place for users to put their scripts.
	CreateDirectory "$INSTDIR\plugins\config\NppSharp"
	
	; Write uninstall strings
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp" "DisplayName" "NppSharp"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp" "UninstallString" '"$INSTDIR\NppSharpUninstall.exe"'
	
	WriteUninstaller "$INSTDIR\NppSharpUninstall.exe"
	
SectionEnd

;---------------------------------------------------------------------------------------------------
;Section "Visual C++ 2005 Redistributable"
;	
;	SetOutPath "$INSTDIR"
;	File "redist\vcredist_x86.exe"
;	ExecWait '"$INSTDIR\vcredist_x86.exe"'
;	
;SectionEnd

;---------------------------------------------------------------------------------------------------
Section "Uninstall"
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp"
	
	Delete "$INSTDIR\Plugins\NppSharp.dll"
	Delete "$INSTDIR\NppSharpInterface.dll"
	Delete "$INSTDIR\NppSharpUninstall.exe"
SectionEnd
