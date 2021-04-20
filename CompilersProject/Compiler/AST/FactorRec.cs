using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class FactorRec {
		public Factor Factor;
		public Operation Operation;
		public FactorRec Rec;

		public FactorRec(Operation operation, Factor factor) {
			Operation = operation;
			Factor = factor;
		}

		public FactorRec(Operation operation, Factor factor, FactorRec rec) {
			Operation = operation;
			Factor = factor;
			Rec = rec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Factor.Reduce());
			reduced.Add(Operation);

			if (Rec == null) return reduced;
			reduced.AddRange(Rec.Reduce());

			return reduced;
		}
	}
}