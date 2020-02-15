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
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.BtnLoadData = new Telerik.WinControls.UI.RadButton();
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BtnLoadData)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // TvFolders
      // 
      this.TvFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
      this.TvFolders.Cursor = System.Windows.Forms.Cursors.Default;
      this.TvFolders.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TvFolders.ForeColor = System.Drawing.Color.Black;
      this.TvFolders.ItemHeight = 34;
      this.TvFolders.LineStyle = Telerik.WinControls.UI.TreeLineStyle.Solid;
      this.TvFolders.Location = new System.Drawing.Point(12, 12);
      this.TvFolders.Name = "TvFolders";
      this.TvFolders.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.TvFolders.ShowLines = true;
      this.TvFolders.Size = new System.Drawing.Size(606, 746);
      this.TvFolders.SpacingBetweenNodes = -1;
      this.TvFolders.TabIndex = 0;
      this.TvFolders.TreeIndent = 34;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "textdocument.png");
      this.imageList1.Images.SetKeyName(1, "folder_green (2).png");
      this.imageList1.Images.SetKeyName(2, "folder (7).png");
      this.imageList1.Images.SetKeyName(3, "folder (8).png");
      this.imageList1.Images.SetKeyName(4, "folder_blue (1).png");
      this.imageList1.Images.SetKeyName(5, "folder (4).png");
      this.imageList1.Images.SetKeyName(6, "folder (9).png");
      this.imageList1.Images.SetKeyName(7, "folder (11).png");
      this.imageList1.Images.SetKeyName(8, "folder-open.png");
      this.imageList1.Images.SetKeyName(9, "folder (11).png");
      this.imageList1.Images.SetKeyName(10, "folder-open.png");
      // 
      // BtnLoadData
      // 
      this.BtnLoadData.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BtnLoadData.Location = new System.Drawing.Point(659, 12);
      this.BtnLoadData.Name = "BtnLoadData";
      this.BtnLoadData.Size = new System.Drawing.Size(129, 29);
      this.BtnLoadData.TabIndex = 1;
      this.BtnLoadData.Text = "Load data";
      // 
      // FormTreeView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1092, 770);
      this.Controls.Add(this.BtnLoadData);
      this.Controls.Add(this.TvFolders);
      this.Name = "FormTreeView";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "FormTreeView";
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BtnLoadData)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

        }

    #endregion
    public Telerik.WinControls.UI.RadTreeView TvFolders;
    public System.Windows.Forms.ImageList imageList1;
    private Telerik.WinControls.UI.RadButton BtnLoadData;
  }
}
