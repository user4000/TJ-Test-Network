using System;
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
   
    private List<Folder> ListFolder = new List<Folder>();

    private List<Setting> ListSetting = new List<Setting>();

    public FormTest2()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      Trace.WriteLine("--> #FormTest2# [EventStartWork]");
      SetEvents();
      Trace.WriteLine("<-- #FormTest2# [EventStartWork]");
    }

    private void SetEvents()
    {
      BxList.Click += EventGetListOfFolders;
    }

    private void EventGetListOfFolders(object sender, EventArgs e)
    {
      
    }
  }
}
