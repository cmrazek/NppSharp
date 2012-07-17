;---------------------------------------------------------------------------------------------------
; NppSharpSetup.nsi
;---------------------------------------------------------------------------------------------------

Name "NppSharp"
OutFile "Output\NppSharp_Setup_1.0.3.3.exe"
InstallDir "$PROGRAMFILES32\Notepad++"

;---------------------------------------------------------------------------------------------------
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;---------------------------------------------------------------------------------------------------
Section ""
	
	; Check for running Notepad++
	FindWindow $0 "Notepad++"
	IntCmp $0 0 NotepadNotRunning
		MessageBox MB_OK|MB_ICONEXCLAMATION "Please ensure Notepad++ is closed before installation."
	NotepadNotRunning:
	
	; Write program files
	SetOutPath "$INSTDIR\plugins"
	File "bin\NppSharp.dll"
	
	SetOutPath "$INSTDIR"
	File "bin\NppSharpInterface.dll"
	File "bin\NppSharpInterface.xml"
	
	CreateDirectory "$INSTDIR\plugins\config\NppSharp"
	CreateDirectory "$INSTDIR\plugins\config\NppSharp\Scripts"
	
	SetOutPath "$INSTDIR\plugins\config\NppSharp"
	File "bin\NppSharpDoc.chm"
	
	; Write uninstall strings
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp" "DisplayName" "NppSharp"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp" "UninstallString" '"$INSTDIR\UninstallNppSharp.exe"'
	WriteUninstaller "$INSTDIR\UninstallNppSharp.exe"
	
SectionEnd

;---------------------------------------------------------------------------------------------------
Section "Uninstall"
	; Check for running Notepad++
	FindWindow $0 "Notepad++"
	IntCmp $0 0 NotepadNotRunning
		MessageBox MB_OK|MB_ICONEXCLAMATION "Please ensure Notepad++ is closed before uninstalling."
	NotepadNotRunning:
	
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NppSharp"
	
	Delete /REBOOTOK "$INSTDIR\NppSharpInterface.dll"
	Delete /REBOOTOK "$INSTDIR\NppSharpInterface.xml"
	
	Delete /REBOOTOK "$INSTDIR\plugins\NppSharp.dll"
	
	Delete /REBOOTOK "$INSTDIR\plugins\config\NppSharp\NppSharpDoc.chm"
	
	Delete /REBOOTOK "$INSTDIR\UninstallNppSharp.exe"
	
	IfRebootFlag 0 NoUninstReboot
		MessageBox MB_YESNO "A reboot is required to finish the uninstall.  Do you wish to reboot now?" IDNO NoUninstReboot
		Reboot
	NoUninstReboot:
SectionEnd
