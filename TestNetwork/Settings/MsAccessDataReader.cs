using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using Telerik.WinControls.UI;

namespace TestNetwork
{
  public class DatabaseSettingsManager
  {
    public string ColumnIdParent { get; } = "IdParent";
    public string ColumnIdFolder { get; } = "IdFolder";
    public string ColumnNameFolder { get; } = "NameFolder";

    public Font FontNode { get; private set; } = null;

    public void SetFont(Font font) => FontNode = font;

    public DataTable GetMsAccessDataTable(string PathToDatabase, string TableName)
    {
      string connString = $"Provider=Microsoft.Jet.OLEDB.4.0;data source={PathToDatabase}";
      DataTable results = new DataTable();
      using (OleDbConnection conn = new OleDbConnection(connString))
      {
        OleDbCommand cmd = new OleDbCommand($"SELECT * FROM {TableName}", conn);
        conn.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.Fill(results);
      }
      return results;
    }

    public void FillTreeView(RadTreeView treeView, DataTable table)
    {
      IEnumerable<DataRow> BadRows = table.Rows.Cast<DataRow>().Where(r => r[ColumnIdFolder].ToString() == r[ColumnIdParent].ToString());
      BadRows.ToList().ForEach(r => r.SetField(ColumnIdParent, DBNull.Value));

      treeView.DisplayMember = ColumnNameFolder;
      treeView.ParentMember = ColumnIdParent;
      treeView.ChildMember = ColumnIdFolder;
      treeView.DataSource = table; // <== This may cause application crash if there is any row having IdParent==IdFolder

      foreach (var item in treeView.Nodes) ProcessOneNode(item);
    }

    private void ProcessOneNode(RadTreeNode node)
    {
      node.ImageIndex = node.Nodes.Count == 0 ? 0 : node.Level + 1;
      node.Font = FontNode ;
      node.Text = node.Text + " => " + node.Level;

      if (node.Level < 3) node.Expand();

      foreach (var item in node.Nodes)
      {
        ProcessOneNode(item);
      }
    }
  }
}


