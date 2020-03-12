using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
      //BxTest.Click += StartTestTimers;
      BxList.Click += TestGetAllFolders;
      BxGetIdFolder.Click += TestGetIdFolder;
    }

    private void TestGetIdFolder(object sender, EventArgs e)
    {
      string FullPath;
      FullPath = ListFolder.PickRandom().FullPath;
      TxOne.Text = FullPath;
      //TxMessage.Clear();

      //int IdFolder = DbSettings.GetIdFolderWithTreeview(FullPath);
      //int IdFolder = DbSettings.GetIdFolderFromDatabase(FullPath);

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

    private void TestGetAllFolders(object sender, EventArgs e)
    {
      ListFolder.Clear();
      ListFolder = DbSettings.GetListOfFolders();
      foreach (var item in ListFolder) Print(item.IdFolder + " --- " + item.FullPath);
    }

    private void StartTestTimers(object sender, EventArgs e)
    {
      Tm1 = new System.Threading.Timer(TestActionCreateFolder, null, 1000, 393 + VxFaker.Random.Int(1, 27));
      Tm2 = new System.Threading.Timer(TestActionRenameFolder, null, 1007, 1394 + VxFaker.Random.Int(1, 28));
      Tm3 = new System.Threading.Timer(TestActionDeleteFolder, null, 1011, 2395 + VxFaker.Random.Int(1, 29));
      Tm4 = new System.Threading.Timer(TestActionCreateFolder, null, 2019, 427 + VxFaker.Random.Int(1, 30));

      Tm5 = new System.Threading.Timer(TestActionAddSettingBoolean, null, 2000, 1393 + VxFaker.Random.Int(1, 27));
      Tm6 = new System.Threading.Timer(TestActionAddSettingDateTime, null, 2007, 1594 + VxFaker.Random.Int(1, 28));
      Tm7 = new System.Threading.Timer(TestActionAddSettingInteger64, null, 2011, 1795 + VxFaker.Random.Int(1, 29));
      Tm8 = new System.Threading.Timer(TestActionAddSettingText, null, 2019, 1900 + VxFaker.Random.Int(1, 30));
      Tm9 = new System.Threading.Timer(TestActionRenameSetting, null, 2000, 2011 + VxFaker.Random.Int(1, 27));
    }

    private void TestDbSettingsGetMaterializedPath(object sender, EventArgs e)
    {
      TxMessage.Clear();
      Stopwatch sw = Stopwatch.StartNew();
      List<Folder> list = DbSettings.GetListOfFolders();
      sw.Stop();
      foreach (var item in list) Print(item.FullPath);
      Print($"{sw.ElapsedMilliseconds}");
      Ms.Message("", "Test has been passed 123").Control(BxTest).Debug();
    }

    private void TestActionCreateFolder(object sender)
    {
      int IdFolder = DbSettings.GetRandomIdFolder();
      string NameFolder = VxFaker.Commerce.ProductAdjective() + VxFaker.Commerce.ProductName() + VxFaker.Company.CatchPhrase() + VxFaker.Person.FullName;
      NameFolder = NameFolder.Substring(1, VxFaker.Random.Int(5, 100));
      ReturnCode code = DbSettings.FolderInsert(IdFolder, NameFolder);
      if (code.Error) Print($"Error Folder Insert: {code.StringValue}");
    }

    private void TestActionRenameFolder(object sender)
    {
      int IdFolder = DbSettings.GetRandomIdFolder();
      if (IdFolder == 0) IdFolder = DbSettings.GetRandomIdFolder();
      if (IdFolder == 0) IdFolder = DbSettings.GetRandomIdFolder();
      if (IdFolder == 0) return;
      string NameFolder = VxFaker.Hacker.Phrase() + "-" + VxFaker.Vehicle.Manufacturer() + "^" + VxFaker.Vehicle.Model() + "_" + VxFaker.Address.FullAddress();
      NameFolder = NameFolder.Substring(1, VxFaker.Random.Int(3, 150));
      ReturnCode code = DbSettings.FolderRename(IdFolder, NameFolder);
      if (code.Error) Print($"Error Folder Rename: {code.StringValue}");
    }

    private void TestActionDeleteFolder(object sender)
    {
      int IdFolder = DbSettings.GetRandomIdFolder();
      DbSettings.FolderDelete(IdFolder, string.Empty);
    }

    private void TestActionAddSettingBoolean(object sender)
    {
      string IdSetting = VxFaker.Name.JobTitle() + VxFaker.Name.JobDescriptor();
      IdSetting = IdSetting.Substring(1, VxFaker.Random.Int(7, 100));
      ReturnCode code = DbSettings.SaveSettingBoolean(true, DbSettings.GetRandomIdFolder(), IdSetting, VxFaker.Random.Bool());
      if (code.Error) Print($"Error TestActionAddSettingBoolean: {code.StringValue}");
    }

    private void TestActionAddSettingDateTime(object sender)
    {
      string IdSetting = VxFaker.Rant.Review();
      IdSetting = IdSetting.Substring(1, VxFaker.Random.Int(7, 100));
      DateTime dt = new DateTime(2000, 1, 1).AddSeconds(VxFaker.Random.Int(1, 600000000));
      ReturnCode code = DbSettings.SaveSettingDatetime(true, DbSettings.GetRandomIdFolder(), IdSetting, dt);
      if (code.Error) Print($"Error TestActionAddSettingDateTime: {code.StringValue}");
    }

    private void TestActionAddSettingInteger64(object sender)
    {
      string IdSetting = VxFaker.Internet.DomainWord() + VxFaker.Internet.UserName() + VxFaker.Internet.UserAgent();
      IdSetting = IdSetting.Substring(1, VxFaker.Random.Int(5, 100));
      ReturnCode code = DbSettings.SaveSettingLong(true, DbSettings.GetRandomIdFolder(), IdSetting, VxFaker.Random.Long());
      if (code.Error) Print($"Error TestActionAddSettingInteger64: {code.StringValue}");
    }

    private void TestActionAddSettingText(object sender)
    {
      string IdSetting = VxFaker.Company.CompanyName() + VxFaker.Lorem.Words(5);
      IdSetting = IdSetting.Substring(1, VxFaker.Random.Int(5, 100));
      string Text = VxFaker.Phone.Random + " " + VxFaker.Address.Random + " " + VxFaker.System.FilePath() + " " + VxFaker.Address.Random;
      Text = Text.Substring(1, VxFaker.Random.Int(1, Text.Length));
      ReturnCode code = DbSettings.SaveSettingText(true, DbSettings.GetRandomIdFolder(), IdSetting, TypeSetting.Text, Text);
      if (code.Error) Print($"Error TestActionAddSettingText: {code.StringValue}");
    }

    private void TestActionRenameSetting(object sender)
    {
      int IdFolder = DbSettings.GetRandomIdFolder();
      string IdSetting = DbSettings.GetRandomIdSetting(IdFolder);
      if (IdSetting.Length == 0) return;
      string NewIdSetting = VxFaker.Commerce.Random + "^" + VxFaker.Company.Random + VxFaker.Hacker.Random;
      NewIdSetting = NewIdSetting.Substring(3, VxFaker.Random.Int(1, NewIdSetting.Length));
      ReturnCode code = DbSettings.SettingRename(IdFolder, IdSetting, NewIdSetting);
      if (code.Error) Print($"Error TestActionRenameSetting: {code.StringValue}");
    }

  }
}
