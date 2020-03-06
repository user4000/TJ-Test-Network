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
      Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
      this.TxOne = new Telerik.WinControls.UI.RadTextBoxControl();
      this.BxTest = new Telerik.WinControls.UI.RadButton();
      this.TxMessage = new Telerik.WinControls.UI.RadTextBoxControl();
      this.TxTwo = new Telerik.WinControls.UI.RadTextBoxControl();
      this.DxTest = new Telerik.WinControls.UI.RadDropDownList();
      ((System.ComponentModel.ISupportInitialize)(this.TxOne)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTwo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // TxOne
      // 
      this.TxOne.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxOne.Location = new System.Drawing.Point(21, 22);
      this.TxOne.Multiline = true;
      this.TxOne.Name = "TxOne";
      this.TxOne.Size = new System.Drawing.Size(801, 38);
      this.TxOne.TabIndex = 0;
      // 
      // BxTest
      // 
      this.BxTest.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BxTest.Location = new System.Drawing.Point(857, 22);
      this.BxTest.Name = "BxTest";
      this.BxTest.Size = new System.Drawing.Size(276, 38);
      this.BxTest.TabIndex = 1;
      this.BxTest.Text = "T E S T";
      // 
      // TxMessage
      // 
      this.TxMessage.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxMessage.IsReadOnly = true;
      this.TxMessage.Location = new System.Drawing.Point(21, 177);
      this.TxMessage.Multiline = true;
      this.TxMessage.Name = "TxMessage";
      this.TxMessage.Size = new System.Drawing.Size(1112, 188);
      this.TxMessage.TabIndex = 0;
      // 
      // TxTwo
      // 
      this.TxTwo.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxTwo.Location = new System.Drawing.Point(21, 76);
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
      radListDataItem2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      radListDataItem2.ForeColor = System.Drawing.Color.RoyalBlue;
      radListDataItem2.Text = "Unknown";
      this.DxTest.Items.Add(radListDataItem2);
      this.DxTest.Location = new System.Drawing.Point(857, 76);
      this.DxTest.Name = "DxTest";
      this.DxTest.Size = new System.Drawing.Size(276, 38);
      this.DxTest.TabIndex = 2;
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1156, 744);
      this.Controls.Add(this.DxTest);
      this.Controls.Add(this.BxTest);
      this.Controls.Add(this.TxMessage);
      this.Controls.Add(this.TxTwo);
      this.Controls.Add(this.TxOne);
      this.Name = "FormTest";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "RadForm1";
      ((System.ComponentModel.ISupportInitialize)(this.TxOne)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTwo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Telerik.WinControls.UI.RadTextBoxControl TxOne;
    private Telerik.WinControls.UI.RadButton BxTest;
    private Telerik.WinControls.UI.RadTextBoxControl TxMessage;
    private Telerik.WinControls.UI.RadTextBoxControl TxTwo;
    private Telerik.WinControls.UI.RadDropDownList DxTest;
  }
}