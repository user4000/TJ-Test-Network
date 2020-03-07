using System.Drawing;

namespace TJStandard
{
  public class ReceivedValueFont
  {
    public static Font DefaultValue { get; } = new Font("Verdana", 9);
    public Font Value { get; set; } = new Font("Verdana", 9);
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueFont(ReturnCode code, Font value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueFont Create(ReturnCode code, Font value) => new ReceivedValueFont(code, value);

    public static ReceivedValueFont Success(Font value) => new ReceivedValueFont(ReturnCodeFactory.Success(), value);

    public static ReceivedValueFont Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueFont(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

