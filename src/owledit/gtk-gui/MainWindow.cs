
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action FileAction;
	
	private global::Gtk.Action btnNewFile;
	
	private global::Gtk.Action btnOpenFile;
	
	private global::Gtk.Action btnSaveFile;
	
	private global::Gtk.Action btnSaveFileAs;
	
	private global::Gtk.Action btnExit;
	
	private global::Gtk.Action BuildAction;
	
	private global::Gtk.Action mediaPlayAction;
	
	private global::Gtk.Action btnPreview;
	
	private global::Gtk.Action removeAction;
	
	private global::Gtk.Action btnQuit;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.HBox hbox1;
	
	private global::Gtk.MenuBar menu;
	
	private global::Gtk.VBox vbox2;
	
	private global::Gtk.Notebook tabSource;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TextView txtStatus;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.btnNewFile = new global::Gtk.Action ("btnNewFile", global::Mono.Unix.Catalog.GetString ("New"), null, "gtk-new");
		this.btnNewFile.ShortLabel = global::Mono.Unix.Catalog.GetString ("New");
		w1.Add (this.btnNewFile, null);
		this.btnOpenFile = new global::Gtk.Action ("btnOpenFile", global::Mono.Unix.Catalog.GetString ("Open"), null, "gtk-open");
		this.btnOpenFile.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open");
		w1.Add (this.btnOpenFile, null);
		this.btnSaveFile = new global::Gtk.Action ("btnSaveFile", global::Mono.Unix.Catalog.GetString ("Save"), null, "gtk-save");
		this.btnSaveFile.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save");
		w1.Add (this.btnSaveFile, null);
		this.btnSaveFileAs = new global::Gtk.Action ("btnSaveFileAs", global::Mono.Unix.Catalog.GetString ("Save as..."), null, "gtk-save-as");
		this.btnSaveFileAs.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save as...");
		w1.Add (this.btnSaveFileAs, null);
		this.btnExit = new global::Gtk.Action ("btnExit", null, null, "gtk-quit");
		this.btnExit.ShortLabel = global::Mono.Unix.Catalog.GetString ("Exit");
		w1.Add (this.btnExit, null);
		this.BuildAction = new global::Gtk.Action ("BuildAction", global::Mono.Unix.Catalog.GetString ("Build"), null, null);
		this.BuildAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build");
		w1.Add (this.BuildAction, null);
		this.mediaPlayAction = new global::Gtk.Action ("mediaPlayAction", global::Mono.Unix.Catalog.GetString ("Build"), null, "gtk-media-play");
		this.mediaPlayAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build");
		w1.Add (this.mediaPlayAction, null);
		this.btnPreview = new global::Gtk.Action ("btnPreview", global::Mono.Unix.Catalog.GetString ("Preview"), null, "gtk-media-play");
		this.btnPreview.ShortLabel = global::Mono.Unix.Catalog.GetString ("Build & Preview");
		w1.Add (this.btnPreview, null);
		this.removeAction = new global::Gtk.Action ("removeAction", global::Mono.Unix.Catalog.GetString ("Close"), null, "gtk-remove");
		this.removeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Close");
		w1.Add (this.removeAction, null);
		this.btnQuit = new global::Gtk.Action ("btnQuit", global::Mono.Unix.Catalog.GetString ("Quit"), null, "gtk-quit");
		this.btnQuit.ShortLabel = global::Mono.Unix.Catalog.GetString ("Quit");
		w1.Add (this.btnQuit, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("owl editor");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menu'><menu name='FileAction' action='FileAction'><menuitem name='btnNewFile' action='btnNewFile'/><menuitem name='btnOpenFile' action='btnOpenFile'/><menuitem name='btnSaveFile' action='btnSaveFile'/><menuitem name='btnSaveFileAs' action='btnSaveFileAs'/><menuitem name='btnQuit' action='btnQuit'/></menu><menu name='BuildAction' action='BuildAction'><menuitem name='mediaPlayAction' action='mediaPlayAction'/><menuitem name='btnPreview' action='btnPreview'/></menu></menubar></ui>");
		this.menu = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menu")));
		this.menu.Name = "menu";
		this.hbox1.Add (this.menu);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.menu]));
		w2.Position = 0;
		this.vbox1.Add (this.hbox1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.tabSource = new global::Gtk.Notebook ();
		this.tabSource.CanFocus = true;
		this.tabSource.Name = "tabSource";
		this.tabSource.CurrentPage = -1;
		this.tabSource.Scrollable = true;
		this.vbox2.Add (this.tabSource);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.tabSource]));
		w4.Position = 0;
		// Container child vbox2.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.txtStatus = new global::Gtk.TextView ();
		this.txtStatus.Name = "txtStatus";
		this.txtStatus.Editable = false;
		this.txtStatus.CursorVisible = false;
		this.txtStatus.WrapMode = ((global::Gtk.WrapMode)(2));
		this.GtkScrolledWindow.Add (this.txtStatus);
		this.vbox2.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
		w6.PackType = ((global::Gtk.PackType)(1));
		w6.Position = 1;
		w6.Expand = false;
		this.vbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox2]));
		w7.PackType = ((global::Gtk.PackType)(1));
		w7.Position = 1;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 460;
		this.DefaultHeight = 424;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.btnNewFile.Activated += new global::System.EventHandler (this.OnBtnNewFileActivated);
		this.btnQuit.Activated += new global::System.EventHandler (this.btnQuit_Activated);
		this.tabSource.ChangeCurrentPage += new global::Gtk.ChangeCurrentPageHandler (this.OnTabSourceChangeCurrentPage);
	}
}
