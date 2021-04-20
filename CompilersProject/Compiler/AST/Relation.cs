using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Relation {
		public Operation Operation;
		public Simple Simple1, Simple2;

		public Relation(Simple simple1) {
			Simple1 = simple1;
		}

		public Relation(Simple simple1, Operation operation, Simple simple2) {
			Simple1 = simple1;
			Operation = operation;
			Simple2 = simple2;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Simple1.Reduce());
			if (Simple2 == null) return reduced;

			reduced.AddRange(Simple2.Reduce());
			reduced.Add(Operation);
			return reduced;
		}
	}
}