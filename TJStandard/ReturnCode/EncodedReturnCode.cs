using System;

namespace TJStandard
{

  /*
  The logic is this:
  When analyzing the json that came from the server, you need to check if there is any encoded message in this json
  If it is there, then an error occurred (in the case if we expected to get a list of objects, and not just the Return Code, EVEN IF ReturnCode = 0)
  If there is no encoded message, then try to deserialize json into a list of objects.
  */

  public class EncodedReturnCode 
  {
    public static string TextMessageStart { get; } = "<<(<<";

    public static string TextMessageEnd { get; } = ">>)>>";

    public static string NumericCodeStart { get; } = "<(<(<";

    public static string NumericCodeEnd { get; } = ">)>)>";

    public static bool TextDoesNotContainEncodedMessage(string text) => !TextContainsEncodedMessage(text);

    public static bool TextContainsEncodedMessage(string text)
    {
      int a = text.IndexOf(TextMessageStart, 0);
      if (a < 0) return false;
      int b = text.IndexOf(TextMessageEnd, a + 1);
      return (b > a);
    }

    private static string ExtractTextFromEncodedMessage(string json, string StartBracket, string EndBracket)
    {
      if (json.Trim().Length < StartBracket.Length + EndBracket.Length + 1) return string.Empty;

      int a = json.IndexOf(StartBracket, 0);
      int b = json.IndexOf(EndBracket, Math.Max(a,0) + 1);
      if ((a < 0) || (b < 0) || (a > b)) return string.Empty;
      string s = string.Empty;
      try {  s = json.Substring(a + StartBracket.Length, b - a - EndBracket.Length); } catch { s = string.Empty; }
      return s.Trim();
    }

    public static string ExtractTextMessage(string json) => ExtractTextFromEncodedMessage(json, TextMessageStart, TextMessageEnd);

    public static string ExtractNumericCode(string json) => ExtractTextFromEncodedMessage(json, NumericCodeStart, NumericCodeEnd);

    public static ReturnCode ReturnCode(string json) // The logic is this: if the message text is empty, then the result = Success, otherwise = depending on the code //
    {
      ReturnCode code = ReturnCodeFactory.Error(ExtractTextMessage(json));   
      code.NumericValue = CxConvert.ToInt32(ExtractNumericCode(json), ReturnCodeFactory.NcError);     
      if (code.StringValue == string.Empty) code.NumericValue = ReturnCodeFactory.NcSuccess;
      return code;
    }

    public static string CreateErrorMessage(string TextMessage, int NumericCode)
    {
      return TextMessageStart + TextMessage.Trim() + TextMessageEnd + " " + NumericCodeStart + NumericCode.ToString() + NumericCodeEnd;
    }
  }
}
