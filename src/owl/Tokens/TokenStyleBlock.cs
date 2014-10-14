using System;

namespace owl
{
	public class TokenStyleBlock : Token
	{
		public readonly string value;

		public TokenStyleBlock (string str, int line) : base (line)
		{
			this.value = str;
		}

		public override string ToString ()
		{
			return this.value;
		}
	}
}

