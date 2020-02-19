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

    public string SqliteDatabase { get; private set; } = string.Empty;

    public Font FontOfNode { get; private set; } = null;

    public void SetFontOfNode(Font font) => FontOfNode = font;

    public void SavePathToDatabase(string PathToDatabase) => SqliteDatabase = PathToDatabase;

    public string GetSqliteConnectionString(string PathToDatabase) => $"Data Source={PathToDatabase}";

    public SQLiteConnection GetSqliteConnection(string PathToDatabase) => new SQLiteConnection(GetSqliteConnectionString(PathToDatabase));

    public string GetSqliteConnectionString() => GetSqliteConnectionString(SqliteDatabase);

    public SQLiteConnection GetSqliteConnection() => GetSqliteConnection(SqliteDatabase);

    public DataTable GetTableFolders()
    {
      DataTable table = new DataTable();
      using (SQLiteConnection connection = GetSqliteConnection())
      {
        using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {TableFolders}", connection))
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

    public ReturnCode FolderInsert(int IdParent, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Папка добавлена: {NameFolder}");
      string sqlSelectCount = "SELECT COUNT(*) FROM FOLDERS WHERE IdParent=@IdParent AND NameFolder=@NameFolder";
      string sqlInsertFolder = "INSERT INTO FOLDERS (IdParent,NameFolder) VALUES (@IdParent,@NameFolder)";
      string sqlSelectIdFolder = "SELECT IdFolder FROM FOLDERS WHERE IdParent=@IdParent AND NameFolder=@NameFolder";

      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
          connection.Open();

          command.CommandText = sqlSelectCount;
          command.Parameters.Add(new SQLiteParameter("@IdParent", IdParent));
          command.Parameters.Add(new SQLiteParameter("@NameFolder", NameFolder));
          int count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 0)
          {
            return ReturnCodeFactory.Error("Папка с таким именем уже существует");
          }

          command.CommandText = sqlInsertFolder;
          command.ExecuteNonQuery();

          command.CommandText = sqlSelectCount;
          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);
          if (count != 1)
          {
            return ReturnCodeFactory.Error("Ошибка при попытке добавления новой папки");
          }

          command.CommandText = sqlSelectIdFolder;
          IdNewFolder = CxConvert.ToInt32(command.ExecuteScalar(), -1); // Trying to get id of a new inserted folder //

          if (IdNewFolder > 0)
          {
            code.IdObject = IdNewFolder;
          }
          else
          {
            return ReturnCodeFactory.Error("Ошибка при попытке добавления новой папки");
          }
        }
      }
      return code;
    }

    public ReturnCode FolderRename(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Папка переименована: {NameFolder}");
      int count = 0;
      string sqlSelectCount = "SELECT COUNT(*) FROM FOLDERS WHERE IdParent = (SELECT IdParent FROM FOLDERS WHERE IdFolder=@IdFolder) AND NameFolder=@NameFolder";
      string sqlRenameFolder = "UPDATE FOLDERS SET NameFolder=@NameFolder WHERE IdFolder=@IdFolder";

      using (SQLiteConnection connection = GetSqliteConnection())
      {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
          connection.Open();

          command.CommandText = sqlSelectCount;
          command.Parameters.Add(new SQLiteParameter("@IdFolder", IdFolder));
          command.Parameters.Add(new SQLiteParameter("@NameFolder", NameFolder));

          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 0)
          {
            return ReturnCodeFactory.Error("Папка с таким именем уже существует");
          }

          command.CommandText = sqlRenameFolder;
          command.ExecuteNonQuery();

          command.CommandText = sqlSelectCount;
          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count < 1)
          {
            return ReturnCodeFactory.Error("Не удалось переименовать папку");
          }
        }
      }
      return code;
    }

    public ReturnCode FolderDelete(int IdFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success("Папка удалена");
      string sqlCountFolder = "SELECT COUNT(*) FROM FOLDERS WHERE IdFolder = @IdFolder";
      string sqlCountChildFolder = "SELECT COUNT(*) FROM FOLDERS WHERE IdParent = @IdFolder";
      string sqlCountChildSettings = "SELECT COUNT(*) FROM SETTINGS WHERE IdFolder = @IdFolder";
      string sqlDeleteFolder = "DELETE FROM FOLDERS WHERE IdFolder=@IdFolder";
      int count = 0;

      using (SQLiteConnection connection = GetSqliteConnection())
      {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
          connection.Open();

          command.CommandText = sqlCountFolder;
          command.Parameters.Add(new SQLiteParameter("@IdFolder", IdFolder));

          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 1) // Folder was not found //
          {
            return ReturnCodeFactory.Error("Указанная вами папка не найдена");
          }

          command.CommandText = sqlCountChildFolder;
          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 0)
          {
            return ReturnCodeFactory.Error("Нельзя удалять папку, внутри которой есть другие папки");
          }

          command.CommandText = sqlCountChildSettings;
          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 0)
          {
            return ReturnCodeFactory.Error("Нельзя удалять папку, которая содержит настройки");
          }

          command.CommandText = sqlDeleteFolder;
          command.ExecuteNonQuery();
          command.CommandText = sqlCountFolder;
          count = CxConvert.ToInt32(command.ExecuteScalar(), -1);

          if (count != 0)
          {
            return ReturnCodeFactory.Error("Не удалось удалить папку");
          }
        }
      }
      return code;
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
      foreach (var item in treeView.Nodes) ProcessOneNodeSetFontAndImageIndex(item);
    }

    private void ProcessOneNodeSetFontAndImageIndex(RadTreeNode node)
    {
      node.ImageIndex = node.Nodes.Count == 0 ? 0 : node.Level + 1;
      node.Font = FontOfNode; // node.Text = node.Text + " => " + node.Level;
      if (node.Level < 3) node.Expand();
      foreach (var item in node.Nodes) ProcessOneNodeSetFontAndImageIndex(item);
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

/*


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




*/

