using System.Data.SQLite;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public ReturnCode SettingCreate(int IdFolder, string IdSetting, int IdType, string value)
    {
      //IdSetting = IdSetting.RemoveSpecialCharacters();
      if (FolderNotFound(IdFolder)) return FolderNotFound();
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
      if (FolderNotFound(IdFolder)) return FolderNotFound();
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
      if (FolderNotFound(IdFolder)) return FolderNotFound();
      int IdType = (int)type;
      //IdSetting = IdSetting.RemoveSpecialCharacters();
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
      if (FolderNotFound(IdFolder)) return FolderNotFound();
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
      if (FolderNotFound(IdFolder)) return FolderNotFound();
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

    public ReturnCode DeleteAllSettingsOfOneFolder(string FolderPath) => DeleteAllSettingsOfOneFolder(GetIdFolder(FolderPath));

    public ReturnCode DeleteAllSettingsOfOneFolder(int IdFolder)
    {
      if (FolderNotFound(IdFolder)) return FolderNotFound();
      ReturnCode code = ReturnCodeFactory.Success($"Settings has been deleted");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        count = command.ZzOpenConnection().ZzAdd("@IdFolder", IdFolder).ZzExecuteNonQuery(DbManager.SqlDeleteAllSettingsOfOneFolder);
        if (count == 0) code = ReturnCodeFactory.Success($"No any settings were deleted");
        count = command.ZzGetScalarInteger(DbManager.SqlAllSettingsCount);
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to delete all settings of the folder");
      }
      return code;
    }

    public ReturnCode SwapRank(int IdFolder, Setting settingOne, Setting settingTwo)
    {
      if (FolderNotFound(IdFolder)) return FolderNotFound();
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
  }
}

