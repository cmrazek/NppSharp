#pragma once

#pragma unmanaged
#include <Windows.h>
#include <string>
#include <list>
#include <map>
#include <vector>
//using namespace std;

namespace npp
{
	#include "PluginInterface.h"
	#include "ILexer.h"
}
#pragma managed

using namespace System;
using namespace System::Collections::Generic;


#define MODULE_NAME				TEXT("NppSharp.dll")
#define PLUGIN_NAME				TEXT("NppSharp")
#define CONFIG_DIR_NAME			TEXT("NppSharp")
#define LOG_FILE_NAME			TEXT("NppSharp.log")
#define MENU_NAME				TEXT("&NppSharp")
#define NPP_PLUGIN_MENU_NAME	TEXT("Plugins")

#define NPPM_SAVECURRENTFILEAS (NPPMSG + 78)
#define NPPM_ALLOCATECMDID   (NPPMSG + 81)
#define NPPM_GETLANGUAGENAME  (NPPMSG + 83)
