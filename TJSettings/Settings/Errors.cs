namespace TJSettings
{
  public enum Errors : int
  {
    Unknown = 100,
    FolderAlreadyExists = 201,
    FolderNotFound = 202,
    FolderHasChildFolder = 203,
    FolderHasSettings = 204,
    SettingNameNotSpecified = 301,
    SettingAlreadyExists = 302,
    SettingDoesNotExist = 303,
    SettingNotFound = 304,
    SettingInsertFailed = 305,
    SettingUpdateFailed = 306,
    SettingInappropriateType = 307
  }
}

