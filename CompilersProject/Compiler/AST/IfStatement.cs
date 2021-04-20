namespace CompilersProject.Compiler.AST {
	public class IfStatement {
		public Body Body, ElseBody;
		public Expression Expression;

		public IfStatement(Expression expression, Body body) {
			Expression = expression;
			Body = body;
		}

		public IfStatement(Expression expression, Body body, Body elseBody) {
			Expression = expression;
			Body = body;
			ElseBody = elseBody;
		}
	}
}