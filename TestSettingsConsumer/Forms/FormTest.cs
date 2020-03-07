using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using TJStandard;
using TJSettings;
using static TestSettingsConsumer.Program;
using static TJFramework.TJFrameworkManager;

namespace TestSettingsConsumer
{
  public partial class FormTest : RadForm, IEventStartWork
  {
    private LocalDatabaseOfSettings DbSettings = new LocalDatabaseOfSettings();

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
      BxTest.Click += EventExecuteTest;
    }

    public void EventStartWork()
    {
      InitSettingsDatabase();
      SetEvents();
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

    private void EventExecuteTest(object sender, EventArgs e)
    {
      string Test = DxTest.Text;
      string Arg1 = TxOne.Text;
      string Arg2 = TxTwo.Text;
      string Result = string.Empty;
      int Num1 = CxConvert.ToInt32(Arg1, -1);

      //if (Test == "SettingGetValue") Result = ExecuteTest1(Arg1, Arg2);

      TxMessage.Text = Result;
      Ms.Message("", "Test has been passed").Control(BxTest).Debug();
    }

    private string ExecuteTest1(string NameFolder, string NameSetting)
    {
      return ""; // ReturnCodeFormatter.ToString(code);
    }
  }
}
