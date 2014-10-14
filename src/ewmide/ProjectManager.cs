using System;
using Gtk;

namespace ewmide
{
	public static class ProjectManager
	{
		public static void CreateNewProject ()
		{
			NewProjectDialog npd = new NewProjectDialog ();
			ResponseType response;
			response = (ResponseType) npd.Run ();
		}
	}
}

