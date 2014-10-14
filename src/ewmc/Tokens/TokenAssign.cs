using System;

namespace ewmc
{
	public class TokenAssign : Token
	{
		public TokenAssign (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "=";
		}
	}
}

