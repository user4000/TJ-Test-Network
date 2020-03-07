using System.Drawing;

namespace TJStandard
{
  public class ReceivedValueColor
  {
    public static Color DefaultValue { get; } = Color.Black;
    public Color Value { get; set; } = Color.Black;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();

    private ReceivedValueColor(ReturnCode code, Color value)
    {
      Value = value; Code = code;
    }

    public static ReceivedValueColor Create(ReturnCode code, Color value) => new ReceivedValueColor(code, value);

    public static ReceivedValueColor Success(Color value) => new ReceivedValueColor(ReturnCodeFactory.Success(), value);

    public static ReceivedValueColor Error(int ErrorCode, string ErrorMessage = CxConvert.Empty)
    {
      return new ReceivedValueColor(ReturnCodeFactory.Error(ErrorCode, ErrorMessage), DefaultValue);
    }
  }
}

