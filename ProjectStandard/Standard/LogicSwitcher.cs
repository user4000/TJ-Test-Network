namespace ProjectStandard
{
  public static class LogicSwitcher
  {
    private const string ConstOn = " V "; // ---- used for visual display in a cell of a grid //

    private const string ConstOff = " X "; // ---- used for visual display in a cell of a grid //

    public static string None { get; } = string.Empty;
    public static string On { get; } = ConstOn;
    public static string Off { get; } = ConstOff;

    public static int GetValue(string LogicSwitcher)
    {
      int result = 0;
      switch (LogicSwitcher)
      {
        case ConstOn: result = 1; break;
        case ConstOff: result = 2; break;
      }
      return result;
    }

    public static string GetStringValue(string LogicSwitcher) => GetValue(LogicSwitcher).ToString();
  }
}
