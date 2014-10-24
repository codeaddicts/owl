using System;

namespace libowl
{
	public class TokenCurlyClosing : Token
	{
		public TokenCurlyClosing (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "}";
		}
	}
}

