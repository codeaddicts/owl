using System;

namespace owl
{
	public class TokenEOF : Token
	{
		public TokenEOF (int line) : base (line)
		{
		}

		public override string ToString ()
		{
			return "End of File";
		}
	}
}

