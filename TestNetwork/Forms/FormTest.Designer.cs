namespace TestNetwork
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
      this.PvTest = new Telerik.WinControls.UI.RadPageView();
      this.PgTestOne = new Telerik.WinControls.UI.RadPageViewPage();
      this.PgTestTwo = new Telerik.WinControls.UI.RadPageViewPage();
      this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
      this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
      this.TxMessage = new Telerik.WinControls.UI.RadTextBox();
      this.TxTest = new Telerik.WinControls.UI.RadButtonTextBox();
      this.BxTest = new Telerik.WinControls.UI.RadButtonElement();
      ((System.ComponentModel.ISupportInitialize)(this.PvTest)).BeginInit();
      this.PvTest.SuspendLayout();
      this.PgTestOne.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
      this.radPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
      this.radPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTest)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // PvTest
      // 
      this.PvTest.Controls.Add(this.PgTestOne);
      this.PvTest.Controls.Add(this.PgTestTwo);
      this.PvTest.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PvTest.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.PvTest.Location = new System.Drawing.Point(0, 0);
      this.PvTest.Name = "PvTest";
      this.PvTest.SelectedPage = this.PgTestOne;
      this.PvTest.Size = new System.Drawing.Size(1010, 687);
      this.PvTest.TabIndex = 0;
      // 
      // PgTestOne
      // 
      this.PgTestOne.Controls.Add(this.radPanel2);
      this.PgTestOne.Controls.Add(this.radPanel1);
      this.PgTestOne.ItemSize = new System.Drawing.SizeF(100F, 28F);
      this.PgTestOne.Location = new System.Drawing.Point(10, 37);
      this.PgTestOne.Name = "PgTestOne";
      this.PgTestOne.Size = new System.Drawing.Size(989, 639);
      this.PgTestOne.Text = "Common test";
      // 
      // PgTestTwo
      // 
      this.PgTestTwo.ItemSize = new System.Drawing.SizeF(96F, 28F);
      this.PgTestTwo.Location = new System.Drawing.Point(10, 37);
      this.PgTestTwo.Name = "PgTestTwo";
      this.PgTestTwo.Size = new System.Drawing.Size(989, 639);
      this.PgTestTwo.Text = "Test settings";
      // 
      // radPanel1
      // 
      this.radPanel1.Controls.Add(this.TxTest);
      this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.radPanel1.Location = new System.Drawing.Point(0, 0);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Size = new System.Drawing.Size(989, 52);
      this.radPanel1.TabIndex = 0;
      // 
      // radPanel2
      // 
      this.radPanel2.Controls.Add(this.TxMessage);
      this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radPanel2.Location = new System.Drawing.Point(0, 52);
      this.radPanel2.Name = "radPanel2";
      this.radPanel2.Size = new System.Drawing.Size(989, 587);
      this.radPanel2.TabIndex = 0;
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
      this.TxMessage.Size = new System.Drawing.Size(989, 587);
      this.TxMessage.TabIndex = 0;
      // 
      // TxTest
      // 
      this.TxTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TxTest.AutoSize = false;
      this.TxTest.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TxTest.Location = new System.Drawing.Point(15, 11);
      this.TxTest.Name = "TxTest";
      this.TxTest.RightButtonItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.BxTest});
      this.TxTest.Size = new System.Drawing.Size(960, 28);
      this.TxTest.TabIndex = 0;
      // 
      // BxTest
      // 
      this.BxTest.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.BxTest.Name = "BxTest";
      this.BxTest.Text = "    test    ";
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1010, 687);
      this.Controls.Add(this.PvTest);
      this.Name = "FormTest";
      // 
      // 
      // 
      this.RootElement.ApplyShapeToControl = true;
      this.Text = "FormTest";
      ((System.ComponentModel.ISupportInitialize)(this.PvTest)).EndInit();
      this.PvTest.ResumeLayout(false);
      this.PgTestOne.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
      this.radPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
      this.radPanel2.ResumeLayout(false);
      this.radPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TxMessage)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TxTest)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

        }

    #endregion

    private Telerik.WinControls.UI.RadPageView PvTest;
    private Telerik.WinControls.UI.RadPageViewPage PgTestOne;
    private Telerik.WinControls.UI.RadPanel radPanel2;
    private Telerik.WinControls.UI.RadTextBox TxMessage;
    private Telerik.WinControls.UI.RadPanel radPanel1;
    private Telerik.WinControls.UI.RadPageViewPage PgTestTwo;
    private Telerik.WinControls.UI.RadButtonTextBox TxTest;
    private Telerik.WinControls.UI.RadButtonElement BxTest;
  }
}
