namespace CompilersProject.Compiler.AST {
	public class Assignment {
		public Expression Expression;
		public ModifiablePrimary ModifiablePrimary;

		public Assignment(ModifiablePrimary modifiablePrimary, Expression expression) {
			ModifiablePrimary = modifiablePrimary;
			Expression = expression;
		}
	}
}