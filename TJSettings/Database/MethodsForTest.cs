using System.Data.SQLite;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public int GetRandomIdFolder()
    {
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        return command.ZzOpenConnection().ZzGetScalarInteger(DbManager.SqlGetRandomIdFolder);
      }
    }

    public string GetRandomIdSetting(int IdFolder)
    {
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        return command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzGetScalarString(DbManager.SqlGetRandomIdSetting);
      }
    }

    public ReturnCode GetRandomSetting()
    {
      ReturnCode code = ReturnCodeFactory.Error();
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlGetRandomSetting, connection).ZzOpenConnection())
      using (SQLiteDataReader reader = command.ExecuteReader())
        while (reader.Read())
        {
          code = ReturnCodeFactory.Create(ReturnCodeFactory.NcSuccess, reader.GetInt32(0), reader.GetString(1), string.Empty);
          //Trace.WriteLine("---> " + ReturnCodeFormatter.ToString(code));
          break;
        }
      return code;
    }
  }
}

