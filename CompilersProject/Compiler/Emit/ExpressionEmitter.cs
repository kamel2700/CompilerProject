using System.Collections.Generic;
using System.Text;
using CompilersProject.Compiler.AST;

namespace CompilersProject.Compiler.Emit {
	public class ExpressionEmitter {
		private readonly Expression _expression;
		private readonly string _varName;

		public ExpressionEmitter(Expression expression, string varName) {
			_expression = expression;
			_varName = varName;
		}

		public string GenerateCode() {
			return GenerateCode(new Dictionary<string, int>());
		}

		public List<string> GenerateCodeList() {
			return GenerateCodeList(new Dictionary<string, int>());
		}

		public string GenerateCode(Dictionary<string, int> parameters) {
			var code = GenerateCodeList(parameters);

			if (code == null) return null;

			var builder = new StringBuilder();

			foreach (var s in code) {
				builder.Append("    ");
				builder.Append(s);
				builder.Append('\n');
			}

			return builder.ToString();
		}

		public List<string> GenerateCodeList(Dictionary<string, int> parameters) {
			var chain = _expression.Reduce();
			var code = new List<string>();

			foreach (var o in chain)
				switch (o) {
					case int _:
						code.Add("ldc.i4.s " + o);
						continue;
					case double _:
						code.Add("ldc.r8 " + o);
						continue;
					case bool b:
						code.Add(b ? "ldc.i4.1" : "ldc.i4.0");
						continue;
					case ModifiablePrimary primary:
						if (parameters.ContainsKey(primary.Identifier))
							code.Add("ldarg.s " + primary.Identifier);
						else
							code.Add("ldloc.s " + primary.Identifier);

						continue;
					case Operation operation:
						switch (operation) {
							case Operation.Add:
								if (!CheckNumbers(code)) return null;
								code.Add("add");
								break;
							case Operation.Sub:
								if (!CheckNumbers(code)) return null;
								code.Add("sub");
								break;
							case Operation.Mul:
								if (!CheckNumbers(code)) return null;
								code.Add("mul");
								break;
							case Operation.Div:
								if (!CheckNumbers(code)) return null;
								code.Add("div");
								break;
							case Operation.Mod:
								if (!CheckNumbers(code)) return null;
								code.Add("rem");
								break;
							case Operation.Gt:
								if (!CheckNumbers(code)) return null;
								code.Add("cgt");
								break;
							case Operation.Lt:
								if (!CheckNumbers(code)) return null;
								code.Add("clt");
								break;
							case Operation.Eq:
								if (!CheckNumbers(code)) return null;
								code.Add("ceq");
								break;
							case Operation.Ge:
								if (!CheckNumbers(code)) return null;
								code.Add("clt");
								code.Add("ldc.i4.0");
								code.Add("ceq");
								break;
							case Operation.Le:
								if (!CheckNumbers(code)) return null;
								code.Add("cgt");
								code.Add("ldc.i4.0");
								code.Add("ceq");
								break;
							case Operation.Ne:
								if (!CheckNumbers(code)) return null;
								code.Add("ceq");
								code.Add("ldc.i4.0");
								code.Add("ceq");
								break;
							case Operation.And:
								if (!CheckBool(code)) return null;
								code.Add("and");
								break;
							case Operation.Or:
								if (!CheckBool(code)) return null;
								code.Add("or");
								break;
							case Operation.Xor:
								if (!CheckBool(code)) return null;
								code.Add("xor");
								break;
						}

						break;
				}

			if (parameters.ContainsKey(_varName))
				code.Add("starg.s " + _varName);
			else
				code.Add("stloc.s " + _varName);

			return code;
		}

		private bool CheckNumbers(List<string> code) {
			if (!(code[^1].Contains(".i4.") || code[^1].Contains(".r8"))) {
//				return false;
			}

			if (!(code[^2].Contains(".i4.") || code[^2].Contains(".r8"))) {
//				return false;
			}

			if (code[^2].Contains(".r8") && !code[^1].Contains(".r8")) {
				code.Add("conv.r8");
				return true;
			}

			if (!code[^2].Contains(".r8") && code[^1].Contains(".r8")) {
				code.Add(code[^1]);
				code[^2] = "conv.r8";
				return true;
			}

			return true;
		}

		private bool CheckBool(List<string> code) {
			return (code[^1][^1] == '0' || code[^1][^1] == '1') &&
			       (code[^2][^1] == '0' || code[^2][^1] == '1') || true;
		}
	}
}