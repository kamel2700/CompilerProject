using System;
using System.IO;
using CompilersProject.Compiler;
using CompilersProject.Compiler.Lexer;

namespace CompilersProject {
	internal static class Program {
		private static void Main() {
			Scanner scanner;
			using (var stream = File.OpenRead("Example4.ilang")) {
				scanner = new Scanner(stream);
				var parser = new Parser {scanner = scanner};

//				parser.Trace = true;
				Console.WriteLine(parser.Parse());

				var root = parser.Root;
				var mainn = new MainProgram();
				mainn.run(root);
			}
		}
	}
}