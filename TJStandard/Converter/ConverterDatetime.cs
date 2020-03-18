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
      catch (Exception ex)
      {
        return ReceivedValueDatetime.Error(ReturnCodeFactory.NcError, ex.Message);
      }
      return ReceivedValueDatetime.Success(dt);
    }
  }
}