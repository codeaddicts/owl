using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using Gdk;

namespace owledit
{
	public static class DocumentManager
	{
		private static List<OwlDocument> documents;
		private static List<TabSetup> tabs;

		private static OwlDocument current;
		public static OwlDocument Current
		{
			get {
				return current;
			}
			set {
				current = value;
			}
		}

		static DocumentManager ()
		{
			documents = new List<OwlDocument> ();
			tabs = new List<TabSetup> ();
		}

		public static void AddDocument (string path)
		{
			if (documents.Any (d => d.path == Path.GetFullPath (path)))
				return;

			OwlDocument doc = new OwlDocument () {
				name = Path.GetFileName (path),
				path = Path.GetFullPath (path),
			};
			AddDocument (doc);
		}

		public static void AddEmptyDocument ()
		{
			AddDocument (new OwlDocument () {
				name = "Unnamed",
				path = null,
				saved = false,
				guid = null,
				source = null,
			});
		}

		public static void AddDocument (OwlDocument doc)
		{
			if (!doc.hasGuidSet ())
				doc.setGuid ();

			documents.Add (doc);

			TabSetup setup = new TabSetup (doc.name);
			setup.setGuid (doc.guid);
			tabs.Add (setup);

			Notebook notebook = Program.window.getNotebook ();
			notebook.AppendPage (setup.window, setup.label);
			notebook.ShowAll ();

			setup.view.Buffer.Changed += (object sender, EventArgs e) => {
				TabSetup ts = DocumentManager.getTabSetupByComparison (setup.view);
				if (ts != default(TabSetup))
				{
					OwlDocument xdoc = DocumentManager.getDocumentByGuid (ts.owlDocumentGuid);
					if (xdoc != default(OwlDocument))
					{
						xdoc.updateSource (setup.view.Buffer.Text);
						setup.label.Text = xdoc.name + " *";
						owlInvoker.CheckErrors ();
					}
				}
			};
		}

		public static void NotifyQuit ()
		{
			documents.ForEach (doc => {
				if (doc.hasUnsafedChanges ())
					PromptSave (doc);
			});
		}

		public static void PromptSave (OwlDocument doc)
		{
			string msg = "The file <b>{0}</b> has <b>unsafed changes</b>!\nWould you like to save the file now?";
			DialogFlags dflags = DialogFlags.Modal | DialogFlags.DestroyWithParent;
			MessageType mtype = MessageType.Question;
			ButtonsType btntype = ButtonsType.YesNo;
			bool use_markup = true;
			MessageDialog dialog =
				new MessageDialog (null, dflags, mtype, btntype, use_markup, msg, doc.name);

			ResponseType response = ResponseType.None;
			response = (ResponseType)dialog.Run ();

			if (response == ResponseType.Yes)
			{
				if (doc.hasPathSet ())
					SaveFile (doc);
				else
					PromptSaveFileDialog (doc);
			}

			dialog.Destroy ();
		}

		public static void PromptSaveFileDialog (OwlDocument doc)
		{
			string title = "Save file";
			FileChooserAction action = FileChooserAction.Save;
			FileChooserDialog fsdialog = new FileChooserDialog (title, null, action);
			fsdialog.AddButton ("Save file", ResponseType.Ok);
			fsdialog.AddButton ("Cancel", ResponseType.Cancel);
			fsdialog.SelectMultiple = false;

			ResponseType response = ResponseType.None;
			response = (ResponseType)fsdialog.Run ();

			if (response == ResponseType.Ok)
			{
				doc.path = Path.GetFullPath (fsdialog.Filename);
				doc.name = Path.GetFileName (doc.path);
				SaveFile (doc);
			}

			fsdialog.Destroy ();
		}

		public static void SaveFile (OwlDocument doc)
		{

		}

		public static void CreateNew ()
		{
			OwlDocument doc = new OwlDocument ();
			doc.name = "Unnamed";
			AddDocument (doc);
		}

		public static OwlDocument getDocumentByGuid (string guid)
		{
			return documents.FirstOrDefault (doc => doc.guid == guid);
		}

		public static TabSetup getTabSetupByComparison (TextView view)
		{
			return tabs.FirstOrDefault (tab => tab.compareTextView (view));
		}

		public static OwlDocument getFirstDocument ()
		{
			return documents.First ();
		}
	}
}

