using System;

namespace ewmc
{
	public class TokenParClosing : Token
	{
		public TokenParClosing (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return ")";
		}
	}
}

