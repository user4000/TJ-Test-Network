using System;
using System.Globalization;

namespace TJStandard
{
  public class ConverterDatetime
  {  
    public string DatetimeFormat { get; } = "yyyy-MM-dd HH:mm:ss";
    public string ToString(DateTime value)
    {
      return value.ToString(DatetimeFormat);
    }

    public ReceivedValueDatetime FromString(string value)
    {
      DateTime dt;
      try
      {
        dt = DateTime.ParseExact(value, DatetimeFormat, CultureInfo.InvariantCulture);
      }
      catch
      {
        return ReceivedValueDatetime.Error(ReturnCodeFactory.NcError);
      }
      return ReceivedValueDatetime.Success(dt);
    }
  }
}