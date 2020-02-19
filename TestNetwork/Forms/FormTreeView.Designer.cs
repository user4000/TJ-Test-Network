namespace TestNetwork
{
    partial class FormTreeView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTreeView));
      this.TvFolders = new Telerik.WinControls.UI.RadTreeView();
      this.ImageListFolders = new System.Windows.Forms.ImageList(this.components);
      this.PvFolders = new Telerik.WinControls.UI.RadPageView();
      this.PgAdd = new Telerik.WinControls.UI.RadPageViewPage();
      this.TxFolderName = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxFolderAdd = new Telerik.WinControls.UI.RadImageButtonElement();
      this.PgRename = new Telerik.WinControls.UI.RadPageViewPage();
      this.TxFolderRename = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxFolderRename = new Telerik.WinControls.UI.RadImageButtonElement();
      this.PgDelete = new Telerik.WinControls.UI.RadPageViewPage();
      this.TxFolderDelete = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxFolderDelete = new Telerik.WinControls.UI.RadImageButtonElement();
      this.PgSearch = new Telerik.WinControls.UI.RadPageViewPage();
      this.TxFolderSearch = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxFolderSearch = new Telerik.WinControls.UI.RadImageButtonElement();
      this.DialogOpenFile = new Telerik.WinControls.UI.RadOpenFileDialog();
      this.TxDatabaseFile = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxSelectFile = new Telerik.WinControls.UI.RadImageButtonElement();
      this.BxOpenFile = new Telerik.WinControls.UI.RadImageButtonElement();
      this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
      this.PnUpper = new Telerik.WinControls.UI.RadPanel();
      this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
      this.ScMain = new Telerik.WinControls.UI.RadSplitContainer();
      this.PnTreeview = new Telerik.WinControls.UI.SplitPanel();
      this.PnCommonSettings = new Telerik.WinControls.UI.SplitPanel();
      this.ScSettings = new Telerik.WinControls.UI.RadSplitContainer();
      this.PnSettings = new Telerik.WinControls.UI.SplitPanel();
      this.PnEditSettings = new Telerik.WinControls.UI.SplitPanel();
      this.BxFolderSearchGotoNext = new Telerik.WinControls.UI.RadImageButtonElement();
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PvFolders)).BeginInit();
      this.PvFolders.SuspendLayout();
      this.PgAdd.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderName)).BeginInit();
      this.PgRename.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderRename)).BeginInit();
      this.PgDelete.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderDelete)).BeginInit();
      this.PgSearch.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderSearch)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxDatabaseFile)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnUpper)).BeginInit();
      this.PnUpper.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
      this.ScMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeview)).BeginInit();
      this.PnTreeview.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PnCommonSettings)).BeginInit();
      this.PnCommonSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ScSettings)).BeginInit();
      this.ScSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PnSettings)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnEditSettings)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // TvFolders
      // 
      this.TvFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
      this.TvFolders.Cursor = System.Windows.Forms.Cursors.Default;
      this.TvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TvFolders.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TvFolders.ForeColor = System.Drawing.Color.Black;
      this.TvFolders.ItemHeight = 34;
      this.TvFolders.LineStyle = Telerik.WinControls.UI.TreeLineStyle.Solid;
      this.TvFolders.Location = new System.Drawing.Point(0, 78);
      this.TvFolders.Name = "TvFolders";
      this.TvFolders.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.TvFolders.ShowLines = true;
      this.TvFolders.Size = new System.Drawing.Size(416, 622);
      this.TvFolders.SpacingBetweenNodes = -1;
      this.TvFolders.TabIndex = 0;
      this.TvFolders.TreeIndent = 34;
      // 
      // ImageListFolders
      // 
      this.ImageListFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListFolders.ImageStream")));
      this.ImageListFolders.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageListFolders.Images.SetKeyName(0, "a1.png");
      this.ImageListFolders.Images.SetKeyName(1, "a2_1.png");
      this.ImageListFolders.Images.SetKeyName(2, "a3.png");
      this.ImageListFolders.Images.SetKeyName(3, "a4.png");
      this.ImageListFolders.Images.SetKeyName(4, "a5.png");
      this.ImageListFolders.Images.SetKeyName(5, "a6.png");
      this.ImageListFolders.Images.SetKeyName(6, "a7.png");
      this.ImageListFolders.Images.SetKeyName(7, "a8.png");
      this.ImageListFolders.Images.SetKeyName(8, "a9.png");
      this.ImageListFolders.Images.SetKeyName(9, "a10.png");
      this.ImageListFolders.Images.SetKeyName(10, "a11.png");
      this.ImageListFolders.Images.SetKeyName(11, "a12.png");
      this.ImageListFolders.Images.SetKeyName(12, "a13.png");
      this.ImageListFolders.Images.SetKeyName(13, "a14.png");
      this.ImageListFolders.Images.SetKeyName(14, "a15.png");
      this.ImageListFolders.Images.SetKeyName(15, "a16.png");
      this.ImageListFolders.Images.SetKeyName(16, "a17.png");
      this.ImageListFolders.Images.SetKeyName(17, "a18.png");
      this.ImageListFolders.Images.SetKeyName(18, "a19.png");
      // 
      // PvFolders
      // 
      this.PvFolders.Controls.Add(this.PgAdd);
      this.PvFolders.Controls.Add(this.PgRename);
      this.PvFolders.Controls.Add(this.PgDelete);
      this.PvFolders.Controls.Add(this.PgSearch);
      this.PvFolders.Dock = System.Windows.Forms.DockStyle.Top;
      this.PvFolders.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.PvFolders.ItemSize = new System.Drawing.Size(70, 25);
      this.PvFolders.ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
      this.PvFolders.Location = new System.Drawing.Point(0, 0);
      this.PvFolders.Name = "PvFolders";
      this.PvFolders.SelectedPage = this.PgSearch;
      this.PvFolders.Size = new System.Drawing.Size(416, 78);
      this.PvFolders.TabIndex = 0;
      ((Telerik.WinControls.UI.RadPageViewStripElement)(this.PvFolders.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.ItemList;
      ((Telerik.WinControls.UI.RadPageViewStripElement)(this.PvFolders.GetChildAt(0))).ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
      // 
      // PgAdd
      // 
      this.PgAdd.Controls.Add(this.TxFolderName);
      this.PgAdd.ItemSize = new System.Drawing.SizeF(70F, 25F);
      this.PgAdd.Location = new System.Drawing.Point(10, 34);
      this.PgAdd.Name = "PgAdd";
      this.PgAdd.Padding = new System.Windows.Forms.Padding(5);
      this.PgAdd.Size = new System.Drawing.Size(395, 33);
      this.PgAdd.Text = "Add";
      this.PgAdd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TxFolderName
      // 
      this.TxFolderName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderName.Location = new System.Drawing.Point(5, 5);
      this.TxFolderName.Name = "TxFolderName";
      this.TxFolderName.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxFolderAdd});
      this.TxFolderName.Size = new System.Drawing.Size(385, 23);
      this.TxFolderName.TabIndex = 5;
      // 
      // BxFolderAdd
      // 
      this.BxFolderAdd.Image = ((System.Drawing.Image)(resources.GetObject("BxFolderAdd.Image")));
      this.BxFolderAdd.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxFolderAdd.Name = "BxFolderAdd";
      this.BxFolderAdd.Text = "add new folder";
      this.BxFolderAdd.ToolTipText = "Add a new folder";
      this.BxFolderAdd.UseCompatibleTextRendering = false;
      // 
      // PgRename
      // 
      this.PgRename.Controls.Add(this.TxFolderRename);
      this.PgRename.ItemSize = new System.Drawing.SizeF(70F, 25F);
      this.PgRename.Location = new System.Drawing.Point(10, 34);
      this.PgRename.Name = "PgRename";
      this.PgRename.Padding = new System.Windows.Forms.Padding(5);
      this.PgRename.Size = new System.Drawing.Size(395, 33);
      this.PgRename.Text = "Rename";
      this.PgRename.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TxFolderRename
      // 
      this.TxFolderRename.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderRename.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderRename.Location = new System.Drawing.Point(5, 5);
      this.TxFolderRename.Name = "TxFolderRename";
      this.TxFolderRename.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxFolderRename});
      this.TxFolderRename.Size = new System.Drawing.Size(385, 23);
      this.TxFolderRename.TabIndex = 6;
      // 
      // BxFolderRename
      // 
      this.BxFolderRename.Image = ((System.Drawing.Image)(resources.GetObject("BxFolderRename.Image")));
      this.BxFolderRename.ImageIndexClicked = 0;
      this.BxFolderRename.ImageIndexHovered = 0;
      this.BxFolderRename.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxFolderRename.Name = "BxFolderRename";
      this.BxFolderRename.Text = "";
      this.BxFolderRename.ToolTipText = "Type new name and press the button";
      this.BxFolderRename.UseCompatibleTextRendering = false;
      // 
      // PgDelete
      // 
      this.PgDelete.Controls.Add(this.TxFolderDelete);
      this.PgDelete.ItemSize = new System.Drawing.SizeF(70F, 25F);
      this.PgDelete.Location = new System.Drawing.Point(10, 34);
      this.PgDelete.Name = "PgDelete";
      this.PgDelete.Padding = new System.Windows.Forms.Padding(5);
      this.PgDelete.Size = new System.Drawing.Size(395, 33);
      this.PgDelete.Text = "Delete";
      this.PgDelete.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TxFolderDelete
      // 
      this.TxFolderDelete.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderDelete.Location = new System.Drawing.Point(5, 5);
      this.TxFolderDelete.Name = "TxFolderDelete";
      this.TxFolderDelete.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxFolderDelete});
      this.TxFolderDelete.Size = new System.Drawing.Size(385, 23);
      this.TxFolderDelete.TabIndex = 6;
      // 
      // BxFolderDelete
      // 
      this.BxFolderDelete.Image = ((System.Drawing.Image)(resources.GetObject("BxFolderDelete.Image")));
      this.BxFolderDelete.ImageIndexClicked = 0;
      this.BxFolderDelete.ImageIndexHovered = 0;
      this.BxFolderDelete.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxFolderDelete.Name = "BxFolderDelete";
      this.BxFolderDelete.Text = "Delete";
      this.BxFolderDelete.ToolTipText = "Delete folder";
      this.BxFolderDelete.UseCompatibleTextRendering = false;
      // 
      // PgSearch
      // 
      this.PgSearch.Controls.Add(this.TxFolderSearch);
      this.PgSearch.ItemSize = new System.Drawing.SizeF(70F, 25F);
      this.PgSearch.Location = new System.Drawing.Point(10, 34);
      this.PgSearch.Name = "PgSearch";
      this.PgSearch.Padding = new System.Windows.Forms.Padding(5);
      this.PgSearch.Size = new System.Drawing.Size(395, 33);
      this.PgSearch.Text = "Search";
      this.PgSearch.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TxFolderSearch
      // 
      this.TxFolderSearch.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderSearch.LeftButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxFolderSearchGotoNext});
      this.TxFolderSearch.Location = new System.Drawing.Point(5, 5);
      this.TxFolderSearch.Name = "TxFolderSearch";
      this.TxFolderSearch.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxFolderSearch});
      this.TxFolderSearch.Size = new System.Drawing.Size(385, 23);
      this.TxFolderSearch.TabIndex = 7;
      // 
      // BxFolderSearch
      // 
      this.BxFolderSearch.Image = ((System.Drawing.Image)(resources.GetObject("BxFolderSearch.Image")));
      this.BxFolderSearch.ImageIndexClicked = 0;
      this.BxFolderSearch.ImageIndexHovered = 0;
      this.BxFolderSearch.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxFolderSearch.Name = "BxFolderSearch";
      this.BxFolderSearch.Text = "Search";
      this.BxFolderSearch.ToolTipText = "Search folder";
      this.BxFolderSearch.UseCompatibleTextRendering = false;
      // 
      // DialogOpenFile
      // 
      this.DialogOpenFile.InitialSelectedLayout = Telerik.WinControls.FileDialogs.LayoutType.MediumIcons;
      // 
      // TxDatabaseFile
      // 
      this.TxDatabaseFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxDatabaseFile.LeftButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxSelectFile});
      this.TxDatabaseFile.Location = new System.Drawing.Point(116, 11);
      this.TxDatabaseFile.Name = "TxDatabaseFile";
      this.TxDatabaseFile.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxOpenFile});
      this.TxDatabaseFile.Size = new System.Drawing.Size(428, 23);
      this.TxDatabaseFile.TabIndex = 5;
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxDatabaseFile.GetChildAt(0).GetChildAt(2).GetChildAt(2))).Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxDatabaseFile.GetChildAt(0).GetChildAt(2).GetChildAt(2))).Margin = new System.Windows.Forms.Padding(0);
      // 
      // BxSelectFile
      // 
      this.BxSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("BxSelectFile.Image")));
      this.BxSelectFile.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxSelectFile.Name = "BxSelectFile";
      this.BxSelectFile.Text = "Select a database file";
      this.BxSelectFile.ToolTipText = "Select a database file";
      // 
      // BxOpenFile
      // 
      this.BxOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("BxOpenFile.Image")));
      this.BxOpenFile.ImageIndexClicked = 0;
      this.BxOpenFile.ImageIndexHovered = 0;
      this.BxOpenFile.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxOpenFile.Name = "BxOpenFile";
      this.BxOpenFile.Text = "Open a database";
      this.BxOpenFile.ToolTipText = "Open a database";
      this.BxOpenFile.UseCompatibleTextRendering = false;
      // 
      // PnUpper
      // 
      this.PnUpper.Controls.Add(this.radLabel1);
      this.PnUpper.Controls.Add(this.TxDatabaseFile);
      this.PnUpper.Dock = System.Windows.Forms.DockStyle.Top;
      this.PnUpper.Location = new System.Drawing.Point(0, 0);
      this.PnUpper.Name = "PnUpper";
      this.PnUpper.Size = new System.Drawing.Size(1092, 45);
      this.PnUpper.TabIndex = 6;
      // 
      // radLabel1
      // 
      this.radLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.radLabel1.Location = new System.Drawing.Point(9, 13);
      this.radLabel1.Name = "radLabel1";
      this.radLabel1.Size = new System.Drawing.Size(95, 19);
      this.radLabel1.TabIndex = 6;
      this.radLabel1.Text = "Database file";
      // 
      // ScMain
      // 
      this.ScMain.Controls.Add(this.PnTreeview);
      this.ScMain.Controls.Add(this.PnCommonSettings);
      this.ScMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ScMain.Location = new System.Drawing.Point(0, 45);
      this.ScMain.Name = "ScMain";
      // 
      // 
      // 
      this.ScMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.ScMain.Size = new System.Drawing.Size(1092, 700);
      this.ScMain.TabIndex = 7;
      this.ScMain.TabStop = false;
      // 
      // PnTreeview
      // 
      this.PnTreeview.Controls.Add(this.TvFolders);
      this.PnTreeview.Controls.Add(this.PvFolders);
      this.PnTreeview.Location = new System.Drawing.Point(0, 0);
      this.PnTreeview.Name = "PnTreeview";
      // 
      // 
      // 
      this.PnTreeview.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.PnTreeview.Size = new System.Drawing.Size(416, 700);
      this.PnTreeview.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1176471F, 0F);
      this.PnTreeview.SizeInfo.MinimumSize = new System.Drawing.Size(350, 0);
      this.PnTreeview.SizeInfo.SplitterCorrection = new System.Drawing.Size(-128, 0);
      this.PnTreeview.TabIndex = 0;
      this.PnTreeview.TabStop = false;
      this.PnTreeview.Text = "splitPanel1";
      // 
      // PnCommonSettings
      // 
      this.PnCommonSettings.Controls.Add(this.ScSettings);
      this.PnCommonSettings.Location = new System.Drawing.Point(420, 0);
      this.PnCommonSettings.Name = "PnCommonSettings";
      // 
      // 
      // 
      this.PnCommonSettings.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.PnCommonSettings.Size = new System.Drawing.Size(672, 700);
      this.PnCommonSettings.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1176471F, 0F);
      this.PnCommonSettings.SizeInfo.SplitterCorrection = new System.Drawing.Size(128, 0);
      this.PnCommonSettings.TabIndex = 1;
      this.PnCommonSettings.TabStop = false;
      this.PnCommonSettings.Text = "splitPanel2";
      // 
      // ScSettings
      // 
      this.ScSettings.Controls.Add(this.PnSettings);
      this.ScSettings.Controls.Add(this.PnEditSettings);
      this.ScSettings.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ScSettings.Location = new System.Drawing.Point(0, 0);
      this.ScSettings.Name = "ScSettings";
      this.ScSettings.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // 
      // 
      this.ScSettings.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.ScSettings.Size = new System.Drawing.Size(672, 700);
      this.ScSettings.TabIndex = 8;
      this.ScSettings.TabStop = false;
      // 
      // PnSettings
      // 
      this.PnSettings.Location = new System.Drawing.Point(0, 0);
      this.PnSettings.Name = "PnSettings";
      // 
      // 
      // 
      this.PnSettings.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.PnSettings.Size = new System.Drawing.Size(672, 348);
      this.PnSettings.TabIndex = 0;
      this.PnSettings.TabStop = false;
      this.PnSettings.Text = "splitPanel3";
      // 
      // PnEditSettings
      // 
      this.PnEditSettings.Location = new System.Drawing.Point(0, 352);
      this.PnEditSettings.Name = "PnEditSettings";
      // 
      // 
      // 
      this.PnEditSettings.RootElement.MinSize = new System.Drawing.Size(25, 25);
      this.PnEditSettings.Size = new System.Drawing.Size(672, 348);
      this.PnEditSettings.TabIndex = 1;
      this.PnEditSettings.TabStop = false;
      this.PnEditSettings.Text = "splitPanel4";
      // 
      // BxFolderSearchGotoNext
      // 
      this.BxFolderSearchGotoNext.Image = ((System.Drawing.Image)(resources.GetObject("BxFolderSearchGotoNext.Image")));
      this.BxFolderSearchGotoNext.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxFolderSearchGotoNext.Name = "BxFolderSearchGotoNext";
      this.BxFolderSearchGotoNext.Text = "Search next";
      this.BxFolderSearchGotoNext.ToolTipText = "go to next";
      // 
      // FormTreeView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1092, 745);
      this.Controls.Add(this.ScMain);
      this.Controls.Add(this.PnUpper);
      this.Name = "FormTreeView";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "TreeView";
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PvFolders)).EndInit();
      this.PvFolders.ResumeLayout(false);
      this.PgAdd.ResumeLayout(false);
      this.PgAdd.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderName)).EndInit();
      this.PgRename.ResumeLayout(false);
      this.PgRename.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderRename)).EndInit();
      this.PgDelete.ResumeLayout(false);
      this.PgDelete.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderDelete)).EndInit();
      this.PgSearch.ResumeLayout(false);
      this.PgSearch.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderSearch)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxDatabaseFile)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnUpper)).EndInit();
      this.PnUpper.ResumeLayout(false);
      this.PnUpper.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
      this.ScMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeview)).EndInit();
      this.PnTreeview.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.PnCommonSettings)).EndInit();
      this.PnCommonSettings.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ScSettings)).EndInit();
      this.ScSettings.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.PnSettings)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnEditSettings)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

        }

    #endregion
    public Telerik.WinControls.UI.RadTreeView TvFolders;
    public System.Windows.Forms.ImageList ImageListFolders;
    internal Telerik.WinControls.UI.RadOpenFileDialog DialogOpenFile;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderName;
    private Telerik.WinControls.UI.RadImageButtonElement BxFolderAdd;
    private Telerik.WinControls.UI.RadButtonTextBox TxDatabaseFile;
    private Telerik.WinControls.UI.RadImageButtonElement BxSelectFile;
    private Telerik.WinControls.UI.RadImageButtonElement BxOpenFile;
    private Telerik.WinControls.UI.RadPageView PvFolders;
    private Telerik.WinControls.UI.RadPageViewPage PgAdd;
    private Telerik.WinControls.UI.RadPageViewPage PgRename;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderRename;
    private Telerik.WinControls.UI.RadImageButtonElement BxFolderRename;
    private Telerik.WinControls.UI.RadPageViewPage PgDelete;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderDelete;
    private Telerik.WinControls.UI.RadImageButtonElement BxFolderDelete;
    private Telerik.WinControls.RadThemeManager radThemeManager1;
    private Telerik.WinControls.UI.RadPanel PnUpper;
    private Telerik.WinControls.UI.RadSplitContainer ScMain;
    private Telerik.WinControls.UI.SplitPanel PnTreeview;
    private Telerik.WinControls.UI.SplitPanel PnCommonSettings;
    private Telerik.WinControls.UI.RadSplitContainer ScSettings;
    private Telerik.WinControls.UI.SplitPanel PnSettings;
    private Telerik.WinControls.UI.SplitPanel PnEditSettings;
    private Telerik.WinControls.UI.RadLabel radLabel1;
    private Telerik.WinControls.UI.RadPageViewPage PgSearch;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderSearch;
    private Telerik.WinControls.UI.RadImageButtonElement BxFolderSearch;
    private Telerik.WinControls.UI.RadImageButtonElement BxFolderSearchGotoNext;
  }
}
