namespace CompilersProject.Compiler.AST {
	public class ForRange {
		public Expression Expression1, Expression2;
		public bool Reverse;

		public ForRange(Expression expression1, Expression expression2) {
			Expression1 = expression1;
			Expression2 = expression2;
			Reverse = false;
		}

		public ForRange(Expression expression1, Expression expression2, bool reverse) {
			Reverse = reverse;
			Expression1 = expression1;
			Expression2 = expression2;
		}
	}
}