using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class ExpressionRec {
		public List<Expression> Expressions;

		public ExpressionRec(Expression expression) {
			Expressions = new List<Expression> {expression};
		}

		public ExpressionRec(Expression expression, ExpressionRec expressionRec) {
			Expressions = new List<Expression> {expression};
			Expressions.AddRange(expressionRec.Expressions);
		}
	}
}