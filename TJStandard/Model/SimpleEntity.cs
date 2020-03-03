using System;

namespace TJStandard.Model
{
  [Serializable]
  public class SimpleEntity
  {
    public int IdObject { get; set; } = 0;
    public string NameObject { get; set; } = string.Empty;
  }
}
