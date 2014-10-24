using System;

namespace owledit
{
	public class OwlDocument
	{
		public string guid;
		public string name;
		public string path;
		public string source;
		public bool saved;

		public OwlDocument ()
		{
			guid = null;
			name = null;
			path = null;
			source = null;
			saved = true;
		}

		public void updateSource (string src)
		{
			source = src;
			saved = false;
		}

		public bool hasPathSet ()
		{
			if (path == null || path == "")
				return false;
			return true;
		}

		public bool hasUnsafedChanges ()
		{
			if (saved)
				return false;
			return true;
		}

		public bool hasGuidSet ()
		{
			if (guid == null || guid == "")
				return false;
			return true;
		}

		public void setGuid ()
		{
			guid = Guid.NewGuid ().ToString ("D");
		}
	}
}

