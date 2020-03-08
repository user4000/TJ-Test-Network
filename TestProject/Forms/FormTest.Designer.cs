namespace TestProject
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
      this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
      this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
      this.TxMessage = new Telerik.WinControls.UI.RadTextBox();
      this.radButton1 = new Telerik.WinControls.UI.RadButton();
      this.BxTest = new Telerik.WinControls.UI.RadButton();
      this.TxInput = new Telerik.WinControls.UI.RadTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
      this.radPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
      this.radPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxInput)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // radPanel1
      // 
      this.radPanel1.Controls.Add(this.TxInput);
      this.radPanel1.Controls.Add(this.BxTest);
      this.radPanel1.Controls.Add(this.radButton1);
      this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.radPanel1.Location = new System.Drawing.Point(0, 0);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Size = new System.Drawing.Size(1131, 192);
      this.radPanel1.TabIndex = 0;
      // 
      // radPanel2
      // 
      this.radPanel2.Controls.Add(this.TxMessage);
      this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radPanel2.Location = new System.Drawing.Point(0, 192);
      this.radPanel2.Name = "radPanel2";
      this.radPanel2.Size = new System.Drawing.Size(1131, 646);
      this.radPanel2.TabIndex = 0;
      // 
      // TxMessage
      // 
      this.TxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TxMessage.Location = new System.Drawing.Point(0, 0);
      this.TxMessage.Multiline = true;
      this.TxMessage.Name = "TxMessage";
      // 
      // 
      // 
      this.TxMessage.RootElement.StretchVertically = true;
      this.TxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TxMessage.Size = new System.Drawing.Size(1131, 646);
      this.TxMessage.TabIndex = 0;
      // 
      // radButton1
      // 
      this.radButton1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.radButton1.Location = new System.Drawing.Point(12, 12);
      this.radButton1.Name = "radButton1";
      this.radButton1.Size = new System.Drawing.Size(123, 27);
      this.radButton1.TabIndex = 0;
      this.radButton1.Text = "radButton1";
      // 
      // BxTest
      // 
      this.BxTest.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.BxTest.Location = new System.Drawing.Point(193, 63);
      this.BxTest.Name = "BxTest";
      this.BxTest.Size = new System.Drawing.Size(123, 27);
      this.BxTest.TabIndex = 0;
      this.BxTest.Text = "Test";
      // 
      // TxInput
      // 
      this.TxInput.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxInput.Location = new System.Drawing.Point(193, 12);
      this.TxInput.Name = "TxInput";
      this.TxInput.Size = new System.Drawing.Size(834, 25);
      this.TxInput.TabIndex = 1;
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1131, 838);
      this.Controls.Add(this.radPanel2);
      this.Controls.Add(this.radPanel1);
      this.Name = "FormTest";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "RadForm1";
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
      this.radPanel1.ResumeLayout(false);
      this.radPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
      this.radPanel2.ResumeLayout(false);
      this.radPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxInput)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Telerik.WinControls.UI.RadPanel radPanel1;
    private Telerik.WinControls.UI.RadPanel radPanel2;
    internal Telerik.WinControls.UI.RadTextBox TxMessage;
    private Telerik.WinControls.UI.RadTextBox TxInput;
    private Telerik.WinControls.UI.RadButton BxTest;
    private Telerik.WinControls.UI.RadButton radButton1;
  }
}