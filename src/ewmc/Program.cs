using System;

/// <summary>
/// Enhanced Web Markup Compiler
/// </summary>
namespace ewmc
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Clear ();
			Console.Write ("".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Enhanced Web Markup Compiler");
			Console.Write ("".PadRight (Console.WindowWidth, '='));

			string input = "";
			string output = "";
			Lexer.VerbosityLevel verbosity = Lexer.VerbosityLevel.Basic;

			bool build_tree = false;

			for (int i = 0; i < args.Length - 1; i++) {
				switch (args [i]) {
				case "-o":
				case "--output":
					if (args.Length >= ++i)
						output = args [i];
					break;
				case "-v":
				case "--verb":
				case "--verbosity":
					if (args.Length >= ++i) {
						Lexer.VerbosityLevel verb;
						if (Enum.TryParse<Lexer.VerbosityLevel> (args [i], out verb))
							verbosity = verb;
					}
					break;
				case "-e":
				case "--eol":
				case "--line-ending":
					if (args.Length >= ++i) {
						//Lexer.LineEndingStyle eol;
						//if (Enum.TryParse<Lexer.LineEndingStyle> (args [i], out eol))
							//eolstyle = eol;
					}
					break;
				case "--tree":
					build_tree = true;
					break;
				default:
					Console.WriteLine ("ERROR: Unsupported option '{0}'", args [i]);
					break;
				}
			}

			if (args.Length == 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("No input file! Aborting.");
				return;
			}

			input = args [args.Length - 1];
			if (string.IsNullOrEmpty (input.Trim ())) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("No input file! Aborting.");
				return;
			}

			Lexer lexer = new Lexer ();
			lexer.Configure (filename: input, verbosity: verbosity);
			lexer.Prepare ();
			Lexer.ErrorCode error = lexer.Scan ();

			Console.WriteLine ();

			if ((int)error > 0) {
				Console.WriteLine ("The compilation didn't finish. Error: {0}", Enum.GetName (typeof(Lexer.ErrorCode), error));
				return;
			}

			if (build_tree)
				lexer.BuildTree ();

			CodeGen generator = new CodeGen (lexer.GetTokens ());
			generator.Build ();
			generator.Beautify ();
			generator.Serialize (output);
		}
	}
}
