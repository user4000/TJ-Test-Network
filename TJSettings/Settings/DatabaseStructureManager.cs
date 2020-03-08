using System;
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
  public class DatabaseStructureManager
  {
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

    public string SqlGetSettingValue { get; private set; } = string.Empty;

    public string SqlGetSettingValueWithCount  { get; private set; } = string.Empty;

    public string SqlDuplicatedRank { get; private set; } = string.Empty;

    public void InitVariables()
    {
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

      SqlGetSettingValue = $"SELECT {CnSettingsSettingValue} FROM {TnSettings} WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";

      SqlGetSettingValueWithCount = $"SELECT IFNULL((SELECT {CnSettingsSettingValue} FROM {TnSettings} WHERE {CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting),'') || COUNT(*) as S FROM {TnSettings} WHERE { CnSettingsIdFolder}=@IdFolder AND {CnSettingsIdSetting}=@IdSetting";
    }

    public ReturnCode CreateNewDatabase(string FileName)
    {
      string sql = string.Empty;
      ReturnCode code = ReturnCodeFactory.Success();
      try
      {
        SQLiteConnection.CreateFile(FileName);
        using (SQLiteConnection connection = new SQLiteConnection($"Data Source={FileName};Version=3;"))
        {
          connection.Open();
          using (SQLiteCommand command = new SQLiteCommand(connection))
          {
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
        }
      }
      catch (Exception ex)
      {
        code = ReturnCodeFactory.Error(ex.Message, "Could not create a new database");
      }
      return code;
    }
  }
}

