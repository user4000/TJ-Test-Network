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
      this.PnTreeview = new Telerik.WinControls.UI.RadPanel();
      this.PnTreeviewUpper = new Telerik.WinControls.UI.RadPanel();
      this.TxFolderName = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxAddFolder = new Telerik.WinControls.UI.RadImageButtonElement();
      this.DialogOpenFile = new Telerik.WinControls.UI.RadOpenFileDialog();
      this.TxDatabaseFile = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxSelectFile = new Telerik.WinControls.UI.RadImageButtonElement();
      this.BxOpenFile = new Telerik.WinControls.UI.RadImageButtonElement();
      this.PvFolders = new Telerik.WinControls.UI.RadPageView();
      this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
      this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
      this.radPageViewPage3 = new Telerik.WinControls.UI.RadPageViewPage();
      this.TxFolderRename = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxRenameFolder = new Telerik.WinControls.UI.RadImageButtonElement();
      this.TxFolderDelete = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxDeleteFolder = new Telerik.WinControls.UI.RadImageButtonElement();
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeview)).BeginInit();
      this.PnTreeview.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeviewUpper)).BeginInit();
      this.PnTreeviewUpper.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderName)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxDatabaseFile)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PvFolders)).BeginInit();
      this.PvFolders.SuspendLayout();
      this.radPageViewPage1.SuspendLayout();
      this.radPageViewPage2.SuspendLayout();
      this.radPageViewPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderRename)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderDelete)).BeginInit();
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
      this.TvFolders.Size = new System.Drawing.Size(400, 222);
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
      // PnTreeview
      // 
      this.PnTreeview.Controls.Add(this.TvFolders);
      this.PnTreeview.Controls.Add(this.PnTreeviewUpper);
      this.PnTreeview.Location = new System.Drawing.Point(64, 170);
      this.PnTreeview.Name = "PnTreeview";
      this.PnTreeview.Size = new System.Drawing.Size(400, 300);
      this.PnTreeview.TabIndex = 2;
      // 
      // PnTreeviewUpper
      // 
      this.PnTreeviewUpper.Controls.Add(this.PvFolders);
      this.PnTreeviewUpper.Dock = System.Windows.Forms.DockStyle.Top;
      this.PnTreeviewUpper.Location = new System.Drawing.Point(0, 0);
      this.PnTreeviewUpper.Name = "PnTreeviewUpper";
      this.PnTreeviewUpper.Size = new System.Drawing.Size(400, 78);
      this.PnTreeviewUpper.TabIndex = 4;
      // 
      // TxFolderName
      // 
      this.TxFolderName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderName.Location = new System.Drawing.Point(5, 5);
      this.TxFolderName.Name = "TxFolderName";
      this.TxFolderName.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxAddFolder});
      this.TxFolderName.Size = new System.Drawing.Size(369, 23);
      this.TxFolderName.TabIndex = 5;
      // 
      // BxAddFolder
      // 
      this.BxAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("BxAddFolder.Image")));
      this.BxAddFolder.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxAddFolder.Name = "BxAddFolder";
      this.BxAddFolder.Text = "add new folder";
      this.BxAddFolder.ToolTipText = "Add a new folder";
      this.BxAddFolder.UseCompatibleTextRendering = false;
      // 
      // DialogOpenFile
      // 
      this.DialogOpenFile.InitialSelectedLayout = Telerik.WinControls.FileDialogs.LayoutType.MediumIcons;
      // 
      // TxDatabaseFile
      // 
      this.TxDatabaseFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxDatabaseFile.LeftButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxSelectFile});
      this.TxDatabaseFile.Location = new System.Drawing.Point(579, 104);
      this.TxDatabaseFile.Name = "TxDatabaseFile";
      this.TxDatabaseFile.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxOpenFile});
      this.TxDatabaseFile.Size = new System.Drawing.Size(380, 23);
      this.TxDatabaseFile.TabIndex = 5;
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxDatabaseFile.GetChildAt(0).GetChildAt(2).GetChildAt(2))).Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
      // PvFolders
      // 
      this.PvFolders.Controls.Add(this.radPageViewPage1);
      this.PvFolders.Controls.Add(this.radPageViewPage2);
      this.PvFolders.Controls.Add(this.radPageViewPage3);
      this.PvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PvFolders.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.PvFolders.ItemSize = new System.Drawing.Size(110, 25);
      this.PvFolders.ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
      this.PvFolders.Location = new System.Drawing.Point(0, 0);
      this.PvFolders.Name = "PvFolders";
      this.PvFolders.SelectedPage = this.radPageViewPage1;
      this.PvFolders.Size = new System.Drawing.Size(400, 78);
      this.PvFolders.TabIndex = 0;
      ((Telerik.WinControls.UI.RadPageViewStripElement)(this.PvFolders.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.ItemList;
      ((Telerik.WinControls.UI.RadPageViewStripElement)(this.PvFolders.GetChildAt(0))).ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
      // 
      // radPageViewPage1
      // 
      this.radPageViewPage1.Controls.Add(this.TxFolderName);
      this.radPageViewPage1.ItemSize = new System.Drawing.SizeF(110F, 25F);
      this.radPageViewPage1.Location = new System.Drawing.Point(10, 34);
      this.radPageViewPage1.Name = "radPageViewPage1";
      this.radPageViewPage1.Padding = new System.Windows.Forms.Padding(5);
      this.radPageViewPage1.Size = new System.Drawing.Size(379, 33);
      this.radPageViewPage1.Text = "Add folder";
      this.radPageViewPage1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // radPageViewPage2
      // 
      this.radPageViewPage2.Controls.Add(this.TxFolderRename);
      this.radPageViewPage2.ItemSize = new System.Drawing.SizeF(110F, 25F);
      this.radPageViewPage2.Location = new System.Drawing.Point(10, 34);
      this.radPageViewPage2.Name = "radPageViewPage2";
      this.radPageViewPage2.Padding = new System.Windows.Forms.Padding(5);
      this.radPageViewPage2.Size = new System.Drawing.Size(379, 33);
      this.radPageViewPage2.Text = "Rename folder";
      this.radPageViewPage2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // radPageViewPage3
      // 
      this.radPageViewPage3.Controls.Add(this.TxFolderDelete);
      this.radPageViewPage3.ItemSize = new System.Drawing.SizeF(110F, 25F);
      this.radPageViewPage3.Location = new System.Drawing.Point(10, 34);
      this.radPageViewPage3.Name = "radPageViewPage3";
      this.radPageViewPage3.Padding = new System.Windows.Forms.Padding(5);
      this.radPageViewPage3.Size = new System.Drawing.Size(379, 33);
      this.radPageViewPage3.Text = "Delete folder";
      this.radPageViewPage3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // TxFolderRename
      // 
      this.TxFolderRename.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderRename.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderRename.Location = new System.Drawing.Point(5, 5);
      this.TxFolderRename.Name = "TxFolderRename";
      this.TxFolderRename.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxRenameFolder});
      this.TxFolderRename.Size = new System.Drawing.Size(369, 23);
      this.TxFolderRename.TabIndex = 6;
      // 
      // BxRenameFolder
      // 
      this.BxRenameFolder.Image = ((System.Drawing.Image)(resources.GetObject("BxRenameFolder.Image")));
      this.BxRenameFolder.ImageIndexClicked = 0;
      this.BxRenameFolder.ImageIndexHovered = 0;
      this.BxRenameFolder.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxRenameFolder.Name = "BxRenameFolder";
      this.BxRenameFolder.Text = "";
      this.BxRenameFolder.ToolTipText = "Type new name and press the button";
      this.BxRenameFolder.UseCompatibleTextRendering = false;
      // 
      // TxFolderDelete
      // 
      this.TxFolderDelete.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxFolderDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFolderDelete.Location = new System.Drawing.Point(5, 5);
      this.TxFolderDelete.Name = "TxFolderDelete";
      this.TxFolderDelete.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxDeleteFolder});
      this.TxFolderDelete.Size = new System.Drawing.Size(369, 23);
      this.TxFolderDelete.TabIndex = 6;
      // 
      // BxDeleteFolder
      // 
      this.BxDeleteFolder.Image = ((System.Drawing.Image)(resources.GetObject("BxDeleteFolder.Image")));
      this.BxDeleteFolder.ImageIndexClicked = 0;
      this.BxDeleteFolder.ImageIndexHovered = 0;
      this.BxDeleteFolder.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxDeleteFolder.Name = "BxDeleteFolder";
      this.BxDeleteFolder.Text = "Delete";
      this.BxDeleteFolder.ToolTipText = "Delete folder";
      this.BxDeleteFolder.UseCompatibleTextRendering = false;
      // 
      // FormTreeView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1092, 695);
      this.Controls.Add(this.TxDatabaseFile);
      this.Controls.Add(this.PnTreeview);
      this.Name = "FormTreeView";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "TreeView";
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeview)).EndInit();
      this.PnTreeview.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.PnTreeviewUpper)).EndInit();
      this.PnTreeviewUpper.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderName)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxDatabaseFile)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PvFolders)).EndInit();
      this.PvFolders.ResumeLayout(false);
      this.radPageViewPage1.ResumeLayout(false);
      this.radPageViewPage1.PerformLayout();
      this.radPageViewPage2.ResumeLayout(false);
      this.radPageViewPage2.PerformLayout();
      this.radPageViewPage3.ResumeLayout(false);
      this.radPageViewPage3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderRename)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxFolderDelete)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion
    public Telerik.WinControls.UI.RadTreeView TvFolders;
    public System.Windows.Forms.ImageList ImageListFolders;
    private Telerik.WinControls.UI.RadPanel PnTreeview;
    private Telerik.WinControls.UI.RadPanel PnTreeviewUpper;
    internal Telerik.WinControls.UI.RadOpenFileDialog DialogOpenFile;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderName;
    private Telerik.WinControls.UI.RadImageButtonElement BxAddFolder;
    private Telerik.WinControls.UI.RadButtonTextBox TxDatabaseFile;
    private Telerik.WinControls.UI.RadImageButtonElement BxSelectFile;
    private Telerik.WinControls.UI.RadImageButtonElement BxOpenFile;
    private Telerik.WinControls.UI.RadPageView PvFolders;
    private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
    private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderRename;
    private Telerik.WinControls.UI.RadImageButtonElement BxRenameFolder;
    private Telerik.WinControls.UI.RadPageViewPage radPageViewPage3;
    private Telerik.WinControls.UI.RadButtonTextBox TxFolderDelete;
    private Telerik.WinControls.UI.RadImageButtonElement BxDeleteFolder;
  }
}
