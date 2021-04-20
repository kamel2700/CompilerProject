using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class RoutineCall {
		public List<Expression> Expressions;
		public string Identifier;

		public RoutineCall(string identifier) {
			Identifier = identifier;
			Expressions = new List<Expression>();
		}

		public RoutineCall(string identifier, Expression expression) {
			Identifier = identifier;
			Expressions = new List<Expression> {expression};
		}

		public RoutineCall(string identifier, Expression expression, ExpressionRec expressionRec) {
			Identifier = identifier;
			Expressions = new List<Expression> {expression};
			Expressions.AddRange(expressionRec.Expressions);
		}
	}
}