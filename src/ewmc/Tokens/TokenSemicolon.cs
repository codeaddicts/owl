using System;

namespace ewmc
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

