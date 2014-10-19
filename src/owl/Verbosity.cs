using System;

namespace owl
{
	public enum VerbosityLevel { silent, erroronly, warnings, basic, debug }

	public static class Verbosity
	{
		public static VerbosityLevel verb;
	}
}

