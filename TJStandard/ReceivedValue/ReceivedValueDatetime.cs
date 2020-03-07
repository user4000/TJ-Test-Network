using System;

namespace TJStandard
{
  public class ReceivedValueDatetime
  {
    public DateTime Value { get; set; } = DateTime.MinValue;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();
  }
}

