
// This file has been generated by the GUI designer. Do not modify.
namespace TraceLab.UI.GTK
{
	public partial class NewExperimentDialog
	{
		private global::Gtk.Table table1;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TextView tbx_description;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Entry tbx_dirPath;
		private global::Gtk.VSeparator vseparator1;
		private global::Gtk.Button btn_selectDir;
		private global::Gtk.Label label4;
		private global::Gtk.Label label5;
		private global::Gtk.Label label6;
		private global::Gtk.Label label7;
		private global::Gtk.Label lbl_ExpName;
		private global::Gtk.Entry tbx_author;
		private global::Gtk.Entry tbx_experimentName;
		private global::Gtk.Entry tbx_fileName;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button btn_ok;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget TraceLab.UI.GTK.NewExperimentDialog
			this.Name = "TraceLab.UI.GTK.NewExperimentDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("New Experimant");
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-new", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			// Internal child TraceLab.UI.GTK.NewExperimentDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(5)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.tbx_description = new global::Gtk.TextView ();
			this.tbx_description.HeightRequest = 132;
			this.tbx_description.CanFocus = true;
			this.tbx_description.Name = "tbx_description";
			this.tbx_description.WrapMode = ((global::Gtk.WrapMode)(2));
			this.GtkScrolledWindow.Add (this.tbx_description);
			this.table1.Add (this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.GtkScrolledWindow]));
			w3.TopAttach = ((uint)(4));
			w3.BottomAttach = ((uint)(5));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.tbx_dirPath = new global::Gtk.Entry ();
			this.tbx_dirPath.CanFocus = true;
			this.tbx_dirPath.Name = "tbx_dirPath";
			this.tbx_dirPath.IsEditable = false;
			this.tbx_dirPath.InvisibleChar = '•';
			this.hbox1.Add (this.tbx_dirPath);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.tbx_dirPath]));
			w4.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator ();
			this.vseparator1.Name = "vseparator1";
			this.hbox1.Add (this.vseparator1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vseparator1]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.btn_selectDir = new global::Gtk.Button ();
			this.btn_selectDir.CanFocus = true;
			this.btn_selectDir.Name = "btn_selectDir";
			this.btn_selectDir.UseUnderline = true;
			// Container child btn_selectDir.Gtk.Container+ContainerChild
			global::Gtk.Alignment w6 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w7 = new global::Gtk.HBox ();
			w7.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-open", global::Gtk.IconSize.Menu);
			w7.Add (w8);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w10 = new global::Gtk.Label ();
			w10.LabelProp = global::Mono.Unix.Catalog.GetString ("Select");
			w10.UseUnderline = true;
			w7.Add (w10);
			w6.Add (w7);
			this.btn_selectDir.Add (w6);
			this.hbox1.Add (this.btn_selectDir);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.btn_selectDir]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			this.table1.Add (this.hbox1);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox1]));
			w15.TopAttach = ((uint)(1));
			w15.BottomAttach = ((uint)(2));
			w15.LeftAttach = ((uint)(1));
			w15.RightAttach = ((uint)(2));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Directory path:");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w16.TopAttach = ((uint)(1));
			w16.BottomAttach = ((uint)(2));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("File name:");
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
			w17.TopAttach = ((uint)(2));
			w17.BottomAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Author:");
			this.table1.Add (this.label6);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1 [this.label6]));
			w18.TopAttach = ((uint)(3));
			w18.BottomAttach = ((uint)(4));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Description:");
			this.table1.Add (this.label7);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1 [this.label7]));
			w19.TopAttach = ((uint)(4));
			w19.BottomAttach = ((uint)(5));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lbl_ExpName = new global::Gtk.Label ();
			this.lbl_ExpName.Name = "lbl_ExpName";
			this.lbl_ExpName.LabelProp = global::Mono.Unix.Catalog.GetString ("Experiment name:");
			this.table1.Add (this.lbl_ExpName);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1 [this.lbl_ExpName]));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.tbx_author = new global::Gtk.Entry ();
			this.tbx_author.CanFocus = true;
			this.tbx_author.Name = "tbx_author";
			this.tbx_author.IsEditable = true;
			this.tbx_author.InvisibleChar = '•';
			this.table1.Add (this.tbx_author);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1 [this.tbx_author]));
			w21.TopAttach = ((uint)(3));
			w21.BottomAttach = ((uint)(4));
			w21.LeftAttach = ((uint)(1));
			w21.RightAttach = ((uint)(2));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.tbx_experimentName = new global::Gtk.Entry ();
			this.tbx_experimentName.CanFocus = true;
			this.tbx_experimentName.Name = "tbx_experimentName";
			this.tbx_experimentName.IsEditable = true;
			this.tbx_experimentName.InvisibleChar = '•';
			this.table1.Add (this.tbx_experimentName);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1 [this.tbx_experimentName]));
			w22.LeftAttach = ((uint)(1));
			w22.RightAttach = ((uint)(2));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.tbx_fileName = new global::Gtk.Entry ();
			this.tbx_fileName.CanFocus = true;
			this.tbx_fileName.Name = "tbx_fileName";
			this.tbx_fileName.IsEditable = true;
			this.tbx_fileName.InvisibleChar = '•';
			this.table1.Add (this.tbx_fileName);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1 [this.tbx_fileName]));
			w23.TopAttach = ((uint)(2));
			w23.BottomAttach = ((uint)(3));
			w23.LeftAttach = ((uint)(1));
			w23.RightAttach = ((uint)(2));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table1);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(w1 [this.table1]));
			w24.Position = 0;
			// Internal child TraceLab.UI.GTK.NewExperimentDialog.ActionArea
			global::Gtk.HButtonBox w25 = this.ActionArea;
			w25.Name = "dialog1_ActionArea";
			w25.Spacing = 10;
			w25.BorderWidth = ((uint)(5));
			w25.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			// Container child buttonCancel.Gtk.Container+ContainerChild
			global::Gtk.Alignment w26 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w27 = new global::Gtk.HBox ();
			w27.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w28 = new global::Gtk.Image ();
			w28.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w27.Add (w28);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w30 = new global::Gtk.Label ();
			w30.LabelProp = global::Mono.Unix.Catalog.GetString ("_Cancel");
			w30.UseUnderline = true;
			w27.Add (w30);
			w26.Add (w27);
			this.buttonCancel.Add (w26);
			this.AddActionWidget (this.buttonCancel, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w34 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w25 [this.buttonCancel]));
			w34.Expand = false;
			w34.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.btn_ok = new global::Gtk.Button ();
			this.btn_ok.Sensitive = false;
			this.btn_ok.CanDefault = true;
			this.btn_ok.CanFocus = true;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.UseUnderline = true;
			// Container child btn_ok.Gtk.Container+ContainerChild
			global::Gtk.Alignment w35 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w36 = new global::Gtk.HBox ();
			w36.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w37 = new global::Gtk.Image ();
			w37.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w36.Add (w37);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w39 = new global::Gtk.Label ();
			w39.LabelProp = global::Mono.Unix.Catalog.GetString ("Create New");
			w39.UseUnderline = true;
			w36.Add (w39);
			w35.Add (w36);
			this.btn_ok.Add (w35);
			this.AddActionWidget (this.btn_ok, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w43 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w25 [this.btn_ok]));
			w43.Position = 1;
			w43.Expand = false;
			w43.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 715;
			this.DefaultHeight = 342;
			this.Show ();
			this.tbx_experimentName.TextDeleted += new global::Gtk.TextDeletedHandler (this.updateProjectName);
			this.tbx_experimentName.TextInserted += new global::Gtk.TextInsertedHandler (this.updateProjectName);
			this.tbx_experimentName.Changed += new global::System.EventHandler (this.OnTbxExperimentNameChanged);
			this.btn_selectDir.Clicked += new global::System.EventHandler (this.buttonSelectDirClickedHandler);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.buttonCancelClickedHandler);
			this.btn_ok.Clicked += new global::System.EventHandler (this.buttonOKClickedHandler);
		}
	}
}
