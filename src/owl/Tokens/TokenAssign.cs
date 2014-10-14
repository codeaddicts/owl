using System;

namespace owl
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

