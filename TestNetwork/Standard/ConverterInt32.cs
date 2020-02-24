using ProjectStandard;

namespace TestNetwork
{
  public class ConverterInt32
  {
    public string ToString(int value)
    {
      return value.ToString();
    }

    public int FromString(string value)
    {
      return CxConvert.ToInt32(value, 0);
    }
  }
}