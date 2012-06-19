#include "stdafx.h"
//#include "log.h"
//#include <stdio.h>
//#include <vcclr.h>
//
//namespace NppSharp
//{
//	static HANDLE	g_hFile = NULL;
//	static wchar_t	*g_pszLogBuf = NULL;
//	static DWORD	g_dwLogBufSize = 0;
//	static DWORD	g_dwLogLen = 0;
//
//	void logOpen(const wchar_t *pszDir)
//	{
//		try
//		{
//			g_dwLogBufSize = 0;
//			g_dwLogLen = 0;
//
//			TCHAR fileName[MAX_PATH] = L"";
//			lstrcpy(fileName, pszDir);
//			lstrcat(fileName, L"\\");
//			lstrcat(fileName, LOG_FILE_NAME);
//
//			if (!g_hFile)
//			{
//				g_hFile = ::CreateFile(fileName, GENERIC_WRITE, FILE_SHARE_READ, NULL,
//					CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
//				if (g_hFile == INVALID_HANDLE_VALUE)
//				{
//					g_hFile = NULL;
//					//::MessageBox(NULL, L"Failed to create log file.", L"Error", MB_OK | MB_ICONERROR);
//				}
//				else
//				{
//					wchar_t	bom = 0xfeff;
//					DWORD	dwNumWritten = 0;
//					if (!::WriteFile(g_hFile, &bom, sizeof(wchar_t), &dwNumWritten, NULL) ||
//						dwNumWritten != sizeof(bom))
//					{
//						g_hFile = NULL;
//						//::MessageBox(NULL, L"Failed to write BOM to log file. Closing.", L"Error", MB_OK | MB_ICONERROR);
//					}
//					else
//					{
//						logGrow(100);
//					}
//				}
//			}
//		}
//		catch (...)
//		{
//			//::MessageBox(NULL, L"Exception in logOpen()", L"Error", MB_OK | MB_ICONERROR);
//			g_hFile = NULL;
//		}
//	}
//
//	void logClose()
//	{
//		try
//		{
//			if (g_hFile)
//			{
//				::CloseHandle(g_hFile);
//				g_hFile = NULL;
//			}
//		}
//		catch (...)
//		{
//			//::MessageBox(NULL, L"Exception in logClose()", L"Error", MB_OK | MB_ICONERROR);
//		}
//	}
//
//	bool logIsOpen()
//	{
//		return g_hFile != NULL;
//	}
//
//	void logGrow(DWORD dwNumChars)
//	{
//		if (dwNumChars * sizeof(wchar_t) > g_dwLogBufSize)
//		{
//			wchar_t	*pszNewBuf = new wchar_t[dwNumChars];
//			if (g_pszLogBuf)
//			{
//				if (g_dwLogLen > 0) memcpy(pszNewBuf, g_pszLogBuf, g_dwLogLen * sizeof(wchar_t));
//				delete [] g_pszLogBuf;
//			}
//			g_pszLogBuf = pszNewBuf;
//			g_dwLogBufSize = dwNumChars * sizeof(wchar_t);
//		}
//	}
//
//	void logStart()
//	{
//		g_dwLogLen = 0;
//	}
//
//	void logEnd()
//	{
//		if (g_dwLogLen > 0)
//		{
//			DWORD dwNumWritten = 0;
//			::WriteFile(g_hFile, g_pszLogBuf, g_dwLogLen * sizeof(wchar_t), &dwNumWritten, NULL);
//		}
//	}
//
//	void logAdd(const wchar_t *pszString, DWORD dwLen)
//	{
//		if (dwLen > 0)
//		{
//			logGrow(g_dwLogLen + dwLen);
//			memcpy(g_pszLogBuf + g_dwLogLen, pszString, dwLen * sizeof(wchar_t));
//			g_dwLogLen += dwLen;
//		}
//	}
//
//	void logAddTimeStamp()
//	{
//		wchar_t		szTime[20];
//		SYSTEMTIME	time;
//
//		::GetLocalTime(&time);
//		int iLen = wsprintf(szTime, L"[%02d:%02d:%02d.%03d] ",
//			time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);
//		logAdd(szTime, (DWORD)iLen);
//	}
//
//	void logWrite(const wchar_t *pszMessage)
//	{
//		if (!g_hFile) return;
//
//		logStart();
//		logAddTimeStamp();
//		logAdd(pszMessage, lstrlen(pszMessage));
//		logAdd(L"\r\n", 2);
//		logEnd();
//	}
//
//	void logWrite(System::String ^message)
//	{
//		if (!g_hFile) return;
//
//		pin_ptr<const wchar_t> str = PtrToStringChars(message);
//
//		logStart();
//		logAddTimeStamp();
//		logAdd(str, lstrlen(str));
//		logAdd(L"\r\n", 2);
//		logEnd();
//	}
//}
