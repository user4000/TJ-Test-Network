using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TJFramework;
using TJStandard;
using TJSettings;
using static TestNetwork.Program;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormTest : RadForm, IEventStartWork, IOutputMessage
  {
    private LocalDatabaseOfSettings DbSettings { get => Manager.DbSettings; }

    public FormTest()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      BxTest.Click += EventTest;
    }

    private void EventTest(object sender, EventArgs e)
    {
      Stopwatch sw = Stopwatch.StartNew();
      sw.Stop();
      BxTest.Click -= EventTest;
      BxTest.Click += EventFindIdFolder;
    }

    private void EventFindIdFolder(object sender, EventArgs e)
    {
      int IdFolder = DbSettings.GetIdFolder(TxTest.Text);
      string s1 = "Item1";
      string s2 = "Item2";
      string s3 = "Nested_Folder";

      IdFolder = DbSettings.GetIdFolder($"Application root folder\\My_Application_Server\\{s1}\\{s2}\\{s3}");
      BxTest.Text = IdFolder.ToString();
      Ms.Message("IdFolder", IdFolder.ToString()).Control(TxTest).Debug();
    }

    public void OutputMessage(string message, string header = "") => Debug(message, header);

    public void Debug(string message, string header = "")
    {
      TxMessage.AppendText((header + " " + message).Trim() + Environment.NewLine);
    }
  }
}
