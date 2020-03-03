using Telerik.WinControls.UI;

namespace TJStandard
{
  public static class XxGridViewRowInfo
  {
    public static string ZzGetCellValue(this GridViewRowInfo row, string FieldName) => row.Cells[Standard.GetGridColumnName(FieldName)].Value.ToString();
  }
}

