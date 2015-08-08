$ErrorActionPreference = "Stop"

if (Test-Path "C:\Program Files\NSIS\makensis.exe")
{
    $nsis = "C:\Program Files\NSIS\makensis.exe"
}
elseif (Test-Path "C:\Program Files (x86)\NSIS\makensis.exe")
{
    $nsis = "C:\Program Files (x86)\NSIS\makensis.exe"
}
else
{
    throw "NSIS could not be found."
}

function Copy-BinFile([string]$fileName)
{
    if (!(Test-Path $fileName)) { throw "File '$fileName' could not be found." }

    Copy-Item -Path $fileName -Destination "$PSScriptRoot\bin\$([System.IO.Path]::GetFileName($fileName))" -Force
}

Copy-BinFile "..\Release\NppSharp.dll"
Copy-BinFile "..\Release\NppSharpInterface.dll"
Copy-BinFile "..\NppSharpCS\bin\Release\NppSharpInterface.XML"
Copy-BinFile "..\Help\Output\NppSharpDoc.chm"
Copy-BinFile "C:\Windows\SysWOW64\MSVCP140.dll"

& $nsis NppSharpSetup.nsi
if ($LASTEXITCODE -ne 0)
{
    throw "MakeNSIS returned exit code $LASTEXITCODE"
}
