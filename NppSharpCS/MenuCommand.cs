
using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	internal class IDM
	{
		public const int Base = 40000;
		public const int File = Base + 1000;
		public const int Edit = Base + 2000;
		public const int Search = Base + 3000;
		public const int View = Base + 4000;
		public const int ViewFold = View + 50;
		public const int ViewUnfold = View + 60;
		public const int Format = Base + 5000;
		public const int Lang = Base + 6000;
		public const int About = Base + 7000;
		public const int Setting = Base + 8000;
		public const int Execute = Base + 9000;
	}

	public enum MenuCommand
	{
		#region File
		FileNew = (IDM.File + 1),
		FileOpen = (IDM.File + 2),
		FileClose = (IDM.File + 3),
		FileCloseAll = (IDM.File + 4),
		FileCloseAllButCurrent = (IDM.File + 5),
		FileSave = (IDM.File + 6) ,
		FileSaveAll = (IDM.File + 7) ,
		FileSaveAs = (IDM.File + 8),
		FileAsianLang = (IDM.File + 9)  ,
		FilePrint = (IDM.File + 10),
		FilePrintNow = 1001,
		FileExit = (IDM.File + 11),
		FileLoadSession = (IDM.File + 12),
		FileSaveSession = (IDM.File + 13),
		FileReload = (IDM.File + 14),
		FileSaveCopyAs = (IDM.File + 15),
		FileDelete = (IDM.File + 16),
		FileRename = (IDM.File + 17),

		FileOpenAllRecentFiles = (IDM.Edit + 40),
		FileCleanRecentFileList = (IDM.Edit + 41),
		#endregion
 
		#region Edit
		EditCut = (IDM.Edit + 1),
		EditCopy = (IDM.Edit + 2),
		EditUndo = (IDM.Edit + 3),
		EditRedo = (IDM.Edit + 4),
		EditPaste = (IDM.Edit + 5),
		EditDelete = (IDM.Edit + 6),
		EditSelectAll = (IDM.Edit + 7),
		
		EditInsertTab = (IDM.Edit + 8),
		EditRemoveTab = (IDM.Edit + 9),
		EditDuplicateLine = (IDM.Edit + 10),
		EditTransposeLine = (IDM.Edit + 11),
		EditSplitLines = (IDM.Edit + 12),
		EditJoinLines = (IDM.Edit + 13),
		EditLineUp = (IDM.Edit + 14),
		EditLineDown = (IDM.Edit + 15),
		EditUppercase = (IDM.Edit + 16),
		EditLowercase = (IDM.Edit + 17),

		EditBlockComment = (IDM.Edit + 22),
		EditStreamComment = (IDM.Edit + 23),
		EditTrimTrailing = (IDM.Edit + 24),
		
		EditRtl = (IDM.Edit+26),
		EditLtr = (IDM.Edit+27),
		EditSetReadOnly = (IDM.Edit+28),
		EditFullPathToClipboard = (IDM.Edit+29),
		EditFileNameToClipboard = (IDM.Edit+30),
		EditCurrentDirToClipboard = (IDM.Edit+31),

		EditClearReadOnly = (IDM.Edit+33),
		EditColumnMode = (IDM.Edit+34),
		EditBlockCommentSet = (IDM.Edit+35),
		EditBlockUncomment = (IDM.Edit+36),

		EditAutoComplete = (50000+0),
		EditAutoCompleteCurrentFile = (50000+1),
		EditFuncionCallTip = (50000+2),
		#endregion
	
		#region Search
		SearchFind = (IDM.Search + 1),
		SearchFindNext = (IDM.Search + 2),
		SearchReplace = (IDM.Search + 3),
		SearchGoToLine = (IDM.Search + 4),
		SearchToggleBookmark = (IDM.Search + 5),
		SearchNextBookmark = (IDM.Search + 6),
		SearchPrevBookmark = (IDM.Search + 7),
		SearchClearBookmarks = (IDM.Search + 8),
		SearchGoToMatchingBrace = (IDM.Search + 9),
		SearchFindPrev = (IDM.Search + 10),
		SearchFindIncrement = (IDM.Search + 11),
		SearchFindInFiles = (IDM.Search + 13),
		SearchVolatileFindNext = (IDM.Search + 14),
		SearchVolatileFindPrev = (IDM.Search + 15),
		SearchCutMarkedLines = (IDM.Search + 18),
		SearchCopyMarkedLines = (IDM.Search + 19),
		SearchPasteMarkedLines = (IDM.Search + 20),
		SearchDeleteMarkedLines = (IDM.Search + 21),
		SearchMarkAllExt1 = (IDM.Search + 22),
		SearchUnmarkAllExt1 = (IDM.Search + 23),
		SearchMarkAllExt2 = (IDM.Search + 24),
		SearchUnmarkAllExt2 = (IDM.Search + 25),
		SearchMarkAllExt3 = (IDM.Search + 26),
		SearchUnmarkAllExt3 = (IDM.Search + 27),
		SearchMarkAllExt4 = (IDM.Search + 28),
		SearchUnmarkAllExt4 = (IDM.Search + 29),
		SearchMarkAllExt5 = (IDM.Search + 30),
		SearchUnmarkAllExt5 = (IDM.Search + 31),
		SearchClearAllMarks = (IDM.Search + 32),
		#endregion

		#region View
		//IDM.ViewToolbarHide = (IDM.View + 1),
		ViewToolbarReduce = (IDM.View + 2),
		ViewToolbarEnlarge = (IDM.View + 3),
		ViewToolbarStandard = (IDM.View + 4),
		ViewReduceTabBar = (IDM.View + 5),
		ViewLockTabBar = (IDM.View + 6) ,
		ViewDrawTabBarTopBar = (IDM.View + 7),
		ViewDrawTabBarInactiveTab = (IDM.View + 8) ,
		ViewPostIt = (IDM.View + 9),
		ViewToggleFoldAll = (IDM.View + 10),
		ViewUserDialog = (IDM.View + 11),
		ViewLineNumber = (IDM.View + 12),
		ViewSymbolMargin = (IDM.View + 13),
		ViewFolderMargin = (IDM.View + 14),
		ViewFolderMarginSimple = (IDM.View + 15),
		ViewFolderMarginArrow = (IDM.View + 16),
		ViewFolderMarginCircle = (IDM.View + 17),
		ViewFolderMarginBox = (IDM.View + 18),
		ViewAllChars = (IDM.View + 19),
		ViewIndentGuide = (IDM.View + 20),
		ViewCurLineHilighting = (IDM.View + 21),
		ViewWrap = (IDM.View + 22),
		ViewZoomIn = (IDM.View + 23),
		ViewZoomOut = (IDM.View + 24),
		ViewTabSpace = (IDM.View + 25),
		ViewEol = (IDM.View + 26),
		ViewEdgeLine = (IDM.View + 27),
		ViewEdgeBackground = (IDM.View + 28),
		ViewToggleUnfoldAll = (IDM.View + 29),
		ViewFoldCurrent = (IDM.View + 30),
		ViewUnfoldCurrent = (IDM.View + 31),
		ViewFullScreenToggle = (IDM.View + 32),
		ViewZoomRestore = (IDM.View + 33),
		ViewAlwaysOnTop = (IDM.View + 34),
		ViewSyncScrollV = (IDM.View + 35),
		ViewSyncScrollH = (IDM.View + 36),
		ViewEdgeNone = (IDM.View + 37),
		ViewDrawTabBarCloseBottUn = (IDM.View + 38),
		ViewDrawTabBarDblClkToClose = (IDM.View + 39),
		ViewRefreshTabBar = (IDM.View + 40),
		ViewWrapSymbol = (IDM.View + 41),
		ViewHideLines = (IDM.View + 42),
		ViewDrawTabBarVertical = (IDM.View + 43),
		ViewDrawTabBarMultiLine = (IDM.View + 44),
		ViewDocChangeMargin = (IDM.View + 45),	

		ViewFold1 = (IDM.ViewFold + 1),
		ViewFold2 = (IDM.ViewFold + 2),
		ViewFold3 = (IDM.ViewFold + 3),
		ViewFold4 = (IDM.ViewFold + 4),
		ViewFold5 = (IDM.ViewFold + 5),
		ViewFold6 = (IDM.ViewFold + 6),
		ViewFold7 = (IDM.ViewFold + 7),
		ViewFold8 = (IDM.ViewFold + 8),

		ViewUnfold1 = (IDM.ViewUnfold + 1),
		ViewUnfold2 = (IDM.ViewUnfold + 2),
		ViewUnfold3 = (IDM.ViewUnfold + 3),
		ViewUnfold4 = (IDM.ViewUnfold + 4),
		ViewUnfold5 = (IDM.ViewUnfold + 5),
		ViewUnfold6 = (IDM.ViewUnfold + 6),
		ViewUnfold7 = (IDM.ViewUnfold + 7),
		ViewUnfold8 = (IDM.ViewUnfold + 8),
	
		ViewGoToAnotherView = 10001,
		ViewCloneToAnotherView = 10002,
		ViewGoToNewInstance = 10003,
		ViewLoadInNewInstance = 10004,

		ViewSwitchToOtherView = (IDM.View + 72),
		#endregion
	
		#region Format
		FormatToDos = (IDM.Format + 1),
		FormatToUnix = (IDM.Format + 2),
		FormatToMac = (IDM.Format + 3),
		FormatAnsi = (IDM.Format + 4),
		FormatUtf8 = (IDM.Format + 5),
		FormatUnicodeBigEndian = (IDM.Format + 6),
		FormatUnicodeLittleEndian = (IDM.Format + 7),
		FormatAsUtf8 = (IDM.Format + 8),
		FormatConvertAnsi = (IDM.Format + 9),
		FormatConvertAsUtf8 = (IDM.Format + 10),
		FormatConvertUtf8 = (IDM.Format + 11),
		FormatConvertUnicodeBigEndian = (IDM.Format + 12),
		FormatConvertUnicodeLittleEndian = (IDM.Format + 13),
		#endregion
	
		#region Language
		LangStyleConfigDialog = (IDM.Lang + 1),
		LangC = (IDM.Lang + 2),
		LangCpp = (IDM.Lang + 3),
		LangJava = (IDM.Lang + 4),
		LangHtml = (IDM.Lang + 5),
		LangXml = (IDM.Lang + 6),
		LangJs = (IDM.Lang + 7),
		LangPhp = (IDM.Lang + 8),
		LangAsp = (IDM.Lang + 9),
		LangCss = (IDM.Lang + 10),
		LangPascal = (IDM.Lang + 11),
		LangPython = (IDM.Lang + 12),
		LangPerl = (IDM.Lang + 13),
		LangObjC = (IDM.Lang + 14) ,
		LangAscii = (IDM.Lang + 15),
		LangText = (IDM.Lang + 16),
		LangRc = (IDM.Lang + 17),
		LangMakeFile = (IDM.Lang + 18),
		LangIni = (IDM.Lang + 19),
		LangSql = (IDM.Lang + 20),
		LangVb = (IDM.Lang + 21),
		LangBatch = (IDM.Lang + 22),
		LangCs = (IDM.Lang + 23),
		LangLua = (IDM.Lang + 24),
		LangTex = (IDM.Lang + 25),
		LangFortran = (IDM.Lang + 26),
		LangSh = (IDM.Lang + 27),
		LangFlash = (IDM.Lang + 28),
		LangNsis = (IDM.Lang + 29),
		LangTcl = (IDM.Lang + 30),
		LangList = (IDM.Lang + 31),
		LangScheme = (IDM.Lang + 32),
		LangAsm = (IDM.Lang + 33),
		LangDiff = (IDM.Lang + 34),
		LangProps = (IDM.Lang + 35),
		LangPs = (IDM.Lang + 36),
		LangRuby = (IDM.Lang + 37),
		LangSmallTalk = (IDM.Lang + 38),
		LangVhdl = (IDM.Lang + 39),
		LangCaml = (IDM.Lang + 40),
		LangKix = (IDM.Lang + 41),
		LangAda = (IDM.Lang + 42),
		LangVerilog = (IDM.Lang + 43),
		LangAu3 = (IDM.Lang + 44),
		LangMatlab = (IDM.Lang + 45),
		LangHaskell = (IDM.Lang + 46),
		LangInno = (IDM.Lang + 47),
		LangCMake = (IDM.Lang + 48),
		LangYaml = (IDM.Lang + 49),
		
		LangExternal = (IDM.Lang + 50),
		LangExternalLimit = (IDM.Lang + 79),

		LangUser = (IDM.Lang + 80),
		LangUserLimit = (IDM.Lang + 110),
		#endregion
	
		#region About
		AboutHomePage = (IDM.About  + 1),
		AboutProjectPage = (IDM.About  + 2),
		AboutOnlineHelp = (IDM.About  + 3),
		AboutForum = (IDM.About  + 4),
		AboutPluginsHome = (IDM.About  + 5),
		AboutUpdateNpp = (IDM.About  + 6),
		AboutWikiFaq = (IDM.About  + 7),
		AboutHelp = (IDM.About  + 8),
		#endregion

		#region Settings
		SettingTabSize = (IDM.Setting + 1),
		SettingTabReplaceSpace = (IDM.Setting + 2),
		SettingHistorySize = (IDM.Setting + 3),
		SettingEdgeSize = (IDM.Setting + 4),
		SettingImportPlugin = (IDM.Setting + 5),
		SettingImportStyleThemes = (IDM.Setting + 6),

		SettingTrayIcon = (IDM.Setting + 8),
		SettingShortcutMapper = (IDM.Setting + 9),
		SettingRememberLastSession = (IDM.Setting + 10),
		SettingPreferences = (IDM.Setting + 11),

		SettingAutoCnbChar = (IDM.Setting + 15),
		#endregion

		#region Macro
		MacroStartRecording = (IDM.Edit + 18),
		MacroStopRecording = (IDM.Edit + 19),
		MacroPLaybackRecorded = (IDM.Edit + 21),
		MacroSaveCurrent = (IDM.Edit + 25),
		MacroRunMultiDialog = (IDM.Edit+32),
		#endregion
	}
}
