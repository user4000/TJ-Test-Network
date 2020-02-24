using ProjectStandard;

namespace TestNetwork
{
  public class ConverterInt64
  {
    public string ToString(long value)
    {
      return value.ToString();
    }

    public long FromString(string value)
    {
      return CxConvert.ToInt64(value, 0);
    }
  }
}