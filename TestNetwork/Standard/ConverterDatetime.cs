using System;
using System.Globalization;

namespace TestNetwork
{
  public class ConverterDatetime
  {
    public string ToString(DateTime value)
    {
      return value.ToString(Program.Manager.DatetimeFormat);
    }

    public DateTime FromString(string value)
    {
      return DateTime.ParseExact(value, Program.Manager.DatetimeFormat, CultureInfo.InvariantCulture);
    }
  }
}