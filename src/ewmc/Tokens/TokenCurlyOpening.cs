using System;

namespace ewmc
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

