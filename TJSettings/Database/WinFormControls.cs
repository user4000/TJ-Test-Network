using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public void FillDropDownListForTableTypes(RadDropDownList combobox)
    {
      combobox.DataSource = TableTypes;
      combobox.ValueMember = DbManager.CnTypesIdType;
      combobox.DisplayMember = DbManager.CnTypesNameType;
      combobox.ZzSetStandardVisualStyle();
    }

    public void FillTreeView(RadTreeView treeView, DataTable table)
    {
      IEnumerable<DataRow> BadRows = table.Rows.Cast<DataRow>().Where(r => r[DbManager.CnFoldersIdFolder].ToString() == r[DbManager.CnFoldersIdParent].ToString());
      BadRows.ToList().ForEach(r => r.SetField(DbManager.CnFoldersIdParent, DBNull.Value));
      treeView.DisplayMember = DbManager.CnFoldersNameFolder;
      treeView.ParentMember = DbManager.CnFoldersIdParent;
      treeView.ChildMember = DbManager.CnFoldersIdFolder;
      treeView.DataSource = table; // <== This may cause application crash if there is any row having IdParent==IdFolder
      treeView.SortOrder = SortOrder.Ascending;
    }
  }
}

