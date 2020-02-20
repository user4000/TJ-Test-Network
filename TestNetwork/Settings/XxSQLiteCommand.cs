using System.Data.SQLite;
using ProjectStandard;

namespace TestNetwork
{
  public static class XxSQLiteCommand
  {
    public static SQLiteCommand ZzAdd(this SQLiteCommand command, string parameterName, object value)
    {
      command.Parameters.Add(new SQLiteParameter(parameterName, value));
      return command;
    }

    public static SQLiteCommand ZzOpenConnection(this SQLiteCommand command)
    {
      command.Connection.Open();
      return command;
    }


    public static SQLiteCommand ZzText(this SQLiteCommand command, string sql)
    {
      command.CommandText = sql; return command;
    }

    public static int ZzExecuteNonQuery(this SQLiteCommand command, string sql)
    {
      command.CommandText = sql; return command.ExecuteNonQuery();
    }

    public static int ZzGetScalarInteger(this SQLiteCommand command, string sql)
    {
      command.CommandText = sql;
      return CxConvert.ToInt32(command.ExecuteScalar(), -1);
    }

    public static int ZzGetScalarInteger(this SQLiteCommand command)
    {
      return CxConvert.ToInt32(command.ExecuteScalar(), -1);
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

