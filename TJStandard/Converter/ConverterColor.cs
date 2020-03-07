using System.Drawing;

namespace TJStandard
{
  public class ConverterColor // TODO: Каждый конвертер должен также сигнализировать об ошибке если преобразование из строки было неудачным.
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