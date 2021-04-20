namespace CompilersProject.Compiler.AST {
	public class RoutineDeclaration {
		public Body Body;
		public string Identifier;
		public Parameters Parameters;
		public VarType VarType;

		public RoutineDeclaration(string identifier, Parameters parameters, Body body) {
			Identifier = identifier;
			Parameters = parameters;
			Body = body;
		}

		public RoutineDeclaration(string identifier, Parameters parameters, VarType varType, Body body) {
			Identifier = identifier;
			Parameters = parameters;
			VarType = varType;
			Body = body;
		}
	}
}