using System;

namespace TJStandard
{
  public class ReceivedValueDatetime
  {
    public static DateTime DefaultValue { get; } = DateTime.MinValue;
    public DateTime Value { get; set; } = DateTime.MinValue;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueDatetime(ReturnCode code, DateTime value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueDatetime Create(ReturnCode code, DateTime value) => new ReceivedValueDatetime(code, value);

    public static ReceivedValueDatetime Success(DateTime value) => new ReceivedValueDatetime(ReturnCodeFactory.Success(), value);

    public static ReceivedValueDatetime Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueDatetime(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

