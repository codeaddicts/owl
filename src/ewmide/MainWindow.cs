using System;
using Gtk;
using ewmide;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void btnNewProject_Clicked (object sender, EventArgs e)
	{
		ProjectManager.CreateNewProject ();
	}
}
