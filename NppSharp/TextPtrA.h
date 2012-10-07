// NppSharp - C#/.NET Scripting Plugin for Notepad++
// Copyright (C) 2012  Chris Mrazek
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
