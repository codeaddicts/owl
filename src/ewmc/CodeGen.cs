using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using TidyNet;
using System.Diagnostics;

namespace owl
{
	public class CodeGen
	{
		private string output;
		private List<Token> tokens;
		private Stack<string> tags;

		public CodeGen (List<Token> tokens)
		{
			this.output = "";
			this.tokens = tokens;
			this.tokens.RemoveAll (t => t is TokenEOL);
			this.tokens.Add (new TokenEOF (0));
			this.tokens.Add (new TokenEOF (0));
			this.tokens.Add (new TokenEOF (0));
			this.tokens.Add (new TokenEOF (0));
			this.tags = new Stack<string> ();
		}

		public void Build ()
		{
			Console.Write ("".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Code Generation");
			Console.Write ("".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Generating HTML code...");

			Stopwatch watch = new Stopwatch ();
			watch.Start ();

			AppendXML ("doctype html public");
			TagOpen ("html");
			TagAttribClose ();

			for (int i = 0; i < tokens.Count - 4; i++) {
				if (IsTagIdentifier (i)) {
					ConstructTagOpening (ref i);
				} else if (IsTagClosing (i)) {
					TagClose ();
				} else if (IsStyleBlock (i)) {
					Append (tokens [i].ToString ());
				} else if (IsContent (i)) {
					Append (tokens [i].ToString ());
				} else if (IsEscapeCharacter (i)) {
					Append (tokens [i].ToString ());
				}
			}

			TagClose ();

			watch.Stop ();
			Console.WriteLine ("Code Generation finished after {0}ms", watch.Elapsed.Milliseconds);
		}

		public void TestCase ()
		{
			AppendXML ("doctype html public");
			TagOpen ("html");
			TagAttribClose ();
			TagOpen ("head");
			TagAttribClose ();
			TagOpen ("meta");
			TagAttribAdd ("author");
			TagAttribAssign ();
			TagAttribValue ("splitty");
			TagAttribCloseTag ();
			TagClose ();
			TagClose ();
		}

		public void Beautify ()
		{
			Console.Write ("\n" + "".PadRight (Console.WindowWidth, '='));
			Console.WriteLine ("Tidying up HTML...");
			Console.Write ("".PadRight (Console.WindowWidth, '='));

			Tidy tidy = new Tidy ();
			tidy.Options.CharEncoding = CharEncoding.UTF8;
			tidy.Options.SmartIndent = true;
			tidy.Options.FixComments = true;
			tidy.Options.TidyMark = false;
			tidy.Options.UpperCaseTags = false;
			tidy.Options.UpperCaseAttrs = false;
			tidy.Options.MakeClean = true;
			tidy.Options.BreakBeforeBR = false;
			tidy.Options.DropEmptyParas = false;
			TidyMessageCollection msg = new TidyMessageCollection ();
			using (MemoryStream msin = new MemoryStream ()) {
				byte[] bytes = Encoding.UTF8.GetBytes (output);
				msin.Write (bytes, 0, bytes.Length);
				msin.Position = 0;
				using (MemoryStream msout = new MemoryStream ()) {
					tidy.Parse (msin, msout, msg);
					output = Encoding.UTF8.GetString (msout.ToArray ());
				}
			}
		}

		public void Serialize (string path)
		{
			using (FileStream file = new FileStream (path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)) {
				using (StreamWriter writer = new StreamWriter (file)) {
					writer.Write (output);
					writer.Flush ();
				}
			}
		}

		private void ConstructTagOpening (ref int i)
		{
			string cur = tokens [i].ToString ();
			TagOpen (tokens [i++].ToString () == "document" ? "body" : cur);

			if (IsAttribOpening (i)) {
				while (!IsAttribClosing (++i)) {
					if (IsAttribute4 (i)) {
						TagAttribAdd (tokens [i++].ToString ());
						TagAttribAssign (i++);
						TagAttribValue (tokens [i].ToString ());
					} else if (IsAttribute5 (i)) {
						TagAttribAdd (tokens [i].ToString ());
					}
				}
				i++;
			}

			if (IsSemicolon (i)) {
				TagAttribCloseTag ();
			}
			else
				TagAttribClose ();
		}

		private bool IsSemicolon (int i)
		{
			if (tokens [i] is TokenSemicolon)
				return true;
			return false;
		}

		private bool IsContent (int i)
		{
			if (tokens [i] is TokenContent)
				return true;
			return false;
		}

		private bool IsEscapeCharacter (int i)
		{
			if (tokens [i] is TokenEscape)
				return true;
			return false;
		}

		private bool IsAssign (int i)
		{
			if (tokens [i] is TokenAssign)
				return true;
			return false;
		}

		private bool IsIdentifier (int i)
		{
			if (tokens [i] is TokenIdentifier)
				return true;
			return false;
		}

		private bool IsTagIdentifier (int i)
		{
			if (IsIdentifier (i) && IsOpeningBracket (i+1))
				return true;
			return false;
		}

		private bool IsTagOpening (int i)
		{
			if (tokens [i] is TokenCurlyOpening)
				return true;
			return false;
		}

		private bool IsAttribOpening (int i)
		{
			if (tokens [i] is TokenParOpening)
				return true;
			return false;
		}

		private bool IsOpeningBracket (int i)
		{
			if (IsAttribOpening (i) || IsTagOpening (i))
				return true;
			return false;
		}

		private bool IsTagClosing (int i)
		{
			if (tokens [i] is TokenCurlyClosing)
				return true;
			return false;
		}

		private bool IsAttribClosing (int i)
		{
			if (tokens [i] is TokenParClosing)
				return true;
			return false;
		}

		private bool IsClosingBracket (int i)
		{
			if (IsAttribClosing (i) || IsTagClosing (i))
				return true;
			return false;
		}

		private bool IsAttribute4 (int i)
		{
			// HTML4: attribute="value"
			if (IsIdentifier (i) && IsAssign (i+1) && IsStringLiteral (i+2))
				return true;
			return false;
		}

		private bool IsAttribute5 (int i)
		{
			// HTML5 (i.e. async): attribute
			if (IsIdentifier (i) && (IsIdentifier (i+1) || IsAttribClosing (i+1)))
				return true;
			return false;
		}

		private bool IsStringLiteral (int i)
		{
			if (tokens [i] is TokenString)
				return true;
			return false;
		}

		private bool IsStyleBlock (int i)
		{
			if (tokens [i] is TokenStyleBlock)
				return true;
			return false;
		}

		private void TagOpen (string tagname)
		{
			tags.Push (tagname);
			// Console.WriteLine ("Opening {0} tag", tagname);
			Append ("<{0}", tagname);
		}

		private void TagAttribAdd (string attrib)
		{
			Append (" {0}", attrib);
		}

		private void TagAttribAssign (int i = 0)
		{
			Append ("=");
		}

		private void TagAttribValue (string val)
		{
			Append ("\"{0}\"", val);
		}

		private void TagAttribClose ()
		{
			// Append (">\n");
			Append (">");
		}

		private void TagAttribCloseTag ()
		{
			string tag = tags.Pop ();
			// Console.WriteLine ("Closing without body: {0}", tag);
			// Append ("/>\n");
			Append ("/>");
		}

		private void TagClose ()
		{
			string tag = tags.Pop ();
			// Console.WriteLine ("Closing: {0}", tag);
			// Append ("</{0}>\n", tag);
			Append ("</{0}>", tag);
		}

		private void Append (string str)
		{
			output += str;
		}

		private void Append (string str, params string[] args)
		{
			Append (string.Format (str, args));
		}

		private void AppendXML (string xml)
		{
			Append ("<!{0}>\n", xml);
		}

		private void AppendComment (string comment)
		{
			Append ("<!--{0}-->\n", comment);
		}
	}
}

