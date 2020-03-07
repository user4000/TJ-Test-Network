namespace TJStandard
{
  public class ReceivedValueText
  {
    public static string DefaultValue { get; } = string.Empty;
    public string Value { get; private set; } = string.Empty;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueText(ReturnCode code, string value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueText Create(ReturnCode code, string value) => new ReceivedValueText(code, value);

    public static ReceivedValueText Success(string value) => new ReceivedValueText(ReturnCodeFactory.Success(), value);

    public static ReceivedValueText Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueText(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

