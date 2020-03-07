using System;
using System.Linq;
using System.Text;

namespace TJStandard
{
  public static class XxString
  {
    public static string Left(this string value, int length)
    {
      if (string.IsNullOrEmpty(value)) return value;
      length = Math.Abs(length);
      return value.Length <= length ? value : value.Substring(0, length);
    }

    public static string Right(this string value, int length)
    {
      value = value ?? string.Empty;
      length = Math.Abs(length);
      return (value.Length >= length) ? value.Substring(value.Length - length, length) : value;
    }

    public static string SurroundWithDoubleQuotes(this string text) => SurroundWith(text, "\"");

    public static string SurroundWith(this string text, string ends) => ends + text + ends;
 
    public static string FirstCharToUpper(this string input)
    {
      switch (input)
      {
        case null: return string.Empty; // throw new ArgumentNullException(nameof(input));
        case "": return string.Empty;   // throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
        default: return input.First().ToString().ToUpper() + input.Substring(1);
      }
    }
  }
}
