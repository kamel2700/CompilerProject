using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class ProgramRoot {
		public List<RoutineDeclaration> RoutineDeclarations;
		public List<SimpleDeclaration> SimpleDeclarations;

		public ProgramRoot(SimpleDeclaration declaration, ProgramRoot programRoot) {
			SimpleDeclarations = new List<SimpleDeclaration>();
			RoutineDeclarations = new List<RoutineDeclaration>();

			SimpleDeclarations.Add(declaration);
			SimpleDeclarations.AddRange(programRoot.SimpleDeclarations);
			RoutineDeclarations.AddRange(programRoot.RoutineDeclarations);
		}

		public ProgramRoot(RoutineDeclaration declaration, ProgramRoot programRoot) {
			SimpleDeclarations = new List<SimpleDeclaration>();
			RoutineDeclarations = new List<RoutineDeclaration>();

			RoutineDeclarations.Add(declaration);
			SimpleDeclarations.AddRange(programRoot.SimpleDeclarations);
			RoutineDeclarations.AddRange(programRoot.RoutineDeclarations);
		}

		public ProgramRoot() {
			SimpleDeclarations = new List<SimpleDeclaration>();
			RoutineDeclarations = new List<RoutineDeclaration>();
		}
	}
}