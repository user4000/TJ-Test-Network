namespace TJStandard
{
  public class ConverterBoolean
  {
    public string ToString(bool value)
    {
      return value ? "1" : "0";
    }

    public bool FromString(string value)
    {
      return value == "0" ? false : true;
    }
  }
}