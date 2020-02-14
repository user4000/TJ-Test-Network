using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectStandard
{
  public static class XxRadTextBoxControl
  {
    public static void EventKeyPressNonNegativeIntegerNumberOnly(object s, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) /* && (e.KeyChar != '.') */ ) e.Handled = true; 
    }

    public static void ZzSetNonNegativeIntegerNumberOnly(this RadTextBoxControl control)
    {
      control.KeyPress += (s, e) => EventKeyPressNonNegativeIntegerNumberOnly(s, e);
    }
  }
}

