using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Expression {
		public Relation Relation;
		public RelationRec RelationRec;

		public Expression(Relation relation) {
			Relation = relation;
		}

		public Expression(Relation relation, RelationRec relationRec) {
			Relation = relation;
			RelationRec = relationRec;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			reduced.AddRange(Relation.Reduce());
			if (RelationRec != null) reduced.AddRange(RelationRec.Reduce());

			return reduced;
		}
	}
}