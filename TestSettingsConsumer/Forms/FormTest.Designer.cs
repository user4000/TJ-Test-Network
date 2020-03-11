namespace TestSettingsConsumer
{
  partial class FormTest
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
      Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
      this.TxOne = new Telerik.WinControls.UI.RadTextBoxControl();
      this.BxTest = new Telerik.WinControls.UI.RadButton();
      this.TxTwo = new Telerik.WinControls.UI.RadTextBoxControl();
      this.DxTest = new Telerik.WinControls.UI.RadDropDownList();
      this.TxMessage = new Telerik.WinControls.UI.RadTextBox();
      this.BxList = new Telerik.WinControls.UI.RadButton();
      this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
      this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
      this.BxGetIdFolder = new Telerik.WinControls.UI.RadButton();
      ((System.ComponentModel.ISupportInitialize)(this.TxOne)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTwo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxList)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
      this.radPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
      this.radPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.BxGetIdFolder)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // TxOne
      // 
      this.TxOne.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxOne.Location = new System.Drawing.Point(14, 16);
      this.TxOne.Multiline = true;
      this.TxOne.Name = "TxOne";
      this.TxOne.Size = new System.Drawing.Size(801, 38);
      this.TxOne.TabIndex = 0;
      // 
      // BxTest
      // 
      this.BxTest.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BxTest.Location = new System.Drawing.Point(158, 143);
      this.BxTest.Name = "BxTest";
      this.BxTest.Size = new System.Drawing.Size(109, 38);
      this.BxTest.TabIndex = 1;
      this.BxTest.Text = "T E S T";
      // 
      // TxTwo
      // 
      this.TxTwo.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxTwo.Location = new System.Drawing.Point(14, 75);
      this.TxTwo.Multiline = true;
      this.TxTwo.Name = "TxTwo";
      this.TxTwo.Size = new System.Drawing.Size(801, 38);
      this.TxTwo.TabIndex = 0;
      // 
      // DxTest
      // 
      this.DxTest.AutoSize = false;
      this.DxTest.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
      this.DxTest.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      radListDataItem1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      radListDataItem1.ForeColor = System.Drawing.Color.RoyalBlue;
      radListDataItem1.Text = "Unknown";
      this.DxTest.Items.Add(radListDataItem1);
      this.DxTest.Location = new System.Drawing.Point(858, 75);
      this.DxTest.Name = "DxTest";
      this.DxTest.Size = new System.Drawing.Size(276, 38);
      this.DxTest.TabIndex = 2;
      // 
      // TxMessage
      // 
      this.TxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxMessage.Location = new System.Drawing.Point(0, 0);
      this.TxMessage.Multiline = true;
      this.TxMessage.Name = "TxMessage";
      // 
      // 
      // 
      this.TxMessage.RootElement.StretchVertically = true;
      this.TxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TxMessage.Size = new System.Drawing.Size(1156, 537);
      this.TxMessage.TabIndex = 3;
      // 
      // BxList
      // 
      this.BxList.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BxList.Location = new System.Drawing.Point(14, 143);
      this.BxList.Name = "BxList";
      this.BxList.Size = new System.Drawing.Size(109, 38);
      this.BxList.TabIndex = 1;
      this.BxList.Text = "L I S T";
      // 
      // radPanel1
      // 
      this.radPanel1.Controls.Add(this.TxOne);
      this.radPanel1.Controls.Add(this.TxTwo);
      this.radPanel1.Controls.Add(this.DxTest);
      this.radPanel1.Controls.Add(this.BxGetIdFolder);
      this.radPanel1.Controls.Add(this.BxList);
      this.radPanel1.Controls.Add(this.BxTest);
      this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.radPanel1.Location = new System.Drawing.Point(0, 0);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Size = new System.Drawing.Size(1156, 207);
      this.radPanel1.TabIndex = 4;
      // 
      // radPanel2
      // 
      this.radPanel2.Controls.Add(this.TxMessage);
      this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radPanel2.Location = new System.Drawing.Point(0, 207);
      this.radPanel2.Name = "radPanel2";
      this.radPanel2.Size = new System.Drawing.Size(1156, 537);
      this.radPanel2.TabIndex = 4;
      // 
      // BxGetIdFolder
      // 
      this.BxGetIdFolder.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BxGetIdFolder.Location = new System.Drawing.Point(858, 16);
      this.BxGetIdFolder.Name = "BxGetIdFolder";
      this.BxGetIdFolder.Size = new System.Drawing.Size(276, 38);
      this.BxGetIdFolder.TabIndex = 1;
      this.BxGetIdFolder.Text = "Get Id Folder";
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1156, 744);
      this.Controls.Add(this.radPanel2);
      this.Controls.Add(this.radPanel1);
      this.Name = "FormTest";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "RadForm1";
      ((System.ComponentModel.ISupportInitialize)(this.TxOne)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTwo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxList)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
      this.radPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
      this.radPanel2.ResumeLayout(false);
      this.radPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.BxGetIdFolder)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Telerik.WinControls.UI.RadTextBoxControl TxOne;
    private Telerik.WinControls.UI.RadButton BxTest;
    private Telerik.WinControls.UI.RadTextBoxControl TxTwo;
    private Telerik.WinControls.UI.RadDropDownList DxTest;
    private Telerik.WinControls.UI.RadTextBox TxMessage;
    private Telerik.WinControls.UI.RadButton BxList;
    private Telerik.WinControls.UI.RadPanel radPanel1;
    private Telerik.WinControls.UI.RadPanel radPanel2;
    private Telerik.WinControls.UI.RadButton BxGetIdFolder;
  }
}