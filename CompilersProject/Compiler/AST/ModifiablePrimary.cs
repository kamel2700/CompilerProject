namespace CompilersProject.Compiler.AST {
	public class ModifiablePrimary {
		public string Identifier;
		public ModifiablePrimaryRec Rec;

		public ModifiablePrimary(string identifier) {
			Identifier = identifier;
		}

		public ModifiablePrimary(string identifier, ModifiablePrimaryRec rec) {
			Identifier = identifier;
			Rec = rec;
		}
	}
}