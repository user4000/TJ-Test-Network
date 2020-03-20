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
using TJStandard.Tools;
using static TestSettingsConsumer.Program;
using static TJFramework.TJFrameworkManager;

namespace TestSettingsConsumer
{
  public partial class FormTest2 : RadForm, IEventStartWork
  {

    private List<Folder> ListFolder = new List<Folder>();

    private List<Setting> ListSetting = new List<Setting>();

    private Faker VxFaker = new Faker();

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
      BxGetIdFolder.Click += EventGetIdFolder;
      BxGetListOfSettings.Click += EventGetListOfSettings;
      BxDeleteSettings.Click += EventDeleteSettings;

      BxFolderForceDelete.Click += EventFolderForceDelete;
      BxFolderForceDeleteAsync.Click += EventFolderForceDelete;
      BxForceDeleteFolderUsingTreeview.Click += EventFolderForceDelete;
      BxCreateTestBranch.Click += EventCreateTestBranch;
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
      foreach (var item in list) names += item.IdFolder + " = " + item.FullPath + Environment.NewLine;
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

    private void EventGetIdFolder(object sender, EventArgs e)
    {
      string FolderFullPath = TxOne.Text;
      TxTwo.Text = FolderFullPath;
      int IdFolder = DbSettings.GetIdFolder(FolderFullPath);
      TxOne.Text = IdFolder.ToString();
    }

    private void EventGetListOfSettings(object sender, EventArgs e)
    {
      string FolderFullPath = TxOne.Text;
      TxTwo.Text = FolderFullPath;
      TxOne.Clear();
      List<string> list = DbSettings.GetSettings(FolderFullPath, TypeSetting.Text);
      string names = string.Empty;
      foreach (var item in list) names += item + Environment.NewLine;
      Print($"SETTINGS OF A FOLDER = {FolderFullPath}");
      Print(names);
    }

    private void EventDeleteSettings(object sender, EventArgs e)
    {
      string FolderFullPath = TxOne.Text;
      TxTwo.Text = FolderFullPath;
      TxOne.Clear();
      ReturnCode code = DbSettings.DeleteAllSettingsOfOneFolder(FolderFullPath);
      Print(ReturnCodeFormatter.ToString(code));
    }

    private void EventCreateTestBranch(object sender, EventArgs e)
    {
      string MainFolder = "TestBranchForDelete300";
      if (DbSettings.FolderExists(MainFolder))
      {
        Ms.Message("ERROR!", $"Folder {MainFolder} already exists").Wire(BxCreateTestBranch).Fail(); return;
      }

      ReturnCode code = DbSettings.FolderInsert(DbSettings.RootFolderName, MainFolder);
      int IdParent = code.IdObject;

      TxMessage.Clear();

      for (int i = 1; i < 301; i++)
      {
        code = DbSettings.FolderInsert(IdParent, VxFaker.Hacker.Phrase() + $"_{i}");
        Print($"Folder >>> {i} {code.Success}");
        int IdParent2 = code.IdObject;
        /*if (code.Success)
          for (int j = 1; j < 11; j++)
          {
            code = DbSettings.FolderInsert(IdParent2, VxFaker.Person.FullName + $"_{i}_{j}");
            Print($"   Folder --- {i} --- {j} --- {code.Success}");
          }*/
      }

      TxMessage.Clear();
      Print("Test branch created.");

    }

    private async void EventFolderForceDelete(object sender, EventArgs e)
    {
      string FolderFullPath = TxOne.Text;
      TxTwo.Text = FolderFullPath;
      TxOne.Clear();

      if (FolderFullPath.Trim().Length < 1)
      {
        Print("ERROR ! Folder name is empty");
        return;
      }

      Print("");
      Print("---------------------------------------------------------------------------------------------");
      BxFolderForceDelete.Enabled = false;
      BxForceDeleteFolderUsingTreeview.Enabled = false;

      Application.DoEvents();

      Stopwatch sw = new Stopwatch();

      await Task.Delay(500);

      Application.DoEvents();

      ReturnCode code = ReturnCodeFactory.Error("ERROR ! No any action was performed!");

      if ((sender as RadButton).Name == BxFolderForceDelete.Name)
      {
        Print(" * * *  FolderForceDelete");
        sw = Stopwatch.StartNew();
        code = DbSettings.FolderForceDelete(FolderFullPath);
      }

      if ((sender as RadButton).Name == BxFolderForceDeleteAsync.Name)
      {
        Print(@" /\/\/\  FolderForceDeleteAsync");
        sw = Stopwatch.StartNew();
        code = await DbSettings.FolderForceDeleteAsync(FolderFullPath);
      }

      if ((sender as RadButton).Name == BxForceDeleteFolderUsingTreeview.Name)
      {
        Print(" ^ ^ ^  FolderForceDeleteUsingTreeview");
        sw = Stopwatch.StartNew();
        code = DbSettings.FolderForceDeleteUsingTreeview(FolderFullPath);
      }

      sw.Stop();

      BxFolderForceDelete.Enabled = true;
      BxForceDeleteFolderUsingTreeview.Enabled = true;

      Application.DoEvents();

      Print(ReturnCodeFormatter.ToString(code));
      Print($"Time = {sw.ElapsedMilliseconds} ms");

      await Task.Delay(500);

      code = DbSettings.FolderDelete(FolderFullPath, FolderFullPath);

      Print($"CHECK is folder deleted: {ReturnCodeFormatter.ToString(code)}");

      CxProcess.Execute(@"e:\restore_test_db.bat", "");
    }
  }
}
