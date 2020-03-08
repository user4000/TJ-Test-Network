namespace TJStandard
{
  public class ConverterInt32
  {
    public string ToString(int value)
    {
      return value.ToString();
    }

    public ReceivedValueInteger32 FromString(string value)
    {
      if (int.TryParse(value, out int x))
      {
        return ReceivedValueInteger32.Success(x);
      }
      else
      {
        return ReceivedValueInteger32.Error(ReturnCodeFactory.NcError);
      }
    }
  }
}