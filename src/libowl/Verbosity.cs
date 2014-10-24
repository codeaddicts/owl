using System;

namespace libowl
{
	public enum VerbosityLevel { silent, erroronly, warnings, basic, debug }

	public static class Verbosity
	{
		public static VerbosityLevel verb;
	}
}

