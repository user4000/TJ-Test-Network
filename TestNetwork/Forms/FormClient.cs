using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using static TestNetwork.Program;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormClient : RadForm, IEventStartWork
  {
    public FormClient()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      BxSetFont.Click += EventSetFont;
    }

    private void EventSetFont(object sender, EventArgs e)
    {
      FontDialog dialog = new FontDialog();
      Font font = null;

      if (TxFontAsString.TextLength > 0)
      {
        font = CxConvert.JsonToObject<Font>(TxFontAsString.Text);
      }
      dialog.Font = font;

      DialogResult result = dialog.ShowDialog();
      if (result == DialogResult.OK)
      {
        //TxFontAsString.Text = dialog.Font.Name + " " + dialog.Font.Size + " " + dialog.Font.Style.ToString();
        TxFontAsString.Text = CxConvert.ObjectToJson(dialog.Font);
      }
    }
  }
}
