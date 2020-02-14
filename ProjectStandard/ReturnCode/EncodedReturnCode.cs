using System;

namespace ProjectStandard
{

/*
Логика такая:
При анализе пришедшего с сервера json нужно проверить - есть ли вообще закодированное сообщение в этом json
Если оно там есть, значит произошла ошибка (в том случае если мы ожидали получить список объектов, а не один только Return Code, ДАЖЕ ЕСЛИ ReturnCode=0)
Если там нет закодированного сообщения, тогда пытаемся десериализовать json в список объектов.
*/

  public class EncodedReturnCode 
  {
    public static string MessageStart { get; } = "<<(<<";

    public static string MessageEnd { get; } = ">>)>>";

    public static string CodeStart { get; } = "<(<(<";

    public static string CodeEnd { get; } = ">)>)>";

    public static bool TextDoesNotContainEncodedMessage(string text) => !TextContainsEncodedMessage(text);

    public static bool TextContainsEncodedMessage(string text)
    {
      int a = text.IndexOf(MessageStart, 0);
      if (a < 0) return false;
      int b = text.IndexOf(MessageEnd, a + 1);
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

    public static string ExtractMessage(string json) => ExtractTextFromEncodedMessage(json, MessageStart, MessageEnd);

    public static string ExtractCode(string json) => ExtractTextFromEncodedMessage(json, CodeStart, CodeEnd);

    public static ReturnCode ReturnCode(string json) // Логика такая: если текст сообщения - пустой, то результат = Успех, иначе = в зависимости от кода //
    {
      ReturnCode code = ReturnCodeFactory.Error(ExtractMessage(json));   
      code.NumericValue = CxConvert.ToInt32(ExtractCode(json), ReturnCodeFactory.NcError);     
      if (code.StringValue == string.Empty) code.NumericValue = ReturnCodeFactory.NcSuccess;
      return code;
    }

    public static string CreateErrorMessage(string ErrorMessage, int Code)
    {
      return MessageStart + ErrorMessage.Trim() + MessageEnd + " " + CodeStart + Code.ToString() + CodeEnd;
    }
  }
}
