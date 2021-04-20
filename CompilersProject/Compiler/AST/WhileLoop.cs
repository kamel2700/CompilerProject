namespace CompilersProject.Compiler.AST {
	public class WhileLoop {
		public Body Body;
		public Expression Expression;

		public WhileLoop(Expression expression, Body body) {
			Expression = expression;
			Body = body;
		}
	}
}