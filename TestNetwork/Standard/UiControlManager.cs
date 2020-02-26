namespace TestNetwork
{
  public class UiControlManager
  {
    public bool FlagAllowChangeSelectedItem { get; private set; } = true;

    public void AllowChangeSelectedItem(bool allow) => FlagAllowChangeSelectedItem = allow;
  }
}