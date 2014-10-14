using System;

namespace owl
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

