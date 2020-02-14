using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ProjectStandard
{
  public static class XxRadButton
  {
    public static void EventTimerTick(object sender, EventArgs e)
    {
      if (!(sender is Timer)) return;
      Timer timer = sender as Timer;
      if (timer.Tag==null) return;
      if (!(timer.Tag is RadButton)) return;
      RadButton button = timer.Tag as RadButton;
      if (button.Enabled == false) button.Enabled = true;
      timer.Tick -= EventTimerTick;
      timer.Enabled = false;
      timer.Dispose();
    }

    public static void ZzEnableAfter(this RadButton button, int MilliSeconds)
    {
      Timer timer = new Timer();
      timer.Interval = MilliSeconds;
      timer.Tag = button;
      timer.Tick += EventTimerTick;
      timer.Enabled = true;
    }
  }
}

