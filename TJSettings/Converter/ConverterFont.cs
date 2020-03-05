using System;
using System.Drawing;
using System.Globalization;
using TJStandard;

namespace TJSettings
{
  public class ConverterFont
  {
    private Font DefaultFont { get; } = new Font("Verdana", 9);

    public string ToString(Font value)
    {
      return CxConvert.ObjectToJson(value);
    }

    public Font FromString(string value)
    {
      Font font = DefaultFont;
      try
      {
        font = CxConvert.JsonToObject<Font>(value);
      }
      catch
      {
        font = DefaultFont;
      }
      return font;
    }
  }
}