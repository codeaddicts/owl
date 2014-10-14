using System;

namespace owl
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

