using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;

namespace TJStandard
{
  public static class XxToolWindow
  {
    private static void EventDockManagerDockStateChanging(object sender, DockStateChangingEventArgs e)
    {
      e.Cancel = (e.NewDockState == DockState.TabbedDocument) || (e.NewDockState == DockState.Floating);    
    }

    public static void ZzForbidMoving(this ToolWindow control)
    {
      control.DockManager.DockStateChanging += EventDockManagerDockStateChanging;
    }

    public static void ZzStandardInitView(this ToolWindow control)
    {
      control.DocumentButtons = DocumentStripButtons.None;
      control.ToolCaptionButtons = ToolStripCaptionButtons.AutoHide;
      control.DockState = DockState.AutoHide;
      control.AllowedDockState = AllowedDockState.AutoHide | AllowedDockState.Docked;
    }

    public static void ZzStandardVisible(this ToolWindow control, bool Visible)
    {
      if (Visible)
      {
        control.AllowedDockState = AllowedDockState.AutoHide | AllowedDockState.Docked;
        control.AutoHideTab.Visibility = ElementVisibility.Visible;
      }
      else
      {
        control.AllowedDockState = AllowedDockState.Hidden;
        control.AutoHideTab.Visibility = ElementVisibility.Hidden;
      }
    }
  }
}

