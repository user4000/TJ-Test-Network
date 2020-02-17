using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls.UI;

namespace TestNetwork
{
  public class LocalDatabaseOfSettings
  {
    public string TableFolders { get; } = "FOLDERS";
    public string TableFoldersColumnIdParent { get; } = "IdParent";
    public string TableFoldersColumnIdFolder { get; } = "IdFolder";
    public string TableFoldersColumnNameFolder { get; } = "NameFolder";

    public Font FontOfNode { get; private set; } = null;

    public void SetFontOfNode(Font font) => FontOfNode = font;

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

    public DataTable GetSqliteDataTable(string PathToDatabase, string TableName)
    {
      DataTable table = new DataTable();
      using (SQLiteConnection connection = new SQLiteConnection($"Data Source={PathToDatabase}"))
      {
        using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {TableName}", connection))
        {
          connection.Open();
          using (SQLiteDataReader reader = command.ExecuteReader())
          {
            table.Load(reader);
          }
        }
      }
      foreach (DataColumn column in table.Columns) if (column.ColumnName == TableFoldersColumnIdParent) column.AllowDBNull = true;
      return table;
    }

    public int FolderInsert(string PathToDatabase, int IdParent, string NameFolder)
    {
      int IdNewFolder = -1;
      using (SQLiteConnection connection = new SQLiteConnection($"Data Source={PathToDatabase}"))
      {
        string sql = $"INSERT INTO FOLDERS (IdParent,NameFolder) VALUES ({IdParent},\"{NameFolder}\")";
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
          connection.Open(); command.ExecuteNonQuery();
          sql = $"SELECT IdFolder FROM FOLDERS WHERE IdParent={IdParent} AND NameFolder=\"{NameFolder}\"";
          command.CommandText = sql;
          IdNewFolder = CxConvert.ToInt32(command.ExecuteScalar(), -1);
        }
      }
      return IdNewFolder;
    }



    public void FillTreeView(RadTreeView treeView, DataTable table)
    {
      IEnumerable<DataRow> BadRows = table.Rows.Cast<DataRow>().Where(r => r[TableFoldersColumnIdFolder].ToString() == r[TableFoldersColumnIdParent].ToString());
      BadRows.ToList().ForEach(r => r.SetField(TableFoldersColumnIdParent, DBNull.Value));
      treeView.DisplayMember = TableFoldersColumnNameFolder;
      treeView.ParentMember = TableFoldersColumnIdParent;
      treeView.ChildMember = TableFoldersColumnIdFolder;
      treeView.DataSource = table; // <== This may cause application crash if there is any row having IdParent==IdFolder
      treeView.SortOrder = SortOrder.Ascending;
      foreach (var item in treeView.Nodes) ProcessOneNode(item);
    }

    private void ProcessOneNode(RadTreeNode node)
    {
      node.ImageIndex = node.Nodes.Count == 0 ? 0 : node.Level + 1;
      node.Font = FontOfNode; // node.Text = node.Text + " => " + node.Level;
      if (node.Level < 3) node.Expand();
      foreach (var item in node.Nodes) ProcessOneNode(item);
    }

    public int GetIdFolder(RadTreeNode node)
    {
      int Id = -1;
      if (node != null)
        try
        {
          DataRowView row = node.DataBoundItem as DataRowView;
          Id = CxConvert.ToInt32(row.Row[TableFoldersColumnIdFolder].ToString(), -1);
        }
        catch
        {

        }
      return Id;
    }
  }
}


