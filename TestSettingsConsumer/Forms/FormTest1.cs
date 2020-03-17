using System;
using System.Collections.Concurrent;
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

namespace TestSettingsConsumer // TODO: Проверить все методы получения и записи переменнных. Вызывать их через тестовую форму. //
{
  public partial class FormTest1 : RadForm, IEventStartWork
  {
    private List<Folder> ListFolder { get; set; } = new List<Folder>();

    private List<Setting> ListSetting { get; set; } = new List<Setting>();

    private List<string> ListFont { get; set; } = new List<string>();


    private ConcurrentDictionary<string, int> OperationsRegistrar = new ConcurrentDictionary<string, int>();

    private Random rnd = new Random();

    private Faker VxFaker = new Faker("en");

    public System.Threading.Timer Tm1;

    public System.Threading.Timer Tm2;

    public System.Threading.Timer Tm3;

    public System.Threading.Timer Tm4;

    public System.Threading.Timer Tm5;

    public System.Threading.Timer Tm6;

    public System.Threading.Timer Tm7;

    public System.Threading.Timer Tm8;

    public System.Threading.Timer Tm9;

    public FormTest1()
    {
      InitializeComponent();
    }

    public void SetEvents()
    {
      //BxTest.Click += StartTestTimersForExperiment2;
      BxList.Enabled = true;
      BxList.Click += FillListFolder;
      BxGetIdFolder.Click += TestGetIdFolder;
      BxTestSettings.Click += TestGetRandomSettingReadWrite;
    }

    public void FillListOfFonts()
    {
      ListFont.Clear();
      ListFont.Add("Arial");
      ListFont.Add("Comic Sans MS");
      ListFont.Add("Verdana");
      ListFont.Add("Calibri");
      ListFont.Add("Consolas");
      ListFont.Add("Courier New");
      ListFont.Add("Microsoft Sans Serif");
      ListFont.Add("Palatino Linotype");
      ListFont.Add("Segoe UI");
      ListFont.Add("Tahoma");
      ListFont.Add("Times New Roman");
      ListFont.Add("Trebuchet MS");
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
      Trace.WriteLine("--> #FormTest1# [EventStartWork]");
      SetEvents();
      FillListOfFonts();
      Trace.WriteLine("<-- #FormTest1# [EventStartWork]");
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

    private string GetNewGuid() => Guid.NewGuid().ToString().Substring(4, 19);

    private void OperationBegin(string guid)
    {
      if (OperationsRegistrar.ContainsKey(guid)) throw new Exception("Error! The same GUID already exist!");
      bool flag = OperationsRegistrar.TryAdd(guid, 0);
      if (flag == false) throw new Exception("Error! Could not add a new guid to the dictionary");
    }

    private void OperationSuccess(string guid)
    {
      if (OperationsRegistrar.ContainsKey(guid))
      {
        bool flag = OperationsRegistrar.TryRemove(guid, out int x);
        if (flag == false) throw new Exception("Error! Could not remove an existing guid from the dictionary");
      }
      else
      {
        throw new Exception("Error! Guid not found!");
      }
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
      //DbSettings.FolderRename(0, "Root");

      ListFolder.Clear();
      ListFolder = DbSettings.GetListOfFolders();

      FillListSetting(sender, e);

      string text = string.Empty;
      foreach (var item in ListFolder) text += item.IdFolder + " --- " + item.FullPath + Environment.NewLine;
      TxMessage.AppendText(text);
      Print($"Folders = {ListFolder.Count}");

      /*
      ReturnCode code = ReturnCodeFactory.Error();    
      Print("Renaming...");
      foreach (var item in ListFolder)
      {
        if (item.NameFolder.Length > 8 )
        {
          code = DbSettings.FolderRename(item.IdFolder, "Item_" + item.IdFolder.ToString());
          //if (code.Success) Print($"RENAMED {item.FullPath}");
        }
      
      }
      Print("STOP");
      */

    }

    private void FillListSetting(object sender, EventArgs e)
    {
      ListSetting.Clear();
      ListSetting = DbSettings.GetAllSettings();
      Print($"Setting = {ListSetting.Count}");
    }

    private string GetFolderPath(Setting setting)
    {
      foreach (var item in ListFolder)
        if (item.IdFolder == setting.IdFolder) return item.FullPath;
      return string.Empty;
    }

    private void StartTestTimersForExperiment1(object sender, EventArgs e)
    {
      /*Tm1 = new System.Threading.Timer(TestActionCreateFolder, null, 1100, 114393 + VxFaker.Random.Int(1, 27));
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
      Ms.Message("START", "Timers were started.").Control(BxTest).Info();*/
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
      TxMessage.Refresh();
      Application.DoEvents();

      /*
      Tm1 = new System.Threading.Timer(TestSelectRandomSetting, null, 1100, 393 + VxFaker.Random.Int(1, 27));
      Tm2 = new System.Threading.Timer(TestSelectRandomSetting, null, 2207, 494 + VxFaker.Random.Int(1, 28));
      Tm3 = new System.Threading.Timer(TestSelectRandomSetting, null, 3311, 395 + VxFaker.Random.Int(1, 29));
      Tm4 = new System.Threading.Timer(TestSelectRandomSetting, null, 4419, 927 + VxFaker.Random.Int(1, 30));
      Tm5 = new System.Threading.Timer(TestActionCreateFolder, null, 5500, 1291 + VxFaker.Random.Int(1, 27));
      Tm6 = new System.Threading.Timer(TestActionAddSettingText, null, 6607, 1594 + VxFaker.Random.Int(1, 28));
      Tm7 = new System.Threading.Timer(TestActionAddSettingInteger64, null, 7711, 1795 + VxFaker.Random.Int(1, 29));
      Tm8 = new System.Threading.Timer(TestActionUpdateRandomSetting, null, 8819, 780 + VxFaker.Random.Int(1, 30));
      Tm9 = new System.Threading.Timer(TestActionUpdateRandomSetting, null, 9900, 871 + VxFaker.Random.Int(1, 27));
      */

      Tm1 = new System.Threading.Timer(TestSelectRandomSetting, null, 1100, 393 + VxFaker.Random.Int(1, 27));
      Tm2 = new System.Threading.Timer(TestSelectRandomSetting, null, 2207, 494 + VxFaker.Random.Int(1, 28));
      Tm3 = new System.Threading.Timer(TestSelectRandomSetting, null, 3311, 547 + VxFaker.Random.Int(1, 29));
      Tm4 = new System.Threading.Timer(TestSelectRandomSetting, null, 4419, 678 + VxFaker.Random.Int(1, 30));
      Tm5 = new System.Threading.Timer(TestActionUpdateRandomSetting, null, 5500, 3300 + VxFaker.Random.Int(1, 27));

      // Tm6 = new System.Threading.Timer(TestActionAddSettingText, null, 6607, 1594 + VxFaker.Random.Int(1, 28));
      // Tm7 = new System.Threading.Timer(TestActionAddSettingInteger64, null, 7711, 1795 + VxFaker.Random.Int(1, 29));
      // Tm8 = new System.Threading.Timer(TestActionUpdateRandomSetting, null, 8819, 780 + VxFaker.Random.Int(1, 30));
      // Tm9 = new System.Threading.Timer(TestActionUpdateRandomSetting, null, 9900, 871 + VxFaker.Random.Int(1, 27));

      BxTest.Click -= StartTestTimersForExperiment2;
      BxTest.Click += StopTestTimersForExperiment2;
      Ms.Message("START Experiment 2", "Timers were started.").Control(BxTest).Info();
      TxMessage.Clear();
    }

    private async void StopTestTimersForExperiment2(object sender, EventArgs e)
    {
      Trace.WriteLine(">>> ============================================================== Stop experiment.");

      Tm1.Change(Timeout.Infinite, Timeout.Infinite);
      Tm2.Change(Timeout.Infinite, Timeout.Infinite);
      Tm3.Change(Timeout.Infinite, Timeout.Infinite);
      Tm4.Change(Timeout.Infinite, Timeout.Infinite);
      Tm5.Change(Timeout.Infinite, Timeout.Infinite);
      //Tm6.Change(Timeout.Infinite, Timeout.Infinite);
      //Tm7.Change(Timeout.Infinite, Timeout.Infinite);
      //Tm8.Change(Timeout.Infinite, Timeout.Infinite);
      //Tm9.Change(Timeout.Infinite, Timeout.Infinite);

      BxTest.Click -= StopTestTimersForExperiment2;
      BxTest.Click += StartTestTimersForExperiment2;

      //Ms.Message("STOP Experiment 2", "Timers were stopped.").Control(BxTest).Ok();

      OperationsRegistrar.TryAdd("Test value 1", 0);
      OperationsRegistrar.TryAdd("Test value 2", 0);
      OperationsRegistrar.TryAdd("Test value 3", 0);

      BxTest.Enabled = false;

      await Task.Delay(15000);

      BxTest.Enabled = true;


      Trace.WriteLine("----------------------------- END ---------------------------");
      foreach (var item in OperationsRegistrar) Trace.WriteLine(item.Key);
    }

    private string GetRandomFolderPath() => ListFolder.PickRandom().FullPath;

    private Setting GetRandomSetting() => ListSetting.PickRandom();

    private void TestActionCreateFolder(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      string guid = "CreateFolder-" + GetNewGuid(); OperationBegin(guid);
      string NameFolder = VxFaker.Commerce.ProductAdjective() + VxFaker.Commerce.ProductName() + VxFaker.Company.CatchPhrase() + VxFaker.Person.FullName;
      NameFolder = NameFolder.SafeSubstring(VxFaker.Random.Int(1, 20), VxFaker.Random.Int(5, 20));
      Trace.WriteLine($"create folder {guid}");
      Stopwatch sw = Stopwatch.StartNew();
      ReturnCode code = DbSettings.FolderInsert(GetRandomFolderPath(), NameFolder);
      sw.Stop();
      if (code.Error)
      {
        Trace.WriteLine("ERROR !!! Could not create a folder.");
        Print($"Error Folder Insert: {code.StringValue}");
      }
      else
      {
        Trace.WriteLine($"CREATE FOLDER {guid} >>>> ms = {sw.ElapsedMilliseconds}");
        OperationSuccess(guid);
      }
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
      Stopwatch sw = Stopwatch.StartNew();
      ReturnCode code = DbSettings.SaveSettingLong(GetRandomFolderPath(), IdSetting, VxFaker.Random.Long());
      sw.Stop();
      if (code.Error) Print($"Error TestActionAddSettingInteger64: {code.StringValue} >>>> ms = {sw.ElapsedMilliseconds}");
    }

    private void TestActionRenameSetting(object sender)
    {
      //int IdFolder = DbSettings.GetRandomIdFolder();
      Setting setting = GetRandomSetting();
      string NewIdSetting = VxFaker.Commerce.Random + "^" + VxFaker.Company.Random + VxFaker.Hacker.Random;
      NewIdSetting = NewIdSetting.SafeSubstring(3, VxFaker.Random.Int(1, NewIdSetting.Length));
      Stopwatch sw = Stopwatch.StartNew();
      ReturnCode code = DbSettings.SettingRename(GetFolderPath(setting), setting.IdSetting, NewIdSetting);
      sw.Stop();
      if (code.Error) Print($"Error TestActionRenameSetting: {code.StringValue} >>>> ms = {sw.ElapsedMilliseconds}");
    }

    private void TestActionAddSettingText(object sender)
    {
      string IdSetting = VxFaker.Company.CompanyName() + VxFaker.Lorem.Sentence(5);
      IdSetting = IdSetting.SafeSubstring(1, VxFaker.Random.Int(5, 100));
      string Text = VxFaker.Phone.PhoneNumber() + " " + VxFaker.Address.SecondaryAddress() + " " + VxFaker.System.DirectoryPath() + " " + VxFaker.Address.StreetAddress();
      Text = Text.SafeSubstring(1, VxFaker.Random.Int(1, Text.Length));

      string guid = "i-" + GetNewGuid(); OperationBegin(guid);
      Trace.WriteLine($"insert ... {guid} ... (({IdSetting}))");
      Stopwatch sw = Stopwatch.StartNew();
      ReturnCode code = DbSettings.SaveSettingText(GetRandomFolderPath(), IdSetting, TypeSetting.Text, Text);
      sw.Stop();
      if (code.Error)
        Trace.WriteLine($"Error [insert] --- {guid} --- TestActionAddSettingText: {code.StringValue}");
      else
      {
        Trace.WriteLine($"INSERT --- {guid} --- (({IdSetting})) >>>> ms = {sw.ElapsedMilliseconds}");
        OperationSuccess(guid);
      }
    }

    private void TestActionUpdateRandomSetting(object sender)
    {
      Setting setting = GetRandomSetting();
      string FolderPath = GetFolderPath(setting);
      ReturnCode code = ReturnCodeFactory.Error("cancel");

      string guid = "U-" + GetNewGuid(); OperationBegin(guid);

      Trace.WriteLine($"update ... {guid} ... [[{setting.IdSetting}]]");

      Stopwatch sw = Stopwatch.StartNew();
      if (setting.IdType == (int)TypeSetting.Boolean)
      {
        code = DbSettings.SaveSettingBoolean(FolderPath, setting.IdSetting, VxFaker.Random.Bool());
      }
      else if (setting.IdType == (int)TypeSetting.Datetime)
      {
        DateTime dt = new DateTime(2000, 1, 1).AddSeconds(VxFaker.Random.Int(1, 600000000));
        code = DbSettings.SaveSettingDatetime(FolderPath, setting.IdSetting, dt);
      }
      else if (setting.IdType == (int)TypeSetting.Integer64)
      {
        code = DbSettings.SaveSettingLong(FolderPath, setting.IdSetting, VxFaker.Random.Long());
      }

      if
       (
       (setting.IdType == (int)TypeSetting.Password) ||
       (setting.IdType == (int)TypeSetting.File) ||
       (setting.IdType == (int)TypeSetting.Folder) ||
       (setting.IdType == (int)TypeSetting.Text))
      {
        string Text = VxFaker.Hacker.Abbreviation() + "~" + VxFaker.Hacker.Adjective() + "$" + VxFaker.Hacker.Phrase() + VxFaker.Company.CompanyName() + VxFaker.Lorem.Sentence(5) + VxFaker.Phone.PhoneNumber() + " " + VxFaker.Address.SecondaryAddress() + " " + VxFaker.System.DirectoryPath() + " " + VxFaker.Address.StreetAddress();
        Text = Text.SafeSubstring(1, VxFaker.Random.Int(1, Text.Length));
        code = DbSettings.SaveSettingText(FolderPath, setting.IdSetting, (TypeSetting)setting.IdType, Text);
      }

      if (setting.IdType == (int)TypeSetting.Color)
      {
        Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        code = DbSettings.SaveSettingColor(FolderPath, setting.IdSetting, color);
      }

      if (setting.IdType == (int)TypeSetting.Font)
      {
        Font font = new Font("Arial", rnd.Next(8, 28));
        code = DbSettings.SaveSettingFont(FolderPath, setting.IdSetting, font);
      }
      sw.Stop();


      if (code.StringValue == "cancel")
      {
        Trace.WriteLine($"^^^ {guid} --- Cancel updating of setting"); return;
      }

      if (code.Error)
        Trace.WriteLine($"!!! {guid} --- Could not update a setting. Error = {code.StringValue}");
      else
      {
        Trace.WriteLine($"UPDATE --- {guid} --- [[{setting.IdSetting}]] --- {setting.IdType} >>>> ms = {sw.ElapsedMilliseconds}");
        OperationSuccess(guid);
      }
    }

    private void TestSelectRandomSetting(object sender)
    {
      Setting setting = GetRandomSetting();
      string guid = "S-" + GetNewGuid(); OperationBegin(guid);
      Trace.WriteLine($"select ... {guid} ... <<{setting.IdSetting}>>");
      Stopwatch sw = Stopwatch.StartNew();
      ReceivedValueText value = DbSettings.GetSettingText(GetFolderPath(setting), setting.IdSetting);
      sw.Stop();
      OperationSuccess(guid);
      Trace.WriteLine($"SELECT --- {guid} --- <<{setting.IdSetting}>> value === {value.Value} >>>> ms = {sw.ElapsedMilliseconds}");
    }

    private void TestGetRandomSettingReadWrite(object sender, EventArgs e)
    {
      if (ListSetting.Count < 1)
      {
        MessageBox.Show("ERROR ! List of Settings is empty.");
        return;
      }

      TxMessage.Clear();
      Setting setting = GetRandomSetting();
      string FolderPath = GetFolderPath(setting);
      TypeSetting type = (TypeSetting)setting.IdType;

      Print($"---------------------- Random setting has been selected: ");
      Print($"IdFolder = {setting.IdFolder}; ------- FolderPath = {FolderPath}");
      Print($"IdSetting = {setting.IdSetting}; ------- Type={type}");
      Print($"String Value = {setting.SettingValue}");

      switch (type)
      {
        case TypeSetting.Boolean:
          TestReadWriteSettingBoolean(FolderPath, setting);
          break;
        case TypeSetting.Color:
          TestReadWriteSettingColor(FolderPath, setting);
          break;
        case TypeSetting.Datetime:
          TestReadWriteSettingColor(FolderPath, setting);
          break;
        case TypeSetting.File:
          TestReadWriteSettingFile(FolderPath, setting);
          break;
        case TypeSetting.Folder:
          TestReadWriteSettingFolder(FolderPath, setting);
          break;
        case TypeSetting.Font:
          TestReadWriteSettingFont(FolderPath, setting);
          break;
        case TypeSetting.Integer64:
          TestReadWriteSettingInteger64(FolderPath, setting);
          break;
        case TypeSetting.Password:
          TestReadWriteSettingPassword(FolderPath, setting);
          break;
        case TypeSetting.Text:
          TestReadWriteSettingText(FolderPath, setting);
          break;
        default:
          break;
      }
    }

    private void TestReadWriteSettingBoolean(string FolderPath, Setting setting)
    {
      ReceivedValueBoolean value = DbSettings.GetSettingBoolean(FolderPath, setting.IdSetting);
      if (value.Code.Error)
      {
        Print($"ERROR !!! Could not get current value of setting {setting.IdSetting}"); return;
      }
      Print($"Current value of setting = {value.Value}");
      //---------------------------------------------------------------------------------------------------------
      bool NewValue = !value.Value;
      ReturnCode code = DbSettings.SaveSettingBoolean(FolderPath, setting.IdSetting, NewValue);
      if (code.Error)
      {
        Print($"ERROR *** Could not SAVE value = {NewValue} of setting {setting.IdSetting}"); return;
      }
      Print($"Saved new value of setting = {NewValue}");
      Thread.Sleep(500);
      //---------------------------------------------------------------------------------------------------------
      value = DbSettings.GetSettingBoolean(FolderPath, setting.IdSetting);
      if (value.Code.Error)
      {
        Print($"ERROR !?!?! Could not get current value of setting {setting.IdSetting}"); return;
      }
      if (value.Value != NewValue )
      {
        Print($"ERROR !*!*! New value = {NewValue} , Current value = {setting.IdSetting}"); return;
      }
      Print("------------------- OK. Test passed. -------------------");
    }

    private void TestReadWriteSettingColor(string FolderPath, Setting setting) // TODO: Fill it and other methods
    {
      //Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
    }

    private void TestReadWriteSettingDatetime(string FolderPath, Setting setting)
    {
      //DateTime NewValue = new DateTime(2000, 1, 1).AddSeconds(VxFaker.Random.Int(1, 600000000));
    }

    private void TestReadWriteSettingFile(string FolderPath, Setting setting)
    {

    }

    private void TestReadWriteSettingFolder(string FolderPath, Setting setting)
    {

    }

    private void TestReadWriteSettingFont(string FolderPath, Setting setting)
    {
      Font NewValue = new Font(ListFont.PickRandom(), rnd.Next(8, 28));
    }

    private void TestReadWriteSettingInteger64(string FolderPath, Setting setting)
    {
      long NewValue = VxFaker.Random.Long();
    }

    private void TestReadWriteSettingPassword(string FolderPath, Setting setting)
    {

    }

    private void TestReadWriteSettingText(string FolderPath, Setting setting)
    {

    }
  }
}
