namespace CompilersProject.Compiler.AST {
	public class ModifiablePrimaryRec {
		public Expression Expression;
		public string Identifier;
		public ModifiablePrimaryRec Rec;

		public ModifiablePrimaryRec(string identifier) {
			Identifier = identifier;
		}

		public ModifiablePrimaryRec(Expression expression) {
			Expression = expression;
		}

		public ModifiablePrimaryRec(string identifier, ModifiablePrimaryRec rec) {
			Identifier = identifier;
			Rec = rec;
		}

		public ModifiablePrimaryRec(Expression expression, ModifiablePrimaryRec rec) {
			Expression = expression;
			Rec = rec;
		}
	}
}