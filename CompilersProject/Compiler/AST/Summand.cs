using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Summand {
		public Factor Factor;
		public FactorRec FactorRec;

		public Summand(Factor factor) {
			Factor = factor;
		}

		public Summand(Factor factor, FactorRec factorRec) {
			Factor = factor;
			FactorRec = factorRec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Factor.Reduce());
			if (FactorRec != null) reduced.AddRange(FactorRec.Reduce());

			return reduced;
		}
	}
}