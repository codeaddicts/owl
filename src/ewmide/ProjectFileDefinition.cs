using System;

namespace ewmide
{
	public class ProjectFileDefinition
	{
		/// <summary>
		/// The filename without extension.
		/// </summary>
		public string FileName;

		/// <summary>
		/// The full path to the file.
		/// </summary>
		public string Path;

		/// <summary>
		/// Is the File currently opened
		/// </summary>
		public bool IsOpened;

		public ProjectFileDefinition ()
		{
		}
	}
}

