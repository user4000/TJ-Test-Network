using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using TJSettings;

namespace TestProject
{
  public class ActorFolderManager : ReceiveActor
  {
    public List<Folder> ListFolders = new List<Folder>();

    public ActorFolderManager()
    {
      Receive<MessageFolderAdd>(EventFolderAdd);
      Receive<MessageFolderDelete>(EventFolderDelete);
      Receive<MessageFolderGenerateRandom>(EventGenerateRandomFolders);
      Receive<MessageRequestFolderCount>(EventRequestFolderCount);
    }

    public void AddRandomFolder()
    {
      ListFolders.Add
        (new Folder
          (
          Faker.RandomNumber.Next(1, 1000),
          Faker.Company.Name() + " " + Faker.Address.StreetName() + " " + Faker.Internet.UserName(),
          Faker.Phone.Number() + Faker.Name.FullName() + " " + Faker.Company.Name(),
          Faker.RandomNumber.Next(1, 1000)
          )
        );
    }

    private bool EventGenerateRandomFolders(MessageFolderGenerateRandom message)
    {
      Sender.Tell("START ---- EventGenerateRandomFolders");
      for (int i = 1; i <= message.Count; i++) AddRandomFolder();
      Sender.Tell("END ---- EventGenerateRandomFolders");
      return true;
    }

    private bool EventFolderDelete(MessageFolderDelete message)
    {
      Folder item = ListFolders.FirstOrDefault(f => f.IdFolder == message.IdFolder);
      if (item != null) ListFolders.Remove(item);
      return true;
    }

    private bool EventFolderAdd(MessageFolderAdd message)
    {
      if (ListFolders.Count > 2000) return true;
      ListFolders.Add(message.VxFolder);
      return true;
    }

    private bool EventRequestFolderCount(MessageRequestFolderCount obj)
    {
      Sender.Tell(Self.Path.ToStringWithAddress());
      Sender.Tell(new MessageResponseFolderCount(ListFolders.Count)); return true;
    }
  }
}
