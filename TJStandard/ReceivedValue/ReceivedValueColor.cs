using System.Drawing;

namespace TJStandard
{
  public class ReceivedValueColor
  {
    public Color Value { get; set; } = Color.Black;
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();
  }
}

