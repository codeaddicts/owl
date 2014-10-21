
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action FileAction;
	
	private global::Gtk.Action NewFileAction;
	
	private global::Gtk.Action OpenFileAction;
	
	private global::Gtk.Action SaveAction;
	
	private global::Gtk.Action SaveAsAction;
	
	private global::Gtk.Action BuildAction;
	
	private global::Gtk.Action InvokeOwlAction;
	
	private global::Gtk.Action GenerateHtmlAsAction;
	
	private global::Gtk.Action ToolsAction;
	
	private global::Gtk.Action PreviewAction;
	
	private global::Gtk.Action BuildAction1;
	
	private global::Gtk.Action BuildAndPreviewAction;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::Gtk.Notebook notebook1;
	
	private global::Gtk.Label label1;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.NewFileAction = new global::Gtk.Action ("NewFileAction", global::Mono.Unix.Catalog.GetString ("New File"), null, null);
		this.NewFileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New File");
		w1.Add (this.NewFileAction, null);
		this.OpenFileAction = new global::Gtk.Action ("OpenFileAction", global::Mono.Unix.Catalog.GetString ("Open File"), null, null);
		this.OpenFileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open File");
		w1.Add (this.OpenFileAction, null);
		this.SaveAction = new global::Gtk.Action ("SaveAction", global::Mono.Unix.Catalog.GetString ("Save"), null, null);
		this.SaveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save");
		w1.Add (this.SaveAction, null);
		this.SaveAsAction = new global::Gtk.Action ("SaveAsAction", global::Mono.Unix.Catalog.GetString ("Save as..."), null, null);
		this.SaveAsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save as...");
		w1.Add (this.SaveAsAction, null);
		this.BuildAction = new global::Gtk.Action ("BuildAction", global::Mono.Unix.Catalog.GetString ("Build"), null, null);
		this.BuildAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build");
		w1.Add (this.BuildAction, null);
		this.InvokeOwlAction = new global::Gtk.Action ("InvokeOwlAction", global::Mono.Unix.Catalog.GetString ("Invoke owl"), null, null);
		this.InvokeOwlAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Generate html");
		w1.Add (this.InvokeOwlAction, null);
		this.GenerateHtmlAsAction = new global::Gtk.Action ("GenerateHtmlAsAction", global::Mono.Unix.Catalog.GetString ("Generate html as..."), null, null);
		this.GenerateHtmlAsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Generate html as...");
		w1.Add (this.GenerateHtmlAsAction, null);
		this.ToolsAction = new global::Gtk.Action ("ToolsAction", global::Mono.Unix.Catalog.GetString ("Tools"), null, null);
		this.ToolsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Tools");
		w1.Add (this.ToolsAction, null);
		this.PreviewAction = new global::Gtk.Action ("PreviewAction", global::Mono.Unix.Catalog.GetString ("Preview"), null, null);
		this.PreviewAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Preview");
		w1.Add (this.PreviewAction, null);
		this.BuildAction1 = new global::Gtk.Action ("BuildAction1", global::Mono.Unix.Catalog.GetString ("Build"), null, null);
		this.BuildAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build");
		w1.Add (this.BuildAction1, null);
		this.BuildAndPreviewAction = new global::Gtk.Action ("BuildAndPreviewAction", global::Mono.Unix.Catalog.GetString ("Build and Preview"), null, null);
		this.BuildAndPreviewAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build and Preview");
		w1.Add (this.BuildAndPreviewAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("owledit");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString (@"<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='NewFileAction' action='NewFileAction'/><menuitem name='OpenFileAction' action='OpenFileAction'/><menuitem name='SaveAction' action='SaveAction'/><menuitem name='SaveAsAction' action='SaveAsAction'/></menu><menu name='ToolsAction' action='ToolsAction'><menuitem name='BuildAction1' action='BuildAction1'/><menuitem name='BuildAndPreviewAction' action='BuildAndPreviewAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.notebook1 = new global::Gtk.Notebook ();
		this.notebook1.CanFocus = true;
		this.notebook1.Name = "notebook1";
		this.notebook1.CurrentPage = 0;
		// Notebook tab
		global::Gtk.Label w3 = new global::Gtk.Label ();
		w3.Visible = true;
		this.notebook1.Add (w3);
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("untitled");
		this.notebook1.SetTabLabel (w3, this.label1);
		this.label1.ShowAll ();
		this.vbox1.Add (this.notebook1);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.notebook1]));
		w4.Position = 1;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 443;
		this.DefaultHeight = 300;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}