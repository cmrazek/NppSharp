#pragma once

#include <vcclr.h>

namespace NppSharp
{
	class LexerWrapper : public npp::ILexer
	{
	public:
		LexerWrapper(NppSharp::ILexer^ clrLexer);
		virtual ~LexerWrapper();

		virtual int SCI_METHOD Version() const;
		virtual void SCI_METHOD Release();
		virtual const char * SCI_METHOD PropertyNames();
		virtual int SCI_METHOD PropertyType(const char *name);
		virtual const char * SCI_METHOD DescribeProperty(const char *name);
		virtual int SCI_METHOD PropertySet(const char *key, const char *val);
		virtual const char * SCI_METHOD DescribeWordListSets();
		virtual int SCI_METHOD WordListSet(int n, const char *wl);
		virtual void SCI_METHOD Lex(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess);
		virtual void SCI_METHOD Fold(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess);
		virtual void * SCI_METHOD PrivateCall(int operation, void *pointer);

		NppSharp::ILexer^	GetLexer() { return _clrLexer; }
		void				Refresh();

		static void	RefreshAllLexers();

	private:
		gcroot<NppSharp::ILexer^>	_clrLexer;
		npp::IDocument*				_doc;

		static std::list<LexerWrapper*>	_activeLexers;
	};

	ref class LexerInfo
	{
	public:
		Type^				type;
		String^				name;
		String^				description;
		NppSharp::ILexer^	instance;
		String^				blockCommentStart;
		String^				blockCommentEnd;
		String^				lineComment;

		// Config file customizations
		String^				addExt;
		List<LexerStyle^>^	styles;
	};
}
