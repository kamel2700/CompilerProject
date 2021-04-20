namespace CompilersProject.Compiler.Lexer {
	public class RawToken {
		public readonly int Line;
		public readonly int Pos;
		public readonly string Val;

		public RawToken(string val, int line, int pos) {
			Val = val;
			Line = line;
			Pos = pos;
		}

		public RawToken(char val, int line, int pos) {
			Val = val.ToString();
			Line = line;
			Pos = pos;
		}

		public override string ToString() {
			return $"[{Line}|{Pos}] {Val}";
		}
	}
}