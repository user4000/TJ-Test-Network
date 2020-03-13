using System.Data.SQLite;
using TJStandard;

namespace TJSettings
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

    public static string ZzGetScalarString(this SQLiteCommand command, string sql)
    {
      command.CommandText = sql;
      return CxConvert.ToString(command.ExecuteScalar());
    }

    public static string ZzGetScalarString(this SQLiteCommand command)
    {
      return CxConvert.ToString(command.ExecuteScalar());
    }
  }
}

