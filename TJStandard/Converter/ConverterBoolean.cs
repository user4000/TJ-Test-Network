namespace TJStandard
{
  public class ConverterBoolean
  {
    public string ToString(bool value)
    {
      return value ? "1" : "0";
    }

    public ReceivedValueBoolean FromString(string value)
    {
      if (((value == "0") || (value == "1")) == false) return ReceivedValueBoolean.Error(ReturnCodeFactory.NcError);
      return ReceivedValueBoolean.Success(value == "0" ? false : true);     
    }
  }
}