using System;

namespace owl
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

