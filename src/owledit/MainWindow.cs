using System;
using System.Linq;
using Gtk;
using Gdk;
using owledit;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	public void setStatusText (string text)
	{
		txtStatus.Buffer.Text = text;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void btnQuit_Activated (object sender, EventArgs e)
	{
		DocumentManager.NotifyQuit ();
		Application.Quit ();
	}

	protected void OnBtnNewFileActivated (object sender, EventArgs e)
	{
		DocumentManager.CreateNew ();
	}

	public Notebook getNotebook ()
	{
		return this.tabSource;
	}

	protected void OnTabSourceChangeCurrentPage (object o, ChangeCurrentPageArgs args)
	{
		TextView view = (TextView)tabSource.Children.FirstOrDefault (w => w.GetType () == typeof(TextView));
		if (view != default(Widget))
		{
			TabSetup ts = DocumentManager.getTabSetupByComparison (view);
			if (ts != default(TabSetup))
			{
				OwlDocument xdoc = DocumentManager.getDocumentByGuid (ts.owlDocumentGuid);
				if (xdoc != default(OwlDocument))
				{
					DocumentManager.Current = xdoc;
				}
			}
		}
	}
}
