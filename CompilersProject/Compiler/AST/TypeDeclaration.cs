namespace CompilersProject.Compiler.AST {
	public class TypeDeclaration {
		public string Identifier;
		public VarType VarType;

		public TypeDeclaration(string identifier, VarType varType) {
			Identifier = identifier;
			VarType = varType;
		}
	}
}