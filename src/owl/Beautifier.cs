using System;
using System.IO;
using System.Text;
using TidyNet;

namespace owl
{
	public static class Beautifier
	{
		private static Tidy tidy;
		private static TidyMessageCollection tidymsg;

		static Beautifier ()
		{
			tidy = new Tidy ();
			tidymsg = new TidyMessageCollection ();
			Configure ();
		}

		private static void Configure ()
		{
			tidy.Options.CharEncoding = CharEncoding.UTF8;
			tidy.Options.SmartIndent = true;
			tidy.Options.MakeClean = false;
			tidy.Options.LogicalEmphasis = true;
			tidy.Options.FixComments = false;
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
			string output = input;

			using (MemoryStream msin = new MemoryStream ()) {
				byte[] bytes = Encoding.UTF8.GetBytes (input);
				msin.Write (bytes, 0, bytes.Length);
				msin.Flush ();
				msin.Position = 0;
				using (MemoryStream msout = new MemoryStream ()) {
					tidy.Parse (msin, msout, tidymsg);
					msout.Flush ();
					byte[] msout_bytes = msout.ToArray ();
					if (msout_bytes.Length > 0)
						output = Encoding.UTF8.GetString (msout_bytes);
					else
						Log.Warning ("The TidyNet library didn't return any value.\nThe generated html code may be really ugly.");
				}
			}

			return output;
		}
	}
}

