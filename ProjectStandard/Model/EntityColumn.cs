﻿using System;

namespace Model
{
  [Serializable]
  public class EntityColumn
  {
    public string CodeObject { get; set; }
    public string ColumnName { get; set; }
    public int ColumnWidth { get; set; }
    public int ColumnRank { get; set; }
  }
}
