using System;

namespace Model
{
  [Serializable]
  public class RoleEntity
  {
    public int IdRole { get; set; }

    public int IdEntity { get; set; }

    public string NameEntity { get; set; }

    public int ActionSelect { get; set; }

    public int ActionInsert { get; set; }

    public int ActionUpdate { get; set; }

    public int ActionDelete { get; set; }
  }
}


