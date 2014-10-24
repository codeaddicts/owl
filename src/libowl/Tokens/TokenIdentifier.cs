using System;

namespace libowl
{
	public class TokenIdentifier : Token
	{
		public readonly string value;

		public TokenIdentifier (string str, int line) : base (line)
		{
			this.value = str;
		}

		public override string ToString ()
		{
			return this.value;
		}
	}
}

