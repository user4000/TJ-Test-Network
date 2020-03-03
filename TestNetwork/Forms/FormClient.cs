using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJStandard;
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
      
    }
  }
}
