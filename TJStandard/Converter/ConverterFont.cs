using System;
using System.Drawing;
using System.Globalization;

namespace TJStandard
{
  public class ConverterFont
  {   
    public string ToString(Font value)
    {
      return CxConvert.ObjectToJson(value);
    }

    public ReceivedValueFont FromString(string value)
    {
      Font font;
      try
      {
        font = CxConvert.JsonToObject<Font>(value);
      }
      catch
      {
        return ReceivedValueFont.Error(ReturnCodeFactory.NcError);
      }
      return ReceivedValueFont.Success(font);
    }
  }
}