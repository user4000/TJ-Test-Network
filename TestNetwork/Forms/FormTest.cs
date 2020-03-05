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
      DbSettings.FillListFolders();
      sw.Stop();
      Debug($"{sw.ElapsedMilliseconds}", "Milliseconds elapsed =");
      Debug($"{DbSettings.ListFolders.Count}", "count =");

      foreach (var item in DbSettings.ListFolders)
        Debug(item.ToString());

      BxTest.Click -= EventTest;
      BxTest.Click += EventFindIdFolder;
    }

    private void EventFindIdFolder(object sender, EventArgs e)
    {
      int IdFolder = DbSettings.GetIdFolder(TxTest.Text);
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
