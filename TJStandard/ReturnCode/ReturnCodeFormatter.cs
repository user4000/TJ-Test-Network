namespace TJStandard
{
  public class ReturnCodeFormatter
  {
    public static char DoubleQuote { get; } = '"';

    public static bool MayBeReturnCode(string json) =>
      json.Contains(DoubleQuote + nameof(ReturnCode.NumericValue)  + DoubleQuote) &&
      json.Contains(DoubleQuote + nameof(ReturnCode.StringValue)   + DoubleQuote) &&
      json.Contains(DoubleQuote + nameof(ReturnCode.IdObject)      + DoubleQuote) &&
      json.Contains(DoubleQuote + nameof(ReturnCode.StringNote)    + DoubleQuote);
    
    public static string ToString(ReturnCode code)
    {
      return $"{code.NumericValue};{code.IdObject};{code.StringValue};{code.StringNote}";
    }
  }
}
