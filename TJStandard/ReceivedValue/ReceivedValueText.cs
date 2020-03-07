namespace TJStandard
{
  public class ReceivedValueText
  {
    public string Value { get; set; } = string.Empty;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();
  }
}

