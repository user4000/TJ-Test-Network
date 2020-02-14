using System;
using System.Collections.Generic;

namespace Model
{
  [Serializable]
  public class UserRole
  {
    public int IdRole { get; set; }

    public int ActionSelect { get; set; }

    public string NameObject { get; set; }
  }
}
