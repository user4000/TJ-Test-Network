namespace TJStandard
{
  public class ReceivedValueInteger32
  {
    public static int DefaultValue { get; } = -1;
    public int Value { get; set; } = -1;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueInteger32(ReturnCode code, int value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueInteger32 Create(ReturnCode code, int value) => new ReceivedValueInteger32(code, value);

    public static ReceivedValueInteger32 Success(int value) => new ReceivedValueInteger32(ReturnCodeFactory.Success(), value);

    public static ReceivedValueInteger32 Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueInteger32(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

