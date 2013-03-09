#pragma once

namespace NppSharp
{
	public ref class TextPtrA : public TextPtr
	{
	public:
		TextPtrA(const char* ptr, int length, int codePage);

		virtual property String^ Text { String^ get() override; }

	private:
		const char*	_ptr;
		int			_length;
		int			_codePage;
		String^		_str;
	};
}
