using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Factor {
		public Expression Expression;
		public Primary Primary;

		public Factor(Primary primary) {
			Primary = primary;
		}

		public Factor(Expression expression) {
			Expression = expression;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();

			if (Primary != null)
				reduced.AddRange(Primary.Reduce());
			else if (Expression != null) reduced.AddRange(Expression.Reduce());

			return reduced;
		}
	}
}