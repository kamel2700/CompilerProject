using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Parameters {
		public List<ParameterDeclaration> ParameterDeclarations;

		public Parameters(ParameterDeclaration declaration) {
			ParameterDeclarations = new List<ParameterDeclaration> {declaration};
		}

		public Parameters(ParameterDeclaration declaration, Parameters parameters) {
			ParameterDeclarations = new List<ParameterDeclaration> {declaration};
			ParameterDeclarations.AddRange(parameters.ParameterDeclarations);
		}

		public Parameters() {
			ParameterDeclarations = new List<ParameterDeclaration>();
		}
	}
}