using System;

namespace ewmc
{
	public class TokenEOL : Token
	{
		public TokenEOL (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "End of Line";
		}
	}
}

