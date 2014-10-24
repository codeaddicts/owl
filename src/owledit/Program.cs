using System;
using System.IO;
using Gtk;

namespace owledit
{
	public class Program
	{
		public static MainWindow window;
		public static void Main (string[] args)
		{
			Application.Init ();
			window = new MainWindow ();
			window.Show ();

			bool initializedNotebook = false;
			if (args.Length == 1)
			{
				if (File.Exists (args[0]))
				{
					DocumentManager.AddDocument (Path.GetFullPath (args[0]));
					initializedNotebook = true;
				}
			}
			if (!initializedNotebook)
			{
				DocumentManager.AddEmptyDocument ();
			}
			DocumentManager.Current = DocumentManager.getFirstDocument ();

			Application.Run ();
		}
	}
}
