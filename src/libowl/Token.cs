﻿using System;

namespace libowl
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

