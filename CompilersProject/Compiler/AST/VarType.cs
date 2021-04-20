namespace CompilersProject.Compiler.AST {
	public class VarType {
		public ArrayType ArrayType;
		private string Identifier;
		public PrimitiveType PrimitiveType;
		public RecordType RecordType;

		public VarType(PrimitiveType type) {
			PrimitiveType = type;
		}

		public VarType(ArrayType arrayType) {
			ArrayType = arrayType;
		}

		public VarType(RecordType recordType) {
			RecordType = recordType;
		}

		public VarType(string identifier) {
			Identifier = identifier;
		}
	}
}