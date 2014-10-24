using System;

namespace libowl
{
	public class TokenComma : Token
	{
		public TokenComma (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return ",";
		}
	}
}

