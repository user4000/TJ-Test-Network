namespace TestNetwork
{
  public class ConverterBoolean
  {
    public string ToString(bool value)
    {
      return value ? "1" : "0";
    }

    public bool FromString(string value)
    {
      return value == "1" ? true : false;
    }
  }
}