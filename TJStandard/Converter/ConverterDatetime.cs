using System;
using System.Globalization;

namespace TJStandard
{
  public class ConverterDatetime
  {
    private DateTime DefaultDatetime { get; } = DateTime.MinValue;
    public string DatetimeFormat { get; } = "yyyy-MM-dd HH:mm:ss";
    public string ToString(DateTime value)
    {
      return value.ToString(DatetimeFormat);
    }

    public DateTime FromString(string value)
    {
      DateTime dt;
      try
      {
        dt = DateTime.ParseExact(value, DatetimeFormat, CultureInfo.InvariantCulture);
      }
      catch
      {
        dt = DefaultDatetime;
      }
      return dt;
    }
  }
}