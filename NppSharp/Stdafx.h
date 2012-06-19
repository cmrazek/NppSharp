// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#pragma unmanaged
#include <Windows.h>
#include <string>
#include <list>
#include <map>
using namespace std;

#include "PluginInterface.h"
#pragma managed

#include "Log.h"

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
