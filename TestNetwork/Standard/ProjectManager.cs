using ProjectStandard;
using TJFramework;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public class ProjectManager: IOutputMessage
  {
    public string DatetimeFormat { get; } = "yyyy-MM-dd hh:mm:ss";

    public ConverterDatetime CvDatetime { get; } = new ConverterDatetime();

    public ConverterBoolean CvBoolean { get; } = new ConverterBoolean();

    public void OutputMessage(string message, string header = "")
    {
      Ms.Message(message, header).NoAlert().Debug();
    }

    public string RemoveSpecialCharacters(string NameFolder)
    {
      return NameFolder.Trim().Replace(' ', '_').RemoveSpecialCharacters();
    }
  }

}