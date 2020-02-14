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
      this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // radTreeView1
      // 
      this.radTreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
      this.radTreeView1.Cursor = System.Windows.Forms.Cursors.Default;
      this.radTreeView1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.radTreeView1.ForeColor = System.Drawing.Color.Black;
      this.radTreeView1.ImageList = this.imageList1;
      this.radTreeView1.ItemHeight = 34;
      this.radTreeView1.LineStyle = Telerik.WinControls.UI.TreeLineStyle.Solid;
      this.radTreeView1.Location = new System.Drawing.Point(48, 32);
      this.radTreeView1.Name = "radTreeView1";
      this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.radTreeView1.ShowLines = true;
      this.radTreeView1.Size = new System.Drawing.Size(553, 684);
      this.radTreeView1.SpacingBetweenNodes = -1;
      this.radTreeView1.TabIndex = 0;
      this.radTreeView1.TreeIndent = 34;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "copy (1).png");
      this.imageList1.Images.SetKeyName(1, "document (2).png");
      this.imageList1.Images.SetKeyName(2, "document_32 (1).png");
      this.imageList1.Images.SetKeyName(3, "document_32.png");
      this.imageList1.Images.SetKeyName(4, "edit (3).png");
      this.imageList1.Images.SetKeyName(5, "edit_add.png");
      this.imageList1.Images.SetKeyName(6, "folder (4).png");
      this.imageList1.Images.SetKeyName(7, "folder (11).png");
      this.imageList1.Images.SetKeyName(8, "folder_blue.png");
      this.imageList1.Images.SetKeyName(9, "folder_green (2).png");
      this.imageList1.Images.SetKeyName(10, "folder-open (1).png");
      this.imageList1.Images.SetKeyName(11, "folder-open.png");
      this.imageList1.Images.SetKeyName(12, "key_add.png");
      this.imageList1.Images.SetKeyName(13, "knode.png");
      this.imageList1.Images.SetKeyName(14, "mail-new.png");
      this.imageList1.Images.SetKeyName(15, "package_editorspackage_editors.png");
      this.imageList1.Images.SetKeyName(16, "textdocument.png");
      this.imageList1.Images.SetKeyName(17, "x-office-document.png");
      // 
      // FormTreeView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1092, 770);
      this.Controls.Add(this.radTreeView1);
      this.Name = "FormTreeView";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "FormTreeView";
      ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

        }

    #endregion
    private System.Windows.Forms.ImageList imageList1;
    public Telerik.WinControls.UI.RadTreeView radTreeView1;
  }
}
