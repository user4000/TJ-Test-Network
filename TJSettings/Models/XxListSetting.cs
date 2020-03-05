using System.Collections.Generic;

namespace TJSettings
{
  public static class XxListSetting
  {
    public static void ZzAdd
      (
      this IList<Setting> list, 
      int IdFolder, 
      string IdSetting, 
      int IdType, 
      string NameType,
      string SettingValue, 
      int Rank,
      string BooleanValue
      )
    {
      list.Add(new Setting(IdFolder, IdSetting, IdType, NameType, SettingValue, Rank, BooleanValue));
    }
  }
}
