using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bogus;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using TJSettings;
using TJStandard;
using static TestSettingsConsumer.Program;
using static TJFramework.TJFrameworkManager;

namespace TestSettingsConsumer
{
  public partial class FormTest2 : RadForm, IEventStartWork
  {
    public FormTest2()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      
    }
  }
}
