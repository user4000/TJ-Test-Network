using System.Drawing;

namespace TJStandard
{
  public class ReceivedValueFont
  {
    public Font Value { get; set; } = new Font("Verdana", 9);
    public ReturnCode Code { get; set; } = ReturnCodeFactory.Error();
  }
}

