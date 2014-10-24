using System;

namespace libowl
{
	public class TokenBracketClosing : Token
	{
		public TokenBracketClosing (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "]";
		}
	}
}

