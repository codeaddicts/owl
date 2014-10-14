using System;

namespace owl
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

