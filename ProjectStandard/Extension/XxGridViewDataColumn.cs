using System;
using Telerik.WinControls.UI;

namespace ProjectStandard
{
  public static class XxGridViewDataColumn
  {
    public static string ZzGuid(this GridViewDataColumn column) => column.Name + "-" + Guid.NewGuid().ToString();

    public static void ZzAdd(this GridViewDataColumn column, RadGridView grid) => grid.MasterTemplate.Columns.Add(column);

    public static GridViewDataColumn ZzPin(this GridViewDataColumn column)
    {
      column.IsPinned = true; return column;
    }
  }
}

