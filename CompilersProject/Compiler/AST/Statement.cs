namespace CompilersProject.Compiler.AST {
	public class Statement {
		public Assignment Assignment;
		public ForLoop ForLoop;
		public IfStatement IfStatement;
		public RoutineCall RoutineCall;
		public WhileLoop WhileLoop;

		public Statement(Assignment assignment) {
			Assignment = assignment;
		}

		public Statement(RoutineCall routineCall) {
			RoutineCall = routineCall;
		}

		public Statement(WhileLoop whileLoop) {
			WhileLoop = whileLoop;
		}

		public Statement(ForLoop forLoop) {
			ForLoop = forLoop;
		}

		public Statement(IfStatement ifStatement) {
			IfStatement = ifStatement;
		}
	}
}