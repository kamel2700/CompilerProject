namespace CompilersProject.Compiler.AST {
	public class VariableDeclaration {
		public Expression Expression;
		public string Identifier;
		public VarType VarType;

		public VariableDeclaration(string identifier, VarType varType) {
			Identifier = identifier;
			VarType = varType;
		}

		public VariableDeclaration(string identifier, VarType varType, Expression expression) {
			Identifier = identifier;
			VarType = varType;
			Expression = expression;
		}

		public VariableDeclaration(string identifier, Expression expression) {
			Identifier = identifier;
			Expression = expression;
		}
	}
}