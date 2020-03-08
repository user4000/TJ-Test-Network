using System;
using System.Linq;
using Akka.Actor;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TJFramework;
using TJStandard;
using TJSettings;
using static TestProject.Program;
using static TJFramework.TJFrameworkManager;
using static TJFramework.Logger.Manager;

namespace TestProject
{
  public partial class FormTest : RadForm, IEventStartWork, ICanPrint
  {
    public System.Threading.Timer Tm1;

    public System.Threading.Timer Tm2;

    public System.Threading.Timer Tm3;

    public System.Threading.Timer Tm4;

    public ExperimentOne TestOne = new ExperimentOne();

    public IActorRef AcFolderManager { get; set; } = null;

    public IActorRef AcCommander { get; set; } = null;


    public FormTest()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      SetEvent();
      SetActor();
    }

    public void Print(string message)
    {
      if (TxMessage.InvokeRequired)
        TxMessage.Invoke((MethodInvoker)delegate
        {
          TxMessage.AppendText(message + Environment.NewLine);
        });
      else
        TxMessage.AppendText(message + Environment.NewLine);
    }

    public void SetEvent()
    {
      BxTest.Click += EventTest;
    }

    private void SetActor()
    {
      AcCommander = AcSystem.ActorOf<ActorCommander>("Actor_Commander");
      AcCommander.Tell(this);
      AcCommander.Tell(new MessageFolderGenerateRandom(1000));
    }

    public Folder GenerateRandomFolder()
    {
      return new Folder
      (
        Faker.RandomNumber.Next(1, 1000),
        Faker.Company.Name() + " " + Faker.Address.StreetName() + " " + Faker.Internet.UserName(),
        Faker.Phone.Number() + Faker.Name.FullName() + " " + Faker.Company.Name(),
        Faker.RandomNumber.Next(1, 1000)
      );       
    }

    private void EventTest(object sender, EventArgs e)
    { 
      //TestOne.AddRandomFolder(1000);

     
      //foreach (var item in TestOne.ListFolders) Print(item.ToString());

      Print("Starting ...");

      /*Tm1.Tick += EventTimerTick;
      Tm1.Interval = 15 + Faker.RandomNumber.Next(1, 10);
      Tm2.Tick += EventTimerTick;
      Tm2.Interval = 14 + Faker.RandomNumber.Next(1, 11);
      Tm3.Tick += EventTimerTick;
      Tm3.Interval = 13 + Faker.RandomNumber.Next(1, 12);
      Tm4.Tick += EventTimerTick;
      Tm4.Interval = 12 + Faker.RandomNumber.Next(1, 15);
      Tm1.Enabled = true;
      Tm2.Enabled = true;
      Tm3.Enabled = true;
      Tm4.Enabled = true;*/

      Tm1 = new System.Threading.Timer(EventTimerTickForActor, null, 100, 193);
      Tm2 = new System.Threading.Timer(EventTimerTickForActor, null, 120, 194);
      Tm3 = new System.Threading.Timer(EventTimerTickForActor, null, 150, 195);
      Tm4 = new System.Threading.Timer(EventTimerTickForActor, null, 170, 197);

      BxTest.Click -= EventTest;
      BxTest.Click += EventShowCount;
      BxTest.Text = "Show count";
    }

    private void EventShowCount(object sender, EventArgs e)
    {
      //Print(TestOne.ListFolders.Count.ToString());
      AcCommander.Tell(new MessageRequestFolderCount());
    }

    private void EventTimerTick(object sender)
    {
     int x = Faker.RandomNumber.Next(1, 1000);
      if (x > 500)
        EventAddRandomFolder();
      else
        EventDeleteRandomFolder();
    }

    private void EventAddRandomFolder()
    {
      if (TestOne.ListFolders.Count > 500) return;
      TestOne.AddRandomFolder();
    }

    private void EventDeleteRandomFolder()
    {
      int random = Faker.RandomNumber.Next(1, 1000);
      Folder item = TestOne.ListFolders.FirstOrDefault(f => f.IdFolder == random);
      if (item != null) TestOne.ListFolders.Remove(item);
    }

    private void EventTimerTickForActor(object sender)
    {
      int x = Faker.RandomNumber.Next(1, 1000);
      if (x > 500)
        AcCommander.Tell(new MessageFolderAdd(GenerateRandomFolder()));
      else
        AcCommander.Tell(new MessageFolderDelete(Faker.RandomNumber.Next(1, 1000)));
    }
  }
}
