#pragma once

namespace NppSharp
{
	void		SetPluginInfo(NppData nppData);
	FuncItem*	GetFuncList(int *pNumFuncsOut);
	void		OnNotify(SCNotification *pNotify);
	void		OnCommand(int cmdId);
}
