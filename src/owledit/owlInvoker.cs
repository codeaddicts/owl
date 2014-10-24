using System;
using libowl;

namespace owledit
{
	public static class owlInvoker
	{
		public static Lexer lex;

		static owlInvoker ()
		{
			lex = new Lexer ();
		}

		public static void InvokeOwl ()
		{
			InvokeOwl (DocumentManager.Current);
		}

		public static void InvokeOwl (OwlDocument doc)
		{

		}

		public static void CheckErrors ()
		{
			CheckErrors (DocumentManager.Current);
		}

		public static void CheckErrors (OwlDocument doc)
		{
			bool abort = false;
			Verbosity.verb = VerbosityLevel.silent;

			lex.Reset ();
			lex.PrepareSource (doc.source);

			lex.onSyntaxError += (err) => {
				string errmsg = GetFriendlyErrorMessage (err);
				Program.window.setStatusText (string.Format ("[{0}]:\n{1}",
					Enum.GetName (typeof(Lexer.ErrorCode), err), errmsg));
				abort = true;
				lex.Abort ();
			};

			Lexer.ErrorCode err_ = lex.Scan ();

			if (!abort)
			{
				string errmsg = GetFriendlyErrorMessage (err_);
				if (err_ != Lexer.ErrorCode.None)
					Program.window.setStatusText (string.Format ("[{0}]:\n{1}",
						Enum.GetName (typeof(Lexer.ErrorCode), err_), errmsg));
				else
					Program.window.setStatusText ("");
			}
		}

		public static string GetFriendlyErrorMessage (Lexer.ErrorCode err)
		{
			switch (err)
			{
				case Lexer.ErrorCode.UnexpectedToken:
					return "The owl compiler recognized an unusual token.";
				case Lexer.ErrorCode.UnexpectedEscape:
					return "Seems like you are using an escape sequence which doesn't exist!\n";
				case Lexer.ErrorCode.UnexpectedStringEnd:
					return "This usually means that you forgot to write the closing quotes of a string literal.";
				case Lexer.ErrorCode.UnexpectedContentEnd:
					return "This usually means that you forgot to write the closing quotes of a content string.";
				case Lexer.ErrorCode.ExpectedClosingBracket:
					return "Please close this block with a '}'";
				case Lexer.ErrorCode.ExpectedClosingParenthesis:
					return "Please close this parameter list with a ')'";
				default:
					return "";
			}
		}
	}
}

