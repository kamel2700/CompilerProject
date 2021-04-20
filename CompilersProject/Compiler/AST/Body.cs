using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Body {
		public List<SimpleDeclaration> SimpleDeclarations;
		public List<Statement> Statements;

		public Body(SimpleDeclaration declaration, Body body) {
			SimpleDeclarations = new List<SimpleDeclaration> {declaration};
			SimpleDeclarations.AddRange(body.SimpleDeclarations);
			Statements = new List<Statement>();
			Statements.AddRange(body.Statements);
		}

		public Body(Statement statment, Body body) {
			Statements = new List<Statement> {statment};
			Statements.AddRange(body.Statements);
			SimpleDeclarations = new List<SimpleDeclaration>();
			SimpleDeclarations.AddRange(body.SimpleDeclarations);
		}

		public Body() {
			SimpleDeclarations = new List<SimpleDeclaration>();
			Statements = new List<Statement>();
		}
	}
}