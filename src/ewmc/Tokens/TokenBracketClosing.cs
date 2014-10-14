using System;

namespace ewmc
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

