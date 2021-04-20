using System.Collections.Generic;
using System.IO;
using CompilersProject.Compiler.AST;
using CompilersProject.Compiler.Emit;

namespace CompilersProject.Compiler {
	public class MainProgram {
		public static bool errors;
		public static Dictionary<string, int> dict;
		public static int index;
		public static int indexLine;

		public static string ExpressionResult(Expression exp, string result) {
			var emitter = new ExpressionEmitter(exp, result);
			var emitterG = emitter.GenerateCode(dict);
			if (emitterG == null) {
				errors = true;
				return "";
			}

			return emitterG;
		}

		public static List<KeyValuePair<string, string>> VariableList(Body body) {
			if (body == null)
				return new List<KeyValuePair<string, string>>();

			var dec = body.SimpleDeclarations;
			var list = new List<KeyValuePair<string, string>>();

			for (var i = 0; i < dec.Count; i++)
				if (dec[i].VariableDeclaration != null && dec[i].VariableDeclaration.VarType != null) {
					if (dec[i].VariableDeclaration.VarType.ArrayType != null)
						list.Add(new KeyValuePair<string, string>(dec[i].VariableDeclaration.Identifier,
							"class [mscorlib]System.Array[]"));
					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Boolean)
						list.Add(new KeyValuePair<string, string>(dec[i].VariableDeclaration.Identifier, "bool"));
					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Int)
						list.Add(new KeyValuePair<string, string>(dec[i].VariableDeclaration.Identifier, "int32"));
					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Real)
						list.Add(new KeyValuePair<string, string>(dec[i].VariableDeclaration.Identifier, "float64"));
				}

			for (var j = 0; j < list.Count; j++)
			for (var k = 0; k < list.Count; k++) {
				if (j == k)
					continue;
				if (list[j].Key.Equals(list[k].Key))
					errors = true;
			}

			List<KeyValuePair<string, string>> list2;
			var statements = body.Statements;
			for (var i = 0; i < statements.Count; i++) {
				if (statements[i].IfStatement != null) {
					list2 = VariableList(statements[i].IfStatement.Body);
					list.AddRange(list2);
					list2 = VariableList(statements[i].IfStatement.ElseBody);
					list.AddRange(list2);
				}

				if (statements[i].WhileLoop != null) {
					list2 = VariableList(statements[i].WhileLoop.Body);
					list.AddRange(list2);
				}

				if (statements[i].ForLoop != null) {
					list2 = VariableList(statements[i].ForLoop.Body);
					list.AddRange(list2);
					index++;
					list.Add(new KeyValuePair<string, string>("V_" + index, "bool"));
				}

				if (statements[i].ForLoop != null || statements[i].WhileLoop != null ||
				    statements[i].IfStatement != null) {
					index++;
					list.Add(new KeyValuePair<string, string>("V_" + index, "bool"));
				}
			}

			return list;
		}

		public static string BodyResult(Body body, string type) {
			if (body == null)
				return "";
			var ans = "";
			if (type != "for" && type != "while")
				ans += "    nop\n";

			var dec = body.SimpleDeclarations;

			for (var i = 0; i < dec.Count; i++) {
				if (dec[i].VariableDeclaration != null && dec[i].VariableDeclaration.VarType != null &&
				    dec[i].VariableDeclaration.Expression != null) {
					if (dec[i].VariableDeclaration.VarType.ArrayType != null)
						ans += defineArray(dec[i].VariableDeclaration.Identifier,
							dec[i].VariableDeclaration.Expression.Relation.Simple1.Summand.Factor.Primary.Ival
								.ToString(), dec[i].VariableDeclaration.VarType.PrimitiveType.ToString());

					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Boolean)
						ans += ExpressionResult(dec[i].VariableDeclaration.Expression,
							dec[i].VariableDeclaration.Identifier);

					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Int)
						ans += ExpressionResult(dec[i].VariableDeclaration.Expression,
							dec[i].VariableDeclaration.Identifier);

					if (dec[i].VariableDeclaration.VarType.PrimitiveType == PrimitiveType.Real)
						ans += ExpressionResult(dec[i].VariableDeclaration.Expression,
							dec[i].VariableDeclaration.Identifier);
				}

				if (dec[i].VariableDeclaration != null && dec[i].VariableDeclaration.VarType == null) //!!!!!!!
					ans += ExpressionResult(dec[i].VariableDeclaration.Expression,
						dec[i].VariableDeclaration.Identifier);
			}

			var resExp = "";
			var statements = body.Statements;
			for (var i = 0; i < statements.Count; i++) {
				if (statements[i].IfStatement != null) {
					++index;
					ans += ExpressionResult(statements[i].IfStatement.Expression, "V_" + index);
					ans += "    ldloc.s    " + "V_" + index + "\n";

					resExp = BodyResult(statements[i].IfStatement.Body, "if");

					ans += "    brfalse.s    " + "IL_" + ++indexLine + "\n";
					ans += resExp + "    IL_" + indexLine + ": " + " nop\n\n";
				}


				if (statements[i].Assignment != null)
					ans += ExpressionResult(statements[i].Assignment.Expression,
						       statements[i].Assignment.ModifiablePrimary.Identifier) + "\n";

				if (statements[i].ForLoop != null) {
					if (statements[i].ForLoop.ForRange.Reverse) {
						ans += ExpressionResult(statements[i].ForLoop.ForRange.Expression2,
							statements[i].ForLoop.Identifier);
						resExp = BodyResult(statements[i].ForLoop.Body, "for");
						ans += ExpressionResult(statements[i].ForLoop.ForRange.Expression1,
							"V_" + ++index);
					} else {
						ans += ExpressionResult(statements[i].ForLoop.ForRange.Expression1,
							statements[i].ForLoop.Identifier);
						resExp = BodyResult(statements[i].ForLoop.Body, "for");
						ans += ExpressionResult(statements[i].ForLoop.ForRange.Expression2,
							"V_" + ++index);
					}

					ans += "    br.s    " + "IL_" + (indexLine + 2) + "\n\n" + "    IL_" +
					       (indexLine + 1) + ": nop\n";
					ans += resExp + "\n" + "    IL_" + (indexLine + 2) + ": " + " ldloc.s    " +
					       statements[i].ForLoop.Identifier + "\n";


					ans += "    ldloc.s    " + "V_" + index + "\n";
					ans += "    cgt\n";
					ans += "    ldc.i4.0\n";
					ans += "    ceq\n";
					ans += "    stloc.s    " + "V_" + ++index + "\n";

					ans += "    ldloc.s    " + "V_" + index + "\n";
					ans += "    brfalse.s    " + "IL_" + (indexLine + 1) + "\n";
					indexLine += 2;
				}

				if (statements[i].WhileLoop != null) {
					resExp = BodyResult(statements[i].WhileLoop.Body, "while");

					ans += "    br.s    " + "IL_" + (indexLine + 2) + "\n\n" + "    IL_" +
					       (indexLine + 1) + ": nop\n";
					ans += resExp + "\n" + "    IL_" + (indexLine + 2) + ": " +
					       ExpressionResult(statements[i].WhileLoop.Expression, "V_" + ++index) + "\n";

					ans += "    ldloc.s    " + "V_" + index + "\n";
					ans += "    brfalse.s    " + "IL_" + (indexLine + 1) + "\n\n";
					indexLine += 2;
				}
			}

			return ans;
		}

		public static string ProgramFunction(ProgramRoot program) {
			var s = ".class public auto ansi beforefieldinit Program extends [mscorlib]System.Object\n{\n";

			for (var i = 0; i < program.RoutineDeclarations.Count; i++)
				s += MainFunction(program.RoutineDeclarations[i], program.RoutineDeclarations[i].Identifier,
					program.RoutineDeclarations[i].Parameters.ParameterDeclarations);

			s +=
				"  .method public hidebysig specialname rtspecialname instance void\n    .ctor() cil managed\n  {\n    .maxstack 8\n    ldarg.0\n    call         instance void [mscorlib]System.Object::.ctor()\n    nop\n    ret\n  }\n}";
			return s;
		}

		public static string MainFunction(RoutineDeclaration routine, string name, List<ParameterDeclaration> param) {
			dict.Clear();
			var listParam = "";
			var typeparam = "";
			for (var i = 0; i < param.Count; i++) {
				if (param[i].Type.PrimitiveType == PrimitiveType.Int)
					typeparam = "int32";
				if (param[i].Type.PrimitiveType == PrimitiveType.Real)
					typeparam = "float64";
				if (param[i].Type.PrimitiveType == PrimitiveType.Boolean)
					typeparam = "bool";

				if (i + 1 == param.Count)
					listParam += typeparam + " " + param[i].Identifier;
				else
					listParam += typeparam + " " + param[i].Identifier + ", ";
				dict.Add(param[i].Identifier, 0);
			}

			var s = "  .method public hidebysig static void " + name + "(" + listParam + ") " + "cil managed\n  {\n";
			if (name == "Main")
				s += "    .entrypoint\n    .maxstack 2\n";
			else
				s += "    .maxstack 2\n";
			var sinit = "    .locals init (\n";
			var sbody = "    nop\n";

			var list = VariableList(routine.Body);
			index = 0;
			var cnt = 0;
			for (var i = 0; i < list.Count; i++)
				if (i + 1 == list.Count)
					sinit += "    [" + i + "] " + list[i].Value + " " + list[i].Key + "\n";
				else
					sinit += "    [" + i + "] " + list[i].Value + " " + list[i].Key + ",\n";
			sinit += "    )\n";

			sbody += BodyResult(routine.Body, "routine") + "    ret\n";

			s += sinit + sbody;
			s += "  }\n\n";
			return s;
		}

		public void run(ProgramRoot p) {
			dict = new Dictionary<string, int>();
			var s = ProgramFunction(p);

			using (var writer = new StreamWriter(File.OpenWrite("out.il"))) {
				if (errors == false)
					writer.WriteLine(s);
				else
					writer.WriteLine("Error");
			}
		}

		private static string defineArray(string name, string size, string type) {
			for (var i = 0; i < size.Length; i++)
				if (!char.IsDigit(size[i]) || size[i].Equals("0"))
					return "//error";

			var output = "";
			switch (type) {
				case "int":
					output = "\n" + "ldc.i4." + size + "\n" + "newarr     [mscorlib]System.Int32]" + "\n" +
					         "stloc.s    " + name;
					break;
				case "real":
					output = "\n" + "ldc.i4." + size + "\n" + "newarr     [mscorlib]System.Double" + "\n" +
					         "stloc.s    " + name;
					break;
				case "bool":
					output = "\n" + "ldc.i4." + size + "\n" + "newarr     [mscorlib]System.Boolean" + "\n" +
					         "stloc.s    " + name;
					break;
				case "Array":
					output = "\n" + "ldc.i4." + size + "\n" + "newarr     [mscorlib]System.Array" + "\n" +
					         "stloc.s    " + name;
					break;
				default:
					output = "//error";
					break;
			}

			return output;
		}

		private static string ApplyArray(string name, string index, string value, string type) {
			if (value.Equals("true"))
				value = "1";
			if (value.Equals("false"))
				value = "0";
			var output = "";
			switch (type) {
				case "real":
					output = "ldloc.s    " + name + "\n ldc.i4." + index + "\n" + "ldc.r8    " + value + "\n" +
					         "stelem.r8";
					break;
				case "int":
					output = "ldloc.s    " + name + "\n ldc.i4." + index + "\n" + "ldc.i4    " + value + "\n" +
					         "stelem.i4";
					break;
				case "Array":
					output = "ldloc.s    " + name + "\n ldc.i4." + index + "\n" + "ldloc.s    " + value + "\n" +
					         "stelem.ref";
					break;
				case "bool":
					output = "ldloc.s    " + name + "\n ldc.i4." + index + "\n" + "ldc.i4." + value + "\n" +
					         "stelem.i1";
					break;
				default:
					output = "/error";
					break;
			}

			return output;
		}
	}
}