using System;

namespace owl
{
	public class TokenContent : Token
	{
		public readonly string value;

		public TokenContent (string str, int line) : base (line)
		{
			this.value = str;
		}

		public override string ToString ()
		{
			return this.value;
		}
	}
}

