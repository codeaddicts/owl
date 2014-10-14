using System;

namespace owl
{
	public enum VerbosityLevel { ErrorOnly, Warnings, Basic, Debug }

	public static class Verbosity
	{
		public static VerbosityLevel verb;
	}
}

