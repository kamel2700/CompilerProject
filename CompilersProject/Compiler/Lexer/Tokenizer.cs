using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompilersProject.Compiler.Lexer {
	internal class Tokenizer {
		private readonly BufferedStream _reader;
		private int _nLine, _nPos;
		private StringBuilder _tokenBuf;

		private TokenType _tokenType, _prevTokenType;


		public Tokenizer(Stream stream) {
			_reader = new BufferedStream(stream);
			Tokens = new List<RawToken>();
		}

		public List<RawToken> Tokens { get; }

		private void ParseByte(int b) {
			var read = (char) b;
			if (read == ' ') {
				FlushTokenBuf(_tokenBuf);
				_nPos++;
				return;
			}

			if (read == '\n') {
				FlushTokenBuf(_tokenBuf);
				_nLine++;
				_nPos = 1;
				return;
			}

			if (char.IsLetter(read) || read == '_') {
				_tokenType = TokenType.Word;

				if (_prevTokenType != TokenType.Word) FlushTokenBuf(_tokenBuf);

				_tokenBuf.Append(read);
				return;
			}

			if (char.IsDigit(read)) {
				switch (_prevTokenType) {
					case TokenType.Word:
					case TokenType.Number:
						_tokenBuf.Append(read);
						break;
					case TokenType.Dash:
						_tokenType = TokenType.Number;
						_tokenBuf.Append(read);
						break;
					default:
						_tokenType = TokenType.Number;
						FlushTokenBuf(_tokenBuf);
						_tokenBuf.Append(read);
						break;
				}

				return;
			}

			if (read == '-') {
				_tokenType = TokenType.Dash;
				FlushTokenBuf(_tokenBuf);
				_tokenBuf.Append(read);
				return;
			}

			if (read == ':') {
				_tokenType = TokenType.Colon;
				FlushTokenBuf(_tokenBuf);
				_tokenBuf.Append(read);
				return;
			}

			if (read == '=') {
				_tokenType = TokenType.Eq;
				if (_prevTokenType == TokenType.Colon) {
					_tokenType = TokenType.Assign;
					_tokenBuf.Append(read);
					FlushTokenBuf(_tokenBuf);
				} else if (_prevTokenType == TokenType.Lt) {
					_tokenType = TokenType.Le;
					_tokenBuf.Append(read);
					FlushTokenBuf(_tokenBuf);
				} else if (_prevTokenType == TokenType.Gt) {
					_tokenType = TokenType.Ge;
					_tokenBuf.Append(read);
					FlushTokenBuf(_tokenBuf);
				} else if (_prevTokenType == TokenType.Slash) {
					_tokenType = TokenType.Ne;
					_tokenBuf.Append(read);
					FlushTokenBuf(_tokenBuf);
				} else {
					FlushTokenBuf(_tokenBuf);
					AddToken(read);
				}

				return;
			}

			if (read == '<') {
				_tokenType = TokenType.Lt;
				FlushTokenBuf(_tokenBuf);
				_tokenBuf.Append(read);
				return;
			}

			if (read == '>') {
				_tokenType = TokenType.Gt;
				FlushTokenBuf(_tokenBuf);
				_tokenBuf.Append(read);
				return;
			}

			if (read == '/') {
				_tokenType = TokenType.Slash;
				FlushTokenBuf(_tokenBuf);
				_tokenBuf.Append(read);
				return;
			}

			if (read == '.') {
				_tokenType = TokenType.Period;
				if (_prevTokenType == TokenType.Period) {
					_tokenType = TokenType.Range;
					_tokenBuf.Append(read);
					FlushTokenBuf(_tokenBuf);
				} else if (_prevTokenType == TokenType.Number) {
					if (_tokenBuf[^1] == '.') {
						_tokenBuf.Remove(_tokenBuf.Length - 1, 1);
						FlushTokenBuf(_tokenBuf);
						AddToken("..");
						_tokenType = TokenType.Range;
					} else {
						_tokenBuf.Append(read);
						_tokenType = TokenType.Number;
					}
				} else {
					FlushTokenBuf(_tokenBuf);
					_tokenBuf.Append(read);
				}

				return;
			}

			if (";,()[]{}+*%".Contains(read)) {
				_tokenType = TokenType.Other;
				FlushTokenBuf(_tokenBuf);
				AddToken(read);
			}
		}

		public void Tokenize() {
			var cursor = _reader.ReadByte();
			_tokenBuf = new StringBuilder();
			_tokenType = TokenType.Word;
			_prevTokenType = TokenType.Word;
			_nLine = 1;
			_nPos = 1;

			while (cursor != -1) {
				do {
					ParseByte(cursor);
				} while (false);

				cursor = _reader.ReadByte();
				_prevTokenType = _tokenType;
			}

			FlushTokenBuf(_tokenBuf);
		}

		private void FlushTokenBuf(StringBuilder tokenBuf) {
			if (tokenBuf.Length > 0) {
				Tokens.Add(new RawToken(tokenBuf.ToString(), _nLine, _nPos));
				_nPos += tokenBuf.Length;
				tokenBuf.Clear();
			}
		}

		private void AddToken(char val) {
			Tokens.Add(new RawToken(val, _nLine, _nPos));
			_nPos++;
		}

		private void AddToken(string val) {
			Tokens.Add(new RawToken(val, _nLine, _nPos));
			_nPos += val.Length;
		}
	}
}