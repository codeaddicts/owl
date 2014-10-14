using System;

namespace owl
{
	public class TokenEscape : Token
	{
		public readonly string value;

		public TokenEscape (string str, int line) : base (line)
		{
			this.value = str;
		}

		public override string ToString ()
		{
			return this.value;
		}
	}
}

