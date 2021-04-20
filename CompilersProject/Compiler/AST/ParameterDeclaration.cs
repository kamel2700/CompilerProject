namespace CompilersProject.Compiler.AST {
	public class ParameterDeclaration {
		public string Identifier, TypeIdentifier;
		public VarType Type;

		public ParameterDeclaration(string identifier, string typeIdentifier) {
			TypeIdentifier = typeIdentifier;
			Type = new VarType(typeIdentifier);
			Identifier = identifier;
		}

		public ParameterDeclaration(string identifier, PrimitiveType type) {
			Identifier = identifier;
			Type = new VarType(type);
		}
	}
}