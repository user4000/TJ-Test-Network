using System.Drawing;
using TJStandard;

namespace TestNetwork
{
  public class ConverterColor
  {
    private Color DefaultColor { get; } = Color.Black;

    public string ToString(Color value)
    {
      return CxConvert.ObjectToJson(value);
    }

    public Color FromString(string value)
    {
      Color color = DefaultColor;
      try
      {
        color = CxConvert.JsonToObject<Color>(value);
      }
      catch
      {
        color = DefaultColor;
      }
      return color;
    }
  }
}