#include "StdAfx.h"
#include "NppInterface.h"
#include "OutputWindow.h"
#include "Docking.h"
#include "resource.h"
#include "ClrUtil.h"

namespace NppSharp
{
	void NppInterface::WriteOutput(String^ text)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(text);
		_outputWindow->Write(str);
	}

	void NppInterface::WriteOutputLine(String^ text)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(text);
		_outputWindow->WriteLine(str);
	}

	void NppInterface::ShowOutputWindow()
	{
		if (_outputWindow->GetVisible()) return;

		HWND nppHandle = _nppHandle;

		HWND hWnd = _outputWindow->GetHandle();
		if (!hWnd) return;

		if (!_outputWindow->GetDocked())
		{
			tTbData	tb;
			memset(&tb, 0, sizeof(tb));
			tb.hClient = hWnd;
			tb.pszName = OW_TITLE;
			tb.pszAddInfo = TEXT("");
			tb.dlgID = _outputWindowCmdIndex;
			tb.pszModuleName = MODULE_NAME;
			tb.uMask = DWS_DF_CONT_BOTTOM;
			tb.hIconTab = ::LoadIcon(::GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_OUTPUT_WINDOW));

			SendMessageW(nppHandle, NPPM_DMMREGASDCKDLG, 0, (LPARAM)&tb);
			SendMessageW(nppHandle, NPPM_DMMSHOW, 0, (LPARAM)hWnd);

			_outputWindow->SetDocked(true);
		}
		else
		{
			SendMessageW(_nppHandle, NPPM_DMMSHOW, 0, (LPARAM)hWnd);
		}

		_outputWindow->SetVisible(true);
	}

	void NppInterface::HideOutputWindow()
	{
		if (!_outputWindow->GetVisible()) return;

		::SendMessageW(_nppHandle, NPPM_DMMHIDE, 0, (LPARAM)_outputWindow->GetHandle());
		_outputWindow->SetVisible(false);
	}

	NppSharp::OutputStyle NppInterface::OutputStyle::get()
	{
		return (NppSharp::OutputStyle)_outputWindow->GetStyle();
	}

	void NppInterface::OutputStyle::set(NppSharp::OutputStyle style)
	{
		_outputWindow->SetStyle((int)style);
	}

	bool NppInterface::OutputWindowVisible::get()
	{
		return _outputWindow->GetVisible();
	}

	void NppInterface::SetShowOutputWindowCommandIndex(int cmdIndex)
	{
		_outputWindowCmdIndex = cmdIndex;
	}

	void NppInterface::ClearOutputWindow()
	{
		_outputWindow->Clear();
	}

	void NppInterface::OutputWindowGoToTop()
	{
		_outputWindow->GoToTop();
	}

	void NppInterface::OutputWindowGoToBottom()
	{
		_outputWindow->GoToBottom();
	}

	void NppInterface::SetOutputStyleDef(OutputStyleDef^ osd, OutputStyleDef^ defaultOsd)
	{
		// Font Name
		std::string fontName;
		if (!String::IsNullOrEmpty(osd->FontName))
		{
			fontName = ClrStringToAString(osd->FontName);
		}
		else if (defaultOsd != nullptr && !String::IsNullOrEmpty(defaultOsd->FontName))
		{
			fontName = ClrStringToAString(defaultOsd->FontName);
		}
		else
		{
			fontName = OW_DEFAULT_FONT_NAME;
		}
		_outputWindow->SetStyleFont((int)osd->Style, fontName.c_str());

		// Size
		int size = OW_DEFAULT_SIZE;
		if (osd->Size.HasValue) size = osd->Size.Value;
		else if (defaultOsd != nullptr && defaultOsd->Size.HasValue) size = defaultOsd->Size.Value;
		_outputWindow->SetStyleSize((int)osd->Style, size);

		// Bold
		bool bold = OW_DEFAULT_BOLD;
		if (osd->Bold.HasValue) bold = osd->Bold.Value;
		else if (defaultOsd != nullptr && defaultOsd->Bold.HasValue) bold = defaultOsd->Bold.Value;
		_outputWindow->SetStyleBold((int)osd->Style, bold);

		// Italic
		bool italic = OW_DEFAULT_ITALIC;
		if (osd->Italic.HasValue) italic = osd->Italic.Value;
		else if (defaultOsd != nullptr && defaultOsd->Italic.HasValue) italic = defaultOsd->Italic.Value;
		_outputWindow->SetStyleItalic((int)osd->Style, italic);

		// Underline
		bool underline = OW_DEFAULT_UNDERLINE;
		if (osd->Underline.HasValue) underline = osd->Underline.Value;
		else if (defaultOsd != nullptr && defaultOsd->Underline.HasValue) underline = defaultOsd->Underline.Value;
		_outputWindow->SetStyleUnderline((int)osd->Style, underline);

		// Fore Color
		int foreColor = OW_DEFAULT_FORECOLOR;
		if (osd->ForeColor.HasValue) foreColor = OW_RGB(osd->ForeColor.Value.R, osd->ForeColor.Value.G, osd->ForeColor.Value.B);
		else if (defaultOsd != nullptr && defaultOsd->ForeColor.HasValue) foreColor = OW_RGB(defaultOsd->ForeColor.Value.R, defaultOsd->ForeColor.Value.G, defaultOsd->ForeColor.Value.B);
		_outputWindow->SetStyleForeColor((int)osd->Style, foreColor);

		// Back Color
		int backColor = OW_DEFAULT_BACKCOLOR;
		if (osd->BackColor.HasValue) backColor = OW_RGB(osd->BackColor.Value.R, osd->BackColor.Value.G, osd->BackColor.Value.B);
		else if (defaultOsd != nullptr && defaultOsd->BackColor.HasValue) backColor = OW_RGB(defaultOsd->BackColor.Value.R, defaultOsd->BackColor.Value.G, defaultOsd->BackColor.Value.B);
		_outputWindow->SetStyleBackColor((int)osd->Style, backColor);
	}
}
