using System;
using System.Windows.Forms;
using Akka.Actor;
using TJFramework;
using static TestProject.Program;
using static TJFramework.Logger.Manager;

namespace TestProject
{
  public class ActorCommander : ReceiveActor
  {
    public FormTest Form { get; set; } = null;

    public IActorRef AcFolderManager { get; set; } = null;

    public ActorCommander()
    {
      Receive<FormTest>(EventInitializeForm);
      Receive<MessageFolderAdd>(obj => { AcFolderManager.Tell(obj); });
      Receive<MessageFolderDelete>(obj => { AcFolderManager.Tell(obj); });
      Receive<MessageFolderGenerateRandom>(obj => { AcFolderManager.Tell(obj);});
      Receive<MessageRequestFolderCount>(obj => { AcFolderManager.Tell(obj);});
      Receive<MessageResponseFolderCount>(EventResponseFolderCount);
      Receive<string>(EventSayHello);       
    }

    private bool EventResponseFolderCount(MessageResponseFolderCount obj)
    {
      Form.Print("FOLDER COUNT = " + obj.Count.ToString()); return true;
    }

    private bool EventSayHello(string obj)
    {
      //Log.Save(MsgType.Debug, "", "EventSayHello");
      Form.Print(obj); return true;
    }

    private bool EventInitializeForm(FormTest form)
    {
      //Log.Save(MsgType.Debug, "", "EventInitializeForm");
      if (Form != null) return true;
      Form = form;
      AcFolderManager = AcSystem.ActorOf<ActorFolderManager>("Actor_Folder_Manager");
      form.Print("Actor Commander is ACTIVE");
      return true;
    }
  }
}

/*
    private bool EventRequestFolderCount(MessageRequestFolderCount obj)
    {
      AcFolderManager.Tell(obj); return true;
    }

    private bool EventGenerateRandomFolders(MessageFolderGenerateRandom obj)
    {
      AcFolderManager.Tell(obj); return true;
    }

    private bool EventFolderDelete(MessageFolderDelete obj)
    {
      AcFolderManager.Tell(obj); return true;
    }

    private bool EventFolderAdd(MessageFolderAdd obj)
    {
      AcFolderManager.Tell(obj); return true;
    }
*/
