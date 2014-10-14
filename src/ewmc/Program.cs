using System;
using System.IO;

/// <summary>
/// Enhanced Web Markup Compiler
/// </summary>
namespace ewmc
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Welcome message
			Console.Clear ();
			Console.Write ("".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Enhanced Web Markup Compiler");
			Console.Write ("".PadRight (Console.WindowWidth, '='));

			// Fields
			string input = "";
			string output = "";
			bool build_tree = false;
			bool beautify = true;
			Lexer.VerbosityLevel verbosity = Lexer.VerbosityLevel.Basic;

			// Check the command-line arguments
			int i;
			for (i = 0; i < args.Length - 1; i++) {
				switch (args [i]) {

				// Output file name
				case "-o":
				case "--output":
					if (args.Length >= ++i)
						output = args [i];
					break;
				
				// Verbosity
				case "-v":
				case "--verb":
				case "--verbosity":
					if (args.Length >= ++i) {
						if (args [i].StartsWith ("-")) {
							verbosity = Lexer.VerbosityLevel.Debug;
							i--;
							break;
						}
						Lexer.VerbosityLevel verb;
						if (Enum.TryParse<Lexer.VerbosityLevel> (args [i], out verb))
							verbosity = verb;
					}
					break;

				// Build token tree?
				case "--tree":
					build_tree = true;
					break;
				
				// Deactivate code beautification?
				case "--notidy":
					beautify = false;
					break;
				
				// Undefined/Unsupported argument
				default:
					Console.WriteLine ("ERROR: Unsupported argument '{0}'", args [i]);
					break;
				}
			}

			// Check if the user provided an input file
			if (args.Length - 1 < ++i || string.IsNullOrEmpty (args [i].Trim ())) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("No input file! Aborting.");
				return;
			}
			input = args [++i];

			// Check if the user provided an output file
			// If not, set the output file to {input_file_name_without_extension}.html
			if (output == "")
				output = string.Format ("{0}.html", Path.GetFileNameWithoutExtension (input));

			// Do the lexical analysis
			Lexer lexer = new Lexer ();
			lexer.Configure (filename: input, verbosity: verbosity);
			lexer.Prepare ();
			Lexer.ErrorCode error = lexer.Scan ();

			Console.WriteLine ();

			// Check for lexer errors
			if ((int)error > 0) {
				Console.WriteLine ("The compilation didn't finish. Error: {0}", Enum.GetName (typeof(Lexer.ErrorCode), error));
				return;
			}

			// Build the tree if the -t/--tree argument given
			if (build_tree)
				lexer.BuildTree ();

			// Build the html code
			CodeGen generator = new CodeGen (lexer.GetTokens ());
			generator.Build ();

			// Beautify the output code
			if (beautify)
				generator.Beautify ();

			// Write the html code to disk
			generator.Serialize (output);
		}
	}
}
