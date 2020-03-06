﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using TJStandard;

namespace TJSettings
{
  public class LocalDatabaseOfSettings
  {
    internal DatabaseStructureManager DbManager = new DatabaseStructureManager();

    public string RootFolderName { get; private set; } = string.Empty;
    public RadTreeView VxTreeview { get; private set; } = new RadTreeView();

    public DataTable VxFolders { get; private set; } = null;

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
      DataTable table = GetTable(DbManager.TnFolders);
      foreach (DataColumn column in table.Columns) if (column.ColumnName == DbManager.CnFoldersIdParent) column.AllowDBNull = true;
      return table;
    }

    private bool FlagInitVariablesHasBeenAlreadyExecuted { get; set; } = false;

    public void InitVariables()
    {
      if (TableTypes != null) TableTypes.Clear();
      TableTypes = GetTable(DbManager.TnTypes);
      FillListFolders();
      if (FlagInitVariablesHasBeenAlreadyExecuted) return;
      // This block will be executed only ONE TIME //
      FlagInitVariablesHasBeenAlreadyExecuted = true;
      DbManager.InitVariables();
    }

    public void FillDropDownListForTableTypes(RadDropDownList combobox)
    {
      combobox.DataSource = TableTypes;
      combobox.ValueMember = DbManager.CnTypesIdType;
      combobox.DisplayMember = DbManager.CnTypesNameType;
      combobox.ZzSetStandardVisualStyle();
    }

    public ReturnCode FolderInsert(int IdParent, string NameFolder)
    {
      ReturnCode code = ReturnCodeFactory.Success($"New folder has been created: {NameFolder}");
      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdParent).ZzAdd("@IdParent", IdParent).ZzAdd("@NameFolder", NameFolder);
        int count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderInsert);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to add a new folder");
        IdNewFolder = command.ZzGetScalarInteger(DbManager.SqlGetIdFolder);
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
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdFolder).ZzAdd("@IdFolder", IdFolder).ZzAdd("@NameFolder", NameFolder);
        count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error("A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderRename);
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
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountSimple).ZzAdd("@IdFolder", IdFolder);
        count = command.ZzGetScalarInteger();
        if (count != 1) return ReturnCodeFactory.Error("The folder you specified was not found");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildFolder);
        if (count != 0) return ReturnCodeFactory.Error("You cannot delete a folder that has other folders inside");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildSettings);
        if (count != 0) return ReturnCodeFactory.Error("You cannot delete a folder that contains settings");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderDelete);
        if (count == 0) return ReturnCodeFactory.Error("Error trying to delete a folder");
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

    public int GetIdFolder(RadTreeNode node)
    {
      int Id = -1;
      if (node != null)
        try
        {
          DataRowView row = node.DataBoundItem as DataRowView;
          Id = CxConvert.ToInt32(row.Row[DbManager.CnFoldersIdFolder].ToString(), -1);
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

        if (ListHasDuplicatedRank(list))
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error("A setting with the same name already exists");
        count = command.ZzAdd("@IdType", IdType).ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingInsert);
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error("The setting with the specified name does not exist");
        count = command.ZzAdd("@SettingValue", value).ZzExecuteNonQuery(DbManager.SqlSettingUpdate);
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSettingNew).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count > 0) return ReturnCodeFactory.Error("A setting with the same name already exists");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSettingOld", IdSettingOld).ZzAdd("@IdSettingNew", IdSettingNew).ZzExecuteNonQuery(DbManager.SqlSettingRename);
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", IdSetting).ZzGetScalarInteger(DbManager.SqlSettingCount);
        if (count != 1) return ReturnCodeFactory.Error("Setting not found");
        count = command.ZzExecuteNonQuery(DbManager.SqlSettingDelete);
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
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingOne.IdSetting).ZzAdd("@Rank", settingTwo.Rank).ZzExecuteNonQuery(DbManager.SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
        command.Parameters.Clear();
        count = command.ZzAdd("@IdFolder", IdFolder).ZzAdd("@IdSetting", settingTwo.IdSetting).ZzAdd("@Rank", settingOne.Rank).ZzExecuteNonQuery(DbManager.SqlSetRank);
        if (count != 1) return ReturnCodeFactory.Error("Error trying to change a rank of a setting");
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
      foreach (var item in ListFolders) if (item.Level == 0) { RootFolderName = item.NameFolder; break; }
    }

    public ReturnCode CreateNewDatabase(string text) => DbManager.CreateNewDatabase(text);

  }
}

