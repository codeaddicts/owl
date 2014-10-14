using System;

namespace ewmc
{
	public class TokenBracketOpening : Token
	{
		public TokenBracketOpening (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "[";
		}
	}
}

