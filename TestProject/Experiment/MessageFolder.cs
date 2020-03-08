using TJSettings;

namespace TestProject
{
  public class MessageFolderAdd
  {
    public Folder VxFolder { get; } = null;
    public MessageFolderAdd(Folder folder) => VxFolder = folder;
  }

  public class MessageFolderDelete
  {
    public int IdFolder { get; } = -1;

    public MessageFolderDelete(int idFolder) => IdFolder = idFolder;
  }

  public class MessageFolderGenerateRandom
  {
    public int Count { get; } = 1;

    public MessageFolderGenerateRandom(int count) => Count = count;
  }

  public class MessageRequestFolderCount
  {

  }

  public class MessageResponseFolderCount
  {
    public int Count { get; } = 0;

    public MessageResponseFolderCount(int count) => Count = count;
  }
}
