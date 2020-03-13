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
  public partial class FormTest : RadForm, IEventStartWork
  {
    private LocalDatabaseOfSettings DbSettings = new LocalDatabaseOfSettings();

    private List<Folder> ListFolder = new List<Folder>();

    private List<Setting> ListSetting = new List<Setting>();

    Faker VxFaker = new Faker("en");

    public System.Threading.Timer Tm1;

    public System.Threading.Timer Tm2;

    public System.Threading.Timer Tm3;

    public System.Threading.Timer Tm4;

    public System.Threading.Timer Tm5;

    public System.Threading.Timer Tm6;

    public System.Threading.Timer Tm7;

    public System.Threading.Timer Tm8;

    public System.Threading.Timer Tm9;

    public FormTest()
    {
      InitializeComponent();
    }

    public void InitSettingsDatabase()
    {
      DbSettings.SetPathToDatabase(Program.ApplicationSettings.SettingsDatabaseLocation);
      DbSettings.InitVariables();
    }

    public void SetEvents()
    {
      BxTest.Click += StartTestTimersForExperiment2;
      BxList.Enabled = false;
      //BxList.Click += FillListFolder;
      BxGetIdFolder.Click += TestGetIdFolder;
    }

    private void TestGetIdFolder(object sender, EventArgs e)
    {
      string FullPath;
      FullPath = ListFolder.PickRandom().FullPath;
      TxOne.Text = FullPath;
      Stopwatch sw = Stopwatch.StartNew();
      int IdFolder = DbSettings.GetIdFolder(FullPath);
      sw.Stop();
      //Print($"{DateTime.Now} ________  Id Folder = {IdFolder}");
      TxTwo.Text = $"{IdFolder} -------- {sw.ElapsedMilliseconds}";
    }

    public void EventStartWork()
    {
      InitSettingsDatabase();
      SetEvents();
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

    private void AddNewTest(string text)
    {
      RadListDataItem item = new RadListDataItem(text);
      item.Font = DxTest.Font;
      DxTest.Items.Add(item);
    }

    private void FillTestList()
    {
      AddNewTest("SettingGetValue");
    }

    private void FillListFolder(object sender, EventArgs e)
    {
      ListFolder.Clear();
      ListFolder = DbSettings.GetListOfFolders();
      string text = string.Empty;
      foreach (var item in ListFolder) text += item.IdFolder + " --- " + item.FullPath + Environment.NewLine;
      TxMessage.AppendText(text);
    }

    private void FillListSetting(object sender, EventArgs e)
    {
      ListSetting.Clear();
      ListSetting = DbSettings.GetAllSettings();
    }

    private string GetFolderPath(Setting setting)
    {
      foreach (var item in ListFolder)
        if (item.IdFolder == setting.IdFolder) return item.FullPath;
      return string.Empty;
    }

    private void StartTestTimersForExperiment1(object sender, EventArgs e)
    {
      Tm1 = new System.Threading.Timer(TestActionCreateFolder, null, 1100, 114393 + VxFaker.Random.Int(1, 27));
      Tm2 = new System.Threading.Timer(TestActionRenameFolder, null, 1207, 115494 + VxFaker.Random.Int(1, 28));
      Tm3 = new System.Threading.Timer(TestActionDeleteFolder, null, 1311, 116395 + VxFaker.Random.Int(1, 29));
      Tm4 = new System.Threading.Timer(TestActionCreateFolder, null, 2419, 114927 + VxFaker.Random.Int(1, 30));

      Tm5 = new System.Threading.Timer(TestActionAddSettingBoolean, null, 2500, 5393 + VxFaker.Random.Int(1, 27));
      Tm6 = new System.Threading.Timer(TestActionAddSettingDateTime, null, 2607, 6594 + VxFaker.Random.Int(1, 28));
      Tm7 = new System.Threading.Timer(TestActionAddSettingInteger64, null, 2711, 4795 + VxFaker.Random.Int(1, 29));
      Tm8 = new System.Threading.Timer(TestActionAddSettingText, null, 2819, 5900 + VxFaker.Random.Int(1, 30));
      Tm9 = new System.Threading.Timer(TestActionRenameSetting, null, 2900, 6011 + VxFaker.Random.Int(1, 27));

      BxTest.Click -= StartTestTimersForExperiment1;
      BxTest.Click += StopTestTimersForExperiment1;
      Ms.Message("START", "Timers were started.").Control(BxTest).Info();
    }

    private void StopTestTimersForExperiment1(object sender, EventArgs e)
    {
      Tm1.Change(Timeout.Infinite, Timeout.Infinite);
      Tm2.Change(Timeout.Infinite, Timeout.Infinite);
      Tm3.Change(Timeout.Infinite, Timeout.Infinite);
      Tm4.Change(Timeout.Infinite, Timeout.Infinite);
      Tm5.Change(Timeout.Infinite, Timeout.Infinite);
      Tm6.Change(Timeout.Infinite, Timeout.Infinite);
      Tm7.Change(Timeout.Infinite, Timeout.Infinite);
      Tm8.Change(Timeout.Infinite, Timeout.Infinite);
      Tm9.Change(Timeout.Infinite, Timeout.Infinite);
      
      BxTest.Click -= StopTestTimersForExperiment1;
      BxTest.Click += StartTestTimersForExperiment1;
      Ms.Message("STOP", "Timers were stopped.").Control(BxTest).Ok();
    }

    private void StartTestTimersForExperiment2(object sender, EventArgs e)
    {
      if (ListFolder.Count == 0) FillListFolder(sender, e);
      if (ListSetting.Count == 0) FillListSetting(sender, e);

      Tm1 = new System.Threading.Timer(TestSelectRandomSetting, null, 1100, 393 + VxFaker.Random.Int(1, 27));
      Tm2 = new System.Threading.Timer(TestSelectRandomSetting, null, 1207, 494 + VxFaker.Random.Int(1, 28));
      Tm3 = new System.Threading.Timer(TestSelectRandomSetting, null, 1311, 395 + VxFaker.Random.Int(1, 29));
      Tm4 = new System.Threading.Timer(TestSelectRandomSetting, null, 2419, 927 + VxFaker.Random.Int(1, 30));
      Tm5 = new System.Threading.Timer(TestSelectRandomSetting, null, 2500, 291 + VxFaker.Random.Int(1, 27));
      Tm6 = new System.Threading.Timer(TestSelectRandomSetting, null, 2607, 594 + VxFaker.Random.Int(1, 28));
      Tm7 = new System.Threading.Timer(TestSelectRandomSetting, null, 2711, 795 + VxFaker.Random.Int(1, 29));
      Tm8 = new System.Threading.Timer(TestActionAddSettingText, null, 2819, 990 + VxFaker.Random.Int(1, 30));
      Tm9 = new System.Threading.Timer(TestActionAddSettingText, null, 2900, 571 + VxFaker.Random.Int(1, 27));

      BxTest.Click -= StartTestTimersForExperiment2;
      BxTest.Click += StopTestTimersForExperiment2;
      Ms.Message("START Experiment 2", "Timers were started.").Control(BxTest).Info();
      TxMessage.Clear();
    }

    private void StopTestTimersForExperiment2(object sender, EventArgs e)
    {
      Tm1.Change(Timeout.Infinite, Timeout.Infinite);
      Tm2.Change(Timeout.Infinite, Timeout.Infinite);
      Tm3.Change(Timeout.Infinite, Timeout.Infinite);
      Tm4.Change(Timeout.Infinite, Timeout.Infinite);
      Tm5.Change(Timeout.Infinite, Timeout.Infinite);
      Tm6.Change(Timeout.Infinite, Timeout.Infinite);
      Tm7.Change(Timeout.Infinite, Timeout.Infinite);
      Tm8.Change(Timeout.Infinite, Timeout.Infinite);
      Tm9.Change(Timeout.Infinite, Timeout.Infinite);

      BxTest.Click -= StopTestTimersForExperiment1;
      BxTest.Click += StartTestTimersForExperiment1;
      Ms.Message("STOP Experiment 2", "Timers were stopped.").Control(BxTest).Ok();
    }

    private string GetRandomFolderPath() => ListFolder.PickRandom().FullPath;

    private Setting GetRandomSetting() => ListSetting.PickRandom();

    private void TestActionCreateFolder(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      string NameFolder = VxFaker.Commerce.ProductAdjective() + VxFaker.Commerce.ProductName() + VxFaker.Company.CatchPhrase() + VxFaker.Person.FullName;
      NameFolder = NameFolder.SafeSubstring(1, VxFaker.Random.Int(5, 100));
      ReturnCode code = DbSettings.FolderInsert(GetRandomFolderPath(), NameFolder);
      if (code.Error) Print($"Error Folder Insert: {code.StringValue}");
    }

    private void TestActionRenameFolder(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      /*if (IdFolder == 0) IdFolder = DbSettings.GetRandomIdFolder();
      if (IdFolder == 0) IdFolder = DbSettings.GetRandomIdFolder();
      if (IdFolder == 0) return;*/
      string NameFolder = VxFaker.Hacker.Phrase() + "-" + VxFaker.Vehicle.Manufacturer() + "^" + VxFaker.Vehicle.Model() + "_" + VxFaker.Address.FullAddress();
      NameFolder = NameFolder.SafeSubstring(1, VxFaker.Random.Int(3, 150));
      ReturnCode code = DbSettings.FolderRename(GetRandomFolderPath(), NameFolder);
      if (code.Error) Print($"Error Folder Rename: {code.StringValue}");
    }

    private void TestActionDeleteFolder(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      DbSettings.FolderDelete(GetRandomFolderPath(), string.Empty);
    }

    private void TestActionAddSettingBoolean(object sender)
    {
      string IdSetting = VxFaker.Name.JobTitle() + VxFaker.Name.JobDescriptor();
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(7, 100));
      ReturnCode code = DbSettings.SaveSettingBoolean(GetRandomFolderPath(), IdSetting, VxFaker.Random.Bool());
      if (code.Error) Print($"Error TestActionAddSettingBoolean: {code.StringValue}");
    }

    private void TestActionAddSettingDateTime(object sender)
    {
      string IdSetting = VxFaker.Rant.Review();
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(7, 100));
      DateTime dt = new DateTime(2000, 1, 1).AddSeconds(VxFaker.Random.Int(1, 600000000));
      ReturnCode code = DbSettings.SaveSettingDatetime(GetRandomFolderPath(), IdSetting, dt);
      if (code.Error) Print($"Error TestActionAddSettingDateTime: {code.StringValue}");
    }

    private void TestActionAddSettingInteger64(object sender)
    {
      string IdSetting = VxFaker.Internet.DomainWord() + VxFaker.Internet.UserName() + VxFaker.Internet.UserAgent();
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(5, 100));
      ReturnCode code = DbSettings.SaveSettingLong(GetRandomFolderPath(), IdSetting, VxFaker.Random.Long());
      if (code.Error) Print($"Error TestActionAddSettingInteger64: {code.StringValue}");
    }

    private void TestActionAddSettingText(object sender)
    {
      string IdSetting = VxFaker.Company.CompanyName() + VxFaker.Lorem.Sentence(5);
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(5, 100));
      string Text = VxFaker.Phone.PhoneNumber() + " " + VxFaker.Address.SecondaryAddress() + " " + VxFaker.System.DirectoryPath() + " " + VxFaker.Address.StreetAddress();
      Text = Text.SafeSubstring(1, VxFaker.Random.Int(1, Text.Length));   
      Trace.WriteLine($"Trying to create a new setting ..... {IdSetting}");
      ReturnCode code = DbSettings.SaveSettingText(GetRandomFolderPath(), IdSetting, TypeSetting.Text, Text);
      if (code.Error) Trace.WriteLine($"Error TestActionAddSettingText: {code.StringValue}");
      else Trace.WriteLine($"New Text Setting has been created ----- {IdSetting}");
    }

    private void TestActionUpdateSettingText(object sender)
    {
      string IdSetting = VxFaker.Hacker.Abbreviation() + "~" + VxFaker.Hacker.Adjective() + "$" + VxFaker.Hacker.Phrase() + VxFaker.Company.CompanyName() + VxFaker.Lorem.Sentence(5);
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(5, 100));
      string Text = VxFaker.Phone.PhoneNumber() + " " + VxFaker.Address.SecondaryAddress() + " " + VxFaker.System.DirectoryPath() + " " + VxFaker.Address.StreetAddress();
      Text = Text.SafeSubstring(1, VxFaker.Random.Int(1, Text.Length));

      Setting setting = GetRandomSetting();

      Trace.WriteLine($"Trying to update setting {setting.IdSetting}");
      ReturnCode code = DbSettings.SaveSettingText(GetFolderPath(setting), setting.IdSetting, TypeSetting.Text, Text);
      if (code.Error) Trace.WriteLine($"Error TestActionAddSettingText: {code.StringValue}");
      else Trace.WriteLine($"New Text Setting has been created ----- {IdSetting}");
    }

    private void TestActionRenameSetting(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      Setting setting = GetRandomSetting();
      string NewIdSetting = VxFaker.Commerce.Random + "^" + VxFaker.Company.Random + VxFaker.Hacker.Random;
      NewIdSetting = NewIdSetting.SafeSubstring(3, VxFaker.Random.Int(1, NewIdSetting.Length));
      ReturnCode code = DbSettings.SettingRename(GetFolderPath(setting), setting.IdSetting, NewIdSetting);
      if (code.Error) Print($"Error TestActionRenameSetting: {code.StringValue}");
    }

    private void TestSelectRandomSetting(object sender)
    {
      Setting setting = GetRandomSetting(); 
      Trace.WriteLine("Trying to get setting value ...");
      ReceivedValueText value = DbSettings.GetSettingText(GetFolderPath(setting), setting.IdSetting);
      Trace.WriteLine($"Ok! value = {value.Value}");
    }
  }
}
