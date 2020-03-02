namespace TestNetwork
{
  partial class FormClient
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
      this.radFontDropDownList1 = new Telerik.WinControls.UI.RadFontDropDownList();
      this.TxFontAsString = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxSelectFile = new Telerik.WinControls.UI.RadImageButtonElement();
      this.BxSetFont = new Telerik.WinControls.UI.RadImageButtonElement();
      ((System.ComponentModel.ISupportInitialize)(this.radFontDropDownList1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxFontAsString)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // radFontDropDownList1
      // 
      this.radFontDropDownList1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.radFontDropDownList1.Location = new System.Drawing.Point(120, 80);
      this.radFontDropDownList1.Name = "radFontDropDownList1";
      this.radFontDropDownList1.SelectOnHover = false;
      this.radFontDropDownList1.ShowTextBox = false;
      this.radFontDropDownList1.Size = new System.Drawing.Size(344, 20);
      this.radFontDropDownList1.TabIndex = 0;
      this.radFontDropDownList1.Text = "radFontDropDownList1";
      // 
      // TxFontAsString
      // 
      this.TxFontAsString.AutoScroll = true;
      this.TxFontAsString.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxFontAsString.LeftButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxSelectFile});
      this.TxFontAsString.Location = new System.Drawing.Point(120, 168);
      this.TxFontAsString.MaxLength = 500;
      this.TxFontAsString.Name = "TxFontAsString";
      this.TxFontAsString.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxSetFont});
      this.TxFontAsString.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.TxFontAsString.Size = new System.Drawing.Size(344, 23);
      this.TxFontAsString.TabIndex = 6;
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxFontAsString.GetChildAt(0).GetChildAt(2).GetChildAt(2))).Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxFontAsString.GetChildAt(0).GetChildAt(2).GetChildAt(2))).Margin = new System.Windows.Forms.Padding(0);
      ((Telerik.WinControls.UI.RadTextBoxItem)(this.TxFontAsString.GetChildAt(0).GetChildAt(2).GetChildAt(2))).AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
      // 
      // BxSelectFile
      // 
      this.BxSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("BxSelectFile.Image")));
      this.BxSelectFile.ImageIndexClicked = 0;
      this.BxSelectFile.ImageIndexHovered = 0;
      this.BxSelectFile.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxSelectFile.Name = "BxSelectFile";
      this.BxSelectFile.Text = "Select a database file";
      this.BxSelectFile.ToolTipText = "Select a database file";
      this.BxSelectFile.UseCompatibleTextRendering = false;
      // 
      // BxSetFont
      // 
      this.BxSetFont.Image = ((System.Drawing.Image)(resources.GetObject("BxSetFont.Image")));
      this.BxSetFont.ImageIndexClicked = 0;
      this.BxSetFont.ImageIndexHovered = 0;
      this.BxSetFont.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxSetFont.Name = "BxSetFont";
      this.BxSetFont.Text = "";
      this.BxSetFont.UseCompatibleTextRendering = false;
      // 
      // FormClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1092, 770);
      this.Controls.Add(this.TxFontAsString);
      this.Controls.Add(this.radFontDropDownList1);
      this.Name = "FormClient";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "RadForm1";
      ((System.ComponentModel.ISupportInitialize)(this.radFontDropDownList1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxFontAsString)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Telerik.WinControls.UI.RadFontDropDownList radFontDropDownList1;
    private Telerik.WinControls.UI.RadButtonTextBox TxFontAsString;
    private Telerik.WinControls.UI.RadImageButtonElement BxSelectFile;
    private Telerik.WinControls.UI.RadImageButtonElement BxSetFont;
  }
}