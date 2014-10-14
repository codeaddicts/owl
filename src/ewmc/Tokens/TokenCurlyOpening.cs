using System;

namespace owl
{
	public class TokenCurlyOpening : Token
	{
		public TokenCurlyOpening (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "{";
		}
	}
}

