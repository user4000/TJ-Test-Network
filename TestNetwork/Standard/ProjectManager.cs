using TJStandard;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public class ProjectManager: IOutputMessage
  {
    public const string Empty = "";

    public string DatetimeFormat { get; } = "yyyy-MM-dd HH:mm:ss";

    public ConverterDatetime CvDatetime { get; } = new ConverterDatetime();

    public ConverterBoolean CvBoolean { get; } = new ConverterBoolean();

    public ConverterInt64 CvInt64 { get; } = new ConverterInt64();

    public ConverterFont CvFont { get; } = new ConverterFont();

    public ConverterColor CvColor { get; } = new ConverterColor();

    public UiControlManager UiControl { get; } = new UiControlManager();

    private FormTreeView FxTreeview { get; set; } = null;

    internal void Init(FormTreeView form)
    {
      FxTreeview = form;
      UiControl.Init(form);
    }

    public void OutputMessage(string message, string header = Empty)
    {
      Ms.Message(message, header).NoAlert().Debug();
    }

    public string RemoveSpecialCharacters(string NameFolder)
    {
      return NameFolder.Trim().Replace(' ', '_').RemoveSpecialCharacters();
    }
  }
}