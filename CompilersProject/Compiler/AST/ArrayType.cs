namespace CompilersProject.Compiler.AST {
	public class ArrayType {
		public Expression Expression;
		public VarType VarType;

		public ArrayType(VarType varType) {
			VarType = varType;
		}

		public ArrayType(Expression expression, VarType varType) {
			Expression = expression;
			VarType = varType;
		}
	}
}