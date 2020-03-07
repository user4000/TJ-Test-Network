namespace TJStandard
{
  public class ReceivedValueInteger64
  {
    public static long DefaultValue { get; } = -1;
    public long Value { get; set; } = -1;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueInteger64(ReturnCode code, long value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueInteger64 Create(ReturnCode code, long value) => new ReceivedValueInteger64(code, value);

    public static ReceivedValueInteger64 Success(long value) => new ReceivedValueInteger64(ReturnCodeFactory.Success(), value);

    public static ReceivedValueInteger64 Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueInteger64(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

