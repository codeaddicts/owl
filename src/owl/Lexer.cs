using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace owl
{
	public partial class Lexer
	{
		private string filename;
		private string source;

		private int pos;
		private int line;
		private int depth;
		private int linew;
		private List<Token> tokens;

		public enum ErrorCode { NoErrors = 0, UnexpectedToken, UnexpectedEscape }

		public Lexer (string filename)
		{
			pos = -1;
			line = 1;
			depth = 0;
			linew = 0;
			tokens = new List<Token> ();
			this.filename = filename;
		}

		public List<Token> GetTokens ()
		{
			return tokens;
		}

		public void Prepare ()
		{
			Log.Write ("Reading file '{0}'", Path.GetFileName (filename));

			// Ensure that the filename has the right format
			filename = Path.GetFullPath (filename);

			// Check if the input file exists
			if (!File.Exists (filename)) {
				Log.Error ("File not found!\n\tFile: '{0}'", filename);
				return;
			}

			using (FileStream file = new FileStream (filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (StreamReader reader = new StreamReader (file)) {
					this.source = reader.ReadToEnd ();
				}
			}

			// Replace line endings with linux-style line endings
			source = source.Replace ("\r\n", "\n");

			// Get the "length" of the line count
			source.All (c => { if (c == '\n') linew++; return true; });
			linew = (linew + 1).ToString ().Length;
		}

		public void BuildTree ()
		{
			Tree tree = new Tree (tokens);
			tree.Build ();
		}

		public ErrorCode Scan ()
		{
			Console.Write ("\n" + "".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Lexical Analysis");
			Console.Write ("".PadRight (Console.WindowWidth, '='));

			Log.Write ("Processing file '{0}'", Path.GetFileName (filename));

			Stopwatch watch = new Stopwatch ();
			watch.Start ();

			while (pos < source.Length && Peek () != -1) {
				while (PeekChar () != '\n' && char.IsWhiteSpace (PeekChar ()))
					Read ();

				// Newline
				if (PeekChar () == '\n') {
					while (PeekChar () == '\n') {
						Read ();
						line++;
						LogElem ("Newline");
						tokens.Add (new TokenEOL (line));
					}
				}

				// String literals
				else if (PeekChar () == '\"') {
					ScanStringLiteral ();
				}

				// Identifiers
				else if (char.IsLetter (PeekChar ())) {
					string ident = ScanIdentifier ();
				}

				// Syntax elements
				else {
					switch (PeekChar ()) {

					// Opening Parenthesis
					case '(':
						Read ();
						LogElem ("Opening Parenthesis");
						tokens.Add (new TokenParOpening (line));
						break;

					// Closing Parenthesis
					case ')':
						Read ();
						LogElem ("Closing Parenthesis");
						tokens.Add (new TokenParClosing (line));
						break;
					
					// Opening Curly Bracket
					case '{':
						depth++;
						Read ();
						LogElem ("Opening Curly Bracket");
						tokens.Add (new TokenCurlyOpening (line));
						ErrorCode err = ScanContent ();
						if (err != ErrorCode.NoErrors) {
							watch.Stop ();
							return err;
						}
						break;
					
					// Closing Curly Bracket
					case '}':
						depth--;
						Read ();
						LogElem ("Closing Curly Bracket");
						tokens.Add (new TokenCurlyClosing (line));
						ErrorCode err0 = ScanContent ();
						if (err0 != ErrorCode.NoErrors) {
							watch.Stop ();
							return err0;
						}
						break;
					case '=':
						Read ();
						LogElem ("Assign");
						tokens.Add (new TokenAssign (line));
						break;
					case ',':
						Read ();
						LogElem ("Comma");
						tokens.Add (new TokenComma (line));
						break;
					case ';':
						Read ();
						LogElem ("Semicolon");
						tokens.Add (new TokenSemicolon (line));
						ErrorCode err1 = ScanContent ();
						if (err1 != ErrorCode.NoErrors) {
							watch.Stop ();
							return err1;
						}
						break;
					case '\\':
						ErrorCode err2 = ScanEscape ();
						if (err2 != ErrorCode.NoErrors) {
							watch.Stop ();
							return err2;
						}
						break;
					default:
						Log.Error ("Unexpected token: '{0}' at line {1}. Aborting.", PeekChar (), line);
						watch.Stop ();
						return ErrorCode.UnexpectedToken;
					}
				}
			}
			tokens.Add (new TokenEOF (line));

			watch.Stop ();
			Console.WriteLine ("Lexical Analysis finished after {0}ms", watch.Elapsed.Milliseconds);

			return ErrorCode.NoErrors;
		}

		public string ScanIdentifier ()
		{
			StringBuilder sb = new StringBuilder ();

			while (char.IsLetterOrDigit (PeekChar ()) || PeekChar () == '_') {
				sb.Append (ReadChar ());
			}
			string str = sb.ToString ();

			LogElem ("Identifier: " + str);
			tokens.Add (new TokenIdentifier (str, line));

			return str;
		}

		public void ScanStringLiteral ()
		{
			Read ();
			StringBuilder sb = new StringBuilder ();

			while (PeekChar () != '\"' && Peek () != -1) {
				sb.Append (ReadChar ());
			}

			Read ();
			LogElem ("StringLiteral: " + sb.ToString ());
			tokens.Add (new TokenString (sb.ToString (), line));
		}

		public ErrorCode ScanContent ()
		{
			while (char.IsWhiteSpace (PeekChar ())) {
				if (PeekChar () == '\n')
					line++;
				Read ();
			}

			if (PeekChar () == '"') {
				LogElem ("String Begin");
				Read ();
				StringBuilder sb = new StringBuilder ();

				while (PeekChar () != '"') {
					if (PeekChar () == '\n') {
						line++;
						sb.Append (ReadChar ());
					} else if (PeekChar () == '\\') {
						string escape = "";
						ErrorCode err = ScanEscape (out escape);
						if (err != ErrorCode.NoErrors)
							return err;
						sb.Append (escape);
					} else {
						sb.Append (ReadChar ());
					}
				}
				LogElem ("String Content: " + sb.ToString ());
				LogElem ("String End");

				Read ();
				tokens.Add (new TokenContent (sb.ToString (), line));
			}

			return ErrorCode.NoErrors;
		}

		public ErrorCode ScanEscape ()
		{
			string str;
			return ScanEscape (out str);
		}

		public ErrorCode ScanEscape (out string escape)
		{
			Read ();
			escape = "";
			switch (PeekChar ()) {
			case 'n':
				Read ();
				escape = "<br/>\n";
				LogElem ("Escape: Newline");
				break;

			case 't':
				Read ();
				escape = "&nbsp;&nbsp;&nbsp;";
				LogElem ("Escape: Tab");
				break;
			case '{':
				Read ();
				escape = "{";
				LogElem ("Escape: Opening Curly Bracket");
				break;
			case '}':
				Read ();
				escape = "}";
				LogElem ("Escape: Closing Curly Bracket");
				break;
			case '(':
				Read ();
				escape = "(";
				LogElem ("Escape: Opening Parenthesis");
				break;
			case ')':
				Read ();
				escape = ")";
				LogElem ("Escape: Closing Parenthesis");
				break;
			case '\"':
				Read ();
				escape = "\"";
				LogElem ("Escape: Quote");
				break;
			default:
				Log.Error ("Unexpected escape character: '{0}' at line {1}. Aborting.", PeekChar (), line);
				return ErrorCode.UnexpectedEscape;
			}
			return ErrorCode.NoErrors;
		}

		public int Peek ()
		{
			return pos < source.Length - 1 ? (int)source [pos + 1] : -1;
		}

		public char PeekChar ()
		{
			return (char)Peek ();
		}

		public int Read ()
		{
			return pos < source.Length - 1 ? (int)source [++pos] : -1;
		}

		public char ReadChar ()
		{
			return (char)Read ();
		}

		public void LogElem (string text)
		{
			Log.Debug ("D:{1:00} L:{0:" + "".PadRight (linew, '0') + "} {2}", line, depth, text);
		}
	}
}

