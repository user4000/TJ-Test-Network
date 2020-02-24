using ProjectStandard;

namespace TestNetwork
{
  public class ConverterInt64
  {
    public string ToString(long value) => value.ToString();
 
    public long FromString(string value) => CxConvert.ToInt64(value, 0);

    public bool IsValid(string value) => CxConvert.IsValidInt64(value);
  }
}