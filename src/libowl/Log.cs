using System;
using System.IO;
using System.Collections.Generic;

namespace libowl
{
	public static class Log
	{
		public static TextWriter writer;

		public static void Init (TextWriter stream)
		{
			writer = stream;
		}

		public delegate void LogTextAvailable (string msg);
		public static event LogTextAvailable OnLogTextAvailable;

		private static void Write (VerbosityLevel verb, string str)
		{
			if ((int)Verbosity.verb >= (int)verb) {
				string verbosity = Enum.GetName (typeof (VerbosityLevel), Verbosity.verb);
				writer.WriteLine ("[{0}] {1}", verbosity, str);
				OnLogTextAvailable (str);
			}
		}

		private static void Write (VerbosityLevel verb, string str, params object[] args)
		{
			Write (verb, string.Format (str, args));
		}

		public static void Write (string str, params object[] args)
		{
			Write (VerbosityLevel.basic, str, args);
		}

		public static void Debug (string str, params object[] args)
		{
			Write (VerbosityLevel.debug, str, args);
		}

		public static void Warning (string str)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Write (VerbosityLevel.warnings, str);
		}

		public static void Warning (string str, params object[] args)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Write (VerbosityLevel.warnings, str, args);
		}

		public static void Error (string str)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Write (VerbosityLevel.erroronly, str);
			Console.ForegroundColor = color;
		}

		public static void Error (string str, params object[] args)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Write (VerbosityLevel.erroronly, str, args);
			Console.ForegroundColor = color;
		}
	}
}

