using System;
using System.IO;
using System.Text;
using TidyNet;

namespace owl
{
	public static class Beautifier
	{
		public static Tidy tidy;

		static Beautifier ()
		{
			tidy = new Tidy ();
			Configure ();
		}

		private static void Configure ()
		{
			tidy.Options.CharEncoding = CharEncoding.UTF8;
			tidy.Options.SmartIndent = true;
			tidy.Options.FixComments = true;
			tidy.Options.MakeClean = true;
			tidy.Options.LogicalEmphasis = true;
			tidy.Options.IndentAttributes = false;
			tidy.Options.TidyMark = false;
			tidy.Options.UpperCaseTags = false;
			tidy.Options.UpperCaseAttrs = false;
			tidy.Options.BreakBeforeBR = false;
			tidy.Options.DropEmptyParas = false;
			tidy.Options.Xhtml = false;
		}

		public static string Beautify (string input)
		{
			string output;

			TidyMessageCollection msg = new TidyMessageCollection ();
			using (MemoryStream msin = new MemoryStream ()) {
				byte[] bytes = Encoding.UTF8.GetBytes (input);
				msin.Write (bytes, 0, bytes.Length);
				msin.Position = 0;
				using (MemoryStream msout = new MemoryStream ()) {
					tidy.Parse (msin, msout, msg);
					output = Encoding.UTF8.GetString (msout.ToArray ());
				}
			}

			return output;
		}
	}
}

