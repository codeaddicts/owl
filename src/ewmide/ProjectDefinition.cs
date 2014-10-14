using System;
using System.Collections.Generic;

namespace ewmide
{
	public class ProjectDefinition
	{
		/// <summary>
		/// The name of the project.
		/// </summary>
		public string ProjectName;

		/// <summary>
		/// The full path to the project file.
		/// </summary>
		public string PathToProjectFile;

		/// <summary>
		/// A list of all files in the project.
		/// </summary>
		public List<ProjectFileDefinition> Files;

		public ProjectDefinition ()
		{

		}
	}
}

