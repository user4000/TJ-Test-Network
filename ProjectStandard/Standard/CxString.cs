using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectStandard.Tools
{
  public static class CxString
  {
    public static NumberFormatInfo Nfi { get; } = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

    public const string Empty = "";

    static CxString()
    {
      Nfi.NumberGroupSeparator = " ";
    }

    public static string FormatNumber(long value) => value.ToString("#,##0", Nfi);

    public static bool IsHexOnly(string StringValue)
    {
      // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
      return Regex.IsMatch(StringValue, @"\A\b[0-9a-fA-F]+\b\Z");
    }

    public static string ReplaceMultipleSpaces(string s) // Убирает несколько идущих подряд пробелов и оставляет только один пробел //
    {
      RegexOptions options = RegexOptions.None; Regex regex = new Regex("[ ]{2,}", options); return regex.Replace(s, " ");
    }

    public static string GetOnlyLetterDigitOrSpace(string str) => new string((from c in str where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());

    public static string GetDateWithMonthInWord(int year, int month, int day)
    {
      string Month = string.Empty;
      switch (month)
      {
        case 1: Month = "января"; break;
        case 2: Month = "февраля"; break;
        case 3: Month = "марта"; break;
        case 4: Month = "апреля"; break;
        case 5: Month = "мая"; break;
        case 6: Month = "июня"; break;
        case 7: Month = "июля"; break;
        case 8: Month = "августа"; break;
        case 9: Month = "сентября"; break;
        case 10: Month = "октября"; break;
        case 11: Month = "ноября"; break;
        case 12: Month = "декабря"; break;
      }
      return day.ToString() + " " + Month + " " + year.ToString() + " г."; 
    }

    public static string GetDateWithMonthInWord(DateTime dt) => GetDateWithMonthInWord(dt.Year, dt.Month, dt.Day);

  }
}
