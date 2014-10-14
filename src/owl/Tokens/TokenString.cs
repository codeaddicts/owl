using System;

namespace owl
{
	public class TokenString : Token
	{
		public readonly string value;

		public TokenString (string str, int line) : base (line)
		{
			this.value = str;
		}

		public override string ToString ()
		{
			return this.value;
		}
	}
}

