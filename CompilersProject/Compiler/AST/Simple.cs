using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Simple {
		public Summand Summand;
		public SummandRec SummandRec;

		public Simple(Summand summand) {
			Summand = summand;
		}

		public Simple(Summand summand, SummandRec summandRec) {
			Summand = summand;
			SummandRec = summandRec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Summand.Reduce());
			if (SummandRec != null) reduced.AddRange(SummandRec.Reduce());

			return reduced;
		}
	}
}