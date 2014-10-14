using System;

namespace owl
{
	public abstract class Token
	{
		public readonly int line;

		public Token (int line)
		{
			this.line = line;
		}
	}
}

