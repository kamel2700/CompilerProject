using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class RelationRec {
		public Operation Operation;
		public RelationRec Rec;
		public Relation Relation;

		public RelationRec(Operation operation, Relation relation) {
			Operation = operation;
			Relation = relation;
		}

		public RelationRec(Operation operation, Relation relation, RelationRec relationRec) {
			Operation = operation;
			Relation = relation;
			Rec = relationRec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Relation.Reduce());
			reduced.Add(Operation);
			if (Rec != null) reduced.AddRange(Rec.Reduce());

			return reduced;
		}
	}
}