using System.Drawing;

namespace TJStandard
{
  public class ConverterColor
  {   
    public string ToString(Color value)
    {
      return CxConvert.ObjectToJson(value);
    }

    public ReceivedValueColor FromString(string value)
    {
      Color color;
      try
      {
        color = CxConvert.JsonToObject<Color>(value);
      }
      catch
      {
        return ReceivedValueColor.Error(ReturnCodeFactory.NcError);
      }
      return ReceivedValueColor.Success(color);
    }
  }
}