using System.Collections.Generic;
using System.IO;
using CompilersProject.Compiler.Lexer;
using gppg;

namespace CompilersProject.Compiler {
	public class YyScanner : IScanner<ValueType, LexLocation> {
		private readonly Tokenizer _tokenizer;
		private bool _initialized;
		private List<RawToken>.Enumerator _tokenIter;

		private Dictionary<string, int> _tokenMap;

		public YyScanner(Stream inputStream) {
			_tokenizer = new Tokenizer(inputStream);
			_initialized = false;
		}

		public override int yylex() {
			if (!_initialized) Initialize();

			if (!_tokenIter.MoveNext()) return 0;

			var raw = _tokenIter.Current;
			yylloc = new LexLocation(raw.Line, raw.Pos, raw.Line, raw.Pos + raw.Val.Length);

			double dval;
			int ival;

			try {
				var tok = _tokenMap[raw.Val];
				return tok;
			} catch (KeyNotFoundException) {
				if (double.TryParse(raw.Val, out dval))
					return _tokenMap["rval"];
				if (int.TryParse(raw.Val, out ival))
					return _tokenMap["ival"];
				return _tokenMap["id"];
			}
		}

		public RawToken TestLex() {
			if (!_initialized) Initialize();

			return _tokenIter.MoveNext() ? _tokenIter.Current : null;
		}

		public void Initialize() {
			_tokenizer.Tokenize();
			_tokenIter = _tokenizer.Tokens.GetEnumerator();
			FillMap();
			_initialized = true;
		}

		private void FillMap() {
			_tokenMap = new Dictionary<string, int>();
			const int shift = 3;
			var tokens =
				"id ival rval bval { } ( ) [ ] , . ; : .. := < > = <= >= /= + - * / % var is type xor or and in for while reverse integer real boolean true false record routine array if then else end loop"
					.Split(' ');
			for (var i = 0; i < tokens.Length; i++) _tokenMap.Add(tokens[i], i + shift);
		}
	}
}