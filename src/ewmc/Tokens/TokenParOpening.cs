using System;

namespace ewmc
{
	public class TokenParOpening : Token
	{
		public TokenParOpening (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "(";
		}
	}
}

