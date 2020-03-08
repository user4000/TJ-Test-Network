namespace TJStandard
{
  public class ConverterInt64
  {
    public string ToString(long value) => value.ToString();

    public bool IsValid(string value) => CxConvert.IsValidInt64(value);

    public ReceivedValueInteger64 FromString(string value)
    {
      if ( long.TryParse(value, out long x) )
      {
        return ReceivedValueInteger64.Success(x);
      }
      else
      {
        return ReceivedValueInteger64.Error(ReturnCodeFactory.NcError);
      }
    }
  }
}