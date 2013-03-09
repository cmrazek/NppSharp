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

	/// <summary>
	/// A built-in Notepad++ menu command.
	/// </summary>
	public enum MenuCommand
	{
		#region File
		/// <summary>
		/// </summary>
		FileNew = (IDM.File + 1),

		/// <summary>
		/// </summary>
		FileOpen = (IDM.File + 2),

		/// <summary>
		/// </summary>
		FileClose = (IDM.File + 3),

		/// <summary>
		/// </summary>
		FileCloseAll = (IDM.File + 4),

		/// <summary>
		/// </summary>
		FileCloseAllButCurrent = (IDM.File + 5),

		/// <summary>
		/// </summary>
		FileSave = (IDM.File + 6) ,

		/// <summary>
		/// </summary>
		FileSaveAll = (IDM.File + 7) ,

		/// <summary>
		/// </summary>
		FileSaveAs = (IDM.File + 8),

		/// <summary>
		/// </summary>
		FileAsianLang = (IDM.File + 9)  ,

		/// <summary>
		/// </summary>
		FilePrint = (IDM.File + 10),

		/// <summary>
		/// </summary>
		FilePrintNow = 1001,

		/// <summary>
		/// </summary>
		FileExit = (IDM.File + 11),

		/// <summary>
		/// </summary>
		FileLoadSession = (IDM.File + 12),

		/// <summary>
		/// </summary>
		FileSaveSession = (IDM.File + 13),

		/// <summary>
		/// </summary>
		FileReload = (IDM.File + 14),

		/// <summary>
		/// </summary>
		FileSaveCopyAs = (IDM.File + 15),

		/// <summary>
		/// </summary>
		FileDelete = (IDM.File + 16),
		
		/// <summary>
		/// </summary>
		FileRename = (IDM.File + 17),

		/// <summary>
		/// </summary>
		FileOpenAllRecentFiles = (IDM.Edit + 40),

		/// <summary>
		/// </summary>
		FileCleanRecentFileList = (IDM.Edit + 41),
		#endregion
 
		#region Edit
		/// <summary>
		/// </summary>
		EditCut = (IDM.Edit + 1),

		/// <summary>
		/// </summary>
		EditCopy = (IDM.Edit + 2),

		/// <summary>
		/// </summary>
		EditUndo = (IDM.Edit + 3),

		/// <summary>
		/// </summary>
		EditRedo = (IDM.Edit + 4),

		/// <summary>
		/// </summary>
		EditPaste = (IDM.Edit + 5),

		/// <summary>
		/// </summary>
		EditDelete = (IDM.Edit + 6),

		/// <summary>
		/// </summary>
		EditSelectAll = (IDM.Edit + 7),
		
		/// <summary>
		/// </summary>
		EditInsertTab = (IDM.Edit + 8),

		/// <summary>
		/// </summary>
		EditRemoveTab = (IDM.Edit + 9),

		/// <summary>
		/// </summary>
		EditDuplicateLine = (IDM.Edit + 10),

		/// <summary>
		/// </summary>
		EditTransposeLine = (IDM.Edit + 11),

		/// <summary>
		/// </summary>
		EditSplitLines = (IDM.Edit + 12),

		/// <summary>
		/// </summary>
		EditJoinLines = (IDM.Edit + 13),

		/// <summary>
		/// </summary>
		EditLineUp = (IDM.Edit + 14),

		/// <summary>
		/// </summary>
		EditLineDown = (IDM.Edit + 15),

		/// <summary>
		/// </summary>
		EditUppercase = (IDM.Edit + 16),

		/// <summary>
		/// </summary>
		EditLowercase = (IDM.Edit + 17),

		/// <summary>
		/// </summary>
		EditBlockComment = (IDM.Edit + 22),

		/// <summary>
		/// </summary>
		EditStreamComment = (IDM.Edit + 23),

		/// <summary>
		/// </summary>
		EditTrimTrailing = (IDM.Edit + 24),
		
		/// <summary>
		/// </summary>
		EditRtl = (IDM.Edit+26),

		/// <summary>
		/// </summary>
		EditLtr = (IDM.Edit+27),

		/// <summary>
		/// </summary>
		EditSetReadOnly = (IDM.Edit+28),

		/// <summary>
		/// </summary>
		EditFullPathToClipboard = (IDM.Edit+29),

		/// <summary>
		/// </summary>
		EditFileNameToClipboard = (IDM.Edit+30),

		/// <summary>
		/// </summary>
		EditCurrentDirToClipboard = (IDM.Edit+31),

		/// <summary>
		/// </summary>
		EditClearReadOnly = (IDM.Edit+33),

		/// <summary>
		/// </summary>
		EditColumnMode = (IDM.Edit+34),

		/// <summary>
		/// </summary>
		EditBlockCommentSet = (IDM.Edit+35),

		/// <summary>
		/// </summary>
		EditBlockUncomment = (IDM.Edit+36),

		/// <summary>
		/// </summary>
		EditAutoComplete = (50000+0),

		/// <summary>
		/// </summary>
		EditAutoCompleteCurrentFile = (50000+1),

		/// <summary>
		/// </summary>
		EditFuncionCallTip = (50000+2),
		#endregion
	
		#region Search
		/// <summary>
		/// </summary>
		SearchFind = (IDM.Search + 1),

		/// <summary>
		/// </summary>
		SearchFindNext = (IDM.Search + 2),

		/// <summary>
		/// </summary>
		SearchReplace = (IDM.Search + 3),

		/// <summary>
		/// </summary>
		SearchGoToLine = (IDM.Search + 4),

		/// <summary>
		/// </summary>
		SearchToggleBookmark = (IDM.Search + 5),

		/// <summary>
		/// </summary>
		SearchNextBookmark = (IDM.Search + 6),

		/// <summary>
		/// </summary>
		SearchPrevBookmark = (IDM.Search + 7),

		/// <summary>
		/// </summary>
		SearchClearBookmarks = (IDM.Search + 8),

		/// <summary>
		/// </summary>
		SearchCutMarkedLines = (IDM.Search + 18),

		/// <summary>
		/// </summary>
		SearchCopyMarkedLines = (IDM.Search + 19),

		/// <summary>
		/// </summary>
		SearchPasteMarkedLines = (IDM.Search + 20),

		/// <summary>
		/// </summary>
		SearchDeleteMarkedLines = (IDM.Search + 21),

		/// <summary>
		/// </summary>
		SearchGoToMatchingBrace = (IDM.Search + 9),

		/// <summary>
		/// </summary>
		SearchFindPrev = (IDM.Search + 10),

		/// <summary>
		/// </summary>
		SearchFindIncrement = (IDM.Search + 11),

		/// <summary>
		/// </summary>
		SearchFindInFiles = (IDM.Search + 13),

		/// <summary>
		/// </summary>
		SearchVolatileFindNext = (IDM.Search + 14),

		/// <summary>
		/// </summary>
		SearchVolatileFindPrev = (IDM.Search + 15),
		
		/// <summary>
		/// </summary>
		SearchMarkAllExt1 = (IDM.Search + 22),

		/// <summary>
		/// </summary>
		SearchUnmarkAllExt1 = (IDM.Search + 23),

		/// <summary>
		/// </summary>
		SearchMarkAllExt2 = (IDM.Search + 24),

		/// <summary>
		/// </summary>
		SearchUnmarkAllExt2 = (IDM.Search + 25),

		/// <summary>
		/// </summary>
		SearchMarkAllExt3 = (IDM.Search + 26),

		/// <summary>
		/// </summary>
		SearchUnmarkAllExt3 = (IDM.Search + 27),

		/// <summary>
		/// </summary>
		SearchMarkAllExt4 = (IDM.Search + 28),

		/// <summary>
		/// </summary>
		SearchUnmarkAllExt4 = (IDM.Search + 29),

		/// <summary>
		/// </summary>
		SearchMarkAllExt5 = (IDM.Search + 30),

		/// <summary>
		/// </summary>
		SearchUnmarkAllExt5 = (IDM.Search + 31),

		/// <summary>
		/// </summary>
		SearchClearAllMarks = (IDM.Search + 32),
		#endregion

		#region View
		//IDM.ViewToolbarHide = (IDM.View + 1),

		/// <summary>
		/// 
		/// </summary>
		ViewToolbarReduce = (IDM.View + 2),

		/// <summary>
		/// 
		/// </summary>
		ViewToolbarEnlarge = (IDM.View + 3),

		/// <summary>
		/// 
		/// </summary>
		ViewToolbarStandard = (IDM.View + 4),

		/// <summary>
		/// 
		/// </summary>
		ViewReduceTabBar = (IDM.View + 5),

		/// <summary>
		/// 
		/// </summary>
		ViewLockTabBar = (IDM.View + 6),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarTopBar = (IDM.View + 7),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarInactiveTab = (IDM.View + 8),

		/// <summary>
		/// 
		/// </summary>
		ViewPostIt = (IDM.View + 9),

		/// <summary>
		/// 
		/// </summary>
		ViewToggleFoldAll = (IDM.View + 10),

		/// <summary>
		/// 
		/// </summary>
		ViewUserDialog = (IDM.View + 11),

		/// <summary>
		/// 
		/// </summary>
		ViewLineNumber = (IDM.View + 12),

		/// <summary>
		/// 
		/// </summary>
		ViewSymbolMargin = (IDM.View + 13),

		/// <summary>
		/// 
		/// </summary>
		ViewFolderMargin = (IDM.View + 14),

		/// <summary>
		/// 
		/// </summary>
		ViewFolderMarginSimple = (IDM.View + 15),

		/// <summary>
		/// 
		/// </summary>
		ViewFolderMarginArrow = (IDM.View + 16),

		/// <summary>
		/// 
		/// </summary>
		ViewFolderMarginCircle = (IDM.View + 17),

		/// <summary>
		/// 
		/// </summary>
		ViewFolderMarginBox = (IDM.View + 18),

		/// <summary>
		/// 
		/// </summary>
		ViewAllChars = (IDM.View + 19),

		/// <summary>
		/// 
		/// </summary>
		ViewIndentGuide = (IDM.View + 20),

		/// <summary>
		/// 
		/// </summary>
		ViewCurLineHilighting = (IDM.View + 21),

		/// <summary>
		/// 
		/// </summary>
		ViewWrap = (IDM.View + 22),

		/// <summary>
		/// 
		/// </summary>
		ViewZoomIn = (IDM.View + 23),

		/// <summary>
		/// 
		/// </summary>
		ViewZoomOut = (IDM.View + 24),

		/// <summary>
		/// 
		/// </summary>
		ViewTabSpace = (IDM.View + 25),

		/// <summary>
		/// 
		/// </summary>
		ViewEol = (IDM.View + 26),

		/// <summary>
		/// 
		/// </summary>
		ViewEdgeLine = (IDM.View + 27),

		/// <summary>
		/// 
		/// </summary>
		ViewEdgeBackground = (IDM.View + 28),

		/// <summary>
		/// 
		/// </summary>
		ViewToggleUnfoldAll = (IDM.View + 29),

		/// <summary>
		/// 
		/// </summary>
		ViewFoldCurrent = (IDM.View + 30),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfoldCurrent = (IDM.View + 31),

		/// <summary>
		/// 
		/// </summary>
		ViewFullScreenToggle = (IDM.View + 32),

		/// <summary>
		/// 
		/// </summary>
		ViewZoomRestore = (IDM.View + 33),

		/// <summary>
		/// 
		/// </summary>
		ViewAlwaysOnTop = (IDM.View + 34),

		/// <summary>
		/// 
		/// </summary>
		ViewSyncScrollV = (IDM.View + 35),

		/// <summary>
		/// 
		/// </summary>
		ViewSyncScrollH = (IDM.View + 36),

		/// <summary>
		/// 
		/// </summary>
		ViewEdgeNone = (IDM.View + 37),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarCloseBottUn = (IDM.View + 38),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarDblClkToClose = (IDM.View + 39),

		/// <summary>
		/// 
		/// </summary>
		ViewRefreshTabBar = (IDM.View + 40),

		/// <summary>
		/// 
		/// </summary>
		ViewWrapSymbol = (IDM.View + 41),

		/// <summary>
		/// 
		/// </summary>
		ViewHideLines = (IDM.View + 42),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarVertical = (IDM.View + 43),

		/// <summary>
		/// 
		/// </summary>
		ViewDrawTabBarMultiLine = (IDM.View + 44),

		/// <summary>
		/// 
		/// </summary>
		ViewDocChangeMargin = (IDM.View + 45),

		/// <summary>
		/// 
		/// </summary>
		ViewFold1 = (IDM.ViewFold + 1),

		/// <summary>
		/// 
		/// </summary>
		ViewFold2 = (IDM.ViewFold + 2),

		/// <summary>
		/// 
		/// </summary>
		ViewFold3 = (IDM.ViewFold + 3),

		/// <summary>
		/// 
		/// </summary>
		ViewFold4 = (IDM.ViewFold + 4),

		/// <summary>
		/// 
		/// </summary>
		ViewFold5 = (IDM.ViewFold + 5),

		/// <summary>
		/// 
		/// </summary>
		ViewFold6 = (IDM.ViewFold + 6),

		/// <summary>
		/// 
		/// </summary>
		ViewFold7 = (IDM.ViewFold + 7),

		/// <summary>
		/// 
		/// </summary>
		ViewFold8 = (IDM.ViewFold + 8),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold1 = (IDM.ViewUnfold + 1),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold2 = (IDM.ViewUnfold + 2),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold3 = (IDM.ViewUnfold + 3),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold4 = (IDM.ViewUnfold + 4),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold5 = (IDM.ViewUnfold + 5),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold6 = (IDM.ViewUnfold + 6),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold7 = (IDM.ViewUnfold + 7),

		/// <summary>
		/// 
		/// </summary>
		ViewUnfold8 = (IDM.ViewUnfold + 8),

		/// <summary>
		/// 
		/// </summary>
		ViewGoToAnotherView = 10001,

		/// <summary>
		/// 
		/// </summary>
		ViewCloneToAnotherView = 10002,

		/// <summary>
		/// 
		/// </summary>
		ViewGoToNewInstance = 10003,

		/// <summary>
		/// 
		/// </summary>
		ViewLoadInNewInstance = 10004,

		/// <summary>
		/// 
		/// </summary>
		ViewSwitchToOtherView = (IDM.View + 72),
		#endregion

		#region Format
		/// <summary>
		/// 
		/// </summary>
		FormatToDos = (IDM.Format + 1),

		/// <summary>
		/// 
		/// </summary>
		FormatToUnix = (IDM.Format + 2),

		/// <summary>
		/// 
		/// </summary>
		FormatToMac = (IDM.Format + 3),

		/// <summary>
		/// 
		/// </summary>
		FormatAnsi = (IDM.Format + 4),

		/// <summary>
		/// 
		/// </summary>
		FormatUtf8 = (IDM.Format + 5),

		/// <summary>
		/// 
		/// </summary>
		FormatUnicodeBigEndian = (IDM.Format + 6),

		/// <summary>
		/// 
		/// </summary>
		FormatUnicodeLittleEndian = (IDM.Format + 7),

		/// <summary>
		/// 
		/// </summary>
		FormatAsUtf8 = (IDM.Format + 8),

		/// <summary>
		/// 
		/// </summary>
		FormatConvertAnsi = (IDM.Format + 9),

		/// <summary>
		/// 
		/// </summary>
		FormatConvertAsUtf8 = (IDM.Format + 10),

		/// <summary>
		/// 
		/// </summary>
		FormatConvertUtf8 = (IDM.Format + 11),

		/// <summary>
		/// 
		/// </summary>
		FormatConvertUnicodeBigEndian = (IDM.Format + 12),

		/// <summary>
		/// 
		/// </summary>
		FormatConvertUnicodeLittleEndian = (IDM.Format + 13),
		#endregion

		#region Language
		/// <summary>
		/// 
		/// </summary>
		LangStyleConfigDialog = (IDM.Lang + 1),

		/// <summary>
		/// 
		/// </summary>
		LangC = (IDM.Lang + 2),

		/// <summary>
		/// 
		/// </summary>
		LangCpp = (IDM.Lang + 3),

		/// <summary>
		/// 
		/// </summary>
		LangJava = (IDM.Lang + 4),

		/// <summary>
		/// 
		/// </summary>
		LangHtml = (IDM.Lang + 5),

		/// <summary>
		/// 
		/// </summary>
		LangXml = (IDM.Lang + 6),

		/// <summary>
		/// 
		/// </summary>
		LangJs = (IDM.Lang + 7),

		/// <summary>
		/// 
		/// </summary>
		LangPhp = (IDM.Lang + 8),

		/// <summary>
		/// 
		/// </summary>
		LangAsp = (IDM.Lang + 9),

		/// <summary>
		/// 
		/// </summary>
		LangCss = (IDM.Lang + 10),

		/// <summary>
		/// 
		/// </summary>
		LangPascal = (IDM.Lang + 11),

		/// <summary>
		/// 
		/// </summary>
		LangPython = (IDM.Lang + 12),

		/// <summary>
		/// 
		/// </summary>
		LangPerl = (IDM.Lang + 13),

		/// <summary>
		/// 
		/// </summary>
		LangObjC = (IDM.Lang + 14),

		/// <summary>
		/// 
		/// </summary>
		LangAscii = (IDM.Lang + 15),

		/// <summary>
		/// 
		/// </summary>
		LangText = (IDM.Lang + 16),

		/// <summary>
		/// 
		/// </summary>
		LangRc = (IDM.Lang + 17),

		/// <summary>
		/// 
		/// </summary>
		LangMakeFile = (IDM.Lang + 18),

		/// <summary>
		/// 
		/// </summary>
		LangIni = (IDM.Lang + 19),

		/// <summary>
		/// 
		/// </summary>
		LangSql = (IDM.Lang + 20),

		/// <summary>
		/// 
		/// </summary>
		LangVb = (IDM.Lang + 21),

		/// <summary>
		/// 
		/// </summary>
		LangBatch = (IDM.Lang + 22),

		/// <summary>
		/// 
		/// </summary>
		LangCs = (IDM.Lang + 23),

		/// <summary>
		/// 
		/// </summary>
		LangLua = (IDM.Lang + 24),

		/// <summary>
		/// 
		/// </summary>
		LangTex = (IDM.Lang + 25),

		/// <summary>
		/// 
		/// </summary>
		LangFortran = (IDM.Lang + 26),

		/// <summary>
		/// 
		/// </summary>
		LangSh = (IDM.Lang + 27),

		/// <summary>
		/// 
		/// </summary>
		LangFlash = (IDM.Lang + 28),

		/// <summary>
		/// 
		/// </summary>
		LangNsis = (IDM.Lang + 29),

		/// <summary>
		/// 
		/// </summary>
		LangTcl = (IDM.Lang + 30),

		/// <summary>
		/// 
		/// </summary>
		LangList = (IDM.Lang + 31),

		/// <summary>
		/// 
		/// </summary>
		LangScheme = (IDM.Lang + 32),

		/// <summary>
		/// 
		/// </summary>
		LangAsm = (IDM.Lang + 33),

		/// <summary>
		/// 
		/// </summary>
		LangDiff = (IDM.Lang + 34),

		/// <summary>
		/// 
		/// </summary>
		LangProps = (IDM.Lang + 35),

		/// <summary>
		/// 
		/// </summary>
		LangPs = (IDM.Lang + 36),

		/// <summary>
		/// 
		/// </summary>
		LangRuby = (IDM.Lang + 37),

		/// <summary>
		/// 
		/// </summary>
		LangSmallTalk = (IDM.Lang + 38),

		/// <summary>
		/// 
		/// </summary>
		LangVhdl = (IDM.Lang + 39),

		/// <summary>
		/// 
		/// </summary>
		LangCaml = (IDM.Lang + 40),

		/// <summary>
		/// 
		/// </summary>
		LangKix = (IDM.Lang + 41),

		/// <summary>
		/// 
		/// </summary>
		LangAda = (IDM.Lang + 42),

		/// <summary>
		/// 
		/// </summary>
		LangVerilog = (IDM.Lang + 43),

		/// <summary>
		/// 
		/// </summary>
		LangAu3 = (IDM.Lang + 44),

		/// <summary>
		/// 
		/// </summary>
		LangMatlab = (IDM.Lang + 45),

		/// <summary>
		/// 
		/// </summary>
		LangHaskell = (IDM.Lang + 46),

		/// <summary>
		/// 
		/// </summary>
		LangInno = (IDM.Lang + 47),

		/// <summary>
		/// 
		/// </summary>
		LangCMake = (IDM.Lang + 48),

		/// <summary>
		/// 
		/// </summary>
		LangYaml = (IDM.Lang + 49),

		/// <summary>
		/// 
		/// </summary>
		LangExternal = (IDM.Lang + 50),

		/// <summary>
		/// 
		/// </summary>
		LangExternalLimit = (IDM.Lang + 79),

		/// <summary>
		/// 
		/// </summary>
		LangUser = (IDM.Lang + 80),

		/// <summary>
		/// 
		/// </summary>
		LangUserLimit = (IDM.Lang + 110),
		#endregion

		#region About
		/// <summary>
		/// 
		/// </summary>
		AboutHomePage = (IDM.About + 1),

		/// <summary>
		/// 
		/// </summary>
		AboutProjectPage = (IDM.About + 2),

		/// <summary>
		/// 
		/// </summary>
		AboutOnlineHelp = (IDM.About + 3),

		/// <summary>
		/// 
		/// </summary>
		AboutForum = (IDM.About + 4),

		/// <summary>
		/// 
		/// </summary>
		AboutPluginsHome = (IDM.About + 5),

		/// <summary>
		/// 
		/// </summary>
		AboutUpdateNpp = (IDM.About + 6),

		/// <summary>
		/// 
		/// </summary>
		AboutWikiFaq = (IDM.About + 7),

		/// <summary>
		/// 
		/// </summary>
		AboutHelp = (IDM.About + 8),
		#endregion

		#region Settings
		/// <summary>
		/// 
		/// </summary>
		SettingTabSize = (IDM.Setting + 1),

		/// <summary>
		/// 
		/// </summary>
		SettingTabReplaceSpace = (IDM.Setting + 2),

		/// <summary>
		/// 
		/// </summary>
		SettingHistorySize = (IDM.Setting + 3),

		/// <summary>
		/// 
		/// </summary>
		SettingEdgeSize = (IDM.Setting + 4),

		/// <summary>
		/// 
		/// </summary>
		SettingImportPlugin = (IDM.Setting + 5),

		/// <summary>
		/// 
		/// </summary>
		SettingImportStyleThemes = (IDM.Setting + 6),

		/// <summary>
		/// 
		/// </summary>
		SettingTrayIcon = (IDM.Setting + 8),

		/// <summary>
		/// 
		/// </summary>
		SettingShortcutMapper = (IDM.Setting + 9),

		/// <summary>
		/// 
		/// </summary>
		SettingRememberLastSession = (IDM.Setting + 10),

		/// <summary>
		/// 
		/// </summary>
		SettingPreferences = (IDM.Setting + 11),

		/// <summary>
		/// 
		/// </summary>
		SettingAutoCnbChar = (IDM.Setting + 15),
		#endregion

		#region Macro
		/// <summary>
		/// 
		/// </summary>
		MacroStartRecording = (IDM.Edit + 18),

		/// <summary>
		/// 
		/// </summary>
		MacroStopRecording = (IDM.Edit + 19),

		/// <summary>
		/// 
		/// </summary>
		MacroPLaybackRecorded = (IDM.Edit + 21),

		/// <summary>
		/// 
		/// </summary>
		MacroSaveCurrent = (IDM.Edit + 25),

		/// <summary>
		/// 
		/// </summary>
		MacroRunMultiDialog = (IDM.Edit + 32),
		#endregion
	}
}
