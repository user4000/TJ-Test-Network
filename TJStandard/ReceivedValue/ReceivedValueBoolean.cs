namespace TJStandard
{
  public class ReceivedValueBoolean
  {
    public static bool DefaultValue { get; } = false;
    public bool Value { get; set; } = false;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueBoolean(ReturnCode code, bool value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueBoolean Create(ReturnCode code, bool value) => new ReceivedValueBoolean(code, value);

    public static ReceivedValueBoolean Success(bool value) => new ReceivedValueBoolean(ReturnCodeFactory.Success(), value);

    public static ReceivedValueBoolean Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueBoolean(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

