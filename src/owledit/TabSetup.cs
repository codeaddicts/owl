using System;
using Gtk;
using Gdk;

namespace owledit
{
	public class TabSetup
	{
		public Label label;
		public ScrolledWindow window;
		public TextView view;
		public string owlDocumentGuid;

		public TabSetup (string text = "Unnamed")
		{
			label = new Label ();
			label.Text = text;

			window = new ScrolledWindow ();
			view = new TextView ();
			window.Add (view);

			owlDocumentGuid = null;
		}

		public bool compareWindow (ScrolledWindow win)
		{
			if (window == win)
				return true;
			return false;
		}

		public bool compareTextView (TextView obj)
		{
			if (view == obj)
				return true;
			return false;
		}

		public void setText (string text)
		{
			label.Text = text;
		}

		public bool hasGuidSet ()
		{
			if (owlDocumentGuid == null || owlDocumentGuid == "")
				return false;
			return true;
		}

		public void setGuid (string guid)
		{
			owlDocumentGuid = guid;
		}

		public OwlDocument getOwlDocument ()
		{
			return DocumentManager.getDocumentByGuid (owlDocumentGuid);
		}
	}
}

