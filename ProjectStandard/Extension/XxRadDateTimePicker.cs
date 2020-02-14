using Telerik.WinControls.UI;

namespace ProjectStandard
{
  public static class XxRadDateTimePicker
  {
    public static int ZzGetDateInteger(this RadDateTimePicker DateTimePicker)
    {
      return DateTimePicker.Value.Year * 10000 + DateTimePicker.Value.Month * 100 + DateTimePicker.Value.Day;
    }
  }
}


