using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using TJStandard;
using System.Drawing;

namespace TJSettings
{
  public class LocalDatabaseOfSettings
  {
    public DatabaseStructureManager DbManager = new DatabaseStructureManager();

    public string RootFolderName { get; private set; } = string.Empty;

    public RadTreeView VxTreeview { get; private set; } = new RadTreeView();

    public DataTable VxFolders { get; private set; } = null;

    public char SingleQuote { get; } = '\'';

    public int IdFolderNotFound { get; } = -1;

    public string FolderPathSeparator { get; } = @"\";

    public Converter CvManager { get; } = new Converter();

    internal DataTable TableTypes { get; private set; } = null;

    public string SqliteDatabase { get; private set; } = string.Empty;

    public void SetPathToDatabase(string PathToDatabase) => SqliteDatabase = PathToDatabase;

    public string GetSqliteConnectionString(string PathToDatabase) => $"Data Source={PathToDatabase}";

    public SQLiteConnection GetSqliteConnection(string PathToDatabase) => new SQLiteConnection(GetSqliteConnectionString(PathToDatabase));

    public SQLiteConnection GetSqliteConnection() => GetSqliteConnection(SqliteDatabase);

    private LocalDatabaseOfSettings(string PathToDatabase)
    {
      SetPathToDatabase(PathToDatabase);
      InitVariables();
    }

    public static LocalDatabaseOfSettings Create(string PathToDatabase) => new LocalDatabaseOfSettings(PathToDatabase);

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

    private void InitVariables()
    {
      if (TableTypes != null) TableTypes.Clear();
      TableTypes = GetTable(DbManager.TnTypes);
      RootFolderName = GetRootFolderName();
    }

    public bool SettingTypeIsText(TypeSetting type)
    {
      return !((type == TypeSetting.Boolean) || (type == TypeSetting.Integer64) || (type == TypeSetting.Datetime));
    }

    public void FillDropDownListForTableTypes(RadDropDownList combobox)
    {
      combobox.DataSource = TableTypes;
      combobox.ValueMember = DbManager.CnTypesIdType;
      combobox.DisplayMember = DbManager.CnTypesNameType;
      combobox.ZzSetStandardVisualStyle();
    }

    public ReturnCode CreateNewDatabase(string text) => DbManager.CreateNewDatabase(text);

    public ReturnCode FolderInsert(string Parent, string NameFolder) => FolderInsert(GetIdFolder(Parent), NameFolder);

    public ReturnCode FolderInsert(int IdParent, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"New folder has been created: {NameFolder}");
      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdParent).ZzAdd("@IdParent", IdParent).ZzAdd("@NameFolder", NameFolder);
        int count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderAlreadyExists, "A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderInsert);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to add a new folder");
        IdNewFolder = command.ZzGetScalarInteger(DbManager.SqlGetIdFolder);
        if (IdNewFolder > 0)
        {
          code.IdObject = IdNewFolder;
        }
        else
        {
          return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to add a new folder");
        }
      }
      return code;
    }

    public ReturnCode FolderRename(string FolderPath, string NameFolder) => FolderRename(GetIdFolder(FolderPath), NameFolder);

    public ReturnCode FolderRename(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been renamed: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdFolder).ZzAdd("@IdFolder", IdFolder).ZzAdd("@NameFolder", NameFolder);
        count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderAlreadyExists, "A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderRename);
        //count = command.ZzGetScalarInteger(SqlFolderCountByIdFolder);
        if (count < 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to rename a folder");
      }
      if (IdFolder == DbManager.IdFolderRoot) RootFolderName = GetRootFolderName();
      return code;
    }

    public ReturnCode FolderDelete(string FolderPath, string NameFolder) => FolderDelete(GetIdFolder(FolderPath), NameFolder);

    public ReturnCode FolderDelete(int IdFolder, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been deleted: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountSimple).ZzAdd("@IdFolder", IdFolder);
        count = command.ZzGetScalarInteger();
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.FolderNotFound, "The folder you specified was not found");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildFolder);
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderHasChildFolder, "You cannot delete a folder that has other folders inside");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildSettings);
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderHasSettings, "You cannot delete a folder that contains settings");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderDelete);
        if (count == 0) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to delete a folder");
      }
      return code;
    }

    public ReturnCode CheckDatabaseStructure()
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

    public string AddRootFolderNameIfNotSpecified(string FullPath)
    {
      FullPath = FullPath.Trim();
      if (FullPath == string.Empty) return RootFolderName;
      FullPath = FullPath.TrimStart(FolderPathSeparator[0]).TrimEnd(FolderPathSeparator[0]);
      if (FullPath.StartsWith(RootFolderName) == false) FullPath = RootFolderName + FolderPathSeparator + FullPath;
      return FullPath;
    }

    public int GetIdFolder(RadTreeNode node)
    {
      int Id = IdFolderNotFound;
      if (node != null)
        try
        {
          DataRowView row = node.DataBoundItem as DataRowView;
          Id = CxConvert.ToInt32(row.Row[DbManager.CnFoldersIdFolder].ToString(), IdFolderNotFound);
        }
        catch { }
      return Id;
    }

    /// <summary>
    /// Find IdFolder by Materialized Full Path (Root Folder Name may be omitted).
    /// </summary>
    public int GetIdFolder(string FullPath) // Find IdFolder by Materialized Full Path - Root Folder Name may be omitted //
    {
      FullPath = AddRootFolderNameIfNotSpecified(FullPath);
      string[] names = FullPath.Split(FolderPathSeparator[0]);
      int IdFolder = 0;
      string sql = string.Empty;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.Connection.Open();
        command.CommandText = DbManager.SqlGetIdFolder;
        for (int i = 0; i < names.Length; i++)
        {
          IdFolder = command.ZzAdd("@IdParent", IdFolder).ZzAdd("@NameFolder", names[i]).ZzGetScalarInteger();
          command.Parameters.Clear();
          if (IdFolder < 0) break;
        }
        command.Connection.Close();
      }
      return IdFolder;
    }

    private int GetIdFolderWithTreeview(string FullPath)
    {
      int x = IdFolderNotFound;

      int ProcessOneNode(RadTreeNode node)
      {
        if (node.FullPath == FullPath) return GetIdFolder(node);
        foreach (var item in node.Nodes)
        {
          x = ProcessOneNode(item);
          if (x != IdFolderNotFound) { return x; }
        }
        return -1;
      }

      DataTable table = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, table);

      int IdFolder = IdFolderNotFound;

      foreach (var item in form.TvFolders.Nodes)
      {
        IdFolder = ProcessOneNode(item);
        if (IdFolder != IdFolderNotFound) break;
      }

      form.TvFolders.DataSource = null;
      table.Clear();
      form.TvFolders.Dispose();
      table.Dispose();
      form.Close();
      return IdFolder;
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
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlSettingSelect, connection))
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

        if (ListHasDuplicatedRank(list)) // Correct rank if duplicate values occur //
        {
          string sql = DbManager.SqlSetRank.Replace("@IdFolder", IdFolder.ToString());
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

    /// <summary>
    /// Get list of settings of the specified type. First argument is a Full Path to a Folder, containing the settings.
    /// </summary>
    public List<string> GetSettings(string FullPath, TypeSetting type)
    {
      List<string> list = new List<string>();
      int IdFolder = GetIdFolder(FullPath);
      if (IdFolder < 0) return list;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlSettingSelect, connection).ZzOpenConnection().ZzAdd("@IdFolder", IdFolder))     
      using (SQLiteDataReader reader = command.ExecuteReader())
        while (reader.Read()) if ( (reader.GetInt32(2) == (int)type) || (type==TypeSetting.Unknown)) list.Add(reader.GetString(1));
      return list;      
    }

    public bool ListHasDuplicatedRank(BindingList<Setting> list)
    {
      var duplicates = list.GroupBy(x => x.Rank).Where(item => item.Count() > 1);
      int count = 0;
      foreach (var duplicate in duplicates) foreach (var item in duplicate) count++;
      return count > 0;
    }

    public string GetRootFolderName()
    {
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlGetRootFolderName, connection).ZzOpenConnection())
      {
        return command.ZzGetScalarString();
      }
    }

    public ReturnCode SaveSettingDatetime(bool AddNewSetting, int IdFolder, string IdSetting, DateTime value)
    {
      string StringValue = CvManager.CvDatetime.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Datetime, StringValue);
    }

    public ReturnCode SaveSettingDatetime(string FolderPath, string IdSetting, DateTime value)
    {
      string StringValue = CvManager.CvDatetime.ToString(value);
      return SettingCreateOrUpdate(FolderPath, IdSetting, TypeSetting.Datetime, StringValue);
    }

    public ReturnCode SaveSettingBoolean(bool AddNewSetting, int IdFolder, string IdSetting, bool value)
    {
      string StringValue = CvManager.CvBoolean.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Boolean, StringValue);
    }

    public ReturnCode SaveSettingBoolean(string FolderPath, string IdSetting, bool value)
    {
      string StringValue = CvManager.CvBoolean.ToString(value);
      return SettingCreateOrUpdate(FolderPath, IdSetting, TypeSetting.Boolean, StringValue);
    }

    public ReturnCode SaveSettingLong(bool AddNewSetting, int IdFolder, string IdSetting, long value)
    {
      string StringValue = CvManager.CvInt64.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Integer64, StringValue);
    }

    public ReturnCode SaveSettingLong(string FolderPath, string IdSetting, long value)
    {
      string StringValue = CvManager.CvInt64.ToString(value);
      return SettingCreateOrUpdate(FolderPath, IdSetting, TypeSetting.Integer64, StringValue);
    }

    public ReturnCode SaveSettingFont(bool AddNewSetting, int IdFolder, string IdSetting, Font value)
    {
      string StringValue = CvManager.CvFont.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Font, StringValue);
    }

    public ReturnCode SaveSettingFont(string FolderPath, string IdSetting, Font value)
    {
      string StringValue = CvManager.CvFont.ToString(value);
      return SettingCreateOrUpdate(FolderPath, IdSetting, TypeSetting.Font, StringValue);
    }

    public ReturnCode SaveSettingColor(bool AddNewSetting, int IdFolder, string IdSetting, Color value)
    {
      string StringValue = CvManager.CvColor.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Color, StringValue);
    }

    public ReturnCode SaveSettingColor(string FolderPath, string IdSetting, Color value)
    {
      string StringValue = CvManager.CvColor.ToString(value);
      return SettingCreateOrUpdate(FolderPath, IdSetting, TypeSetting.Color, StringValue);
    }

    public ReturnCode SaveSettingText(bool AddNewSetting, int IdFolder, string IdSetting, TypeSetting type, string value)
    {
      if (SettingTypeIsText(type) == false)
      {
        return ReturnCodeFactory.Error((int)Errors.SettingInappropriateType, "Incorrect [TypeSetting] value for the [SaveSettingText] method.");
      }
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, type, value);
    }

    public ReturnCode SaveSettingText(string FolderPath, string IdSetting, TypeSetting type, string value)
    {
      if (SettingTypeIsText(type) == false)
      {
        return ReturnCodeFactory.Error((int)Errors.SettingInappropriateType, "Incorrect [TypeSetting] value for the [SaveSettingText] method.");
      }
      return SettingCreateOrUpdate(FolderPath, IdSetting, type, value);
    }

    public ReturnCode SettingCreate(int IdFolder, string IdSetting, int IdType, string value)
    {
      IdSetting = IdSetting.RemoveSpecialCharacters();
      ReturnCode code = ReturnCodeFactory.Success($"New setting has been created: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error((int)Errors.SettingNameNotSpecified, "Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error((int)Errors.SettingAlreadyExists, "A setting with the same name already exists");
        count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingInsert);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingInsertFailed, "Error trying to add a new setting");
        code.StringNote = value;
      }
      return code;
    }

    public ReturnCode SettingUpdate(int IdFolder, string IdSetting, int IdType, string value)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Changes saved: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error((int)Errors.SettingNameNotSpecified, "Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingDoesNotExist, "The setting with the specified name does not exist");
        count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingUpdate);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingUpdateFailed, "Error trying to change a value of the setting");
        code.StringNote = value;
      }
      return code;
    }

    private ReturnCode SettingCreateOrUpdate(bool AddNewSetting, int IdFolder, string IdSetting, TypeSetting type, string value)
    {
      return AddNewSetting ? SettingCreate(IdFolder, IdSetting, (int)(type), value) : SettingUpdate(IdFolder, IdSetting, (int)(type), value);
    }

    private ReturnCode SettingCreateOrUpdate(string FolderPath, string IdSetting, TypeSetting type, string value)
    {
      int IdFolder = GetIdFolder(FolderPath);
      int IdType = (int)type;
      IdSetting = IdSetting.RemoveSpecialCharacters();
      ReturnCode code = ReturnCodeFactory.Success($"New setting has been created: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error((int)Errors.SettingNameNotSpecified, "Setting name not specified");
      int count = 0;

      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count > 0) // UPDATE value of existing setting // 
        {
          code = ReturnCodeFactory.Success($"Changes saved: {IdSetting}");
          count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingUpdate);
          if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingUpdateFailed, "Error trying to change a value of the setting");
          code.StringNote = value;
        }
        else // ADD a new value of setting // 
        {
          count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingInsert);
          if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingInsertFailed, "Error trying to add a new setting");
          code.StringNote = value;
        }
      }
      return code;
    }

    public ReturnCode SettingRename(string FolderPath, string IdSettingOld, string IdSettingNew)
    {
      return SettingRename(GetIdFolder(FolderPath), IdSettingOld, IdSettingNew);
    }

    public ReturnCode SettingRename(int IdFolder, string IdSettingOld, string IdSettingNew)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Setting has been renamed: {IdSettingNew}");
      if (IdSettingNew.Trim().Length < 1) return ReturnCodeFactory.Error((int)Errors.SettingNameNotSpecified, "New setting name not specified");

      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSettingNew).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error((int)Errors.SettingAlreadyExists, "A setting with the same name already exists");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSettingOld", IdSettingOld).ZzAdd("@IdSettingNew", IdSettingNew).ZzExecuteNonQuery(DbManager.SqlSettingRename);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to rename a setting");
      }
      return code;
    }

    public ReturnCode SettingDelete(string FolderPath, string IdSetting)
    {
      return SettingDelete(GetIdFolder(FolderPath), IdSetting);
    }

    public ReturnCode SettingDelete(int IdFolder, string IdSetting)
    {
      ReturnCode code = ReturnCodeFactory.Success($"Setting has been deleted: {IdSetting}");
      if (IdSetting.Trim().Length < 1) return ReturnCodeFactory.Error((int)Errors.SettingNameNotSpecified, "Setting name not specified");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.SettingNotFound, "Setting not found");
        count = command.ZzExecuteNonQuery(DbManager.SqlSettingDelete);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to delete a setting");
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingOne.IdSetting).ZzAdd("@Rank", settingTwo.Rank).ZzExecuteNonQuery(DbManager.SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingTwo.IdSetting).ZzAdd("@Rank", settingOne.Rank).ZzExecuteNonQuery(DbManager.SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
      }
      return code;
    }

    public ReceivedValueText GetStringValueOfSettingThisIsPreviousVersionOfMethod(string FolderPath, string IdSetting)
    {
      int IdFolder = GetIdFolder(FolderPath);
      if (IdFolder < 0) return ReceivedValueText.Error((int)Errors.FolderNotFound, "Folder not found");
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        int count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count == 0) return ReceivedValueText.Error((int)Errors.SettingDoesNotExist, "Setting does not exist");
        string value = command.ZzGetScalarString(DbManager.SqlGetSettingValue);
        return ReceivedValueText.Success(value);
      }
    }

    public ReceivedValueText GetStringValueOfSetting(string FolderPath, string IdSetting) // TODO: Test it and compare with previous method
    {
      int IdFolder = GetIdFolder(FolderPath);
      if (IdFolder < 0) return ReceivedValueText.Error((int)Errors.FolderNotFound, "Folder not found");
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        string value = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarString(DbManager.SqlGetSettingValueWithCount);
        if (value.Length < 1) return ReceivedValueText.Error((int)Errors.Unknown, "Query has returned empty string");
        if (value[value.Length - 1] == '0') return ReceivedValueText.Error((int)Errors.SettingDoesNotExist, "Setting does not exist");
        if (value.Length == 1) return ReceivedValueText.Success(string.Empty);
        return ReceivedValueText.Success(value.Remove(value.Length - 1));
      }
    }

    public ReceivedValueBoolean GetSettingBoolean(string FolderPath, string IdSetting)
    {
      ReceivedValueText TextValue = GetStringValueOfSetting(FolderPath, IdSetting);
      if (TextValue.Code.Error) return ReceivedValueBoolean.Error(TextValue.Code.NumericValue, TextValue.Code.StringValue);
      return CvManager.CvBoolean.FromString(TextValue.Value);
    }

    public ReceivedValueInteger64 GetSettingInteger64(string FolderPath, string IdSetting)
    {
      ReceivedValueText TextValue = GetStringValueOfSetting(FolderPath, IdSetting);
      if (TextValue.Code.Error) return ReceivedValueInteger64.Error(TextValue.Code.NumericValue, TextValue.Code.StringValue);
      return CvManager.CvInt64.FromString(TextValue.Value);
    }

    public ReceivedValueColor GetSettingColor(string FolderPath, string IdSetting)
    {
      ReceivedValueText TextValue = GetStringValueOfSetting(FolderPath, IdSetting);
      if (TextValue.Code.Error) return ReceivedValueColor.Error(TextValue.Code.NumericValue, TextValue.Code.StringValue);
      return CvManager.CvColor.FromString(TextValue.Value);
    }

    public ReceivedValueDatetime GetSettingDatetime(string FolderPath, string IdSetting)
    {
      ReceivedValueText TextValue = GetStringValueOfSetting(FolderPath, IdSetting);
      if (TextValue.Code.Error) return ReceivedValueDatetime.Error(TextValue.Code.NumericValue, TextValue.Code.StringValue);
      return CvManager.CvDatetime.FromString(TextValue.Value);
    }

    public ReceivedValueText GetSettingText(string FolderPath, string IdSetting)
    {
      return GetStringValueOfSetting(FolderPath, IdSetting);
    }

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

    /// <summary>
    /// Get all settings of all folders.
    /// </summary>
    public List<Setting> GetAllSettings()
    {
      ReturnCode code = ReturnCodeFactory.Error();
      List<Setting> list = new List<Setting>();
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlGetAllSettings, connection).ZzOpenConnection())
      using (SQLiteDataReader reader = command.ExecuteReader())
        while (reader.Read())
          list.Add(Setting.Create
            (
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetInt32(2),
            reader.GetString(3),
            reader.GetInt32(4)
            ));
      return list;
    }

    public List<Folder> GetListOfFolders()
    {
      List<Folder> list = new List<Folder>();
      void ProcessOneNode(RadTreeNode node)
      {
        list.Add(Folder.Create(GetIdFolder(node), node.Text, node.FullPath, node.Level));
        foreach (var item in node.Nodes) ProcessOneNode(item);
      }
      DataTable table = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, table);
      foreach (var item in form.TvFolders.Nodes) ProcessOneNode(item);
      form.TvFolders.DataSource = null;
      table.Clear();
      form.TvFolders.Dispose();
      table.Dispose();
      form.Close();
      return list;
    }

    public List<string> GetListOfFolders(string ParentFolderPath)
    {
      List<string> list = new List<string>();
      DataTable table = new DataTable();
      int IdFolder = GetIdFolder(ParentFolderPath);
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlFolderGetChildren, connection).ZzAdd("@IdFolder",IdFolder))
      using (SQLiteDataReader reader = command.ZzOpenConnection().ExecuteReader())
      {
        table.Load(reader);
      }
      if (table.Rows.Count > 0) list = (from DataRow row in table.Rows select row[0].ToString()).ToList();
      table.Clear();
      return list;
    }
  }
}

