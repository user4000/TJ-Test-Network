namespace TestNetwork
{
  public class TypeSettingConverter
  {
    public static TypeSetting FromInteger(int value)
    {
      TypeSetting type = TypeSetting.Unknown;
      try { type = (TypeSetting)value; } catch { }
      return type;
    }
  }
}
