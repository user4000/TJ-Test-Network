namespace TJSettings
{
    partial class FxTreeView
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
      this.TvFolders = new Telerik.WinControls.UI.RadTreeView();
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // TvFolders
      // 
      this.TvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TvFolders.Location = new System.Drawing.Point(0, 0);
      this.TvFolders.Name = "TvFolders";
      this.TvFolders.Size = new System.Drawing.Size(400, 400);
      this.TvFolders.SpacingBetweenNodes = -1;
      this.TvFolders.TabIndex = 0;
      // 
      // FxTreeView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(400, 400);
      this.Controls.Add(this.TvFolders);
      this.Name = "FxTreeView";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.Text = "FxTreeView";
      this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
      ((System.ComponentModel.ISupportInitialize)(this.TvFolders)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

        }

    #endregion

    internal Telerik.WinControls.UI.RadTreeView TvFolders;
  }
}
