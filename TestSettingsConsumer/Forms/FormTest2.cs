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
      BxListAllFolders.Click += EventGetListOfFolders;
      BxGetChildrenOfOneFolder.Click += EventGetChildrenOfOneFolder;
    }



    private void PrintInner(string message)
    {
      TxMessage.AppendText(message + Environment.NewLine);
    }

    public void Print(string message)
    {
      if (TxMessage.InvokeRequired)
        TxMessage.Invoke((MethodInvoker)delegate { PrintInner(message); });
      else
        PrintInner(message);
    }

    private void EventGetListOfFolders(object sender, EventArgs e)
    {
      TxMessage.Clear();
      string names = string.Empty;
      List<Folder> list = DbSettings.GetListOfFolders();
      foreach (var item in list) names += item.FullPath + Environment.NewLine;
      TxMessage.Text = names;
    }

    private void EventGetChildrenOfOneFolder(object sender, EventArgs e)
    {
      string ParentFolderFullPath = TxOne.Text;
      List<string> list = DbSettings.GetListOfFolders(ParentFolderFullPath);
      string names = string.Empty;
      foreach (var item in list) names += item + Environment.NewLine;
      Print($"-------------------- {ParentFolderFullPath}");
      Print(names);
      TxTwo.Text = ParentFolderFullPath;
      TxOne.Clear();

    }
  }
}
