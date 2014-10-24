using System;

namespace libowl
{
	public class TokenSemicolon : Token
	{
		public TokenSemicolon (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return ";";
		}
	}
}

