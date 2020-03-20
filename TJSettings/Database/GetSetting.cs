using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    private async Task<BindingList<Setting>> GetSettings(RadTreeNode node)
    {
      if (node == null) return null;
      return await GetSettings(GetIdFolder(node));
    }

    public async Task<BindingList<Setting>> GetSettings(int IdFolder)
    {
      BindingList<Setting> list = new BindingList<Setting>();
      if (FolderNotFound(IdFolder)) return list;

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
        while (reader.Read()) if ((reader.GetInt32(2) == (int)type) || (type == TypeSetting.Unknown)) list.Add(reader.GetString(1));
      return list;
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

    public ReceivedValueText GetStringValueOfSetting(string FolderPath, string IdSetting) 
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

    public ReceivedValueFont GetSettingFont(string FolderPath, string IdSetting)
    {
      ReceivedValueText TextValue = GetStringValueOfSetting(FolderPath, IdSetting);
      if (TextValue.Code.Error) return ReceivedValueFont.Error(TextValue.Code.NumericValue, TextValue.Code.StringValue);
      return CvManager.CvFont.FromString(TextValue.Value);
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

    public ReturnCode CheckIdSettingCharacters(string IdSetting)
    {
      if (IdSetting.Trim() == string.Empty) return ReturnCodeFactory.Error("Incorrect name of the setting.");
      string CorrectedIdSetting = IdSetting.RemoveSpecialCharacters();
      if (CorrectedIdSetting != IdSetting)
        return ReturnCodeFactory.Error($"Special characters are not allowed in the setting name. Allowed name may be = {CorrectedIdSetting}");
      return ReturnCodeFactory.Success();
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

    public bool ListHasDuplicatedRank(BindingList<Setting> list)
    {
      var duplicates = list.GroupBy(x => x.Rank).Where(item => item.Count() > 1);
      int count = 0;
      foreach (var duplicate in duplicates) foreach (var item in duplicate) count++;
      return count > 0;
    }
  }
}

