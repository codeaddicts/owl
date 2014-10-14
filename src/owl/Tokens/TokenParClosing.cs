using System;

namespace owl
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

