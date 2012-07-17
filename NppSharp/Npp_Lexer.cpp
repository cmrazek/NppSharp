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

#include "StdAfx.h"
#include "NppInterface.h"
#include "LexerWrapper.h"
#include "Main.h"

using namespace System::IO;
using namespace System::Xml;
using namespace System::Text;
using namespace System::Globalization;

namespace NppSharp
{
	int NppInterface::AddLexer(Type^ lexerType, String^ name, String^ description,
		String^ blockCommentStart, String^ blockCommentEnd, String^ lineComment)
	{
		try
		{
			// Add the lexer.
			LexerInfo^ info = gcnew LexerInfo();
			info->type = lexerType;
			info->name = name;
			info->description = description;
			info->blockCommentStart = blockCommentStart;
			info->blockCommentEnd = blockCommentEnd;
			info->lineComment = lineComment;

			info->instance = (ILexer^)Activator::CreateInstance(info->type);

			// Set the styles
			int styleIndex = 0;
			info->styles = gcnew List<LexerStyle^>();
			for each (LexerStyle^ style in info->instance->Styles)
			{
				info->styles->Add(style);
				style->Index = styleIndex++;
			}

			_lexers->Add(info);
			return _lexers->Count - 1;
		}
		catch (Exception^ ex)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, ex->ToString());
			return -1;
		}
	}

	void NppInterface::RefreshCustomLexers()
	{
		LexerWrapper::RefreshAllLexers();
	}

	int NppInterface::GetLexerCount()
	{
		return _lexers->Count;
	}

	String^ NppInterface::GetLexerName(int index)
	{
		return _lexers[index]->name;
	}

	String^ NppInterface::GetLexerDescription(int index)
	{
		return _lexers[index]->description;
	}

	npp::ILexer* NppInterface::GetLexer(int index)
	{
		try
		{
			return new LexerWrapper(_lexers[index]->instance);
		}
		catch (Exception^ ex)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, ex->ToString());
			return NULL;
		}
	}

	void NppInterface::GenerateLexerConfigFile()
	{
		if (_lexerFileCreated) return;

		LoadOldLexerConfigFile();

		// Create new file.
		String^ str;
		MemoryStream^ memStream = gcnew MemoryStream();
		
		XmlWriterSettings^ xmlSettings = gcnew XmlWriterSettings();
		xmlSettings->Encoding = Encoding::GetEncoding(1252);
		xmlSettings->Indent = true;

		XmlWriter^ xml = XmlWriter::Create(memStream, xmlSettings);
		xml->WriteStartDocument();
		xml->WriteStartElement("NotepadPlus");

		if (_lexers->Count > 0)
		{
			xml->WriteStartElement("Languages");

			int lexerIndex = 0;
			for each (LexerInfo^ info in _lexers)
			{
				ILexer^ lexer = info->instance;

				xml->WriteStartElement("Language");

				String^ lexerName = info->name;
				xml->WriteAttributeString("name", lexerName);
				xml->WriteAttributeString("ext", BuildExtensionList(lexer->Extensions));

				if (!String::IsNullOrEmpty(info->blockCommentStart)) xml->WriteAttributeString("commentStart", info->blockCommentStart);
				if (!String::IsNullOrEmpty(info->blockCommentEnd)) xml->WriteAttributeString("commentEnd", info->blockCommentEnd);
				if (!String::IsNullOrEmpty(info->lineComment)) xml->WriteAttributeString("commentLine", info->lineComment);

				xml->WriteEndElement();	// Language
				lexerIndex++;
			}
			xml->WriteEndElement();	// Languages

			xml->WriteStartElement("LexerStyles");

			lexerIndex = 0;
			for each (LexerInfo^ info in _lexers)
			{
				ILexer^ lexer = info->instance;

				xml->WriteStartElement("LexerType");

				str = info->name;
				if (String::IsNullOrEmpty(str)) str = String::Format("Language {0}", lexerIndex);
				xml->WriteAttributeString("name", str);

				xml->WriteAttributeString("desc", info->description);
				xml->WriteAttributeString("ext", info->addExt);

				int styleIndex = 0;
				for each (LexerStyle^ style in info->styles)
				{
					xml->WriteStartElement("WordsStyle");

					str = style->Name;
					if (String::IsNullOrEmpty(str)) str = String::Format("Style {0}", styleIndex);
					xml->WriteAttributeString("name", str);

					xml->WriteAttributeString("styleID", styleIndex.ToString());

					// fgColor
					Color color = style->ForeColor;
					if (color == Color::Transparent) color = Color::Black;
					xml->WriteAttributeString("fgColor", ColorToWebHex(color));

					// bgColor
					color = style->BackColor;
					if (color == Color::Transparent) color = Color::White;
					xml->WriteAttributeString("bgColor", ColorToWebHex(color));

					xml->WriteAttributeString("fontName", style->FontName);
					xml->WriteAttributeString("fontStyle", ((int)style->FontStyle).ToString());
					xml->WriteAttributeString("fontSize", style->FontSize <= 0 ? String::Empty : style->FontSize.ToString());

					xml->WriteEndElement();	// WordsStyle
					styleIndex++;
				}

				xml->WriteEndElement();	// LexerType
				lexerIndex++;
			}
			xml->WriteEndElement();	// LexerStyles
		}

		xml->WriteEndElement();	// NotepadPlus
		xml->WriteEndDocument();
		xml->Close();

		memStream->Seek(0, SeekOrigin::Begin);
		array<byte>^ buf = gcnew array<byte>((int)memStream->Length);
		memStream->Read(buf, 0, (int)memStream->Length);

		String^ configDir = ConfigDir;
		String^ fileName = Path::Combine(ConfigDir, String::Concat(PLUGIN_NAME, ".xml"));
		File::WriteAllBytes(fileName, buf);

		_lexerFileCreated = true;
	}

	void NppInterface::LoadOldLexerConfigFile()
	{
		// Loads the existing config file so any user's customizations are not lost.

		try
		{
			String^ str;

			// Load the file
			String^ configDir = ConfigDir;
			String^ fileName = Path::Combine(ConfigDir, String::Concat(PLUGIN_NAME, ".xml"));
			if (!File::Exists(fileName)) return;

			XmlDocument^ xmlDoc = gcnew XmlDocument();
			xmlDoc->Load(fileName);

			// Load lexer styles
			for each (XmlNode^ lexerTypeNode in xmlDoc->SelectNodes("/NotepadPlus/LexerStyles/LexerType"))
			{
				if (lexerTypeNode->NodeType != XmlNodeType::Element) continue;
				XmlElement^ lexerTypeElement = (XmlElement^)lexerTypeNode;

				String^ langName = lexerTypeElement->GetAttribute("name");
				for each (LexerInfo^ info in _lexers)
				{
					if (!info->name->Equals(langName)) continue;

					String^ ext = lexerTypeElement->GetAttribute("ext");
					if (!String::IsNullOrEmpty(ext)) info->addExt = ext;

					for each (XmlNode^ styleNode in lexerTypeElement->SelectNodes("WordsStyle"))
					{
						if (styleNode->NodeType != XmlNodeType::Element) continue;
						XmlElement^ styleElement = (XmlElement^)styleNode;

						String^ styleName = styleElement->GetAttribute("name");
						if (String::IsNullOrEmpty(styleName)) continue;

						for each (LexerStyle^ style in info->styles)
						{
							if (!style->Name->Equals(styleName)) continue;

							if (styleElement->HasAttribute("fgColor"))
							{
								str = styleElement->GetAttribute("fgColor");
								Color foreColor = WebHexToColor(str);
								if (foreColor != Color::Transparent) style->ForeColor = foreColor;
							}

							if (styleElement->HasAttribute("bgColor"))
							{
								str = styleElement->GetAttribute("bgColor");
								Color backColor = WebHexToColor(str);
								if (backColor != Color::Transparent) style->BackColor = backColor;
							}

							if (styleElement->HasAttribute("fontName"))
							{
								str = styleElement->GetAttribute("fontName");
								if (str == nullptr) str = String::Empty;
								style->FontName = str;
							}

							if (styleElement->HasAttribute("fontStyle"))
							{
								if (!String::IsNullOrEmpty(str = styleElement->GetAttribute("fontStyle")))
								{
									int fontStyle = 0;
									if (Int32::TryParse(str, fontStyle) && fontStyle >= 0) style->FontStyle = (System::Drawing::FontStyle)fontStyle;
								}
							}

							if (styleElement->HasAttribute("fontSize"))
							{
								if (!String::IsNullOrEmpty(str = styleElement->GetAttribute("fontSize")))
								{
									int fontSize = 0;
									if (Int32::TryParse(str, fontSize) && fontSize > 0) style->FontSize = fontSize;
								}
							}

							break;
						}
					}

					break;
				}
			}
		}
		catch (Exception^ ex)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, ex->ToString());
		}
	}

	String^ NppInterface::CleanLexerExtension(String^ ext)
	{
		ext = ext->Trim()->ToLower();
		if (ext->StartsWith(".")) ext = ext->Substring(1);
		return ext;
	}

	String^ NppInterface::BuildExtensionList(IEnumerable<String^>^ extList)
	{
		StringBuilder^ sb = gcnew StringBuilder();

		for each (String^ ext in extList)
		{
			String^ e = CleanLexerExtension(ext);
			if (!String::IsNullOrEmpty(e))
			{
				if (sb->Length > 0) sb->Append(" ");
				sb->Append(e);
			}
		}

		return sb->ToString();
	}

	String^ NppInterface::ColorToWebHex(Color color)
	{
		return String::Concat(color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
	}

	Color NppInterface::WebHexToColor(String^ str)
	{
		if (String::IsNullOrEmpty(str)) return Color::Transparent;

		int val;
		if (!Int32::TryParse(str, NumberStyles::HexNumber, CultureInfo::CurrentCulture, val)) return Color::Transparent;

		return Color::FromArgb((val >> 16) & 0xff, (val >> 8) & 0xff, val & 0xff);
	}
}
