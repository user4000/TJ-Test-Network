using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace TJStandard
{
  public static class XxRadGridView
  {
    private static string ZzGetStringValue(this RadGridView grid, string GridColumnName)
    {// Получим значение ячейки в указанном столбце текущей строки //
      string s = string.Empty;
      try
      {
        GridViewRowInfo selectedRow = grid.CurrentCell.RowInfo;
        s = Convert.ToString(selectedRow.Cells[GridColumnName].Value); /* Внимание! Имя столбца грида а не имя столбца данных таблицы */
      }
      catch { }
      return s;
    }

    public static string ZzGetStringValueByFieldName(this RadGridView grid, string FieldName) => ZzGetStringValue(grid, Standard.GetGridColumnName(FieldName));

    public static int ZzGetIntegerValueByFieldName(this RadGridView grid, string FieldName, int DefaultValue) => CxConvert.ToInt32(ZzGetStringValue(grid, Standard.GetGridColumnName(FieldName)), DefaultValue);

    public static void ZzSetGridMainProperties(this RadGridView grid, int RowHeight = 30, bool AllowRowResize = false)
    {
      grid.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
      grid.TableElement.RowHeight = RowHeight;
      grid.AllowRowResize = AllowRowResize;
    }

    public static void ZzLoadColumnWidth(this RadGridView grid, Dictionary<string, int> d)
    {
      int width = 0;
      try
      {
        for (int i = 0; i < grid.ColumnCount; i++)
          if (d.TryGetValue(grid.Columns[i].FieldName, out width))
            grid.Columns[i].Width = width;
      }
      catch { }
    }

    public static void ZzSaveColumnWidth(this RadGridView grid, Dictionary<string, int> d)
    {
      d.Clear();
      for (int i = 0; i < grid.ColumnCount; i++)
        d.Add(grid.Columns[i].FieldName, grid.Columns[i].Width);
    }

    public static GridViewDataColumn ZzGetByFieldName(this RadGridView grid, string FieldName)
    {
      GridViewDataColumn[] collection = grid.Columns.GetColumnByFieldName(FieldName);
      if (collection.Count<GridViewDataColumn>() == 1) return collection.First(); else return null;
    }

    public static int ZzGetColumnWidthByFieldName(this RadGridView grid, string FieldName)
    {
      GridViewDataColumn[] collection = grid.Columns.GetColumnByFieldName(FieldName);
      if (collection.Count<GridViewDataColumn>() == 1) return collection.First().Width; else return -1;
    }

    public static bool ZzIsColumnVisibleByFieldName(this RadGridView grid, string FieldName)
    {
      GridViewDataColumn[] collection = grid.Columns.GetColumnByFieldName(FieldName);
      if (collection.Count<GridViewDataColumn>() == 1) return collection.First().IsVisible; else return false;
    }

    public static int ZzGetRowIndex(this RadGridView grid, string GridColumnName, string Value, bool MakeCurrent)
    {
      int rowIndex = -1; GridViewRowInfo row = null;

      try
      {
        row = grid.Rows
         .Cast<GridViewRowInfo>()
         .Where(r => r.Cells[GridColumnName].Value.ToString().Equals(Value))
         .First();
        rowIndex = row.Index;
      }
      catch { rowIndex = -1; }

      if ((rowIndex >= 0) && MakeCurrent) grid.Rows[rowIndex].IsCurrent = true;

      return rowIndex;
    }

    public static int ZzGetRowIndexForeach(this RadGridView grid, string GridColumnName, string Value, bool MakeCurrent)
    {
      int rowIndex = -1;
      foreach (GridViewRowInfo row in grid.Rows)
      {
        if (row.Cells[GridColumnName].Value.ToString().Equals(Value))
        {
          rowIndex = row.Index;
          break;
        }
      }
      if ((rowIndex >= 0) && MakeCurrent) grid.Rows[rowIndex].IsCurrent = true;
      return rowIndex;
    }

    public static void ZzSetAllColumnsReadOnly(this RadGridView grid)
    {
      foreach (GridViewDataColumn col in grid.Columns)
        if (!col.ReadOnly) col.ReadOnly = true;
    }

    public static void ZzDisposeAllColumns(this RadGridView grid)
    {
      GridViewDataColumn col;
      for (int i = 0; i < grid.ColumnCount; i++)
      {
        col = grid.Columns[0];
        grid.Columns.Remove(col);
        col.FieldName = string.Empty;
        col.Dispose();
      }
      grid.Columns.Clear();
    }

    public static void ZzDispose(this RadGridView grid)
    {
      grid.Visible = false;
      grid.DataSource = null;
      grid.ZzDisposeAllColumns();
      grid.Dispose();
    }

    public static void ZzFilter(this RadGridView grid, string FieldName, FilterOperator filterOperator, object value, bool ClearPreviousFilter = true)
    {
      if (ClearPreviousFilter) grid.FilterDescriptors.Clear();
      grid.FilterDescriptors.Add(Standard.GetGridColumnName(FieldName), FilterOperator.StartsWith, value);
    }
  }
}

/*
public static async Task ZzFilterRefresh(this RadGridView grid)
{
  grid.EnableFiltering = false;
  var t = Task.Factory.StartNew(() => { Task.Delay(10).Wait(); }); await t;
  grid.EnableFiltering = true;
}
*/