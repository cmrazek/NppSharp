#pragma once

namespace NppSharp
{
	typedef npp::ILexer* (*LexerFactoryFunction)();

	void			SetPluginInfo(npp::NppData nppData);
	npp::FuncItem*	GetFuncList(int *pNumFuncsOut);
	void			OnNotify(npp::SCNotification *pNotify);
	void			OnCommand(int cmdId);

	int						OnGetLexerCount();
	void					OnGetLexerName(int num, char *buf, int bufLen);
	void					OnGetLexerStatusText(int num, wchar_t *buf, int bufLen);
	LexerFactoryFunction	OnGetLexerFactory(int index);

	void	WriteOutputLine(String^ message);
	void	WriteOutputLine(OutputStyle style, String^ message);
}
