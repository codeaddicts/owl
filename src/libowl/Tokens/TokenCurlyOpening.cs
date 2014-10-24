using System;

namespace libowl
{
	public class TokenCurlyOpening : Token
	{
		public TokenCurlyOpening (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "{";
		}
	}
}

