namespace CompilersProject.Compiler.AST {
	public class SimpleDeclaration {
		public TypeDeclaration TypeDeclaration;
		public VariableDeclaration VariableDeclaration;

		public SimpleDeclaration(VariableDeclaration declaration) {
			VariableDeclaration = declaration;
		}

		public SimpleDeclaration(TypeDeclaration declaration) {
			TypeDeclaration = declaration;
		}
	}
}