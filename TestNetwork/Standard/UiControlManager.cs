namespace TestNetwork
{
  public class UiControlManager
  {
    private FormTreeView FxTreeview { get; set; } = null;

    internal void Init(FormTreeView form) => FxTreeview = form;

    public bool FlagAllowChangeSelectedItem { get; private set; } = true;

    public void AllowChangeSelectedItem(bool allow) => FlagAllowChangeSelectedItem = allow;
  }
}