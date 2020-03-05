using System;
using System.Globalization;

namespace TJSettings
{
  public class ConverterDatetime
  {
    public string DatetimeFormat { get; } = "yyyy-MM-dd HH:mm:ss";
    public string ToString(DateTime value)
    {
      return value.ToString(DatetimeFormat);
    }

    public DateTime FromString(string value)
    {
      return DateTime.ParseExact(value, DatetimeFormat, CultureInfo.InvariantCulture);
    }
  }
}