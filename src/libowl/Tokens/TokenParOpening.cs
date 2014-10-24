using System;

namespace libowl
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

