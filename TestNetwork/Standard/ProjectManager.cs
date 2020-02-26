using ProjectStandard;
using TJFramework;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public class ProjectManager: IOutputMessage
  {
    public string DatetimeFormat { get; } = "yyyy-MM-dd HH:mm:ss";

    public ConverterDatetime CvDatetime { get; } = new ConverterDatetime();

    public ConverterBoolean CvBoolean { get; } = new ConverterBoolean();

    public ConverterInt64 CvInt64 { get; } = new ConverterInt64();

    public UiControlManager UiControl { get; } = new UiControlManager();

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