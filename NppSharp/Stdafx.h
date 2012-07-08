// NppSharp - C#/.NET Plugin for Notepad++
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

#pragma once

#pragma unmanaged
#include <Windows.h>
#include <string>
#include <list>
#include <map>
#include <vector>
using namespace std;

namespace npp
{
	#include "PluginInterface.h"
	#include "ILexer.h"
}
#pragma managed

using namespace System;
using namespace System::Collections::Generic;


#define MODULE_NAME			TEXT("NppSharp.dll")
#define PLUGIN_NAME			TEXT("NppSharp")
#define CONFIG_DIR_NAME		TEXT("NppSharp")
#define LOG_FILE_NAME		TEXT("NppSharp.log")
#define MENU_NAME			TEXT("&NppSharp")

#define NPPM_SAVECURRENTFILEAS (NPPMSG + 78)
#define NPPM_ALLOCATECMDID   (NPPMSG + 81)
#define NPPM_GETLANGUAGENAME  (NPPMSG + 83)
