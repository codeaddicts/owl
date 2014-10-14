using System;
using System.IO;

namespace owl
{
	public static class Log
	{
		private static void Write (TextWriter writer, VerbosityLevel verb, string str)
		{
			if ((int)Verbosity.verb >= (int)verb) {
				string verbosity = Enum.GetName (typeof (VerbosityLevel), Verbosity.verb);
				writer.Write ("[{0}] {1}", verbosity, str);
			}
		}

		private static void Write (TextWriter writer, VerbosityLevel verb, string str, params object[] args)
		{
			if ((int)Verbosity.verb >= (int)verb) {
				string verbosity = Enum.GetName (typeof (VerbosityLevel), Verbosity.verb);
				writer.Write ("[{0}] {1}", verbosity, string.Format (str, args));
			}
		}

		public static void Write (VerbosityLevel verb, string str)
		{
			Write (Console.Out, verb, str);
		}

		public static void Write (VerbosityLevel verb, string str, params object[] args)
		{
			Write (Console.Out, verb, str, args);
		}

		public static void Write (string str, params object[] args)
		{
			Write (Console.Out, VerbosityLevel.Basic, str, args);
		}

		public static void Debug (string str, params object[] args)
		{
			Write (Console.Out, VerbosityLevel.Debug, str, args);
		}

		public static void Warning (string str)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Write (Console.Out, VerbosityLevel.Warnings, str);
		}

		public static void Warning (string str, params object[] args)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Write (Console.Out, VerbosityLevel.Warnings, str, args);
		}

		public static void Error (string str)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Write (Console.Error, VerbosityLevel.ErrorOnly, str);
			Console.ForegroundColor = color;
		}

		public static void Error (string str, params object[] args)
		{
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Write (Console.Error, VerbosityLevel.ErrorOnly, str, args);
			Console.ForegroundColor = color;
		}
	}
}

