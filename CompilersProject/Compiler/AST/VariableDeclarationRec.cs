using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class VariableDeclarationRec {
		public List<VariableDeclaration> VariableDeclarations;

		public VariableDeclarationRec(VariableDeclaration declaration) {
			VariableDeclarations = new List<VariableDeclaration> {declaration};
		}

		public VariableDeclarationRec(VariableDeclaration declaration, VariableDeclarationRec rec) {
			VariableDeclarations = new List<VariableDeclaration> {declaration};
			VariableDeclarations.AddRange(rec.VariableDeclarations);
		}
	}
}