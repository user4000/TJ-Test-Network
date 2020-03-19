using System;
using System.Drawing;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public bool SettingTypeIsText(TypeSetting type)
    {
      return !((type == TypeSetting.Boolean) || (type == TypeSetting.Integer64) || (type == TypeSetting.Datetime));
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

    public ReturnCode SaveSettingInteger64(bool AddNewSetting, int IdFolder, string IdSetting, long value)
    {
      string StringValue = CvManager.CvInt64.ToString(value);
      return SettingCreateOrUpdate(AddNewSetting, IdFolder, IdSetting, TypeSetting.Integer64, StringValue);
    }

    public ReturnCode SaveSettingInteger64(string FolderPath, string IdSetting, long value)
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
  }
}

