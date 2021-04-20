namespace CompilersProject.Compiler.AST {
	public class ForLoop {
		public Body Body;
		public ForRange ForRange;
		public string Identifier;

		public ForLoop(string identifier, ForRange forRange, Body body) {
			Identifier = identifier;
			ForRange = forRange;
			Body = body;
		}
	}
}