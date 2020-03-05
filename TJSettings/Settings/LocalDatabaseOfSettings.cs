using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TJStandard;
using Telerik.WinControls.UI;

namespace TJSettings
{
  // TODO: Create COMMON public method to READ a VARIABLE
  // TODO: Create COMMON public method to CHANGE a VARIABLE
  // TODO: Consider a hierarchical system of folder names for accessing variables without a numerical folder identifier
  public class LocalDatabaseOfSettings
  {
    public string TnFolders { get; } = "FOLDERS";
    public string CnFoldersIdParent { get; } = "IdParent";
    public string CnFoldersIdFolder { get; } = "IdFolder";
    public string CnFoldersNameFolder { get; } = "NameFolder";

    public string TnSettings { get; } = "SETTINGS";

    public RadTreeView VxTreeview { get; private set; } = new RadTreeView();

    public DataTable VxFolders { get; private set; } = null;

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

    public char SingleQuote { get; } = '\'';

    public string FolderPathSeparator { get; } = @"\";

    public Converter CvManager { get; } = new Converter();

    internal DataTable TableTypes { get; private set; } = null;

    public List<Folder> ListFolders { get; private set; } = new List<Folder>();

    public string SqliteDatabase { get; private set; } = string.Empty;

    public void SetPathToDatabase(string PathToDatabase) => SqliteDatabase = PathToDatabase;

    public string GetSqliteConnectionString(string PathToDatabase) => $"Data Source={PathToDatabase}";

    public SQLiteConnection GetSqliteConnection(string PathToDatabase) => new SQLiteConnection(GetSqliteConnectionString(PathToDatabase));

    public string GetSqliteConnectionString() => GetSqliteConnectionString(SqliteDatabase);

    public SQLiteConnection GetSqliteConnection() => GetSqliteConnection(SqliteDatabase);

    public string SqlSettingSelect { get; private set; } = string.Empty;

    public string SqlSettingInsert { get; private set; } = string.Empty;

    public string SqlSettingUpdate { get; private set; } = string.Empty;

    public string SqlSettingRename { get; private set; } = string.Empty;

    public string SqlSettingDelete { get; private set; } = string.Empty;

    public string SqlSettingCount { get; private set; } = string.Empty;

    public string SqlFolderCountByIdParent { get; private set; } = string.Empty;

    public string SqlFolderInsert { get; private set; } = string.Empty;

    public string SqlGetIdFolder { get; private set; } = string.Empty;

    public string SqlFolderCountByIdFolder { get; private set; } = string.Empty;

    public string SqlFolderRename { get; private set; } = string.Empty;

    public string SqlFolderCountSimple { get; private set; } = string.Empty;

    public string SqlCountChildFolder { get; private set; } = string.Empty;

    public string SqlCountChildSettings { get; private set; } = string.Empty;

    public string SqlFolderDelete { get; private set; } = string.Empty;

    public string SqlSetRank { get; private set; } = string.Empty;

    public string SqlDuplicatedRank { get; private set; } = string.Empty;

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

    private bool FlagInitVariablesHasBeenAlreadyExecuted { get; set; } = false;

    public void InitVariables()
    {
      if (TableTypes != null) TableTypes.Clear();
      TableTypes = GetTable(TnTypes);

      FillListFolders();

      if (FlagInitVariablesHasBeenAlreadyExecuted) return;

      // This block will be executed only ONE TIME //

      FlagInitVariablesHasBeenAlreadyExecuted = true;

      SqlSettingSelect = $"SELECT " +
        $"{CnSettingsIdFolder}," +
        $"{CnSettingsIdSetting}," +
        $"{CnSettingsIdType}," +
        $"{VnSettingsNameType}," +
        $"{CnSettingsSettingValue}," +
        $"{CnSettingsRank}," +
        $"{VnSettingsBooleanValue} " +
        $"FROM {VnSettings} WHERE {CnSettingsIdFolder}=@IdFolder";

      SqlSettingCount = $"SELECT COUNT(*) from {TnSettings} WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlSettingInsert =
      $"INSERT INTO {TnSettings} ({CnSettingsIdFolder}, {CnSettingsIdSetting}, {CnSettingsIdType}, {CnSettingsSettingValue}, {CnSettingsRank})" +
      $" VALUES (@IdFolder, @IdSetting, @IdType, @SettingValue, (SELECT IFNULL(MAX({CnSettingsRank}), 0) + 1 FROM {TnSettings} WHERE {CnSettingsIdFolder} = @IdFolder))";

      SqlSettingUpdate =
      $"UPDATE {TnSettings} SET {CnSettingsSettingValue}=@SettingValue " +
      $"WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlSettingRename =
      $"UPDATE {TnSettings} SET {CnSettingsIdSetting}=@IdSettingNew " +
      $"WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSettingOld";

      SqlSettingDelete =
      $"DELETE FROM {TnSettings} " +
      $"WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlFolderCountByIdParent = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent}=@IdParent AND {CnFoldersNameFolder}=@NameFolder";
      SqlFolderInsert = $"INSERT INTO {TnFolders} ({CnFoldersIdParent},{CnFoldersNameFolder}) VALUES (@IdParent,@NameFolder)";
      SqlGetIdFolder = $"SELECT {CnFoldersIdFolder} FROM {TnFolders} WHERE {CnFoldersIdParent}=@IdParent AND {CnFoldersNameFolder}=@NameFolder";

      SqlFolderCountByIdFolder = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent} = (SELECT {CnFoldersIdParent} FROM {TnFolders} WHERE {CnFoldersIdFolder}=@IdFolder) AND {CnFoldersNameFolder}=@NameFolder";
      SqlFolderRename = $"UPDATE {TnFolders} SET {CnFoldersNameFolder}=@NameFolder WHERE {CnFoldersIdFolder}=@IdFolder";

      SqlFolderCountSimple = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdFolder} = @IdFolder";
      SqlCountChildFolder = $"SELECT COUNT(*) FROM {TnFolders} WHERE {CnFoldersIdParent} = @IdFolder";
      SqlCountChildSettings = $"SELECT COUNT(*) FROM {TnSettings} WHERE {CnFoldersIdFolder} = @IdFolder";
      SqlFolderDelete = $"DELETE FROM {TnFolders} WHERE {CnFoldersIdFolder}=@IdFolder";

      SqlSetRank = $"UPDATE {TnSettings} SET {CnSettingsRank}=@Rank WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlDuplicatedRank = $"SELECT COUNT(*) FROM (SELECT * FROM {TnSettings} WHERE {CnSettingsIdFolder}=@IdFolder GROUP BY {CnSettingsRank} HAVING COUNT(*) > 1)";
    }

    public void FillDropDownListForTableTypes(RadDropDownList combobox)
    {
      combobox.DataSource = TableTypes;
      combobox.ValueMember = CnTypesIdType;
      combobox.DisplayMember = CnTypesNameType;
      combobox.ZzSetStandardVisualStyle();
    }

    public ReturnCode FolderInsert(int IdParent, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"New folder has been created: {NameFolder}");
      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountByIdParent).ZzAdd("@IdParent", IdParent).ZzAdd("@NameFolder", NameFolder);
        int count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(SqlFolderInsert);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to add a new folder");
        IdNewFolder = command.ZzGetScalarInteger(SqlGetIdFolder);
        if (IdNewFolder > 0)
        {
          code.IdObject = IdNewFolder;
        }
        else
        {
          return ReturnCodeFactory.Error("Error trying to add a new folder");
        }
      }
      return code;
    }

    public ReturnCode FolderRename(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been renamed: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountByIdFolder).ZzAdd("@IdFolder", IdFolder).ZzAdd("@NameFolder", NameFolder);
        count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(SqlFolderRename);
        //count = command.ZzGetScalarInteger(SqlFolderCountByIdFolder);
        if (count < 1) return ReturnCodeFactory.Error("Error trying to rename a folder");
      }
      return code;
    }

    public ReturnCode FolderDelete(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been deleted: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(SqlFolderCountSimple).ZzAdd("@IdFolder", IdFolder);
        count = command.ZzGetScalarInteger();
        if (count != 1) return ReturnCodeFactory.Error("The folder you specified was not found");
        count = command.ZzGetScalarInteger(SqlCountChildFolder);
        if (count != 0) return ReturnCodeFactory.Error("You cannot delete a folder that has other folders inside");
        count = command.ZzGetScalarInteger(SqlCountChildSettings);
        if (count != 0) return ReturnCodeFactory.Error("You cannot delete a folder that contains settings");
        count = command.ZzExecuteNonQuery(SqlFolderDelete);
        if (count == 0) return ReturnCodeFactory.Error("Error trying to delete a folder");
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

    public int GetIdFolder(string FullPath)
    {
      foreach (var item in ListFolders) if (item.FullPath == FullPath) return item.IdFolder;
      return -1;
    }

    private async Task<BindingList<Setting>> GetSettings(RadTreeNode node)
    {
      if (node == null) return null;
      return await GetSettings(GetIdFolder(node));
    }

    public async Task<BindingList<Setting>> GetSettings(int IdFolder)
    {
      BindingList<Setting> list = new BindingList<Setting>();
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(SqlSettingSelect, connection))
      {
        command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder);
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

        if (ListHasDuplicatedRank(list))
        {
          string sql = SqlSetRank.Replace("@IdFolder", IdFolder.ToString());
          string temp = string.Empty;

          for (int i = 0; i < list.Count; i++)
          {
            list[i].Rank = i + 1;
            temp += sql.Replace("@Rank", list[i].Rank.ToString()).Replace("@IdSetting", SingleQuote + list[i].IdSetting + SingleQuote) + "; \n";
          }
          command.Parameters.Clear();
          command.ZzExecuteNonQuery(temp);
        }
      }
      return list;
    }

    public bool ListHasDuplicatedRank(BindingList<Setting> list)
    {
      var duplicates = list.GroupBy(x => x.Rank).Where(item => item.Count() > 1);
      int count = 0;
      foreach (var duplicate in duplicates) foreach (var item in duplicate) count++;
      return count > 0;
    }

    public ReturnCode SaveSettingDatetime(bool AddNewSetting, int IdFolder, string IdSetting, DateTime value)
    {
      string StringValue = CvManager.CvDatetime.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Datetime), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingBoolean(bool AddNewSetting, int IdFolder, string IdSetting, bool value)
    {
      string StringValue = CvManager.CvBoolean.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Boolean), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingLong(bool AddNewSetting, int IdFolder, string IdSetting, long value)
    {
      string StringValue = CvManager.CvInt64.ToString(value);
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(TypeSetting.Integer64), StringValue)
        :
        SettingUpdate(IdFolder, IdSetting, StringValue);
    }

    public ReturnCode SaveSettingText(bool AddNewSetting, int IdFolder, string IdSetting, TypeSetting type, string value)
    {
      return
        AddNewSetting
        ?
        SettingCreate(IdFolder, IdSetting, (int)(type), value)
        :
        SettingUpdate(IdFolder, IdSetting, value);
    }

    public ReturnCode SettingCreate(int IdFolder, string IdSetting, int IdType, string value)
    {
      ReturnCode code = ReturnCodeFactory.Success($"New setting has been created: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error("Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error("A setting with the same name already exists");
        count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(SqlSettingInsert);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to add a new setting");
        code.StringNote = value;
      }
      return code;
    }

    public ReturnCode SettingUpdate(int IdFolder, string IdSetting, string value)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Changes saved: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error("Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error("The setting with the specified name does not exist");
        count = command.ZzAdd("@SettingValue", value).ZzExecuteNonQuery(SqlSettingUpdate);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a value of the setting");
        code.StringNote = value;
      }
      return code;
    }

    public ReturnCode SettingRename(int IdFolder, string IdSettingOld, string IdSettingNew)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Setting has been renamed: {IdSettingNew}");
      if (IdSettingNew.Trim().Length < 1) return ReturnCodeFactory.Error("New setting name not specified");

      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSettingNew).ZzGetScalarInteger(SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error("A setting with the same name already exists");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSettingOld", IdSettingOld).ZzAdd("@IdSettingNew", IdSettingNew).ZzExecuteNonQuery(SqlSettingRename);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to rename a setting");
      }
      return code;
    }

    public ReturnCode SettingDelete(int IdFolder, string IdSetting)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Setting has been deleted: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error("Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error("Setting not found");
        count = command.ZzExecuteNonQuery(SqlSettingDelete);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to delete a setting");
      }
      return code;
    }

    public ReturnCode SwapRank(int IdFolder, Setting settingOne, Setting settingTwo)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Setting rank exchange completed");
      if ((settingOne == null) || (settingTwo == null)) return ReturnCodeFactory.Error("At least one parameter is null");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingOne.IdSetting).ZzAdd("@Rank", settingTwo.Rank).ZzExecuteNonQuery(SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingTwo.IdSetting).ZzAdd("@Rank", settingOne.Rank).ZzExecuteNonQuery(SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
      }
      return code;
    }

    public ReturnCode CreateNewDatabase(string FileName)
    {
      string sql = string.Empty;
      ReturnCode code = ReturnCodeFactory.Success();
      try
      {
        SQLiteConnection.CreateFile(FileName);
        SQLiteConnection connection = new SQLiteConnection($"Data Source={FileName};Version=3;");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(connection);

        sql = @"CREATE TABLE FOLDERS 
        (
        IdFolder INTEGER CONSTRAINT PK_FOLDERS PRIMARY KEY ON CONFLICT ROLLBACK AUTOINCREMENT NOT NULL ON CONFLICT ROLLBACK, 
        IdParent INTEGER NOT NULL ON CONFLICT ROLLBACK CONSTRAINT FK_FOLDERS REFERENCES FOLDERS (IdFolder) 
        ON DELETE RESTRICT ON UPDATE CASCADE, NameFolder TEXT (255) NOT NULL
        );";
        command.ZzExecuteNonQuery(sql);

        sql = @"
        INSERT INTO FOLDERS VALUES(0, 0, 'application_root_folder');
        INSERT INTO FOLDERS VALUES(1, 0, 'local_database');";
        command.ZzExecuteNonQuery(sql);

        sql = @"
        CREATE TABLE TYPES (IdType INTEGER PRIMARY KEY NOT NULL, NameType TEXT (255) NOT NULL, Note TEXT (4000));";
        command.ZzExecuteNonQuery(sql);

        sql = @"
        INSERT INTO TYPES VALUES(0,'unknown',NULL);
        INSERT INTO TYPES VALUES(1,'boolean',NULL);
        INSERT INTO TYPES VALUES(2,'datetime',NULL);
        INSERT INTO TYPES VALUES(3,'integer',NULL);
        INSERT INTO TYPES VALUES(4,'text',NULL);
        INSERT INTO TYPES VALUES(5,'password',NULL);
        INSERT INTO TYPES VALUES(6,'folder name',NULL);
        INSERT INTO TYPES VALUES(7,'file name',NULL);
        INSERT INTO TYPES VALUES(8,'font',NULL);
        INSERT INTO TYPES VALUES(9,'color',NULL);";
        command.ZzExecuteNonQuery(sql);

        sql = @"CREATE TABLE SETTINGS (
        IdFolder INTEGER NOT NULL, 
        IdSetting TEXT NOT NULL, 
        IdType INTEGER NOT NULL, 
        SettingValue TEXT, 
        Rank INTEGER NOT NULL, 
        CONSTRAINT PK_SETTINGS PRIMARY KEY (IdFolder, IdSetting) 
        ON CONFLICT ROLLBACK, CONSTRAINT FK_SETTINGS_FOLDERS FOREIGN KEY (IdFolder) 
        REFERENCES FOLDERS (IdFolder) ON DELETE RESTRICT ON UPDATE CASCADE, 
        CONSTRAINT FK_SETTINGS_TYPES FOREIGN KEY (IdType) REFERENCES TYPES (IdType) 
        ON DELETE RESTRICT ON UPDATE CASCADE);";
        command.ZzExecuteNonQuery(sql);

        sql = @"DELETE FROM sqlite_sequence;";
        command.ZzExecuteNonQuery(sql);

        sql = @"INSERT INTO sqlite_sequence VALUES('FOLDERS',2);";
        command.ZzExecuteNonQuery(sql);

        sql = @"CREATE UNIQUE INDEX UX_NameFolder ON FOLDERS(IdParent, NameFolder);";
        command.ZzExecuteNonQuery(sql);

        sql = @"CREATE VIEW V_SETTINGS AS
            SELECT A.IdFolder,
                   A.IdSetting,
                   A.IdType,
                   B.NameType,
                   A.SettingValue,
                   A.Rank,
                   CASE WHEN A.IdType = 1 THEN A.SettingValue ELSE '' END AS BooleanValue
              FROM SETTINGS A
              LEFT JOIN TYPES B
              ON A.IdType = B.IdType
              ORDER BY A.Rank, A.IdSetting;";
        command.ZzExecuteNonQuery(sql);

        connection.Close();
      }
      catch (Exception ex)
      {
        code = ReturnCodeFactory.Error(ex.Message, "Could not create a new database");
      }
      return code;
    }

    public void FillListFolders()
    {
      void ProcessOneNode(RadTreeNode node)
      {
        ListFolders.Add(Folder.Create(GetIdFolder(node), node.Text, node.FullPath, node.Level));
        foreach (var item in node.Nodes) ProcessOneNode(item);
      }
      ListFolders.Clear();
      VxFolders = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, VxFolders);      
      foreach (var item in form.TvFolders.Nodes) ProcessOneNode(item);
      form.Close();
    }
  }
}

