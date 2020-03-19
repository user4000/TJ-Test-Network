using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    private void SetPathToDatabase(string PathToDatabase) => SqliteDatabase = PathToDatabase;

    public string GetSqliteConnectionString(string PathToDatabase) => $"Data Source={PathToDatabase}";

    public SQLiteConnection GetSqliteConnection(string PathToDatabase) => new SQLiteConnection(GetSqliteConnectionString(PathToDatabase));

    public SQLiteConnection GetSqliteConnection() => GetSqliteConnection(SqliteDatabase);

    public DataTable GetTable(string TableName)
    {
      DataTable table = new DataTable();
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {TableName}", connection))
      using (SQLiteDataReader reader = command.ZzOpenConnection().ExecuteReader())
      {
        table.Load(reader);
      }
      return table;
    }

    public DataTable GetTableFolders()
    {
      DataTable table = GetTable(DbManager.TnFolders);
      foreach (DataColumn column in table.Columns) if (column.ColumnName == DbManager.CnFoldersIdParent) column.AllowDBNull = true;
      return table;
    }

    private string GetScalarString(string SqlScalarStringQuery)
    {
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(SqlScalarStringQuery, connection).ZzOpenConnection())
      {
        return command.ZzGetScalarString();
      }
    }

    public ReturnCode ConnectToDatabase(string PathToDatabase = "")
    {
      if (PathToDatabase == string.Empty)
      {
        PathToDatabase = Path.Combine(Path.Combine(Application.StartupPath, DefaultFolder), DefaultFileName);
      }

      //MessageBox.Show(PathToDatabase);

      SetPathToDatabase(PathToDatabase);
      ReturnCode code;
      try
      {
        code = CheckDatabaseStructure();
      }
      catch
      {
        return ReturnCodeFactory.Error("The file you specified is not a SQLite database.");
      }

      if (code.Error) return code;

      if (TableTypes != null) TableTypes.Clear();
      try
      {
        TableTypes = GetTable(DbManager.TnTypes);
        RootFolderName = GetRootFolderName();
      }
      catch (Exception ex)
      {
        RootFolderName = string.Empty;
        return ReturnCodeFactory.Error("Could not read data from the database file.", ex.Message);
      }
      return ReturnCodeFactory.Success();
    }

    private ReturnCode CheckDatabaseStructure()
    {
      ReturnCode code = ReturnCodeFactory.Success($"Database structure is ok");
      int CheckObjectCount = 4;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlCheckDatabaseStructure);
        if (command.ZzGetScalarInteger() != CheckObjectCount) code = ReturnCodeFactory.Error(ReturnCodeFactory.NcError, "The database structure is not compliant with the standard. When working with the database errors may occur.");
      }
      return code;
    }

    public ReturnCode CreateNewDatabase(string text) => DbManager.CreateNewDatabase(text);

  }
}

