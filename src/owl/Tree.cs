using System;
using System.Collections.Generic;

namespace owl
{
	public class Tree
	{
		private List<Token> tokens;

		public Tree (List<Token> tokens)
		{
			this.tokens = tokens;
		}

		int depth = 0;
		int mul = 2;
		public void Build ()
		{
			Console.WriteLine ("[tree]");
			Console.CursorTop--;
			for (int i = 0; i < tokens.Count; i++) {
				string pad = "".PadRight (depth * mul, ' ');
				if (tokens [i] is TokenIdentifier)
					Console.Write ("\n{0} {1}", pad, tokens [i].ToString ());
				else if (tokens [i] is TokenBracketOpening || tokens [i] is TokenCurlyOpening || tokens [i] is TokenParOpening) {
					depth++;
				} else if (tokens [i] is TokenBracketClosing || tokens [i] is TokenCurlyClosing || tokens [i] is TokenParClosing) {
					depth--;
				} else if (tokens [i] is TokenAssign)
					Console.Write (" = ");
				else if (tokens [i] is TokenString)
					Console.Write (tokens [i].ToString ());
				else if (tokens [i] is TokenContent)
					Console.Write ("\n{0} Content: {1}", pad, tokens [i].ToString ().Trim ().Replace ("\n", "").Replace ("\t", ""));
				else if (tokens [i] is TokenStyleBlock)
					Console.Write ("\n{0} Style Block", pad);
				else if (tokens [i] is TokenEOF)
					Console.Write ("\n[/tree]");
			}
			Console.WriteLine ();
		}
	}
}

