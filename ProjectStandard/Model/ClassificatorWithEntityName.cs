using System;

namespace Model
{
  [Serializable]
  public class ClassificatorWithEntityName : Classificator
  {
    public string EntityName { get; set; }
  }
}
