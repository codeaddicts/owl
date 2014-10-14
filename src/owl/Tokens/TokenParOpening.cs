using System;

namespace owl
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

