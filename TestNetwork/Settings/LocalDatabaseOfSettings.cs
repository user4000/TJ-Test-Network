using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls.UI;
using static TestNetwork.Program;

namespace TestNetwork
{
  public class LocalDatabaseOfSettings
  {
    public IOutputMessage Debug { get; private set; } = null;
    public string TnFolders { get; } = "FOLDERS";
    public string CnFoldersIdParent { get; } = "IdParent";
    public string CnFoldersIdFolder { get; } = "IdFolder";
    public string CnFoldersNameFolder { get; } = "NameFolder";

    public string TnSettings { get; } = "SETTINGS";

    public string CnSettingsIdFolder { get; } = "IdFolder";
    public string CnSettingsIdSetting { get; } = "IdSetting";
    public string CnSettingsIdType { get; } = "IdType";
    public string CnSettingsSettingValue { get; } = "SettingValue";
    public string CnSettingsRank { get; } = "Rank";

    public string VnSettings { get; } = "V_SETTINGS";
    public string VnSettingsBooleanValue { get; } = "BooleanValue";
    public string VnSettingsNameType { get; } = "NameType";

    public string TnTypes { get; } = "TYPES";
    public string CnTypesIdType { get; } = "IdType";
    public string CnTypesNameType { get; } = "NameType";

    internal DataTable TableTypes { get; private set; } = null;

    public string SqliteDatabase { get; private set; } = string.Empty;

    public void SavePathToDatabase(string PathToDatabase) => SqliteDatabase = PathToDatabase;

    public string GetSqliteConnectionString(string PathToDatabase) => $"Data Source={PathToDatabase}";

    public SQLiteConnection GetSqliteConnection(string PathToDatabase) => new SQLiteConnection(GetSqliteConnectionString(PathToDatabase));

    public string GetSqliteConnectionString() => GetSqliteConnectionString(SqliteDatabase);

    public SQLiteConnection GetSqliteConnection() => GetSqliteConnection(SqliteDatabase);

    public string SqlSettingSelect { get; private set; } = string.Empty;

    public string SqlSettingInsert { get; private set; } = string.Empty;

    public string SqlSettingUpdate { get; private set; } = string.Empty;

    public string SqlSettingCount { get; private set; } = string.Empty;

    public string SqlFolderCountByIdParent { get; private set; } = string.Empty;

    public string SqlFolderInsert { get; private set; } = string.Empty;

    public string SqlGetIdFolder { get; private set; } = string.Empty;

    public string SqlFolderCountByIdFolder { get; private set; } = string.Empty;

    public string SqlFolderRename { get; private set; } = string.Empty;

    public string SqlFolderCountSimple { get; private set; } = string.Empty;

    public string SqlCountChildFolder { get; private set; } = string.Empty;

    public string SqlCountChildSettings { get; private set; } = string.Empty;

    public string SqlDeleteFolder { get; private set; } = string.Empty;


    public DataTable GetTable(string TableName)
    {
      DataTable table = new DataTable();
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {TableName}", connection))
      {
        connection.Open();
        using (SQLiteDataReader reader = command.ExecuteReader())
        {
          table.Load(reader);
        }
      }
      return table;
    }

    public DataTable GetTableFolders()
    {
      DataTable table = GetTable(TnFolders);
      foreach (DataColumn column in table.Columns) if (column.ColumnName == CnFoldersIdParent) column.AllowDBNull = true;
      return table;
    }

    private bool FlagInitVariables { get; set; } = false;

    public void InitVariables(IOutputMessage OutputMessageDevice)
    {
      if (TableTypes != null) TableTypes.Clear();
      TableTypes = GetTable(TnTypes);

      if (FlagInitVariables) return;
      FlagInitVariables = true;

      Debug = OutputMessageDevice;

      SqlSettingSelect = $"SELECT " +
        $"{CnSettingsIdFolder}," +
        $"{CnSettingsIdSetting}," +
        $"{CnSettingsIdType}," +
        $"{VnSettingsNameType}," +
        $"{CnSettingsSettingValue}," +
        $"{CnSettingsRank}," +
        $"{VnSettingsBooleanValue} " +
        $"FROM {VnSettings}";

      SqlSettingCount = $"SELECT COUNT(*) from {TnSettings} WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlSettingInsert =
      $"INSERT INTO {TnSettings} ({CnSettingsIdFolder}, {CnSettingsIdSetting}, {CnSettingsIdType}, {CnSettingsSettingValue}, {CnSettingsRank})" +
      $" VALUES (@IdFolder, @IdSetting, @IdType, @SettingValue, (SELECT IFNULL(MAX({CnSettingsRank}), 0) + 1 FROM {TnSettings} WHERE {CnSettingsIdFolder} = @IdFolder))";

      SqlSettingUpdate =
      $"UPDATE {TnSettings} SET {CnSettingsSettingValue}=@SettingValue " +
      $"WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlFolderCountByIdParent = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent}=@IdParent AND {CnFoldersNameFolder}=@NameFolder";
      SqlFolderInsert = $"INSERT INTO {TnFolders} ({CnFoldersIdParent},{CnFoldersNameFolder}) VALUES (@IdParent,@NameFolder)";
      SqlGetIdFolder = $"SELECT {CnFoldersIdFolder} FROM {TnFolders} WHERE {CnFoldersIdParent}=@IdParent AND {CnFoldersNameFolder}=@NameFolder";

      SqlFolderCountByIdFolder = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent} = (SELECT {CnFoldersIdParent} FROM {TnFolders} WHERE {CnFoldersIdFolder}=@IdFolder) AND {CnFoldersNameFolder}=@NameFolder";
      SqlFolderRename = $"UPDATE {TnFolders} SET {CnFoldersNameFolder}=@NameFolder WHERE {CnFoldersIdFolder}=@IdFolder";

      SqlFolderCountSimple = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdFolder} = @IdFolder";
      SqlCountChildFolder = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent} = @IdFolder";
      SqlCountChildSettings = $"SELECT COUNT(*) FROM SETTINGS WHERE {CnFoldersIdFolder} = @IdFolder";
      SqlDeleteFolder = $"DELETE FROM {TnFolders} WHERE {CnFoldersIdFolder}=@IdFolder";

    }

    public void FillDropDownListForTableTypes(RadDropDownList combobox)
    {
      combobox.DataSource = TableTypes;
      combobox.ValueMember = CnTypesIdType;
      combobox.DisplayMember = CnTypesNameType;
      combobox.ZzSetStandardVisualStyle();
    }

    public ReturnCode FolderInsert(int IdParent, string NameFolder) // TODO: Extract sql commands //
    {
      ReturnCode code = ReturnCodeFactory.Success($"Папка добавлена: {NameFolder}");
      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountByIdParent).ZzAdd("@IdParent", IdParent).ZzAdd("@NameFolder", NameFolder);
        int count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("Папка с таким именем уже существует");
        count = command.ZzExecuteNonQuery(SqlFolderInsert);
        if (count != 1) return ReturnCodeFactory.Error("Ошибка при попытке добавления новой папки");
        IdNewFolder = command.ZzGetScalarInteger(SqlGetIdFolder);
        if (IdNewFolder > 0)
        {
          code.IdObject = IdNewFolder;
        }
        else
        {
          return ReturnCodeFactory.Error("Ошибка при попытке добавления новой папки");
        }
      }
      return code;
    }

    public ReturnCode FolderRename(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Папка переименована: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountByIdFolder).ZzAdd("@IdFolder", IdFolder).ZzAdd("@NameFolder", NameFolder);
        count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("Папка с таким именем уже существует");
        count = command.ZzExecuteNonQuery(SqlFolderRename);
        //count = command.ZzGetScalarInteger(SqlFolderCountByIdFolder);
        if (count < 1) return ReturnCodeFactory.Error("Не удалось переименовать папку");
      }
      return code;
    }

    public ReturnCode FolderDelete(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Папка удалена: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountSimple).ZzAdd("@IdFolder", IdFolder);
        count = command.ZzGetScalarInteger();
        if (count != 1) return ReturnCodeFactory.Error("Указанная вами папка не найдена");
        count = command.ZzGetScalarInteger(SqlCountChildFolder);
        if (count != 0) return ReturnCodeFactory.Error("Нельзя удалять папку, внутри которой есть другие папки");
        count = command.ZzGetScalarInteger(SqlCountChildSettings);
        if (count != 0) return ReturnCodeFactory.Error("Нельзя удалять папку, которая содержит настройки");
        count = command.ZzExecuteNonQuery(SqlDeleteFolder);
        if (count == 0) return ReturnCodeFactory.Error("Не удалось удалить папку");
      }
      return code;
    }

    public void FillTreeView(RadTreeView treeView, DataTable table)
    {
      IEnumerable<DataRow> BadRows = table.Rows.Cast<DataRow>().Where(r => r[CnFoldersIdFolder].ToString() == r[CnFoldersIdParent].ToString());
      BadRows.ToList().ForEach(r => r.SetField(CnFoldersIdParent, DBNull.Value));
      treeView.DisplayMember = CnFoldersNameFolder;
      treeView.ParentMember = CnFoldersIdParent;
      treeView.ChildMember = CnFoldersIdFolder;
      treeView.DataSource = table; // <== This may cause application crash if there is any row having IdParent==IdFolder
      treeView.SortOrder = SortOrder.Ascending;
    }

    public int GetIdFolder(RadTreeNode node)
    {
      int Id = -1;
      if (node != null)
        try
        {
          DataRowView row = node.DataBoundItem as DataRowView;
          Id = CxConvert.ToInt32(row.Row[CnFoldersIdFolder].ToString(), -1);
        }
        catch { }
      return Id;
    }

    private async Task<BindingList<Setting>> GetSettings(RadTreeNode node)
    {
      if (node == null) return null;
      return await GetSettings(GetIdFolder(node));
    }

    public async Task<BindingList<Setting>> GetSettings(int IdFolder)
    {
      BindingList<Setting> list = new BindingList<Setting>();
      string sql = $"{SqlSettingSelect} WHERE IdFolder={IdFolder}";
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(sql, connection))
      {
        connection.Open();
        using (SQLiteDataReader reader = command.ExecuteReader())
          while (await reader.ReadAsync())
            list.ZzAdd
              (
                IdFolder: reader.GetInt32(0),
                IdSetting: reader.GetString(1),
                IdType: reader.GetInt32(2),
                NameType: reader.GetString(3),
                SettingValue: reader.GetString(4),
                Rank: reader.GetInt32(5),
                BooleanValue: reader.GetString(6)
              );
      }
      return list;
    }


    public ReturnCode SaveSettingDatetime(bool AddNewSetting, int IdFolder, string IdSetting, DateTime value)
    {
      string StringValue = Manager.CvDatetime.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Datetime), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingBoolean(bool AddNewSetting, int IdFolder, string IdSetting, bool value)
    {
      string StringValue = Manager.CvBoolean.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Boolean), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingLong(bool AddNewSetting, int IdFolder, string IdSetting, long value)
    {
      string StringValue = Manager.CvInt64.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Integer64), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingText(bool AddNewSetting, int IdFolder, string IdSetting, string value)
    {
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Text), value)
        :
        SettingUpdate(IdFolder, IdSetting, value);
    }


    public ReturnCode SettingCreate(int IdFolder, string IdSetting, int IdType, string value)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Новая переменная создана: {IdSetting}");
      if (IdSetting.Trim().Length < 1) ReturnCodeFactory.Error("Не указано название переменной");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error("Переменная с таким именем уже существует");
        count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(SqlSettingInsert);
        if (count != 1) return ReturnCodeFactory.Error("Не удалось создать переменную");
        code.StringNote = value;
      }
      return code;
    }

    public ReturnCode SettingUpdate(int IdFolder, string IdSetting, string value)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Изменения сохранены: {IdSetting}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error("Переменной с таким именем не существует");
        count = command.ZzAdd("@SettingValue", value).ZzExecuteNonQuery(SqlSettingUpdate);
        if (count != 1) return ReturnCodeFactory.Error("Не удалось изменить переменную");
        code.StringNote = value;
      }
      return code;
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

