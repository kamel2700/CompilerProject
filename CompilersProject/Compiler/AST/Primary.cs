using System.Collections.Generic;

namespace CompilersProject.Compiler.AST {
	public class Primary {
		public bool Bval;
		public int Ival;
		public ModifiablePrimary ModifiablePrimary;
		public double Rval;
		public int ValType;

		public Primary(string tok) {
			if (int.TryParse(tok, out Ival)) {
				ValType = 1;
				return;
			}

			if (double.TryParse(tok, out Rval)) {
				ValType = 2;
				return;
			}

			ValType = 0;
		}

		public Primary(int ival) {
			Ival = ival;
			ValType = 1;
		}

		public Primary(double rval) {
			Rval = rval;
			ValType = 2;
		}

		public Primary(bool bval) {
			Bval = bval;
			ValType = 3;
		}

		public Primary(ModifiablePrimary modifiablePrimary) {
			ModifiablePrimary = modifiablePrimary;
			ValType = 4;
		}

		public List<object> Reduce() {
			var reduced = new List<object>();
			switch (ValType) {
				case 1:
					reduced.Add(Ival);
					break;
				case 2:
					reduced.Add(Rval);
					break;
				case 3:
					reduced.Add(Bval);
					break;
				case 4:
					reduced.Add(ModifiablePrimary);
					break;
			}

			return reduced;
		}
	}
}