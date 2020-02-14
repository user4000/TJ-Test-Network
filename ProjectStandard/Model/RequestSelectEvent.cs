using System;

namespace Model
{
  [Serializable]
  public class RequestSelectEvent
  {
    public string Command { get; set; }
    public int IdUser { get; set; }
    public int RowCount { get; set; }
    public int Day { get; set; }
  }
}
