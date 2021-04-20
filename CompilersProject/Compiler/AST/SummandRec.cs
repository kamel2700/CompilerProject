using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class SummandRec {
		public Operation Operation;
		public SummandRec Rec;
		public Summand Summand;

		public SummandRec(Operation operation, Summand summand) {
			Operation = operation;
			Summand = summand;
		}

		public SummandRec(Operation operation, Summand summand, SummandRec rec) {
			Operation = operation;
			Summand = summand;
			Rec = rec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Summand.Reduce());
			reduced.Add(Operation);

			if (Rec == null) return reduced;
			reduced.AddRange(Rec.Reduce());

			return reduced;
		}
	}
}