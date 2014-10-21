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

		/// <summary>
		/// Initializes a new instance of the <see cref="owl.Lexer"/> class.
		/// </summary>
		/// <param name='filename'>
		/// Filename.
		/// </param>
		public Lexer (string filename)
		{
			pos = -1;
			line = 1;
			depth = 0;
			linew = 0;
			tokens = new List<Token> ();
			this.filename = filename;
		}

		/// <summary>
		/// Returns the token list.
		/// </summary>
		/// <returns>
		/// The tokens.
		/// </returns>
		public List<Token> GetTokens ()
		{
			return tokens;
		}

		/// <summary>
		/// Prepare the Lexer for scanning.
		/// </summary>
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
			linew = CalculateLineNumberWidth (source);
		}

		public int CalculateLineNumberWidth (string source) {
			int width = 0;
			source.All (c => { if (c == '\n') width++; return true; });
			width = (width + 1).ToString ().Length;
			return width;
		}

		/// <summary>
		/// Builds the token tree.
		/// </summary>
		public void BuildTree ()
		{
			Tree tree = new Tree (tokens);
			tree.Build ();
		}

		/// <summary>
		/// Scans the source for tokens.
		/// </summary>
		/// <returns>
		/// An ErrorCode.
		/// </returns>
		public ErrorCode Scan ()
		{
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
					
						// Preprocessor Directive
						case '#':
							Read ();
							LogElem ("Preprocessor Directive");
							ScanPreprocessorDirective ();
							break;

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
							
						// Assign
						case '=':
							Read ();
							LogElem ("Assign");
							tokens.Add (new TokenAssign (line));
							break;
							
						// Comma
						case ',':
							Read ();
							LogElem ("Comma");
							tokens.Add (new TokenComma (line));
							break;
							
						// Semicolon
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
							
						// Backslash
						case '\\':
							ErrorCode err2 = ScanEscape ();
							if (err2 != ErrorCode.NoErrors) {
								watch.Stop ();
								return err2;
							}
							break;
							
						// Default
						default:
							Log.Error ("Unexpected token: '{0}' at line {1}. Aborting.", PeekChar (), line);
							watch.Stop ();
							return ErrorCode.UnexpectedToken;
					}
				}
			}
			tokens.Add (new TokenEOF (line));

			watch.Stop ();
			Log.Write ("Lexical Analysis finished after {0}ms", watch.Elapsed.Milliseconds);

			return ErrorCode.NoErrors;
		}

		public void SkipWhitespace ()
		{
			while (char.IsWhiteSpace (PeekChar ())) {
				if (PeekChar () == '\n')
					line++;
				Read ();
			}
		}

		public void ScanPreprocessorDirective ()
		{
			SkipWhitespace ();
			string ident = ScanIdentifier (false);
			SkipWhitespace ();

			switch (ident)
			{
				case "include":
					string path;
					ScanContent (out path, false);
					if (path != "" && File.Exists (Path.GetFullPath (path)))
					{
						LogElem ("Including file: " + path);
						using (FileStream fs = new FileStream (Path.GetFullPath (path), FileMode.Open, FileAccess.Read, FileShare.Read))
						{
							using (StreamReader reader = new StreamReader (fs))
							{
								string str = reader.ReadToEnd ();
								str = str.Replace ("\r\n", "\n");
								AppendAt (str, pos + 1);
								LogElem ("Contents: " + str);
								linew = CalculateLineNumberWidth (source);
							}
						}
					}
					break;
			}
		}

		/// <summary>
		/// Scans an identifier.
		/// </summary>
		/// <returns>
		/// The identifier.
		/// </returns>
		public string ScanIdentifier (bool add_token = true)
		{
			StringBuilder sb = new StringBuilder ();

			while (char.IsLetterOrDigit (PeekChar ()) || PeekChar () == '_') {
				sb.Append (ReadChar ());
			}
			string str = sb.ToString ();

			if (add_token) {
				LogElem ("Identifier: " + str);
				tokens.Add (new TokenIdentifier (str, line));
			}

			return str;
		}

		/// <summary>
		/// Scans a string literal.
		/// </summary>
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

		/// <summary>
		/// Scans content.
		/// </summary>
		/// <returns>
		/// An ErrorCode
		/// </returns>
		public ErrorCode ScanContent (out string content, bool add_token = true)
		{
			SkipWhitespace ();
			content = "";

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

				Read ();
				if (add_token)
				{
					LogElem ("String Content: " + sb.ToString ());
					LogElem ("String End");
					tokens.Add (new TokenContent (sb.ToString (), line));
				}
				else
				{
					content = sb.ToString ();
				}
			}

			return ErrorCode.NoErrors;
		}

		public ErrorCode ScanContent ()
		{
			string str;
			return ScanContent (out str);
		}

		/// <summary>
		/// Scans an escape sequence.
		/// </summary>
		/// <returns>
		/// An ErrorCode
		/// </returns>
		public ErrorCode ScanEscape ()
		{
			string str;
			return ScanEscape (out str);
		}

		/// <summary>
		/// Scans an escape sequence.
		/// </summary>
		/// <returns>
		/// An ErrorCode
		/// </returns>
		/// <param name='escape'>
		/// Output variable for the escape sequence
		/// </param>
		public ErrorCode ScanEscape (out string escape)
		{
			Read ();
			escape = "";
			switch (PeekChar ()) {
			// Newline
			case 'n':
				Read ();
				escape = "<br/>\n";
				LogElem ("Escape: Newline");
				break;
				
			// Tab
			case 't':
				Read ();
				escape = "&nbsp;&nbsp;&nbsp;";
				LogElem ("Escape: Tab");
				break;
				
			// Opening Curly Bracket
			case '{':
				Read ();
				escape = "{";
				LogElem ("Escape: Opening Curly Bracket");
				break;
				
			// Closing Curly Bracket
			case '}':
				Read ();
				escape = "}";
				LogElem ("Escape: Closing Curly Bracket");
				break;
				
			// Opening Parenthesis
			case '(':
				Read ();
				escape = "(";
				LogElem ("Escape: Opening Parenthesis");
				break;
				
			// Closing Parenthesis
			case ')':
				Read ();
				escape = ")";
				LogElem ("Escape: Closing Parenthesis");
				break;
				
			// Quote
			case '\"':
				Read ();
				escape = "\"";
				LogElem ("Escape: Quote");
				break;
				
			// Apostrophe
			case '\'':
				Read ();
				escape = "\'";
				LogElem ("Escape: Apostrophe");
				break;
				
			default:
				Log.Error ("Unexpected escape character: '{0}' at line {1}. Aborting.", PeekChar (), line);
				return ErrorCode.UnexpectedEscape;
			}
			return ErrorCode.NoErrors;
		}

		/// <summary>
		/// Returns the next character from the source
		/// or -1 if EOF.
		/// </summary>
		/// <returns>
		/// The char.
		/// </returns>
		public int Peek ()
		{
			return pos < source.Length - 1 ? (int)source [pos + 1] : -1;
		}

		/// <summary>
		/// Returns the next character from the source
		/// or (char)0 if EOF.
		/// </summary>
		/// <returns>
		/// The char.
		/// </returns>
		public char PeekChar ()
		{
			if (Peek () != -1)
				return (char)Peek ();
			else
				return (char)0;
		}

		/// <summary>
		/// Returns the next character from the source
		/// or -1 if EOF and increments the position by one.
		/// </summary>
		/// <returns>
		/// The char.
		/// </returns>
		public int Read ()
		{
			return pos < source.Length - 1 ? (int)source [++pos] : -1;
		}

		/// <summary>
		/// Returns the next character from the source
		/// or (char)0 if EOF and increments the position by one.
		/// </summary>
		/// <returns>
		/// The char.
		/// </returns>
		public char ReadChar ()
		{
			if (Peek () != -1)
				return (char)Read ();
			else
				return (char)0;
		}

		public void AppendAt (string str, int pos)
		{
			StringBuilder sb = new StringBuilder ();
			sb = sb.Append (source);
			sb = sb.Insert (pos, str);
			source = sb.ToString ();
		}

		/// <summary>
		/// Adds an element to the log.
		/// </summary>
		/// <param name='text'>
		/// Text.
		/// </param>
		public void LogElem (string text)
		{
			Log.Debug ("D:{1:00} L:{0:" + "".PadRight (linew, '0') + "} {2}", line, depth, text);
		}
	}
}

